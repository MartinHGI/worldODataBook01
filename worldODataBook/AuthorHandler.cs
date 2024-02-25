using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worldODataBook
{
    internal class AuthorHandler
    {
        AuthorDAO authorDAO = new AuthorDAO();
        Program program = new Program();
        public void Execute()
        {
            //výpis možností
            Console.WriteLine("Vyberte operaci pro tabulku Author:");  
            Console.WriteLine("1. Vložit do databáze");
            Console.WriteLine("2. Smazat z databáze");
            Console.WriteLine("3. Úprava záznamu");
            Console.WriteLine("4. Výpis databáze");
            Console.WriteLine("5. Vložit do databáze ze souboru");

            int operation = int.Parse(Console.ReadLine());

            int id;
            string name;
            string surname;
            string birth_date;

            switch (operation)// další switch který podle vooby uživatel zavolá adekvátní fce z DAO
            {
                

                case 1:
                    Console.Write("Jmeno autora: ");
                    name = Console.ReadLine();
                    Console.Write("Příjmení autora: ");
                    surname = Console.ReadLine();
                    Console.Write("Datum narození autora ve formátu (rok-měsíc-den): ");
                    birth_date = Console.ReadLine();
                    Author authorSave = new Author(name, surname, birth_date);
                    authorDAO.Save(authorSave);

                    break;
                case 2:
                    Console.Write("Zadajte id autora: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    authorDAO.Delete(id);

                    break;
                case 3:
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
                case 4:
                    Console.WriteLine("List autorů");
                    foreach (Author i in authorDAO.GetAll())
                    {
                        Console.WriteLine(i);
                    }

                    break;
                case 5:



                    break;
                default:
                    Console.WriteLine("Neplatná volba.");
                    break;
            }
        }
    }
}
