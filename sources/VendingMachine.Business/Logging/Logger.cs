using log4net;

namespace Nagarro.VendingMachine.Business.Logging
{
    public class Logger<K> : ILogger<K>
    {
        private readonly ILog _log;

        public Logger()
        {
            _log = LogManager.GetLogger(typeof(K));
        }

        public void Info(string message)
        {
            _log.Info(message);
        }
    }
}
