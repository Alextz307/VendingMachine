using Moq;
using Nagarro.VendingMachine.Presentation.Commands;

namespace Nagarro.VendingMachine.Tests.UseCases.LookUseCaseTests
{
    public class LookCommandTests
    {
        private readonly Mock<IUseCaseFactory> useCaseFactory;

        public LookCommandTests()
        {
            useCaseFactory = new Mock<IUseCaseFactory>();
        }

        [Fact]
        public void HavingNullUseCaseFactory_WhenCallingConstructor_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LookCommand(null);
            });
        }

        [Fact]
        public void HavingUserOrAdminLoggedIn_CanExecuteIsTrue()
        {
            LookCommand lookCommand = new(useCaseFactory.Object);
            Assert.True(lookCommand.CanExecute);
        }

        [Fact]
        public void WhenInitializingTheUseCase_NameIsCorrect()
        {
            LookCommand lookCommand = new(useCaseFactory.Object);

            Assert.Equal("look", lookCommand.Name);
        }

        [Fact]
        public void WhenInitializingTheUseCase_DescriptionHasValue()
        {
            LookCommand lookCommand = new(useCaseFactory.Object);

            Assert.NotEmpty(lookCommand.Description);
        }
    }
}