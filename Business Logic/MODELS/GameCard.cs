namespace Business_Logic.MODELS;
public record GameCard
{
    public KeyValuePair<string, int> CardSuit { get; set; }
    public KeyValuePair<string, int> CardRank { get; set; }
    public string ImgPath { get; set; }

    public GameCard()
    {
    }

    public GameCard(KeyValuePair<string, int> cardSuit, KeyValuePair<string, int> cardRank)
    {
        CardSuit = cardSuit;
        CardRank = cardRank;
    }
}
