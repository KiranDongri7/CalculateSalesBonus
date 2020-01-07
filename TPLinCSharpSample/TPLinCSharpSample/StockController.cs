namespace TPLinCSharpSample
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading;

    public class StockController
    {
       private ConcurrentDictionary<string, int> stock;
       private int totalQuantityBought;
       private int totalQuantitySold;
       private ToDoQueue toDoQueue;

        public StockController(ToDoQueue bonusCalculator)
        {
            this.stock = new ConcurrentDictionary<string, int>();
            this.toDoQueue = bonusCalculator;
        }

        public void BuyStock(SalesPerson person, string item, int quantity)
        {
            this.stock.AddOrUpdate(item, quantity, (key, oldValue) => oldValue + quantity);
            Interlocked.Add(ref totalQuantityBought, quantity);
            this.toDoQueue.AddTrade(new Trade(person, -quantity));
        }

        public bool TrySellItem(SalesPerson person, string item)
        {
            var success = false;
            var newStockLevel = stock.AddOrUpdate(item,
                (itemName) => 
                {
                    success = false;
                    return 0;
                },
                (itemName, oldValue) => 
                {
                    if (oldValue == 0)
                    {
                        success = false;
                        return 0;
                    }
                    else
                    {
                        success = true;
                        return oldValue - 1;
                    }
                });
            if (success)
            {
                Interlocked.Increment(ref totalQuantitySold);
                this.toDoQueue.AddTrade(new Trade(person, 1));
            }
            return success;
        }

        public bool TrySellItem2(SalesPerson person, string item)
        {
            var newStockLevel = stock.AddOrUpdate(item, -1, (key, oldValue) => oldValue - 1);
            if (newStockLevel < 0)
            {
                this.stock.AddOrUpdate(item, 1, (key, oldValue) => oldValue + 1);
                return false;
            }
            else
            {
                Interlocked.Increment(ref totalQuantitySold);
                this.toDoQueue.AddTrade(new Trade(person, 1));
                return true;
            }
        }

        public void DisplayStatus()
        {
            var totalStock = this.stock.Values.Sum();
            Console.WriteLine("\r\nBought = " + this.totalQuantityBought);
            Console.WriteLine("Sold   = " + this.totalQuantitySold);
            Console.WriteLine("Stock  = " + totalStock);
            var error = totalStock + this.totalQuantitySold - this.totalQuantityBought;
            
            if (error == 0)
            {
                Console.WriteLine("Stock levels match");
            }
            else
            {
                Console.WriteLine("Error in stock level: " + error);
            }

            Console.WriteLine();
            Console.WriteLine("Stock levels by item:");

            foreach (string itemName in Program.AllShirtNames)
            {
                var stockLevel = this.stock.GetOrAdd(itemName, 0);
                Console.WriteLine("{0,-30}: {1}", itemName, stockLevel);
            }
        }
    }
}
