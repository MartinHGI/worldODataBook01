using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worldODataBook
{

    // Třída pro manipulaci s tabulkou autorů.
    internal class AuthorHandler
    {
        AuthorDAO authorDAO = new AuthorDAO();
        Program program = new Program();
        public void Execute()
        {
            // Výpis možností pro manipulaci s databází autorů
            Console.WriteLine("Vyberte operaci pro tabulku Author:");  
            Console.WriteLine("1. Vložit do databáze");
            Console.WriteLine("2. Smazat z databáze");
            Console.WriteLine("3. Úprava záznamu");
            Console.WriteLine("4. Výpis databáze");
            Console.WriteLine("5. Vložit do databáze ze souboru");

            int operation = int.Parse(Console.ReadLine());// Čtení volby

            int id;
            string name;
            string surname;
            string birth_date;
            string CSVName;

            switch (operation)// další switch který podle vooby uživatel zavolá fce z DAO
            {
                

                case 1: // Vložení nového záznamu do databáze
                    Console.Write("Jmeno autora: ");
                    name = Console.ReadLine();
                    Console.Write("Příjmení autora: ");
                    surname = Console.ReadLine();
                    Console.Write("Datum narození autora ve formátu (rok-měsíc-den): ");
                    birth_date = Console.ReadLine();
                    Author authorSave = new Author(name, surname, birth_date);
                    authorDAO.Save(authorSave);

                    break;
                case 2:// Smazání záznamu z databáze
                    Console.Write("Zadajte id autora: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    authorDAO.Delete(id);

                    break;
                case 3:// Úprava záznamu v databázi
                    Console.Write("Zadejte id autora: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Jmeno autora: ");
                    name = Console.ReadLine();
                    Console.Write("Příjmení autora: ");
                    surname = Console.ReadLine();
                    Console.Write("Datum narození autora ve formátu (rok-měsíc-den): ");
                    birth_date = Console.ReadLine();
                    Author authorUpdate = new Author(id, name, surname, birth_date);
                    authorDAO.Save(authorUpdate);

                    break;
                case 4:// Výpis všech záznamů v databázi
                    Console.WriteLine("List autorů");
                    foreach (Author i in authorDAO.GetAll())
                    {
                        Console.WriteLine(i);
                    }

                    break;
                case 5:// Import záznamů ze souboru do databáze

                    authorDAO.Import();

                    break;
                default:
                    Console.WriteLine("Neplatná volba.");
                    break;
            }
        }
    }
}
