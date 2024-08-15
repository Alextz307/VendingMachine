using Moq;
using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces;
using Nagarro.VendingMachine.Business.UseCases;
using Nagarro.VendingMachine.Business.Logging;

namespace Nagarro.VendingMachine.Tests.UseCases.BuyUseCaseTests
{
    public class ConstructorTests
    {
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IBuyView> buyView;
        private readonly Mock<IPaymentService> paymentService;
        private readonly Mock<ILogger<BuyUseCase>> logger;
        private readonly Mock<Random> randomGenerator;

        public ConstructorTests()
        {
            productRepository = new Mock<IProductRepository>();
            buyView = new Mock<IBuyView>();
            paymentService = new Mock<IPaymentService>();
            logger = new Mock<ILogger<BuyUseCase>>();
            randomGenerator = new Mock<Random>();
        }

        [Fact]
        public void HavingNullProductRepository_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new BuyUseCase(null, buyView.Object, paymentService.Object, logger.Object, randomGenerator.Object);
            });
        }

        [Fact]
        public void HavingNullBuyView_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new BuyUseCase(productRepository.Object, null, paymentService.Object, logger.Object, randomGenerator.Object);
            });
        }

        [Fact]
        public void HavingNullPaymentService_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new BuyUseCase(productRepository.Object, buyView.Object, null, logger.Object, randomGenerator.Object);
            });
        }

        [Fact]
        public void HavingNullLogger_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new BuyUseCase(productRepository.Object, buyView.Object, paymentService.Object, null, randomGenerator.Object);
            });
        }

        [Fact]
        public void HavingNullRandomGenerator_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new BuyUseCase(productRepository.Object, buyView.Object, paymentService.Object, logger.Object, null);
            });
        }
    }
}