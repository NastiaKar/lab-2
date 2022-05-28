using static System.Console;
using System.Text.Json;
using System.Threading;

namespace Lab_2
{
    class Beer : IDisposable
    {
        private int _beerBottles;
        public string BeerName { get; set; }
        public int BeerBottles
        {
            set
            {
                if (value > 0)
                    _beerBottles = value;
            }
            get => _beerBottles;
        }
        public Beer(string beerName = "Corona", int beerBottles = 3)
        {
            BeerName = beerName;
            BeerBottles = beerBottles;
        }
        public void Print() => WriteLine($"Name: {BeerName}     Amount: {BeerBottles}");
        
        public void Dispose()
        {
            WriteLine("Deleted.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Beer defBeer = new Beer();
            Beer favBeer = new Beer("Blanc", 1024987);

            defBeer.Print();
            favBeer.Print();
            WriteLine(new string('-', 50));
            
            WriteLine();
            Thread.Sleep(500);
            
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.SerializeAsync(fs, favBeer);
                WriteLine("Data has been saved.");
            }
            
            WriteLine();
            Thread.Sleep(500);
            WriteLine(new string('-', 50));
            WriteLine();
            Thread.Sleep(500);
            
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                Beer? desBeer = JsonSerializer.Deserialize<Beer>(fs);
                WriteLine(desBeer.BeerName + " " + desBeer.BeerBottles);
                WriteLine("Data has been restored!");
            }
            
            WriteLine();
            Thread.Sleep(500);
            WriteLine(new string('-', 50));
            WriteLine();
            
            Thread.Sleep(500);
            defBeer.Dispose();
            Thread.Sleep(500);
            favBeer.Dispose();
            ReadKey();
        }
    }
}

