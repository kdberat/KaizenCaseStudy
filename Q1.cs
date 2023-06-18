using System;
using System.Collections.Generic;

public class CodeGenerator
{
    private const string Char = "ACDEFGHKLMNPRTXYZ234579"; // Gecerli karakterlerin listesi

    private const int CodeLength = 8; // Olusturulan kodun uzunlugu

    private static readonly Random Random = new Random(); // Rastgele sayi uretmek icin kullanilan nesne

    public static string GenerateUniqueCode()
    {
        var code = GenerateCode(); // Baslangicta bir kod olustur

        var usedCodes = new HashSet<string>(); // Gecerli kodlari saklamak icin kullanilan set
        usedCodes.Add(code); // Olusturulan kodu kullanilan kodlar setine ekle

        while (usedCodes.Count < TotalPossibleCodes()) // Kullanilan kod sayisi, mumkun olan tum kod sayisina esit olana kadar calis
        {
            code = GenerateCode(); // Yeni bir kod olustur

            if (!usedCodes.Contains(code)) // Olusturulan kod zaten kullanilan kodlarda yoksa
            {
                usedCodes.Add(code); // Kodu kullanilan kodlar setine ekle
                return code; // Kodu dondur
            }
        }

        throw new Exception("Unique code generation failed.."); // Essiz bir kod uretilemedi hatasi
    }

    private static string GenerateCode()
    {
        var code = ""; // Olusturulan kod

        while (code.Length < CodeLength) // Kodun istenen uzunluga ulasana kadar calis
        {
            var randomIndex = Random.Next(Char.Length); // Gecerli karakterlerin indeks aralıginda rastgele bir indeks sec
            var randomCharacter = Char[randomIndex]; // Secilen indeksteki karakteri al
            code += randomCharacter; // Kodun sonuna karakteri ekle
        }

        return code; // Olusturulan kodu dondur
    }

    private static long TotalPossibleCodes()
    {
        return (long)Math.Pow(Char.Length, CodeLength); // Mumkün olan toplam kod sayisıni hesapla
    }
}

public class Program
{
    public static void Main()
    {
        // Kod üretimi
        var generatedCode = CodeGenerator.GenerateUniqueCode(); // Essiz bir kod üret
        Console.WriteLine("Generated Code: " + generatedCode); // Uretilen kodu ekrana yazdir
        Console.ReadLine(); // Kullanicinin programı kapatmak icin bir tusa basmasini bekler
    }
}
