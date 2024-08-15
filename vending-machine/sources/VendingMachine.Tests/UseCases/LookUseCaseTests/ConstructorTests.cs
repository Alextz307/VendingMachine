using log4net.Core;
using Moq;
using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Business.UseCases;
using Nagarro.VendingMachine.Business.Logging;

namespace Nagarro.VendingMachine.Tests.UseCases.LookUseCaseTests
{
    public class ConstructorTests
    {
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IShelfView> shelfView;
        private readonly Mock<ILogger<LookUseCase>> logger;

        public ConstructorTests()
        {
            productRepository = new Mock<IProductRepository>();
            shelfView = new Mock<IShelfView>();
            logger = new Mock<ILogger<LookUseCase>>();  
        }

        [Fact]
        public void HavingNullProductRepository_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LookUseCase(null, shelfView.Object, logger.Object);
            });
        }

        [Fact]
        public void HavingNullShelfView_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LookUseCase(productRepository.Object, null, logger.Object);
            });
        }

        [Fact]
        public void HavingNullLogger_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LookUseCase(productRepository.Object, shelfView.Object, null);
            });
        }
    }
}