using Autofac;
using Nagarro.VendingMachine.Presentation.Commands;

namespace Nagarro.VendingMachine
{
    internal class UseCaseFactory : IUseCaseFactory
    {
        private readonly IComponentContext _container;

        public UseCaseFactory(IComponentContext container)
        {
            _container = container;
        }

        public TUseCase Create<TUseCase>()
        {
            return _container.Resolve<TUseCase>();
        }
    }
}