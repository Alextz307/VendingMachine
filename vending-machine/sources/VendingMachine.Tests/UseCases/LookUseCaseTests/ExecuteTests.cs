using Moq;
using Nagarro.VendingMachine.Domain;
using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Business.UseCases;
using Nagarro.VendingMachine.Business.Logging;

namespace Nagarro.VendingMachine.Tests.UseCases.LookUseCaseTests
{
    public class ExecuteTests
    {
        private readonly Mock<IProductRepository> productRepository;
        private readonly Mock<IShelfView> shelfView;
        private readonly Mock<ILogger<LookUseCase>> logger;

        private readonly LookUseCase lookUseCase;

        public ExecuteTests()
        {
            productRepository = new Mock<IProductRepository>();
            shelfView = new Mock<IShelfView>();
            logger = new Mock<ILogger<LookUseCase>>();

            lookUseCase = new LookUseCase(productRepository.Object, shelfView.Object, logger.Object);
        }

        [Fact]
        public void HavingALookUseCaseInstance_WhenExecuted_TheProductsAreRetrieved()
        {
            lookUseCase.Execute();
               
            productRepository.Verify(x => x.GetAllInStock(), Times.Once);
        }

        [Fact]
        public void HavingALookUseCaseInstance_WhenExecuted_TheProductsAreDisplayed()
        {
            lookUseCase.Execute();

            shelfView.Verify(x => x.DisplayProducts(It.IsAny<IEnumerable<Product>>()), Times.Once);
        }

        [Fact]
        public void HavingAListOfProducts_WhenExecuted_OnlyAvailableProductsAreDisplayed()
        {
            List<Product> allProducts = new()
            {
                new Product { Id = 1, Name = "Fanta", Price = 5, Quantity = 2 },
                new Product { Id = 2, Name = "Cola", Price = 2, Quantity = 0}
            };

            List<Product> expectedProducts = new() { allProducts[0] };

            productRepository.Setup(x => x.GetAllInStock()).Returns(expectedProducts);

            lookUseCase.Execute();

            shelfView.Verify(x => x.DisplayProducts(expectedProducts), Times.Once);
        }
    }
}