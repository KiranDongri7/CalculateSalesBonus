namespace TPLinCSharpSample
{
    using System;
    using System.Threading;

    public class SalesPerson
    {
        public string Name { get; private set; }

        public SalesPerson(string personName)
        {
            this.Name = personName;
        }

        public void Work(StockController stockController, TimeSpan workDay)
        {
            var random = new Random(this.Name.GetHashCode());
            var start = DateTime.Now;
            while (DateTime.Now - start < workDay)
            {
                Thread.Sleep(random.Next(100));
                var buy = (random.Next(6) == 0);
                var itemName = Program.AllShirtNames[random.Next(Program.AllShirtNames.Count)];
                if (buy)
                {
                    var quantity = random.Next(9) + 1;
                    stockController.BuyStock(this, itemName, quantity);
                    DisplayPurchase(itemName, quantity);
                }
                else
                {
                    var success = stockController.TrySellItem(this, itemName);
                    DisplaySaleAttempt(success, itemName);
                }
            }
            Console.WriteLine("SalesPerson {0} signing off", this.Name);
        }

        public void DisplayPurchase(string itemName, int quantity)
        {
            Console.WriteLine("Thread {0}: {1} bought {2} of {3}", Thread.CurrentThread.ManagedThreadId, this.Name, quantity, itemName);
        }

        public void DisplaySaleAttempt(bool success, string itemName)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            if (success)
            {
                Console.WriteLine(string.Format("Thread {0}: {1} sold {2}", threadId, this.Name, itemName));
            }
            else
            {
                Console.WriteLine(string.Format("Thread {0}: {1}: Out of stock of {2}", threadId, this.Name, itemName));
            }
        }
    }
}
