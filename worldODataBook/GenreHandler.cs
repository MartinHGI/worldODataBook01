using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worldODataBook
{
    internal class GenreHandler
    {
        GenreDAO genreDAO = new GenreDAO();

        public void Execute()
        {
            Console.WriteLine("Vyberte operaci pro tabulku Genre:");
            Console.WriteLine("1. Vložit do databáze");
            Console.WriteLine("2. Smazat z databáze");
            Console.WriteLine("3. Úprava záznamu");
            Console.WriteLine("4. Výpis databáze");
            Console.WriteLine("5. Vložit do databáze ze souboru");

            int operation = int.Parse(Console.ReadLine());

            int id;
            string name;

            switch (operation)// další switch který podle vooby uživatel zavolá fce z DAO
            {
                case 1:// Vložení nového záznamu do databáze
                    Console.Write("Jméno žánru: ");
                    name = Console.ReadLine();
                    Genre genreS = new Genre(name);

                    genreDAO.Save(genreS);
                    break;
                case 2:// Smazání záznamu z databáze
                    Console.Write("Zadajte id žánru: ");
                    id = Convert.ToInt32(Console.ReadLine());

                    genreDAO.Delete(id);
                    break;
                case 3:// Úprava záznamu v databázi
                    Console.Write("Zadejte id žánru který chcete upravit: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Jméno žánru: ");
                    name = Console.ReadLine();
                    Genre genreU = new Genre(id, name);

                    genreDAO.Save(genreU);
                    break;
                case 4:// Výpis všech záznamů v databázi
                    Console.WriteLine("List žánrů");
                    foreach (Genre i in genreDAO.GetAll())
                    {
                        Console.WriteLine(i);
                    }

                    break;
                case 5:// Import záznamů ze souboru do databáze

                    genreDAO.Import();


                    break;
                default:
                    Console.WriteLine("Neplatná volba.");
                    break;
            }
        }
    }
}
