using Moq;
using Nagarro.VendingMachine.Domain;
using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Business.Exceptions;
using Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces;
using Nagarro.VendingMachine.Business.UseCases;
using Nagarro.VendingMachine.Business.Logging;

namespace Nagarro.VendingMachine.Tests.UseCases.BuyUseCaseTests
{
    public class ExecuteTests
    {
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IBuyView> buyView;
        private readonly Mock<IPaymentService> paymentService;
        private readonly Mock<ILogger<BuyUseCase>> logger;
        private readonly Mock<Random> randomMock;
        private readonly BuyUseCase buyUseCase;

        private readonly Product product = new() { Id = 1, Name = "Fanta", Price = 5, Quantity = 2 };

        public ExecuteTests()
        {
            productRepository = new Mock<IProductRepository>();
            buyView = new Mock<IBuyView>();
            paymentService = new Mock<IPaymentService>();
            randomMock = new Mock<Random>();
            logger = new Mock<ILogger<BuyUseCase>>();
            buyUseCase = new BuyUseCase(productRepository.Object, buyView.Object, paymentService.Object, logger.Object, randomMock.Object);
        }
        
        [Fact]
        public void HavingABuyUseCaseInstance_WhenExecuted_AnIdIsRequested()
        {
            productRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(product);

            buyUseCase.Execute();

            buyView.Verify(x => x.RequestId(), Times.Once);
        }

        [Fact]
        public void HavingABuyUseCaseInstance_WhenExecutedWithAValidId_ThenTheRequestedProductIsRetrievedFromRepository()
        {
            buyView.Setup(x => x.RequestId()).Returns(1);
            productRepository.Setup(x => x.GetById(1)).Returns(product);

            buyUseCase.Execute();

            productRepository.Verify(x => x.GetById(1), Times.Once);
            Assert.Equal(productRepository.Object.GetById(1), product);
        }

        [Fact]
        public void HavingABuyUseCaseInstance_WhenNoProductHasRequiredId_ThrowsException()
        {
            buyView.Setup(x => x.RequestId()).Returns(1);
            productRepository.Setup(x => x.GetById(1)).Returns((Product?)null);

            Assert.Throws<InvalidProductIdException>(() =>
            {
                buyUseCase.Execute();
            });
        }

        [Fact]
        public void HavingABuyUseCaseInstance_WhenQuantityOfRequiredProductIsZero_ThrowsException()
        {
            buyView.Setup(x => x.RequestId()).Returns(1);

            product.Quantity = 0;
            productRepository.Setup(x => x.GetById(1)).Returns(product);

            Assert.Throws<InsufficientStockException>(() =>
            {
                buyUseCase.Execute();
            });
        }

        [Fact]
        public void HavingABuyUseCaseInstance_WhenUserConfirmsPayment_PaymentExecutionIsCalled()
        {
            buyView.Setup(x => x.RequestId()).Returns(1);

            productRepository.Setup(x => x.GetById(1)).Returns(product);
            buyView.Setup(x => x.ConfirmPay(product.Name)).Returns(true);

            randomMock.Setup(x => x.Next(0, 5)).Returns(1);

            buyUseCase.Execute();

            paymentService.Verify(x => x.Execute(product), Times.Once);
        }

        [Fact]
        public void HavingABuyUseCaseInstance_WhenUserDoesNotConfirmPayment_PaymentExecutionIsNotCalled()
        {
            buyView.Setup(x => x.RequestId()).Returns(1);

            productRepository.Setup(x => x.GetById(1)).Returns(product);
            buyView.Setup(x => x.ConfirmPay(product.Name)).Returns(false);

            buyUseCase.Execute();

            paymentService.Verify(x => x.Execute(product), Times.Never);
        }

        [Fact]
        public void HavingABuyUseCaseInstance_WhenUserConfirmsPayment_DecrementStockIsCalled()
        {
            buyView.Setup(x => x.RequestId()).Returns(1);

            productRepository.Setup(x => x.GetById(1)).Returns(product);
            buyView.Setup(x => x.ConfirmPay(product.Name)).Returns(true);
            paymentService.Setup(x => x.Execute(product)).Returns(true);

            randomMock.Setup(x => x.Next(0, 5)).Returns(1);

            buyUseCase.Execute();

            productRepository.Verify(x => x.DecrementStock(product), Times.Once);
        }

        [Fact]
        public void HavingABuyUseCaseInstance_WhenUserDoesNotConfirmPayment_DecrementStockIsNotCalled()
        {
            buyView.Setup(x => x.RequestId()).Returns(1);

            productRepository.Setup(x => x.GetById(1)).Returns(product);
            buyView.Setup(x => x.ConfirmPay(product.Name)).Returns(false);

            buyUseCase.Execute();
            
            productRepository.Verify(x => x.DecrementStock(product), Times.Never);
        }

        [Fact]
        public void HavingABuyUseCaseInstance_WhenProductIsStuck_ThrowsException()
        {
            buyView.Setup(x => x.RequestId()).Returns(1);

            productRepository.Setup(x => x.GetById(1)).Returns(product);
            buyView.Setup(x => x.ConfirmPay(product.Name)).Returns(true);
            paymentService.Setup(x => x.Execute(product)).Returns(true);

            randomMock.Setup(x => x.Next(0, 5)).Returns(0);

            Assert.Throws<ProductStuckException>(() =>
            {
                buyUseCase.Execute();
            });
        }

        [Fact]
        public void HavingABuyUseCaseInstance_WhenProductIsNotStuck_ProductIsDispensed()
        {
            buyView.Setup(x => x.RequestId()).Returns(1);

            productRepository.Setup(x => x.GetById(1)).Returns(product);
            buyView.Setup(x => x.ConfirmPay(product.Name)).Returns(true);
            paymentService.Setup(x => x.Execute(product)).Returns(true);

            randomMock.Setup(x => x.Next(0, 5)).Returns(1);

            buyUseCase.Execute();

            buyView.Verify(x => x.DispenseProduct(product.Name), Times.Once);
        }
    }
}