using Moq;
using Nagarro.VendingMachine.Business.Logging;
using Nagarro.VendingMachine.Business.PresentationInterfaces.PaymentTerminals;
using Nagarro.VendingMachine.Business.Services.PaymentService;

namespace Nagarro.VendingMachine.Tests.Services.PaymentServiceTests.CardPaymentTests
{
    public class ConstructorTests
    {
        private readonly Mock<ICardPaymentTerminal> cardPaymentTerminal;
        private readonly Mock<ILogger<CardPayment>> logger;

        public ConstructorTests()
        {
            cardPaymentTerminal = new Mock<ICardPaymentTerminal>();
            logger = new Mock<ILogger<CardPayment>>();
        }

        [Fact]
        public void HavingNullCardPaymentTerminal_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CardPayment(null, logger.Object);
            });
        }

        [Fact]
        public void HavingNullLogger_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CardPayment(cardPaymentTerminal.Object, null);
            });
        }

        [Fact]
        public void WhenInitializingTheServiceCase_NameIsCorrect()
        {
            CardPayment cardPayment = new CardPayment(cardPaymentTerminal.Object, logger.Object);

            Assert.Equal("card", cardPayment.Name);
        }
    }
}