using Godot;

public partial class StatsDisplay : Label
{
    [Export] StatsType StatsType;

    private bool _isPopping = false;

    private float _maxScale = 1.2f;
    private float _scale = 1.0f;

    public StatsDisplay()
    {
        Stats.StatsChanged += Stats_StatsChanged;
    }

    public override void _Ready()
    {
        SetText();
    }

    public override void _ExitTree()
    {
        Stats.StatsChanged -= Stats_StatsChanged;
    }

    public override void _Process(double delta)
    {
        if (_isPopping)
        {
            if (_scale < _maxScale)
            {
                _scale += 1f * (float)delta;

                if (_scale > _maxScale)
                    _scale = _maxScale;

                Scale = new Vector2(1, _scale);
            }
            else
            {
                _isPopping = false;
            }
        }
        else if (_scale > 1.0f)
        {
            _scale = 1.0f;
            Scale = Vector2.One;
        }
    }

    private void Stats_StatsChanged(StatsType statsType)
    {
        SetText();

        if (StatsType == StatsType.Unknown || StatsType == statsType)
            Pop();
    }

    private void SetText()
    {
        switch (StatsType)
        {
            case StatsType.AgentCounter:
                Text = Stats.AgentCounter.ToString("00");
                break;

            case StatsType.AgentSpeed:
                Text = Stats.AgentSpeed.ToString("00");
                break;

            case StatsType.BulletCounter:
                Text = Stats.BulletCounter.ToString("00");
                break;

            case StatsType.BulletSpeed:
                Text = Stats.BulletSpeed.ToString("00");
                break;
        }
    }

    private void Pop()
    {
        Scale = Vector2.One;
        _isPopping = true;
    }
}