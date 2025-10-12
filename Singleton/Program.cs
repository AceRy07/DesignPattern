using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace Singleton
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // CustomerManager customerManager2 = new CustomerManager(); // Hata verir çünkü constructor private

            var customerManager = CustomerManager.CreateAsSingleton();
            customerManager.Save();
        }

        class CustomerManager
        {
            private static CustomerManager _customerManager;

            static object LockObject = new object();

            // Amaç dış erişime engellemktir
            private CustomerManager()
            {

            }

            public static CustomerManager CreateAsSingleton()
            {
                /*
                // Eğer CustomerManager nesnesi daha önce oluşturulmamışsa oluştur.
                if (_customerManager ==null)
                {
                    _customerManager = new CustomerManager();
                }

                return _customerManager;
                */

                // ?? -> Eğer demek. _customerManager null ise yeni bir CustomerManager nesnesi oluştur.
                //return _customerManager ?? (_customerManager = new CustomerManager());

                // Çoklu thread'lerde aynı anda erişim olduğunda tek bir instance (nesne) oluşturulması için kilitleniyor.
                lock (LockObject)
                {
                    if(_customerManager == null)
                    {
                        _customerManager = new CustomerManager();
                    }

                    return _customerManager;
                }
            }

            public void Save()
            {
                Console.WriteLine("Saved");
            }
        }
    }
}
