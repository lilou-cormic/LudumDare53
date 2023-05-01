using Godot;

public partial class CurrencyDisplay : Label
{
    private bool _isPopping = false;

    private float _maxScale = 1.2f;
    private float _scale = 1.0f;

    public CurrencyDisplay()
    {
        GameManager.CurrencyChanged += CurrencyManager_CurrencyChanged;
    }

    public override void _Ready()
    {
        SetText();
    }

    public override void _ExitTree()
    {
        GameManager.CurrencyChanged -= CurrencyManager_CurrencyChanged;
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

    private void CurrencyManager_CurrencyChanged()
    {
        SetText();

        Pop();
    }

    private void SetText()
    {
        Text = GameManager.Currency.ToString("000");
    }

    private void Pop()
    {
        Scale = Vector2.One;
        _isPopping = true;
    }
}