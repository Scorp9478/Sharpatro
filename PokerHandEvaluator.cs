namespace Sharpatro
{
    public enum HandType
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush
    }

    public class HandScore
    {
        public HandType Type { get; set; }
        public int BaseChips { get; set; }
        public int Mult { get; set; }
        public int CardsChips { get; set; }

        public int TotalScore => (BaseChips + CardsChips) * Mult;
    }

    public static class PokerHandEvaluator
    {
        public static HandScore Evaluate(List<Card> selectedCards)
        {
            if (selectedCards == null || selectedCards.Count == 0)
            {
                throw new ArgumentException("Debes seleccionar al menos una carta para jugar.");
            }

            HandType handType = DetermineHandType(selectedCards);
            (int baseChips, int mult) = GetBaseStats(handType);

            int cardsChips = 0;
            foreach (Card card in selectedCards)
            {
                cardsChips += card.BaseChips;
            }

            return new HandScore
            {
                Type = handType,
                BaseChips = baseChips,
                Mult = mult,
                CardsChips = cardsChips
            };
        }

        private static HandType DetermineHandType(List<Card> cards)
        {
            bool isFlush = CheckFlush(cards);
            bool isStraight = CheckStraight(cards);

            if (isFlush && isStraight) return HandType.StraightFlush;

            var rankGroups = cards.GroupBy(c => c.Rank).ToList();

            if (rankGroups.Any(g => g.Count() == 4)) return HandType.FourOfAKind;

            bool hasThree = rankGroups.Any(g => g.Count() == 3);
            bool hasTwo = rankGroups.Any(g => g.Count() == 2);
            int pairCount = rankGroups.Count(g => g.Count() == 2);

            if (hasThree && hasTwo) return HandType.FullHouse;
            if (isFlush) return HandType.Flush;
            if (isStraight) return HandType.Straight;
            if (hasThree) return HandType.ThreeOfAKind;
            if (pairCount == 2) return HandType.TwoPair;
            if (pairCount == 1) return HandType.Pair;

            return HandType.HighCard;
        }

        private static bool CheckFlush(List<Card> cards)
        {
            if (cards.Count < 5) return false;
            return cards.All(c => c.Suit == cards[0].Suit);
        }

        private static bool CheckStraight(List<Card> cards)
        {
            if (cards.Count < 5) return false;

            var orderedRanks = cards.Select(c => (int)c.Rank).OrderBy(r => r).ToList();

            for (int i = 0; i < orderedRanks.Count - 1; i++)
            {
                if (orderedRanks[i + 1] != orderedRanks[i] + 1)
                {
                    return false;
                }
            }

            return true;
        }

        private static (int baseChips, int mult) GetBaseStats(HandType type)
        {
            return type switch
            {
                HandType.StraightFlush => (100, 8),
                HandType.FourOfAKind => (60, 7),
                HandType.FullHouse => (40, 4),
                HandType.Flush => (35, 4),
                HandType.Straight => (30, 4),
                HandType.ThreeOfAKind => (30, 3),
                HandType.TwoPair => (20, 2),
                HandType.Pair => (10, 2),
                HandType.HighCard => (5, 1),
                _ => (0, 0)
            };
        }
    }
}