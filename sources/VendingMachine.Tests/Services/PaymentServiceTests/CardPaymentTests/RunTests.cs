using Moq;
using Nagarro.VendingMachine.Business.Logging;
using Nagarro.VendingMachine.Business.PresentationInterfaces.PaymentTerminals;
using Nagarro.VendingMachine.Business.Services.PaymentService;

namespace Nagarro.VendingMachine.Tests.Services.PaymentServiceTests.CardPaymentTests
{
    public class RunTests
    {
        private readonly Mock<ICardPaymentTerminal> cardPaymentTerminal;
        private readonly Mock<ILogger<CardPayment>> logger;
        private readonly CardPayment cardPayment;

        private readonly string validCardNumber = "4140497029752970";

        public RunTests()
        {
            cardPaymentTerminal = new Mock<ICardPaymentTerminal>(); 
            logger = new Mock<ILogger<CardPayment>>();

            cardPayment = new CardPayment(cardPaymentTerminal.Object, logger.Object);
        }

        [Fact]
        public void HavingACardPaymentInstance_WhenExecuted_AskForCardNumberIsCalled()
        {
            cardPaymentTerminal.Setup(x => x.AskForCardNumber()).Returns(validCardNumber);

            cardPayment.Run(10m);

            cardPaymentTerminal.Verify(x => x.AskForCardNumber(), Times.Once);
        }

        [Fact]
        public void HavingACardPaymentInstance_WhenTransactionIsCanceled_DisplayTransactionCanceledMessageIsCalled()
        {
            cardPaymentTerminal.Setup(x => x.AskForCardNumber()).Returns((string?)null);

            cardPayment.Run(10m);

            cardPaymentTerminal.Verify(x => x.DisplayTransactionCanceledMessage(), Times.Once);
        }

        [Fact]
        public void HavingACardPaymentInstance_WhenInputIsNotAValidCardNumber_DisplayInvalidCardNumberMessageIsCalled()
        {
            cardPaymentTerminal.SetupSequence(x => x.AskForCardNumber()).Returns("1").Returns(validCardNumber);

            cardPayment.Run(10m);

            cardPaymentTerminal.Verify(x => x.DisplayInvalidCardNumberMessage(), Times.Once);
        }

        [Fact]
        public void HavingACardPaymentInstance_WhenInputIsAValidCardNumber_DisplaySuccessMessageIsCalled()
        {
            cardPaymentTerminal.Setup(x => x.AskForCardNumber()).Returns(validCardNumber);

            cardPayment.Run(10m);

            cardPaymentTerminal.Verify(x => x.DisplaySuccessMessage(), Times.Once);
        }
    }
}