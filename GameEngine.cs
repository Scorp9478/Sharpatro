using System;
using System.Collections.Generic;
using System.Linq;

namespace Sharpatro
{
    public class GameEngine
    {
        private Deck _deck;
        private Hand _hand;

        private int _targetScore;
        private int _currentScore;
        private int _handsLeft;
        private int _discardsLeft;

        public GameEngine(int targetScore = 300, int hands = 4, int discards = 3)
        {
            _deck = new Deck();
            _hand = new Hand(8);
            _targetScore = targetScore;
            _currentScore = 0;
            _handsLeft = hands;
            _discardsLeft = discards;
        }

        public void StartRound()
        {
            _deck.Reset();
            _hand.Clear();
            _currentScore = 0;

            RefillHand();

            bool roundOver = false;

            while (!roundOver)
            {
                DisplayGameState();

                Console.WriteLine("\n¿Qué quieres hacer?");
                Console.WriteLine(" - Para jugar cartas, escribe los números separados por espacios (ej: 1 2 3)");
                Console.WriteLine(" - Para descartar cartas, escribe 'd' seguido de los números (ej: d 4 5)");
                Console.Write("\nTu elección: ");

                string input = (Console.ReadLine() ?? "").Trim();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("\nEntrada no válida. Pulsa Enter para continuar...");
                    Console.ReadLine();
                    continue;
                }

                if (input.StartsWith("d ", StringComparison.OrdinalIgnoreCase) || input.Equals("d", StringComparison.OrdinalIgnoreCase))
                {
                    HandleDiscard(input);
                }
                else
                {
                    HandlePlay(input);
                }

                if (_currentScore >= _targetScore)
                {
                    DisplayGameState();
                    Console.WriteLine("\n ¡VICTORIA! Has superado la ciega requerida.");
                    roundOver = true;
                }
                else if (_handsLeft <= 0)
                {
                    DisplayGameState();
                    Console.WriteLine("\n GAME OVER: Te has quedado sin manos antes de alcanzar el objetivo.");
                    roundOver = true;
                }
            }
        }

        private void RefillHand()
        {
            while (_hand.Count < 8)
            {
                _hand.AddCard(_deck.Draw());
            }
        }

        private void DisplayGameState()
        {
            Console.Clear();
            Console.WriteLine("===========================================");
            Console.WriteLine($" PUNTUACIÓN: {_currentScore} / {_targetScore}");
            Console.WriteLine($" MANOS RESTANTES: {_handsLeft}  |  DESCARTES RESTANTES: {_discardsLeft}");
            Console.WriteLine("===========================================\n");

            _hand.PrintHand();
        }

        private void HandlePlay(string input)
        {
            List<int> indices = ParseIndices(input);

            if (indices.Count == 0 || indices.Count > 5)
            {
                Console.WriteLine("\nDebes seleccionar entre 1 y 5 cartas para jugar. Pulsa Enter...");
                Console.ReadLine();
                return;
            }

            List<Card> selectedCards = new List<Card>();

            foreach (int index in indices.OrderByDescending(i => i))
            {
                selectedCards.Add(_hand.RemoveCardAt(index));
            }

            selectedCards.Reverse();

            HandScore score = PokerHandEvaluator.Evaluate(selectedCards);
            _currentScore += score.TotalScore;
            _handsLeft--;

            Console.WriteLine($"\n¡Jugada realizada! -> {score.Type}");
            Console.WriteLine($"Puntos obtenidos: ({score.BaseChips} + {score.CardsChips}) * {score.Mult} = {score.TotalScore}");
            Console.WriteLine("Pulsa Enter para continuar...");
            Console.ReadLine();

            RefillHand();
        }

        private void HandleDiscard(string input)
        {
            if (_discardsLeft <= 0)
            {
                Console.WriteLine("\nNo te quedan descartes. Pulsa Enter para continuar...");
                Console.ReadLine();
                return;
            }

            string numbersPart = input.Length > 1 ? input.Substring(2).Trim() : "";
            List<int> indices = ParseIndices(numbersPart);

            if (indices.Count == 0 || indices.Count > 5)
            {
                Console.WriteLine("\nDebes seleccionar entre 1 y 5 cartas para descartar. Pulsa Enter...");
                Console.ReadLine();
                return;
            }

            foreach (int index in indices.OrderByDescending(i => i))
            {
                _hand.RemoveCardAt(index);
            }

            _discardsLeft--;

            Console.WriteLine($"\nHas descartado {indices.Count} carta(s). Pulsa Enter para continuar...");
            Console.ReadLine();

            RefillHand();
        }

        private List<int> ParseIndices(string input)
        {
            List<int> indices = new List<int>();
            string[] parts = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string part in parts)
            {
                if (int.TryParse(part, out int number))
                {
                    int zeroBasedIndex = number - 1;
                    if (zeroBasedIndex >= 0 && zeroBasedIndex < _hand.Count)
                    {
                        if (!indices.Contains(zeroBasedIndex))
                        {
                            indices.Add(zeroBasedIndex);
                        }
                    }
                }
            }

            return indices;
        }
    }
}