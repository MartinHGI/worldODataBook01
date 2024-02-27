using System;
using DAOTretak;

namespace worldODataBook
{
    class Program
    {
        static void Main(string[] args)
        {

            // Inicializace handlerů pro jednotlivé tabulky
            AuthorHandler author = new AuthorHandler();
            GenreHandler genre = new GenreHandler();
            BookHandler book = new BookHandler();
            CustomerHandler customer = new CustomerHandler();
            PurchaseHandler purchase = new PurchaseHandler();

            bool i = true;
            while (i)
            {
                try
                {
                    Console.WriteLine("Vyberte tabulku:");
                    Console.WriteLine("1. Author");
                    Console.WriteLine("2. Genre");
                    Console.WriteLine("3. Book");
                    Console.WriteLine("4. Customer");
                    Console.WriteLine("5. Purchase");
                    Console.WriteLine("6. End");

                    int Choice = int.Parse(Console.ReadLine());


                    // Výběr odpovídajícího handleru pro danou tabulku
                    switch (Choice)//
                    {
                        case 1:
                            author.Execute();
                            break;
                        case 2:
                            genre.Execute();
                            break;
                        case 3:
                            book.Execute();
                            break;
                        case 4:
                            customer.Execute();
                            break;
                        case 5:
                            purchase.Execute();
                            break;
                        case 6:
                            i = false;
                            break;
                        default:
                            Console.WriteLine("Neplatná volba.");
                            return;
                    }
                }
                catch (Exception e)
                {
                    // Zpráva o chybě v případě výjimky
                    Console.WriteLine(e + "něco jsi udělal špatně zkus to znovu a pokud problém setrvá kontaktujte podporu");
                }
            }

            /*
			Dictionary<string, OperationHandler> actions = new Dictionary<string, OperationHandler>();
			actions.Add("author", new HandlerAuthor());
			actions.Add("genre", new HandlerGenre());
			actions.Add("book", new HandlerBook());
			actions.Add("customer", new HandlerCustomer());
			actions.Add("purchase", new HandlerPurchase());
			*/


          

            
            

           




        }
    }

}    