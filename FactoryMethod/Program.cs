using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager(new LoggerFactory2());
            customerManager.Save();
            Console.ReadLine();
        }
    }

    // Factory Method Pattern
    // DataAccessLayer Abstract Factory Pattern ile benzerlik gösterir.
    public interface ILoggerFactory
    {
        ILogger CreateLogger();
    }

    // DataAccessLayer Concrete Factory Pattern ile benzerlik gösterir.
    public class LoggerFactory : ILoggerFactory
        // Biz yazılımda classlar sade (çıplak inheritace) bir interface'den inherit edilmelidir.
    {
        // Factory Method budur.
        public ILogger CreateLogger()
        {
            // Business to decide factory
            return new EdLogger();
        }
    }

    public class LoggerFactory2 : ILoggerFactory
    {
        // Factory Method budur.
        public ILogger CreateLogger()
        {
            // Business to decide factory
            return new LogfNetLogger();
        }
    }

    // VeriTabanı gibi düşünülebilir.
    // ILogger interface'i farklı loglama teknikleri için kullanılabilir.
    public interface ILogger
    {
        void Log();
    }

    // Kulanıcı arayüzüne loglama
    public class EdLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged with EdLogger");
        }
    }

    public class LogfNetLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logged with LogfNetLogger");
        }
    }

    public class CustomerManager
    {
        private ILoggerFactory _loggerFactory;

        public CustomerManager(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public void Save()
        {
            Console.WriteLine("Saved");
            ILogger logger = _loggerFactory.CreateLogger();
            logger.Log();
        }
    }
}