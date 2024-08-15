using Moq;
using Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces;
using Nagarro.VendingMachine.Business.PresentationInterfaces.PaymentTerminals;
using Nagarro.VendingMachine.Business.Services.PaymentService;
using Nagarro.VendingMachine.Business.Logging;

namespace Nagarro.VendingMachine.Tests.Services.PaymentServiceTests.CashPaymentTests
{
    public class ConstructorTests
    {
        private readonly Mock<ICashPaymentTerminal> cashPaymentTerminal;
        private readonly Mock<ICurrencyManager> currencies;
        private readonly Mock<ILogger<CashPayment>> logger;

        public ConstructorTests()
        {
            cashPaymentTerminal = new Mock<ICashPaymentTerminal>();
            currencies = new Mock<ICurrencyManager>();
            logger = new Mock<ILogger<CashPayment>>();
        }

        [Fact]
        public void HavingNullCashPaymentTerminal_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CashPayment(null, currencies.Object, logger.Object);
            });
        }

        [Fact]
        public void HavingNullCurrencies_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CashPayment(cashPaymentTerminal.Object, null, logger.Object);
            });
        }

        [Fact]
        public void HavingNullLogger_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CashPayment(cashPaymentTerminal.Object, currencies.Object, null);
            });
        }

        [Fact]
        public void WhenInitializingTheServiceCase_NameIsCorrect()
        {
            CashPayment cashPayment = new CashPayment(cashPaymentTerminal.Object, currencies.Object, logger.Object);

            Assert.Equal("cash", cashPayment.Name);
        }
    }
}