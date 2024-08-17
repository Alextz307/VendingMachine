using Nagarro.VendingMachine.Domain;
using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Business.Logging;

namespace Nagarro.VendingMachine.Business.UseCases
{
    public class LookUseCase : IUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IShelfView _shelfView;
        private readonly ILogger<LookUseCase> _logger;

        public LookUseCase(IProductRepository productRepository, IShelfView shelfView, ILogger<LookUseCase> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _shelfView = shelfView ?? throw new ArgumentNullException(nameof(shelfView));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Execute()
        {   
            IEnumerable<Product> availableProducts = _productRepository.GetAllInStock();
            _shelfView.DisplayProducts(availableProducts);

            _logger.Info("Look action performed");
        }
    }
}