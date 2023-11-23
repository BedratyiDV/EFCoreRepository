using Microsoft.EntityFrameworkCore;

namespace EFCoreRepository
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var movie1 = new Movie()
            {
                Articul = 1013,
                Name = "OrdinaryMovie3",
                IsAdult = false
            };
            var movie2 = new Movie()
            {
                Articul = 2013,
                Name = "AdultMovie3",
                IsAdult = true
            };

            using (var myDb = new MlDbContext())

            {
                //Add section

                myDb.Movies.Add(movie1);
                myDb.Movies.Add(movie2);

                //Update section

                var omm = myDb.Movies.FirstOrDefault(x => x.Articul == 1013);

                if ((omm != null) && (omm.Name != "Edited_OrdinaryMovie3"))
                { 
                omm.Name = "Edited_OrdinaryMovie3";
                }

                //Delete section

                var movieToDelete = myDb.Movies.Find(Guid.Parse("2B6C8712-77E8-4CF2-A05A-E26F10C2AF24"));

                if (movieToDelete != null)
                {
                    Console.WriteLine($"{movieToDelete.Id}, {movieToDelete.Articul}, {movieToDelete.Name}");
                    myDb.Movies.Remove(movieToDelete);
                }

                myDb.SaveChanges();

                //Read section

                var allMovies = myDb.Movies.ToList();

                foreach (var m in allMovies)

                {
                    Console.WriteLine($"Movie Id {m.Id} \t Movie Articul: {m.Articul}  " +
                        $"\t movie Name: {m.Name} \t Is Adult: {m.IsAdult}");
                    Console.WriteLine();
                }
                //Section for checking "Foreach" and Indexator

                var lib = new MovieLibrary(myDb);

                foreach (var m in lib)

                {
                    Console.WriteLine($"Movie: {m}");
                }
                var qwe = lib[2013];
                Console.WriteLine(qwe);
            }
        }
    }
}