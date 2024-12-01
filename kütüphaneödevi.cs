using System;
using System.Collections.Generic;

class Program
{
    // Kitap sınıfı
    class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public int Quantity { get; set; }
    }

    // Kiralama sınıfı
    class Rental
    {
        public string UserName { get; set; }
        public string BookName { get; set; }
        public DateTime ReturnDate { get; set; }
    }

    // Kitaplar, kiralamalar ve başlangıç bütçesi
    static List<Book> books = new List<Book>();
    static List<Rental> rentals = new List<Rental>();
    static double initialBudget = 100.0; // Başlangıç bütçesi

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nKütüphane Yönetim Sistemi");
            Console.WriteLine("1. Kitap Ekle");
            Console.WriteLine("2. Kitap Kirala");
            Console.WriteLine("3. Kitap İade");
            Console.WriteLine("4. Kitap Ara");
            Console.WriteLine("5. Raporlama");
            Console.WriteLine("6. Çıkış");
            Console.WriteLine("7. Günlük Kiralama Fiyatını Göster");
            Console.WriteLine("8. Bütçeyi Göster");
            Console.Write("Seçiminiz: ");
            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    RentBook();
                    break;
                case "3":
                    ReturnBook();
                    break;
                case "4":
                    SearchBook();
                    break;
                case "5":
                    ReportBooks();
                    break;
                case "6":
                    Console.WriteLine("Çıkış yapılıyor...");
                    return;
                case "7":
                    ShowRentalPrice();
                    break;
                case "8":
                    ShowBudget();
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim!");
                    break;
            }
        }
    }

    static void AddBook()
    {
        Console.Write("Kitap Adı: ");
        string name = Console.ReadLine();
        Console.Write("Yazar Adı: ");
        string author = Console.ReadLine();
        Console.Write("Yayın Yılı: ");
        string year = Console.ReadLine();
        Console.Write("Adet: ");
        int quantity = int.Parse(Console.ReadLine());
        Console.Clear();

        foreach (var book in books)
        {
            if (book.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                book.Quantity += quantity;
                Console.WriteLine($"{name} stok adedi güncellendi.");
                return;
            }
        }

        books.Add(new Book
        {
            Name = name,
            Author = author,
            Year = year,
            Quantity = quantity
        });

        Console.WriteLine($"{name} kütüphaneye eklendi.");
    }

    static void RentBook()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Kütüphanede kitap bulunmuyor.");
            return;
        }

        Console.WriteLine($"Mevcut bütçeniz: {initialBudget} TL");
        Console.WriteLine("Mevcut Kitaplar:");
        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {books[i].Name} (Stok: {books[i].Quantity})");
        }

        Console.Write("Kiralamak istediğiniz kitabın numarasını seçin: ");
        int choice = int.Parse(Console.ReadLine()) - 1;

        if (choice < 0 || choice >= books.Count)
        {
            Console.WriteLine("Geçersiz seçim.");
            return;
        }

        var selectedBook = books[choice];
        if (selectedBook.Quantity == 0)
        {
            Console.WriteLine("Stokta yeterli kitap yok.");
            return;
        }

        Console.Write("Kaç gün kiralamak istiyorsunuz? ");
        int days = int.Parse(Console.ReadLine());
        double cost = days * 5;

        if (initialBudget < cost)
        {
            Console.WriteLine("Bütçeniz yeterli değil.");
            return;
        }

        selectedBook.Quantity--;
        DateTime returnDate = DateTime.Now.AddDays(days);
        Console.Write("Adınız: ");
        string userName = Console.ReadLine();

        rentals.Add(new Rental
        {
            UserName = userName,
            BookName = selectedBook.Name,
            ReturnDate = returnDate
        });

        initialBudget -= cost;

        Console.WriteLine($"{selectedBook.Name} kitabı kiralandı. İade tarihi: {returnDate.ToShortDateString()}.");
        Console.WriteLine($"Kalan bütçeniz: {initialBudget} TL");
    }

    static void ReturnBook()
    {
        Console.Write("Adınız: ");
        string userName = Console.ReadLine();
        Console.Write("İade edilen kitabın adı: ");
        string bookName = Console.ReadLine();

        var rental = rentals.Find(r => r.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
                                       r.BookName.Equals(bookName, StringComparison.OrdinalIgnoreCase));

        if (rental != null)
        {
            rentals.Remove(rental);
            var book = books.Find(b => b.Name.Equals(bookName, StringComparison.OrdinalIgnoreCase));
            if (book != null)
            {
                book.Quantity++;
                Console.WriteLine($"{bookName} kitabı iade edildi.");
            }
        }
        else
        {
            Console.WriteLine("Bu kiralama kaydı bulunamadı.");
        }
    }

    static void SearchBook()
    {
        Console.Write("Kitap adıyla mı, yazar adıyla mı arama yapmak istiyorsunuz? (adı/yazar): ");
        string choice = Console.ReadLine().ToLower();
        Console.Write("Arama terimi: ");
        string keyword = Console.ReadLine().ToLower();

        var results = books.FindAll(b =>
            (choice == "adı" && b.Name.ToLower().Contains(keyword)) ||
            (choice == "yazar" && b.Author.ToLower().Contains(keyword)));

        if (results.Count > 0)
        {
            Console.WriteLine("Arama Sonuçları:");
            foreach (var book in results)
            {
                Console.WriteLine($"{book.Name} - {book.Author} ({book.Year}) - Stok: {book.Quantity}");
            }
        }
        else
        {
            Console.WriteLine("Aradığınız kriterlere uygun kitap bulunamadı.");
        }
    }

    static void ReportBooks()
    {
        Console.WriteLine("1. Tüm kitapları listele");
        Console.WriteLine("2. Belirli bir yazara ait kitapları listele");
        Console.WriteLine("3. Belirli bir yayın yılına ait kitapları listele");
        Console.WriteLine("4. Kirada olan kitapları listele");
        Console.Write("Seçiminiz: ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Name} - {book.Author} ({book.Year}) - Stok: {book.Quantity}");
            }
        }
        else if (choice == "2")
        {
            Console.Write("Yazar adı: ");
            string author = Console.ReadLine();
            foreach (var book in books.FindAll(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"{book.Name} - {book.Author} ({book.Year}) - Stok: {book.Quantity}");
            }
        }
        else if (choice == "3")
        {
            Console.Write("Yayın yılı: ");
            string year = Console.ReadLine();
            foreach (var book in books.FindAll(b => b.Year == year))
            {
                Console.WriteLine($"{book.Name} - {book.Author} ({book.Year}) - Stok: {book.Quantity}");
            }
        }
        else if (choice == "4")
        {
            foreach (var rental in rentals)
            {
                Console.WriteLine($"{rental.BookName} - {rental.UserName} - İade tarihi: {rental.ReturnDate.ToShortDateString()}");
            }
        }
        else
        {
            Console.WriteLine("Geçersiz seçim!");
        }
    }

    static void ShowRentalPrice()
    {
        const double dailyRentalPrice = 5.0; // Günlük kiralama ücreti
        Console.WriteLine($"Günlük kiralama fiyatı: {dailyRentalPrice} TL");
    }

    static void ShowBudget()
    {
        Console.WriteLine($"Mevcut bütçeniz: {initialBudget} TL");
    }
}
