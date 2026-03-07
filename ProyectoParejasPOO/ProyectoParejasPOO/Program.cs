namespace ProyectoParejasPOO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Presiona cualquier tecla para comenzar...");
            Console.ReadKey();
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }
    }
}
