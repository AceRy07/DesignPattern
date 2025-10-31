using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Orijinal müşteri nesnesi oluşturuluyor
            Customer customer1 = new Customer { FirstName="Engin", LastName="Demiroğ", City="Ankara", Id=1};

            // 2. customer1'in TAMAMEN BAĞIMSIZ bir kopyası alınıyor (Clone ile)
            Customer customer2 = (Customer)customer1.Clone();

            // 3. Kopya nesnede değişiklik yapıyoruz → orijinal etkilenmeyecek!
            customer2.City = "İstanbul";

            // 4. Çıktı: İki nesne de farklı değerler gösterir → çünkü ayrı nesneler!
            Console.WriteLine(customer1.FirstName);
            Console.WriteLine(customer2.FirstName);
        }
    }

    // Tüm kişiler için ortak temel sınıf (abstract = doğrudan nesne oluşturulamaz)
    public abstract class Person
    {
        // Her alt sınıf kendi kopyalama yöntemini tanımlamak ZORUNDA
        public abstract Person Clone();
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    // Müşteri sınıfı: Person'dan miras alıyor
    public class Customer : Person
    {
        public string City { get; set; }

        // Clone metodu: Bu nesnenin kopyasını oluşturur
        // MemberwiseClone() → Tüm alanları (field) tek tek kopyalar
        // Bu işleme "Shallow Copy" (Yüzeysel Kopyalama) denir
        public override Person Clone()
        {
            // Person sınıfındaki MemberwiseClone metodunu kullanarak field'lerin kopyalanmasını sağlıyoruz.
            // bu Shallow Copy (Yüzeysel kopyalama) olarak adlandırılır.
            return (Person)MemberwiseClone();
        }
    }

    // Çalışan sınıfı: Yine Person'dan miras alıyor
    public class Employee : Person
    {
        public decimal Salary { get; set; }

        // Aynı şekilde kopyalama yapılıyor
        public override Person Clone()
        {
            return (Person)MemberwiseClone();
        }
    }
}
