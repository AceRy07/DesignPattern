using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] Karisik = new int[6];
            Random r = new Random();
            for (int sıra = 0; sıra < 6; sıra++)
                Karisik[sıra] = r.Next(40);

            Console.WriteLine("Karisik dizisi:");
            foreach (int eleman in Karisik)
                Console.Write(eleman + " ");

            int[] Notlar;
            Notlar = (int[])Karisik.Clone();

            Console.WriteLine("\n\nNotlar dizisi:");
            foreach (int eleman in Notlar)
                Console.Write(eleman + " ");

            for (int sıra = 0; sıra < 6; sıra++)
                Karisik[sıra] = r.Next(40);

            Console.WriteLine("\n\nİçeriği değişen Karisik dizisi:");
            foreach (int eleman in Karisik)
                Console.Write(eleman + " ");

            Console.WriteLine("\n\nNotlar dizisi:");
            foreach (int eleman in Notlar)
                Console.Write(eleman + " ");
        }
    }
}
