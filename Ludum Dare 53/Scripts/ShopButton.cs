using Godot;

public partial class ShopButton : TextureButton
{
    [Export] int Price = 150;

    public override void _Ready()
    {
        GetNode<Label>("PriceText").Text = Price.ToString();

        GameManager.CurrencyChanged += GameManager_CurrencyChanged;
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
        Disabled = Price > GameManager.Currency;
    }

    public void OnPressed()
    {
        GameManager.ChangeCurrency(-Price);
    }
}
