using System;

class Program
{
    static void Main()
    {
        // İki sayıyı tanımla
        double sayi1 = 8;
        double sayi2 = 13;

        // Geometrik ortalama hesapla
        double geometrikOrtalama = Math.Sqrt(sayi1 * sayi2);

        // Sonucu yazdır
        Console.WriteLine($"Sayılar: {sayi1} ve {sayi2}");
        Console.WriteLine($"Geometrik Ortalama: {geometrikOrtalama}");
    }
}
