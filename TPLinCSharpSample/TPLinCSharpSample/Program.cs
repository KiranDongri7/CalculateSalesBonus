namespace TPLinCSharpSample
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    class Program
    {
        public static readonly List<string> AllShirtNames = new List<string>
        { 
            "Levis", 
            "Roadster",
            "KGF",
            "Rowdy" 
        };

        static void Main(string[] args)
        {

            StaffLogsForBonuses staffLogs = new StaffLogsForBonuses();
            ToDoQueue toDoQueue = new ToDoQueue(staffLogs);

            SalesPerson[] people = {   new SalesPerson("Kiran"),
                                       new SalesPerson("Ratan"),
                                       new SalesPerson("Vishal") };

            var controller = new StockController(toDoQueue);

            var workDay = new TimeSpan(0, 0, 1);

            var t1 = Task.Run(() => people[0].Work(controller, workDay));
            var t2 = Task.Run(() => people[1].Work(controller, workDay));
            var t3 = Task.Run(() => people[2].Work(controller, workDay));
            
            var bonusLogger = Task.Run(() => toDoQueue.MonitorAndLogTrades());
            var bonusLogger2 = Task.Run(() => toDoQueue.MonitorAndLogTrades());

            Task.WaitAll(t1, t2, t3);

            toDoQueue.CompleteAdding();

            Task.WaitAll(bonusLogger, bonusLogger2);

            controller.DisplayStatus();
            staffLogs.DisplayReport(people);

            Console.ReadKey();
        }
    }
}
