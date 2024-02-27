using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace worldODataBook
{
    internal class PurchaseHandler
    {
        PurchaseDAO purchaseDAO = new PurchaseDAO();

        public void Execute()
        {
            Console.WriteLine("Vyberte operaci pro tabulku Purchase:");
            Console.WriteLine("1. Vložit do databáze");
            Console.WriteLine("2. Smazat z databáze");
            Console.WriteLine("3. Úprava záznamu");
            Console.WriteLine("4. Výpis databáze");
            Console.WriteLine("5. Vložit do databáze ze souboru");

            int operation = int.Parse(Console.ReadLine());

            int id;
            int customer_id;
            int book_id;
            string purchase_date;

            switch (operation)// další switch který podle vooby uživatel zavolá adekvátní fce z DAO
            {
                case 1:// Vložení nového záznamu do databáze

                    Console.WriteLine("Id zákazníka: ");
                    customer_id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Id knihy: ");
                    book_id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Den vypůjčení: ");
                    purchase_date = Console.ReadLine();
                    Purchase purchaseSave = new Purchase(customer_id, book_id, purchase_date);

                    purchaseDAO.Save(purchaseSave);
                    break;
                case 2:// Smazání záznamu z databáze
                    Console.Write("Zadajte id obědnávky: ");
                    id = Convert.ToInt32(Console.ReadLine());


                    purchaseDAO.Delete(id);
                    break;
                case 3:// Úprava záznamu v databázi
                    Console.Write("Zadejte id obědnávky: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Id zákazníka: ");
                    customer_id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Id knihy: ");
                    book_id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Den vypůjčení: ");
                    purchase_date = Console.ReadLine();
                    Purchase purchaseUpdate = new Purchase(id, customer_id, book_id, purchase_date);

                    purchaseDAO.Save(purchaseUpdate);
                    break;
                case 4:// Výpis všech záznamů v databázi
                    Console.WriteLine("List košíků");
                    foreach (Purchase i in purchaseDAO.GetAll())
                    {
                        Console.WriteLine(i);
                    }


                    break;
                case 5:// Import záznamů ze souboru do databáze

                    purchaseDAO.Import();



                    break;
                default:
                    Console.WriteLine("Neplatná volba.");
                    break;
            }
        }
    }
}
