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

            Reset();
        }

        public void Reset()
        {
            _cards.Clear();

            foreach (Suit suit in Enum.GetValues<Suit>())
            {
                foreach (Rank rank in Enum.GetValues<Rank>())
                {
                    _cards.Add(new Card(suit, rank));
                }
            }

            Shuffle();
        }

        public void Shuffle()
        {
            for (int i = _cards.Count - 1; i > 0; i--)
            {
                int randomIndex = _random.Next(i + 1);

                Card temp = _cards[i];
                _cards[i] = _cards[randomIndex];
                _cards[randomIndex] = temp;
            }
        }

        public Card Draw()
        {
            if (_cards.Count == 0)
            {
                throw new InvalidOperationException("No quedan cartas en el mazo.");
            }

            Card drawnCard = _cards[0];
            _cards.RemoveAt(0);
            return drawnCard;
        }
    }
}