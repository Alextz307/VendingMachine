using Autofac;
using Nagarro.VendingMachine.Business.Logging;
using Nagarro.VendingMachine.Business.Services.AuthenticationService;
using Nagarro.VendingMachine.Business.PresentationInterfaces.Views;
using Nagarro.VendingMachine.Business.PresentationInterfaces.PaymentTerminals;
using Nagarro.VendingMachine.Presentation.Commands;
using Nagarro.VendingMachine.Presentation.Commands.Reports;
using Nagarro.VendingMachine.Presentation.Views;
using Nagarro.VendingMachine.Presentation.PaymentTerminals;
using Nagarro.VendingMachine.Business.Services.PaymentService.Interfaces;
using Nagarro.VendingMachine.Business.Services.PaymentService;
using Nagarro.VendingMachine.Business.Services.FileService;
using Nagarro.VendingMachine.Business.Services.ReportService;
using Nagarro.VendingMachine.Business.DataAccessInterfaces;
using Nagarro.VendingMachine.DataAccess.SQLite;
using System.IO;
using System.Reflection;
using System;
using System.Configuration;
using Nagarro.VendingMachine.DataAccess;

namespace Nagarro.VendingMachine
{
    internal class Bootstrapper
    {
        private static IContainer _container;

        public static void Run()
        {
            BuildApplication();

            IApplication application = _container.Resolve<IApplication>();

            application.Run();
        }

        private static void BuildApplication()
        {
            ContainerBuilder builder = new();

            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));

            builder.RegisterType<MainView>().As<IMainView>();

            builder.RegisterType<UseCaseFactory>().As<IUseCaseFactory>();

            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>().SingleInstance();

            builder.RegisterType<LoginCommand>().As<IConsoleCommand>();
            builder.RegisterType<LogoutCommand>().As<IConsoleCommand>();
            builder.RegisterType<LookCommand>().As<IConsoleCommand>();
            builder.RegisterType<BuyCommand>().As<IConsoleCommand>();
            builder.RegisterType<StockReportCommand>().As<IConsoleCommand>();
            builder.RegisterType<SalesReportCommand>().As<IConsoleCommand>();
            builder.RegisterType<VolumeReportCommand>().As<IConsoleCommand>();

            builder.RegisterType<VendingMachineApplication>().As<IApplication>();

            builder.RegisterType<ShelfView>().As<IShelfView>();
            builder.RegisterType<BuyView>().As<IBuyView>();
            builder.RegisterType<LoginView>().As<ILoginView>();
            builder.RegisterType<ReportsView>().As<IReportsView>();
            builder.RegisterType<CashPaymentTerminal>().As<ICashPaymentTerminal>();
            builder.RegisterType<CardPaymentTerminal>().As<ICardPaymentTerminal>();

            builder.RegisterType<CurrencyManager>().As<ICurrencyManager>();
            builder.RegisterType<PaymentService>().As<IPaymentService>();
            builder.RegisterType<CashPayment>().As<IPaymentAlgorithm>();
            builder.RegisterType<CardPayment>().As<IPaymentAlgorithm>();
            builder.RegisterType<PaymentAlgorithmsManager>().As<IPaymentAlgorithmsManager>();

            string reportType = ConfigurationManager.AppSettings["ReportType"];
            if (reportType == "xml")
            {
                builder.RegisterType<XMLReportSerializer>().As<IReportSerializer>();
            }
            else if (reportType == "json")
            {
                builder.RegisterType<JsonReportSerializer>().As<IReportSerializer>();
            }

            builder.RegisterType<FileService>().As<IFileService>();
            builder.RegisterType<ReportService>().As<IReportService>();

            builder.RegisterType<Random>().AsSelf();
            
            string dbRelativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database.db");
            builder.RegisterType<VendingMachineContext>().AsSelf().SingleInstance().WithParameter("connectionString", dbRelativePath);

            // SQLite - EF
            // builder.RegisterType<SqliteDbProductRepositoryEF>().As<IProductRepository>();

            // SQLite
            string productDbRelativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProductsSQLite.db");
            builder.RegisterType<SqliteDbProductRepository>().As<IProductRepository>().WithParameter("connectionString", productDbRelativePath);

            // LiteDB
            /*
            string productDbRelativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProductsLiteDb.db");
            builder.RegisterType<LiteDbProductRepository>().As<IProductRepository>().WithParameter("connectionString", productDbRelativePath).SingleInstance();
            */


            // InMemory
            // builder.RegisterType<InMemoryProductRepository>().As<IProductRepository>();

            // SQLite - EF
            // builder.RegisterType<SqliteDbSaleRepositoryEF>().As<ISaleRepository>();

            // SQLite
            string saleDbRelativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SalesSQLite.db");
            builder.RegisterType<SqliteDbSaleRepository>().As<ISaleRepository>().WithParameter("connectionString", saleDbRelativePath).SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.Load("VendingMachine.Business"))
                   .Where(t => t.GetInterfaces().Length == 1 && t.Namespace.StartsWith("Nagarro.VendingMachine.Business.UseCases"))
                   .AsSelf();

            _container = builder.Build();
        }
    }
}
