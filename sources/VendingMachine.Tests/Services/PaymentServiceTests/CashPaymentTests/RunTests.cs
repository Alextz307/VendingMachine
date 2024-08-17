using Moq;
using Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces;
using Nagarro.VendingMachine.Business.PresentationInterfaces.PaymentTerminals;
using Nagarro.VendingMachine.Business.Services.PaymentService;
using Nagarro.VendingMachine.Business.Logging;

namespace Nagarro.VendingMachine.Tests.Services.PaymentServiceTests.CashPaymentTests
{
    public class RunTests
    {
        private readonly Mock<ICashPaymentTerminal> cashPaymentTerminal;
        private readonly Mock<ICurrencyManager> currencyManager;
        private readonly Mock<ILogger<CashPayment>> logger;
        private readonly CashPayment cashPayment;

        private readonly Dictionary<string, decimal> currencyDictionary = new Dictionary<string, decimal>
        {
            { "2 lei", 2m },
            { "1 leu", 1m },
        };

        public RunTests()
        {
            cashPaymentTerminal = new Mock<ICashPaymentTerminal>();
            currencyManager = new Mock<ICurrencyManager>();
            logger = new Mock<ILogger<CashPayment>>();
            cashPayment = new CashPayment(cashPaymentTerminal.Object, currencyManager.Object, logger.Object);
        }

        [Fact]
        public void HavingACashPaymentInstance_WhenPriceIsZero_DisplayUnitsIsNotCalled()
        {
            cashPayment.Run(0m);

            cashPaymentTerminal.Verify(x => x.DisplayUnits(), Times.Never);
        }

        [Fact]
        public void HavingACashPaymentInstance_WhenPriceIsNotZero_DisplayUnitsIsCalled()
        {
            cashPaymentTerminal.Setup(x => x.AskForMoney(It.IsAny<decimal>())).Returns(It.IsAny<string>());
            currencyManager.Setup(x => x.CheckCurrency(It.IsAny<string>())).Returns(true);
            currencyManager.Setup(x => x.GetValue(It.IsAny<string>())).Returns(10m);

            cashPayment.Run(10m);

            cashPaymentTerminal.Verify(x => x.DisplayUnits(), Times.Once);
        }

        [Fact]
        public void HavingACashPaymentInstance_WhenTransactionIsCanceled_DisplayTransactionCanceledMessageIsCalled()
        {
            cashPaymentTerminal.Setup(x => x.AskForMoney(It.IsAny<decimal>())).Returns((string?)null);

            cashPayment.Run(10m);

            cashPaymentTerminal.Verify(x => x.DisplayTransactionCanceledMessage(), Times.Once);
        }
        
        [Fact]
        public void HavingACashPaymentInstance_WhenInputIsNotAValidCurrency_DisplayInvalidCurrencyMessageIsCalled()
        {
            cashPaymentTerminal.SetupSequence(x => x.AskForMoney(10m)).Returns("1 euro").Returns("10 lei");
            currencyManager.Setup(x => x.CheckCurrency("1 euro")).Returns(false);
            currencyManager.Setup(x => x.CheckCurrency("10 lei")).Returns(true);
            currencyManager.Setup(x => x.GetValue("10 lei")).Returns(10m);

            cashPayment.Run(10m);

            cashPaymentTerminal.Verify(x => x.DisplayInvalidCurrencyMessage(), Times.Once);
        }

        [Fact]
        public void HavingACashPaymentInstance_WhenUserInsertedCurrency_AskForMoneyIsCalledWithCorrectParameter()
        {
            cashPaymentTerminal.SetupSequence(x => x.AskForMoney(10m)).Returns("2 lei").Returns("8 lei");
            currencyManager.Setup(x => x.CheckCurrency("2 lei")).Returns(true);
            currencyManager.Setup(x => x.CheckCurrency("8 lei")).Returns(true);
            currencyManager.Setup(x => x.GetValue("2 lei")).Returns(2m);
            currencyManager.Setup(x => x.GetValue("8 lei")).Returns(8m);

            cashPayment.Run(10m);

            cashPaymentTerminal.Verify(x => x.AskForMoney(8m), Times.Once);
        }

        [Fact]
        public void HavingACashPaymentInstance_WhenPaidAmountIsEqualToPrice_GetCurrenciesIsNotCalled()
        {
            cashPaymentTerminal.SetupSequence(x => x.AskForMoney(10m)).Returns("2 lei").Returns("8 lei");
            currencyManager.Setup(x => x.CheckCurrency("2 lei")).Returns(true);
            currencyManager.Setup(x => x.CheckCurrency("8 lei")).Returns(true);
            currencyManager.Setup(x => x.GetValue("2 lei")).Returns(2m);
            currencyManager.Setup(x => x.GetValue("8 lei")).Returns(8m);

            cashPayment.Run(10m);

            currencyManager.Verify(x => x.GetCurrencies(), Times.Never);
        }

