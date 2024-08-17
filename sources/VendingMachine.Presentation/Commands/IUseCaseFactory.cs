namespace Nagarro.VendingMachine.Presentation.Commands
{
    public interface IUseCaseFactory
    {
        TUseCase Create<TUseCase>();
    }
}