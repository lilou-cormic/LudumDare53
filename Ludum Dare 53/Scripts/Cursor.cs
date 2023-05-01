using Godot;

public partial class Cursor : Sprite2D
{
    private static Cursor _instance;

    private Texture2D _ArrowImage;

    private Texture2D _HandImage;

    private Texture2D _TargetImage;

    public Cursor()
    {
        _instance = this;
    }

    public override void _Ready()
    {
        _ArrowImage = GetNode<Sprite2D>("ArrowSprite").Texture;
        _HandImage = GetNode<Sprite2D>("HandSprite").Texture;
        _TargetImage = GetNode<Sprite2D>("TargetSprite").Texture;

        Input.MouseMode = Input.MouseModeEnum.Hidden;
    }

    public override void _ExitTree()
    {
        Input.MouseMode = Input.MouseModeEnum.Visible;
    }

    public override void _Process(double delta)
    {
        Position = GetGlobalMousePosition();
    }

    public override void _Notification(int what)
    {
        switch ((long)what)
        {
            case MainLoop.NotificationApplicationFocusOut:
                Visible = false;
                break;

            case MainLoop.NotificationApplicationFocusIn:
                Visible = true;
                break;
        }
    }

    public static void SetCursor(CursorType cursorType)
    {
        switch (cursorType)
        {
            case CursorType.Arrow:
                _instance.Texture = _instance._ArrowImage;
                break;

            case CursorType.Hand:
                _instance.Texture = _instance._HandImage;
                break;

            case CursorType.Target:
                _instance.Texture = _instance._TargetImage;
                break;
        }
    }
}