        [Fact]
        public void HavingACashPaymentInstance_WhenPaidAmountIsEqualToPrice_GiveBackChangeIsNotCalled()
        {
            cashPaymentTerminal.SetupSequence(x => x.AskForMoney(10m)).Returns("2 lei").Returns("8 lei");
            currencyManager.Setup(x => x.CheckCurrency("2 lei")).Returns(true);
            currencyManager.Setup(x => x.CheckCurrency("8 lei")).Returns(true);
            currencyManager.Setup(x => x.GetValue("2 lei")).Returns(2m);
            currencyManager.Setup(x => x.GetValue("8 lei")).Returns(8m);

            cashPayment.Run(10m);

            cashPaymentTerminal.Verify(x => x.GiveBackChange(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void HavingACashPaymentInstance_WhenPaidAmountIsMoreThanPrice_GetCurrenciesIsCalled()
        {
            cashPaymentTerminal.Setup(x => x.AskForMoney(10m)).Returns("2 lei");
            cashPaymentTerminal.Setup(x => x.AskForMoney(8m)).Returns("9 lei");
            currencyManager.Setup(x => x.CheckCurrency("2 lei")).Returns(true);
            currencyManager.Setup(x => x.CheckCurrency("9 lei")).Returns(true);
            currencyManager.Setup(x => x.GetValue("2 lei")).Returns(2m);
            currencyManager.Setup(x => x.GetValue("9 lei")).Returns(9m);
            currencyManager.Setup(x => x.GetCurrencies()).Returns(currencyDictionary);

            cashPayment.Run(10m);

            currencyManager.Verify(x => x.GetCurrencies(), Times.Once);
        }

        [Fact]
        public void HavingACashPaymentInstance_WhenPaidAmountIsMoreThanPrice_GiveBackChangeIsCalled()
        {
            cashPaymentTerminal.Setup(x => x.AskForMoney(10m)).Returns("5 lei");
            cashPaymentTerminal.Setup(x => x.AskForMoney(5m)).Returns("10 lei");
            currencyManager.Setup(x => x.CheckCurrency("5 lei")).Returns(true);
            currencyManager.Setup(x => x.CheckCurrency("10 lei")).Returns(true);
            currencyManager.Setup(x => x.GetValue("5 lei")).Returns(5m);
            currencyManager.Setup(x => x.GetValue("10 lei")).Returns(10m);
            currencyManager.Setup(x => x.GetCurrencies()).Returns(currencyDictionary);

            cashPayment.Run(10m);

            cashPaymentTerminal.Verify(x => x.GiveBackChange(It.IsAny<int>(), It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        public void HavingACashPaymentInstance_WhenPaidAmountIsMoreThanPrice_GiveBackChangeIsNeverCalledWithUnitsEqualToZero()
        {
            cashPaymentTerminal.Setup(x => x.AskForMoney(10m)).Returns("5 lei");
            cashPaymentTerminal.Setup(x => x.AskForMoney(5m)).Returns("10 lei");
            currencyManager.Setup(x => x.CheckCurrency("5 lei")).Returns(true);
            currencyManager.Setup(x => x.CheckCurrency("10 lei")).Returns(true);
            currencyManager.Setup(x => x.GetValue("5 lei")).Returns(5m);
            currencyManager.Setup(x => x.GetValue("10 lei")).Returns(10m);
            currencyManager.Setup(x => x.GetCurrencies()).Returns(currencyDictionary);

            cashPayment.Run(10m);

            cashPaymentTerminal.Verify(x => x.GiveBackChange(0, It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void HavingACashPaymentInstance_WhenPaidAmountIsMoreThanPrice_ChangeIsGivenBackCorrectly()
        {
            cashPaymentTerminal.Setup(x => x.AskForMoney(10m)).Returns("5 lei");
            cashPaymentTerminal.Setup(x => x.AskForMoney(5m)).Returns("10 lei");
            currencyManager.Setup(x => x.CheckCurrency("5 lei")).Returns(true);
            currencyManager.Setup(x => x.CheckCurrency("10 lei")).Returns(true);
            currencyManager.Setup(x => x.GetValue("5 lei")).Returns(5m);
            currencyManager.Setup(x => x.GetValue("10 lei")).Returns(10m);
            currencyManager.Setup(x => x.GetCurrencies()).Returns(currencyDictionary);

            cashPayment.Run(10m);

            cashPaymentTerminal.Verify(x => x.GiveBackChange(2, It.IsAny<string>()), Times.Once);
            cashPaymentTerminal.Verify(x => x.GiveBackChange(1, It.IsAny<string>()), Times.Once);
        }
    }
}