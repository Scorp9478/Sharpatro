using System;

namespace Sharpatro
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Sharpatro - Balatro en C#";

            GameEngine engine = new GameEngine(targetScore: 300, hands: 4, discards: 3);
            engine.StartRound();

            Console.WriteLine("\n¡Gracias por jugar a Sharpatro!");
            Console.WriteLine("Pulsa cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}