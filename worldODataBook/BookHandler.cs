using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worldODataBook
{
    internal class BookHandler
    {
        BookDAO bookDAO = new BookDAO();

        public void Execute()
        {
            Console.WriteLine("Vyberte operaci pro tabulku Book:");
            Console.WriteLine("1. Vložit do databáze");
            Console.WriteLine("2. Smazat z databáze");
            Console.WriteLine("3. Úprava záznamu");
            Console.WriteLine("4. Výpis databáze");
            Console.WriteLine("5. Vložit do databáze ze souboru");

            int operation = int.Parse(Console.ReadLine());

            int id;
            int genre_id;
            int author_id;
            string name;
            string release_date;

            switch (operation)// další switch který podle vooby uživatel zavolá adekvátní fce z DAO
            {
                case 1:
                    Console.Write("Id žánru: ");
                    genre_id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Id autora: ");
                    author_id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Jmeno knihy: ");
                    name = Console.ReadLine();
                    Console.Write("Den vydání: ");
                    release_date = Console.ReadLine();
                    Book bookSave = new Book(genre_id, author_id, name, release_date);

                    bookDAO.Save(bookSave);
                    break;
                case 2:
                    Console.Write("Zadajte id knihy kterou chcete smazat: ");
                    id = Convert.ToInt32(Console.ReadLine());

                    bookDAO.Delete(id);
                    break;
                case 3:
                    Console.Write("Zadejte id knihy kterou chcete upravit: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Id žánru: ");
                    genre_id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Id autora: ");
                    author_id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Jmeno knihy: ");
                    name = Console.ReadLine();
                    Console.Write("Den vydání: ");
                    release_date = Console.ReadLine();
                    Book bookUpdate = new Book(id, genre_id, author_id, name, release_date);

                    bookDAO.Save(bookUpdate);
                    break;
                case 4:
                    Console.WriteLine("List knih");
                    foreach (Book i in bookDAO.GetAll())
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
