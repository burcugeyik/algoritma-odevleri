using System;
using System.Collections.Generic;

class Program
{
    // Ürün sınıfı
    class Urun
    {
        public string Adi { get; set; }
        public double BirimFiyati { get; set; }
        public int Adet { get; set; }
    }

    static Dictionary<string, Urun> depo = new Dictionary<string, Urun>();
    static double kullaniciParasi = 1000; // Kullanıcının başlangıç parası

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Depo Yönetim Sistemi");
            Console.WriteLine("1. Ürün Ekle");
            Console.WriteLine("2. Ürün Satış");
            Console.WriteLine("3. Depodaki Ürünleri Görüntüle");
            Console.WriteLine("4. Çıkış");
            Console.Write("Seçiminiz: ");
            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    UrunEkle();
                    break;
                case "2":
                    UrunSatis();
                    break;
                case "3":
                    DepodakiUrunleriGoster();
                    break;
                case "4":
                    Console.WriteLine("Çıkış yapılıyor...");
                    return;
                default:
                    Console.WriteLine("Hatalı seçim! Lütfen tekrar deneyin.");
                    break;
            }
        }
    }

    static void UrunEkle()
    {
        Console.WriteLine("Ürün Ekleme");
        Console.WriteLine("1. Yeni Ürün Ekle");
        Console.WriteLine("2. Mevcut Ürünü Güncelle");
        Console.Write("Seçiminiz (1/2): ");
        string secim = Console.ReadLine();

        if (secim == "1")
        {
            Console.Write("Ürün Adı: ");
            string urunAdi = Console.ReadLine().ToLower();

            if (depo.ContainsKey(urunAdi))
            {
                Console.WriteLine("Bu ürün zaten mevcut. Mevcut ürün için '2' seçeneğini kullanabilirsiniz.");
                return;
            }

            Console.Write("Birim Fiyatı: ");
            double birimFiyati = double.Parse(Console.ReadLine());
            Console.Write("Adet: ");
            int adet = int.Parse(Console.ReadLine());

            depo[urunAdi] = new Urun { Adi = urunAdi, BirimFiyati = birimFiyati, Adet = adet };
            Console.WriteLine("Ürün başarıyla eklendi.");
        }
        else if (secim == "2")
        {
            Console.Write("Güncellenecek Ürün Adı: ");
            string urunAdi = Console.ReadLine().ToLower();

            if (!depo.ContainsKey(urunAdi))
            {
                Console.WriteLine("Bu ürün depoda bulunmamaktadır.");
                return;
            }

            Console.Write("Eklemek İstediğiniz Adet: ");
            int adet = int.Parse(Console.ReadLine());

            depo[urunAdi].Adet += adet;
            Console.WriteLine("Ürün adedi güncellendi.");
        }
        else
        {
            Console.WriteLine("Hatalı seçim!");
        }
    }

    static void UrunSatis()
    {
        Console.WriteLine("Ürün Satışı");
        Console.Write("Satın Almak İstediğiniz Ürün Adı: ");
        string urunAdi = Console.ReadLine().ToLower();

        if (!depo.ContainsKey(urunAdi))
        {
            Console.WriteLine("Bu ürün depoda bulunmamaktadır.");
            return;
        }

        Urun urun = depo[urunAdi];
        Console.Write("Almak İstediğiniz Adet: ");
        int adet = int.Parse(Console.ReadLine());

        double toplamTutar = urun.BirimFiyati * adet;

        if (urun.Adet < adet)
        {
            Console.WriteLine("Depoda yeterli ürün bulunmamaktadır!");
            return;
        }

        if (kullaniciParasi < toplamTutar)
        {
            Console.WriteLine("Yeterli paranız yok!");
            return;
        }

        // Satış işlemi
        urun.Adet -= adet;
        kullaniciParasi -= toplamTutar;
        Console.WriteLine("Satış başarılı! Kalan paranız: {kullaniciParasi:F2}");
    }

    static void DepodakiUrunleriGoster()
    {
        Console.WriteLine(" Depodaki Ürünler");

        if (depo.Count == 0)
        {
            Console.WriteLine("Depoda hiçbir ürün bulunmamaktadır.");
            return;
        }

        foreach (var urun in depo.Values)
        {
            Console.WriteLine("Ürün Adı: {urun.Adi}, Birim Fiyatı: {urun.BirimFiyati:F2}, Adet: {urun.Adet}");
        }
    }
}