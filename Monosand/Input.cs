namespace Monosand;

public static class Input
{
    private const string MouseButtonNotSupport = "Only MouseButton Left,Middle and Right are supported.";
    private static KeyboardState preKeyState = default;
    private static KeyboardState curKeyState = default;
    private static MouseState preMouseState = default;
    private static MouseState curMouseState = default;
    //private static TouchPanelState preTouchState = default;
    //private static TouchPanelState curTouchState = default;

    public static Vector2 MousePosition
    {
        get
        {
            var s = Core.CoreIns.Scene;
            if (s is not null)
            {
                var m = s.Camera.Matrix;
                Vector2 pos = curMouseState.Position.ToVector2();
                pos = Vector2.Transform(pos, m);
                return pos;
            }
            else
            {
                throw new InvalidOperationException("Cannot get mouse position while the game scene is null.");
            }
        }
    }

    internal static void UpdateWith(in KeyboardState keyboardState, in MouseState mouseState)
    {
        preKeyState = curKeyState;
        curKeyState = keyboardState;
        preMouseState = curMouseState;
        curMouseState = mouseState;
    }

    public static bool IsKeyPressed(Keys key)
        => curKeyState.IsKeyDown(key);

    public static bool IsKeyJustPressed(Keys key)
        => !preKeyState.IsKeyDown(key) && curKeyState.IsKeyDown(key);

    public static bool IsKeyJustReleased(Keys key)
        => preKeyState.IsKeyDown(key) && !curKeyState.IsKeyDown(key);

    public static ButtonState GetMouseButtonState(in MouseState mouseState, MouseButton mouseButton)
        => mouseButton switch
        {
            MouseButton.Left => mouseState.LeftButton,
            MouseButton.Middle => mouseState.MiddleButton,
            MouseButton.Right => mouseState.RightButton,
            _ => throw new NotSupportedException(MouseButtonNotSupport)
        };

    public static bool IsMouseButtonPressed(MouseButton mouseButton)
        => GetMouseButtonState(curMouseState, mouseButton) == ButtonState.Pressed;

    public static bool IsMouseButtonJustPressed(MouseButton mouseButton)
        => GetMouseButtonState(preMouseState, mouseButton) == ButtonState.Released &&
           GetMouseButtonState(curMouseState, mouseButton) == ButtonState.Pressed;

    public static bool IsMouseButtonJustReleased(MouseButton mouseButton)
        => GetMouseButtonState(preMouseState, mouseButton) == ButtonState.Pressed &&
           GetMouseButtonState(curMouseState, mouseButton) == ButtonState.Released;
}

public enum MouseButton
{
    Left = 1,
    Middle = 2,
    Right = 3
}