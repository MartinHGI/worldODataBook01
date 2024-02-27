using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace worldODataBook
{
    internal class CustomerHandler
    {
        CustomerDAO customerDAO = new CustomerDAO();

        public void Execute()
        {
            Console.WriteLine("Vyberte operaci pro tabulku Customer:");
            Console.WriteLine("1. Vložit do databáze");
            Console.WriteLine("2. Smazat z databáze");
            Console.WriteLine("3. Úprava záznamu");
            Console.WriteLine("4. Výpis databáze");
            Console.WriteLine("5. Vložit do databáze ze souboru");

            int operation = int.Parse(Console.ReadLine());

            int id;
            string name;
            string surname;
            string email;

            switch (operation)// další switch který podle vooby uživatel zavolá adekvátní fce z DAO
            {
                case 1:// Vložení nového záznamu do databáze
                    Console.Write("Jmeno zákazníka: ");
                    name = Console.ReadLine();
                    Console.Write("Příjmení zákazníka: ");
                    surname = Console.ReadLine();
                    Console.Write("Email zákazníka: ");
                    email = Console.ReadLine();
                    Customer customerSave = new Customer(name, surname, email);

                    customerDAO.Save(customerSave);
                    break;
                case 2:// Smazání záznamu z databáze
                    Console.Write("Zadajte id zákazníka: ");
                    id = Convert.ToInt32(Console.ReadLine());

                    customerDAO.Delete(id);
                    break;
                case 3:// Úprava záznamu v databázi
                    Console.Write("Zadejte id zákazníka: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Jmeno upraveného zákazníka: ");
                    name = Console.ReadLine();
                    Console.Write("Příjmení upraveného zákazníka: ");
                    surname = Console.ReadLine();
                    Console.Write("Email upraveného zákazníka: ");
                    email = Console.ReadLine();
                    Customer customerUpdate = new Customer(id, name, surname, email);

                    customerDAO.Save(customerUpdate);
                    break;
                case 4:// Výpis všech záznamů v databázi
                    Console.WriteLine("List Zákazníků");
                    foreach (Customer i in customerDAO.GetAll())
                    {
                        Console.WriteLine(i);
                    }

                    break;
                case 5:// Import záznamů ze souboru do databáze

                    customerDAO.Import();

                    break;
                default:
                    Console.WriteLine("Neplatná volba.");
                    break;
            }
        }
    }
}
