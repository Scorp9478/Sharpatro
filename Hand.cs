namespace Sharpatro
{
    public class Hand
    {
        private List<Card> _cards;
        private int _maxSize;

        public int Count => _cards.Count;

        public Hand(int maxSize = 8)
        {
            _cards = new List<Card>();
            _maxSize = maxSize;
        }

        public bool AddCard(Card card)
        {
            if (_cards.Count >= _maxSize)
            {
                return false;
            }

            _cards.Add(card);
            return true;
        }

        public Card RemoveCardAt(int index)
        {
            if (index < 0 || index >= _cards.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "El índice de la carta no es válido.");
            }

            Card removedCard = _cards[index];
            _cards.RemoveAt(index);
            return removedCard;
        }

        public void PrintHand()
        {
            Console.WriteLine("--- TU MANO ---");

            if (_cards.Count == 0)
            {
                Console.WriteLine("(La mano está vacía)");
                return;
            }

            for (int i = 0; i < _cards.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {_cards[i]}");
            }
        }

        public void Clear()
        {
            _cards.Clear();
        }
    }
}