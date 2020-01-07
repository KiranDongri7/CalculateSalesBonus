namespace TPLinCSharpSample.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TPLinCSharpSample;
    using Shouldly;

    [TestClass]
    public class StaffLogsForBonusesTests
    {
        [TestMethod]
        public void ProcessTrade_AlwaysIncreases_PurchasesByPerson()
        {
            // Arrange
            var staffResult = new StaffLogsForBonuses();
            var stubPerson = new SalesPerson("Kiran");
            var stubSale = new Trade(stubPerson, 0);

            // Act
            staffResult.ProcessTrade(stubSale);

            // Assert
            staffResult.purchasesByPerson.Count.ShouldBe(1);
            staffResult.salesByPerson.Count.ShouldBe(0);
        }

        [TestMethod]
        public void ProcessTrade_AlwaysIncreases_SalesByPerson()
        {
            // Arrange
            var staffResult = new StaffLogsForBonuses();
            var stubPerson = new SalesPerson("Kiran");
            var stubSale = new Trade(stubPerson, 10);

            // Act
            staffResult.ProcessTrade(stubSale);

            // Assert
            staffResult.salesByPerson.Count.ShouldBe(1);
            staffResult.purchasesByPerson.Count.ShouldBe(0);
        }
    }
}