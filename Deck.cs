namespace Sharpatro
{
    public class Deck
    {
        private List<Card> _cards;
        private Random _random;

        public Deck()
        {
            _cards = new List<Card>();
            _random = new Random();
        }
    }
}