using Moq;
using Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Business.Services.PaymentService;
using Nagarro.VendingMachine.Business.DataAccessInterfaces;

namespace Nagarro.VendingMachine.Tests.Services.PaymentServiceTests.PaymentServiceTests
{
    public class ConstructorTests
    {
        private readonly Mock<IBuyView> buyView;
        private readonly Mock<IPaymentAlgorithmsManager> paymentAlgorithms;
        private readonly Mock<ISaleRepository> saleRepository;

        public ConstructorTests()
        {
            buyView = new Mock<IBuyView>();
            paymentAlgorithms = new Mock<IPaymentAlgorithmsManager>();
            saleRepository = new Mock<ISaleRepository>();
        }

        [Fact]
        public void HavingNullBuyView_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new PaymentService(null, paymentAlgorithms.Object, saleRepository.Object);
            });
        }

        [Fact]
        public void HavingNullPaymentAlgorithms_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new PaymentService(buyView.Object, null, saleRepository.Object);
            });
        }

        [Fact]
        public void HavingNullSaleRepository_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new PaymentService(buyView.Object, paymentAlgorithms.Object, null);
            });
        }
    }
}