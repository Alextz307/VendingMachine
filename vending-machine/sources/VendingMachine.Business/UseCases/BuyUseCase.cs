using Nagarro.VendingMachine.Domain;
using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Business.Exceptions;
using Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces;
using Nagarro.VendingMachine.Business.Logging;

namespace Nagarro.VendingMachine.Business.UseCases
{
    public class BuyUseCase : IUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IBuyView _buyView;
        private readonly IPaymentService _paymentService;
        private readonly ILogger<BuyUseCase> _logger;
        private readonly Random _randomGenerator;
        
        public BuyUseCase(IProductRepository productRepository, IBuyView buyView, IPaymentService paymentService, ILogger<BuyUseCase> logger, Random randomGenerator)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _buyView = buyView ?? throw new ArgumentNullException(nameof(buyView));
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _randomGenerator = randomGenerator ?? throw new ArgumentNullException(nameof(randomGenerator));
        }

        private bool ProductStuck()
        {
            return _randomGenerator.Next(0, 5) == 0;
        }

        public void Execute()
        {
            int productId = _buyView.RequestId();
            Product product = _productRepository.GetById(productId) ?? throw new InvalidProductIdException(productId);

            if (product.Quantity == 0)
            {
                throw new InsufficientStockException(product.Name);
            }

            if (!_buyView.ConfirmPay(product.Name))
            {
                return;
            }

            if (_paymentService.Execute(product))
            {
                _logger.Info(product.Name + " bought");

                _productRepository.DecrementStock(product);

                if (ProductStuck())
                {
                    throw new ProductStuckException(product.Name);
                }
                _buyView.DispenseProduct(product.Name);
            }
        }
    }
}