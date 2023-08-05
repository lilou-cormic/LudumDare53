using Godot;

public partial class ShopButton : TextureButton
{
    [Export] int Price = 150;

    [Export] int NumberOfUses = 0; // 0 for infinite

    [Export] private int PriceIncreasePercentage = 0;

    private int _NumberOfUsesLeft = 0;

    public override void _Ready()
    {
        GetNode<Label>("PriceText").Text = Price.ToString();

        GameManager.CurrencyChanged += GameManager_CurrencyChanged;

        _NumberOfUsesLeft = NumberOfUses;

        SetActive();
    }

    public override void _ExitTree()
    {
        GameManager.CurrencyChanged -= GameManager_CurrencyChanged;
    }

    private void GameManager_CurrencyChanged()
    {
        SetActive();
    }

    private void SetActive()
    {
        if (NumberOfUses > 0 && _NumberOfUsesLeft <= 0)
            Disabled = true;
        else
            Disabled = Price > GameManager.Currency;
    }

    public void OnPressed()
    {
        Price += (Price * PriceIncreasePercentage) / 100;
        Price -= Price % 5;
        GetNode<Label>("PriceText").Text = Price.ToString();

        if (NumberOfUses > 0 && _NumberOfUsesLeft > 0)
        {
            _NumberOfUsesLeft--;

            if (_NumberOfUsesLeft <= 0)
                GetNode<Label>("PriceText").Text = "---";
        }

        GetNode<AudioStreamPlayer2D>("SoundPlayer").Play();

        SetActive();

        GameManager.ChangeCurrency(-Price);
    }
}
