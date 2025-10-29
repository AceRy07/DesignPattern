using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Neden kullanıyoruz? 
            // Bu kısım, Abstract Factory deseninin nasıl çalıştığını göstermek için bir örnek başlatıcı. 
            // ProductManager sınıfını, belirli bir fabrika (Factory2) ile başlatıyoruz ve GetAll() metodunu çağırıyoruz.
            // Neden tercih ediyoruz? 
            // Fabrika sınıflarını değiştirerek (örneğin Factory1'e geçerek) tüm sistemi kolayca uyarlayabiliyoruz, 
            // bu da kodun modüler olmasını sağlar. If-else blokları yerine soyut fabrika kullanıyoruz.
            // Avantajları: 
            // - Esneklik: Farklı loglama/caching kombinasyonlarını hızlıca değiştirebiliriz.
            // - Bakım kolaylığı: Yeni bir fabrika eklemek için sadece yeni bir sınıf yazmak yeterli, mevcut kod değişmez.
            // - Open-Closed Principle: Kod kapalıyken (değişmezken) genişletilebilir.
            ProductManager productManager = new ProductManager(new Factory2());
            productManager.GetAll();
        }
    }

    // Abstract Logging sınıfı
    // Neden kullanıyoruz? 
    // Loglama işlemini soyutlamak için. Farklı loglama yöntemleri (Log4Net, NLogger) bu sınıftan türeyecek.
    // Neden tercih ediyoruz? 
    // Soyut sınıf sayesinde, alt sınıflar aynı arayüzü (Log metodu) uygular, bu da polimorfizm sağlar.
    // Avantajları: 
    // - Genişletilebilirlik: Yeni bir loglama yöntemi eklemek için sadece yeni bir alt sınıf yazılır.
    // - Bağımsızlık: Üst katmanlar (ProductManager gibi) somut sınıflara bağlı kalmaz, sadece soyut sınıfa bağlı olur (Dependency Inversion).
    public abstract class Logging
    {
        public abstract void Log(string message);
    }

    // Log4NetLogger: Logging'in somut implementasyonu
    // Neden kullanıyoruz? 
    // Gerçek bir loglama kütüphanesini (Log4Net) simüle etmek için. Mesajı konsola yazdırıyor.
    // Neden tercih ediyoruz? 
    // Farklı loglama ihtiyaçları için (dosyaya yazma, veritabanına kaydetme vb.) kolayca değiştirilebilir.
    // Avantajları: 
    // - Değiştirilebilirlik: Sistemde Log4Net yerine başka bir logger'a geçmek için sadece fabrika değiştirilir.
    // - Test edilebilirlik: Unit testlerde mock logger kullanılabilir.
    public class Log4NetLogger : Logging
    {
        public override void Log(string message)
        {
            Console.WriteLine("Log4Net: " + message);
        }
    }

    // NLogger: Başka bir somut logger
    // Neden kullanıyoruz? 
    // Alternatif loglama yöntemini göstermek için (NLog gibi bir kütüphane simülasyonu).
    // Neden tercih ediyoruz? 
    // Çeşitlilik sağlamak; farklı projelerde farklı logger'lar kullanılabilir.
    // Avantajları: 
    // - Performans: Gerçek hayatta NLog daha hızlı olabilir, bu desende kolayca entegre edilir.
    // - Esneklik: Logger'ları runtime'da (çalışma zamanında) değiştirebiliriz.
    public class NLogger : Logging
    {
        public override void Log(string message)
        {
            Console.WriteLine("NLogger: " + message);
        }
    }

    // Abstract Caching sınıfı
    // Neden kullanıyoruz? 
    // Caching işlemini soyutlamak için. Farklı caching yöntemleri (MemCache, Redis) bu sınıftan türeyecek.
    // Neden tercih ediyoruz? 
    // Loglama gibi, caching de soyutlanarak bağımlılık azaltılır.
    // Avantajları: 
    // - Yeniden kullanılabilirlik: Aynı caching arayüzü birden fazla yerde kullanılabilir.
    // - Scalability: Büyük sistemlerde Redis gibi dağıtık caching'e geçmek kolaylaşır.
    public abstract class Caching
    {
        public abstract void Cache(string data);
    }

    // MemCache: Caching'in somut implementasyonu
    // Neden kullanıyoruz? 
    // Bellek tabanlı caching'i simüle etmek için.
    // Neden tercih ediyoruz? 
    // Küçük ölçekli uygulamalarda hızlı ve basit olduğu için.
    // Avantajları: 
    // - Hız: Yerel bellekte çalıştığı için düşük latency (gecikme).
    // - Kolay entegrasyon: Fabrika üzerinden otomatik olarak seçilebilir.
    public class MemCache : Caching
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cached with MemCache");
        }
    }

    // RedisCache: Başka bir somut caching
    // Neden kullanıyoruz? 
    // Dağıtık caching'i (Redis gibi) göstermek için.
    // Neden tercih ediyoruz? 
    // Büyük ölçekli, çok sunuculu sistemlerde veri tutarlılığı sağlar.
    // Avantajları: 
    // - Dayanıklılık: Veri kaybını önler, kalıcı depolama desteği var.
    // - Ölçeklenebilirlik: Bulut ortamlarında kolayca ölçeklenir.
    public class RedisCache : Caching
    {
        public override void Cache(string data)
        {
            Console.WriteLine("Cached with RedisCache");
        }
    }


    // Abstract Factory
    // Burada amacımız farklı alanlardaki bilgileri bir arada tutmak.
    // Buradan yeni fabrikalar üretebiliriz.
    // Abstract Factory: CrossCuttingConcernsFactory
    // Neden kullanıyoruz? 
    // Farklı alanlardaki nesneleri (Logging ve Caching) bir arada üretmek için. 
    // "Cross-cutting concerns" (loglama, caching gibi yatay kesen işlemler) için fabrika.
    // Neden tercih ediyoruz? 
    // Tek bir fabrika üzerinden birden fazla ilgili nesne grubu üretmek; if-else bloklarından kurtulmak.
    // Avantajları: 
    // - Grup yönetimi: İlgili nesneleri (logger + cache) tutarlı bir şekilde bir arada tutar.
    // - Factory Method deseninin genişletilmiş hali: Birden fazla ürün ailesini destekler.
    // - Decoupling: Client (ProductManager) fabrika detaylarını bilmez, sadece soyut fabrika kullanır.
    public abstract class CrossCuttingConcernsFactory
    {
        public abstract Logging CreateLogger();
        public abstract Caching CreateCaching();
    }

    // FactoryDen kastımız iş Süreclerimiz.
    // DataAcces katmanında loglama, yazıcıdan çıktı, yazıcıdan sadece tarama isteyebiliriz.
    // Bu sayede if else bloklarından kurtulmuş oluruz.

    // Factory1: Bir fabrika implementasyonu
    // Neden kullanıyoruz? 
    // Log4Net + Redis kombinasyonunu üretmek için.
    // Neden tercih ediyoruz? 
    // İş süreçlerine göre (örneğin, üretim ortamı) belirli kombinasyonlar tanımlamak.
    // Avantajları: 
    // - Konfigürasyon kolaylığı: Config dosyasında fabrika değiştirerek tüm sistemi uyarla.
    // - No if-else: Yeni kombinasyon için yeni fabrika ekle, kod değişmez.
    public class Factory1 : CrossCuttingConcernsFactory
    {
        public override Caching CreateCaching()
        {
            return new RedisCache();
        }

        public override Logging CreateLogger()
        {
            return new Log4NetLogger();   
        }
    }

    // Factory2: Başka bir fabrika
    // Neden kullanıyoruz? 
    // NLogger + MemCache kombinasyonunu üretmek için.
    // Neden tercih ediyoruz? 
    // Test veya geliştirme ortamı gibi farklı senaryolar için.
    // Avantajları: 
    // - Çevreye göre uyarlama: Geliştirme için hafif (MemCache), üretim için güçlü (Redis) seçilebilir.
    // - Extensibility: Yeni fabrika eklemekle sistem genişler.
    public class Factory2 : CrossCuttingConcernsFactory
    {
        public override Caching CreateCaching()
        {
            return new MemCache();
        }

        public override Logging CreateLogger()
        {
            return new NLogger();
        }
    }


    // Client -> Kullanan kişi

    // Client sınıfı: ProductManager
    // Neden kullanıyoruz? 
    // Abstract Factory'yi kullanan ana sınıf. Fabrika üzerinden logger ve cache alır.
    // Neden tercih ediyoruz? 
    // Dependency Injection (Constructor Injection) ile fabrika enjekte edilir, somut sınıflara bağımlı olunmaz.
    // Avantajları: 
    // - Loose Coupling: Değişiklikler client'ı etkilemez.
    // - Single Responsibility: Sadece ürün yönetimini yapar, log/cache detaylarını bilmez.
    // - Reusability: Farklı fabrikalarla aynı client tekrar kullanılabilir.
    public class ProductManager
    {
        private CrossCuttingConcernsFactory _crrossCuttingConcernsFactory;

        Logging _logging;
        Caching _caching;
        
        public ProductManager(CrossCuttingConcernsFactory crossCuttingConcernsFactory)
        {
            _crrossCuttingConcernsFactory = crossCuttingConcernsFactory;
            _logging = _crrossCuttingConcernsFactory.CreateLogger();
            _caching = _crrossCuttingConcernsFactory.CreateCaching();
        }

        public void GetAll()
        {
            // Bunu kullanabiliriz ama her seferinde new lemek yerine girmiş gibi olur.
            //_crrossCuttingConcernsFactory.CreateLogger().Log("Logged");


            // Neden kullanıyoruz? 
            // Log ve cache işlemlerini tetiklemek için.
            // Neden tercih ediyoruz? 
            // Fabrika üzerinden alınan nesneleri kullanmak; her seferinde new'lemek yerine.
            // Avantajları: 
            // - Performans: Nesneler constructor'da oluşturulur, tekrar tekrar yaratılmaz.
            // - Kolay bakım: Log/cache değiştirmek için sadece fabrika değiştir.
            //_crossCuttingConcernsFactory.CreateLogger().Log("Logged"); // Alternatif ama tercih edilmez, çünkü her seferinde yeni nesne yaratır.
            _logging.Log("Logged");
            _caching.Cache("Data");

            Console.WriteLine("Products listed!");
        }
    }
}
