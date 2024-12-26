using System;
using System.Collections.Generic;

namespace AkilliGeriDonusumKutusu
{
    class Program
    {
        // Ürün bilgileri için sınıf
        class Urun
        {
            public string Barkod { get; set; }
            public bool GeriDonusturulebilir { get; set; }
        }

        // Kullanıcı bilgileri için sınıf
        class Kullanici
        {
            public string KullaniciId { get; set; }
            public int Puan { get; set; }
        }

        // Ürün ve kullanıcı listeleri
        static List<Urun> urunListesi = new List<Urun>();
        static List<Kullanici> kullaniciListesi = new List<Kullanici>();

        static void Main(string[] args)
        {
            Console.WriteLine("Akıllı Geri Dönüşüm Kutusuna Hoş Geldiniz!");

            while (true)
            {
                Console.WriteLine("Menü:");
                Console.WriteLine("1. Yeni barkod ekle");
                Console.WriteLine("2. Ürün okut");
                Console.WriteLine("3. Kullanıcı puanlarını görüntüle");
                Console.WriteLine("4. Çıkış");
                Console.Write("Bir seçenek giriniz: ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        BarkodEkle();
                        break;
                    case "2":
                        UrunOkut();
                        break;
                    case "3":
                        PuanlariGoruntule();
                        break;
                    case "4":
                        Console.WriteLine("Çıkış yapılıyor. Teşekkürler!");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçenek! Lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        static void BarkodEkle()
        {
            Console.Write("Yeni ürün için barkod numarası giriniz: ");
            string yeniBarkod = Console.ReadLine();

            // Barkod listede kontrol edilir
            if (urunListesi.Exists(u => u.Barkod == yeniBarkod))
            {
                Console.WriteLine("Bu barkod zaten mevcut.");
                return;
            }

            Console.Write("Bu ürün geri dönüştürülebilir mi? (Evet/Hayır): ");
            string cevap = Console.ReadLine().Trim().ToLower();

            bool geriDonusturulebilir = cevap == "evet";

            // Yeni ürün listesine eklenir
            urunListesi.Add(new Urun { Barkod = yeniBarkod, GeriDonusturulebilir = geriDonusturulebilir });

            Console.WriteLine("Barkod başarıyla eklendi.");

        }

        static void UrunOkut()
        {
            Console.Write("Lütfen kullanıcı ID'nizi giriniz: ");
            string kullaniciId = Console.ReadLine();

            // Kullanıcı listede kontrol edilir, yoksa eklenir
            Kullanici kullanici = kullaniciListesi.Find(k => k.KullaniciId == kullaniciId);
            if (kullanici == null)
            {
                kullanici = new Kullanici { KullaniciId = kullaniciId, Puan = 0 };
                kullaniciListesi.Add(kullanici);
            }

            Console.Write("Barkod numarasını okutunuz: ");
            string barkod = Console.ReadLine();

            // Barkod listede kontrol edilir
            Urun urun = urunListesi.Find(u => u.Barkod == barkod);

            if (urun != null)
            {
                if (urun.GeriDonusturulebilir)
                {
                    Console.WriteLine("Bu ürün geri dönüştürülebilir!");
                    kullanici.Puan += 10; // Her geri dönüştürülen ürün için 10 puan eklenir
                    Console.WriteLine("Tebrikler!  hesabınıza 10 puan eklendi.");
                }
                else
                {
                    Console.WriteLine("Bu ürün geri dönüştürülemez. hesabınıza 0 puan eklendi.");
                }
            }
            else
            {
                Console.WriteLine("Barkod veritabanında bulunamadı. Lütfen geçerli bir ürün okutunuz.");
            }

            // Kullanıcının toplam puanı
            Console.WriteLine("Güncel Puanınız: {kullanici.Puan}");
        }

        static void PuanlariGoruntule()
        {
            Console.WriteLine("Kullanıcı Puanları:");
            if (kullaniciListesi.Count == 0)
            {
                Console.WriteLine("Mevcut kullanıcı puanı yoktur.");
            }
            else
            {
                foreach (var kullanici in kullaniciListesi)
                {
                    Console.WriteLine("Kullanıcı ID: {kullanici.KullaniciId}, Puan: {kullanici.Puan}");
                }
            }
        }
    }
}
