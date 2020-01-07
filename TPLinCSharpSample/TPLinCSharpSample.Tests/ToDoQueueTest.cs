namespace TPLinCSharpSample.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    public class ToDoQueueTest 
    {
        [TestMethod]
        public void Ctor_AlwaysReturns_ValidInstance()
        {
            // Act
            var fixture = this.CreateFixture();

            // Assert
            fixture.ShouldNotBeNull();
        }

        [TestMethod]
        public void AddTrade_OnAdd_PushesIntoTradeQueue()
        {
            //Arrange
            var fixture = this.CreateFixture();
            var stubSalesPerson = new SalesPerson("Kiran");
            var stubTrade = new Trade(stubSalesPerson, 10);

            //Act
            fixture.AddTrade(stubTrade);

            //Assert
            fixture.TradeQueue.Count.ShouldBe(1);
        }

        [TestMethod]
        public void TradeQueue_OnCompleteAdding_ProcessesEntireQueue()
        {
            //Arrange
            var fixture = this.CreateFixture();

            //Act
            fixture.CompleteAdding();

            //Assert
            fixture.TradeQueue.Count.ShouldBe(0);

        }

        private ToDoQueue CreateFixture()
        {
            var stubStaffLogsForBonuses = new StaffLogsForBonuses();
            var fixture = new ToDoQueue(stubStaffLogsForBonuses);
            return fixture;
        }
    }
}
