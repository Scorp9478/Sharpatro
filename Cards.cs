namespace Sharpatro
{
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum Rank
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14
    }

    public class Card
    {
        public Suit Suit { get; private set; }
        public Rank Rank { get; private set; }
        public int BaseChips { get; private set; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
            BaseChips = GetBaseChips(rank);
        }

        private int GetBaseChips(Rank rank)
        {
            return rank switch
            {
                Rank.Jack or Rank.Queen or Rank.King => 10,
                Rank.Ace => 11,
                _ => (int)rank
            };
        }

        public override string ToString()
        {
            string rankSymbol = Rank switch
            {
                Rank.Ace => "A",
                Rank.King => "K",
                Rank.Queen => "Q",
                Rank.Jack => "J",
                _ => ((int)Rank).ToString()
            };

            string suitSymbol = Suit switch
            {
                Suit.Hearts => "♥",
                Suit.Diamonds => "♦",
                Suit.Clubs => "♣",
                Suit.Spades => "♠",
                _ => throw new System.ArgumentOutOfRangeException()
            };

            return rankSymbol + suitSymbol;
        }
    }
}