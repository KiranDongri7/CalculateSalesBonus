namespace TPLinCSharpSample.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    public class SalesPersonTest
    {
        [TestMethod]
        public void Ctor_AlwaysReturns_ValidInstance()
        {
            // Act
            var fixture = this.CreateFixture("Kiran");

            // Assert
            fixture.ShouldNotBeNull();
        }

        private SalesPerson CreateFixture(string person)
        {
            var fixture = new SalesPerson(person);
            return fixture;
        }
    }
}
