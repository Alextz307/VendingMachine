using Moq;
using Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Business.Services.PaymentService;
using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Domain;

namespace Nagarro.VendingMachine.Tests.Services.PaymentServiceTests.PaymentServiceTests
{
    public class ExecuteTests
    {
        private readonly Mock<IBuyView> buyView;
        private readonly Mock<IPaymentAlgorithmsManager> paymentAlgorithms;
        private readonly Mock<ISaleRepository> saleRepository;
        private readonly PaymentService paymentService;

        private readonly Mock<IPaymentAlgorithm> paymentAlgorithm = new Mock<IPaymentAlgorithm>();
        private readonly Product product = new() { Id = 1, Name = "Chococolate", Price = 10, Quantity = 20 };

        public ExecuteTests()
        {
            buyView = new Mock<IBuyView>();
            paymentAlgorithms = new Mock<IPaymentAlgorithmsManager>();
            saleRepository = new Mock<ISaleRepository>();
            paymentService = new PaymentService(buyView.Object, paymentAlgorithms.Object, saleRepository.Object);
        }
         
        [Fact]
        public void HavingAPaymentServiceInstance_WhenExecuted_DisplayPaymentMethodsIsCalled()
        {
            buyView.Setup(x => x.AskForPaymentMethod(paymentAlgorithms.Object.GetPaymentAlgorithms())).Returns(It.IsAny<string>());
            paymentAlgorithms.Setup(x => x.GetPaymentAlgorithmByName(It.IsAny<string>())).Returns(paymentAlgorithm.Object);

            paymentService.Execute(product);

            buyView.Verify(x => x.DisplayPaymentMethods(paymentAlgorithms.Object.GetPaymentAlgorithms()), Times.Once);
        }

        [Fact]
        public void HavingAPaymentServiceInstance_WhenExecuted_AskForPaymentMethodIsCalled()
        {
            buyView.Setup(x => x.AskForPaymentMethod(paymentAlgorithms.Object.GetPaymentAlgorithms())).Returns(It.IsAny<string>());
            paymentAlgorithms.Setup(x => x.GetPaymentAlgorithmByName(It.IsAny<string>())).Returns(paymentAlgorithm.Object);

            paymentService.Execute(product);

            buyView.Verify(x => x.AskForPaymentMethod(paymentAlgorithms.Object.GetPaymentAlgorithms()), Times.Once);
        }

        [Fact]
        public void HavingACardPaymentInstance_WhenTransactionIsCanceled_DisplayTransactionCanceledMessageIsCalled()
        {
            buyView.Setup(x => x.AskForPaymentMethod(paymentAlgorithms.Object.GetPaymentAlgorithms())).Returns((string?)null);

            paymentService.Execute(product);

            buyView.Verify(x => x.DisplayTransactionCanceledMessage(), Times.Once);
        }

        [Fact]
        public void HavingACardPaymentInstance_WhenUserSelectsPaymentMethod_GetPaymentAlgorithmByIdIsCalled()
        {
            buyView.Setup(x => x.AskForPaymentMethod(paymentAlgorithms.Object.GetPaymentAlgorithms())).Returns("cash");
            paymentAlgorithms.Setup(x => x.GetPaymentAlgorithmByName("cash")).Returns(paymentAlgorithm.Object);

            paymentService.Execute(product);

            paymentAlgorithms.Verify(x => x.GetPaymentAlgorithmByName("cash"), Times.Once);
        }

        [Fact]
        public void HavingACardPaymentInstance_WhenUserSelectsPaymentMethod_PaymentIsRun()
        {
            buyView.Setup(x => x.AskForPaymentMethod(paymentAlgorithms.Object.GetPaymentAlgorithms())).Returns("card");
            paymentAlgorithms.Setup(x => x.GetPaymentAlgorithmByName("card")).Returns(paymentAlgorithm.Object);

            paymentService.Execute(product);

            paymentAlgorithm.Verify(x => x.Run(10m), Times.Once);
        }
    }
}