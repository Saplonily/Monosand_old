using System.Reflection;

namespace Monosand;

public sealed class Tracker
{
    // 类(隐式, 显式被track的都有键)  ->  需要添加到的所有类(即所有父类和自己)
    internal static Dictionary<Type, List<Type>> toBeTrackeds=new();
    private readonly Dictionary<Type, List<object>> tracks;

    public Tracker()
    {
        tracks = new();
    }

    public static void InitWithAssembly(Assembly asm)
    {
        var types = asm.GetTypes();
        foreach (var t in types)
        {
            var attr = t.GetCustomAttribute<Tracked>();
            if (attr is not null)
            {
                if (attr.Inherit)
                {
                    var subTypes = types.Where(t.IsAssignableTo);
                    foreach (var subt in subTypes)
                    {
                        if (toBeTrackeds.TryGetValue(subt, out var list))
                            list.Add(t);
                        else
                            toBeTrackeds[subt] = new List<Type>() { subt, t };
                    }
                }
                else
                {
                    toBeTrackeds[t] = new(2) { t };
                }
            }
        }
    }

    internal void OnAdd(object obj)
    {
        if (toBeTrackeds.TryGetValue(obj.GetType(), out var trackList))
            foreach (var type in trackList)
                if (tracks.TryGetValue(type, out var list))
                    list.Add(obj);
                else
                    tracks[type] = new(2) { obj };
    }

    internal void OnRemove(object obj)
    {
        if (toBeTrackeds.TryGetValue(obj.GetType(), out var trackList))
            foreach (var type in trackList)
                if (tracks.TryGetValue(type, out var list))
                    list.Remove(obj);
    }

    public IEnumerable<T> Get<T>()
    {
        if (tracks.TryGetValue(typeof(T), out var list))
            return list.Cast<T>();
        else
            return !toBeTrackeds.ContainsKey(typeof(T)) 
                ? throw new InvalidOperationException("Type not tracked.") 
                : Enumerable.Empty<T>();
    }

    public T First<T>()
        => Get<T>().First();

    public T FirstOrDefault<T>()
        => Get<T>().FirstOrDefault();
}