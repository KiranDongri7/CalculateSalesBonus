namespace TPLinCSharpSample
{
    using System;
    using System.Collections.Concurrent;

    public class ToDoQueue 
    {
        private readonly BlockingCollection<Trade> queue;
        private readonly StaffLogsForBonuses staffLogs;
        
        public ToDoQueue(StaffLogsForBonuses staffResults)
        {
            this.queue = new BlockingCollection<Trade>(new ConcurrentBag<Trade>());
            this.staffLogs = staffResults;
        }

        public BlockingCollection<Trade> TradeQueue
        {
            get => this.queue;
        }

        public void AddTrade(Trade transaction)
        {
            this.queue.Add(transaction);
        }

        public void CompleteAdding()
        {
            this.queue.CompleteAdding();
        }

        public void MonitorAndLogTrades()
        {
            while (true)
            {
                try
                {
                    Trade nextTransaction = this.queue.Take();
                    this.staffLogs.ProcessTrade(nextTransaction);
                    Console.WriteLine("Processing transaction from " + nextTransaction.Person.Name);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
        }
    }
}
