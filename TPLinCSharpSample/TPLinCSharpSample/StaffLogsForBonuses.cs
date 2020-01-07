namespace TPLinCSharpSample
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;

    public class StaffLogsForBonuses
    {
        public ConcurrentDictionary<SalesPerson, int> salesByPerson = new ConcurrentDictionary<SalesPerson, int>();
        public ConcurrentDictionary<SalesPerson, int> purchasesByPerson = new ConcurrentDictionary<SalesPerson, int>();

        public void ProcessTrade(Trade sale)
        {
            Thread.Sleep(300);

            if (sale.QuantitySold > 0)
            {
                this.salesByPerson.AddOrUpdate(
                    sale.Person,
                    sale.QuantitySold,
                    (key, oldValue) => oldValue + sale.QuantitySold);
            }
            else
            {
                this.purchasesByPerson.AddOrUpdate(
                   sale.Person,
                   -sale.QuantitySold,
                   (key, oldValue) => oldValue - sale.QuantitySold);
            }
        }


        public void DisplayReport(SalesPerson[] people)
        {
            Console.WriteLine();
            Console.WriteLine("Transactions by salesperson:");
            foreach (SalesPerson person in people)
            {
                int sales = this.salesByPerson.GetOrAdd(person, 0);
                int purchases = this.purchasesByPerson.GetOrAdd(person, 0);
                Console.WriteLine("{0,15} sold {1,3}, bought {2,3} items, total {3}", person.Name, sales, purchases, sales + purchases);
            }
        }
    }
}
