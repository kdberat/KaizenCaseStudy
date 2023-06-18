using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ReceiptParser
{
    public static void Main()
    {
        string jsonResponse = @"
        {
            ""description"": [
            ""TESKKURLER"",
            ""GUNEYDOGU TEKS. GIDA INS SAN. LTD.STI."",
            ""ORNEKTEPE MAH.ETIBANK CAD.SARAY APT."",
            ""N:43-1 BEYOGLU/ISTANBUL"",
            ""GUNESLI V.D. 4350078928 V. NO."",
            ""TARIH : 26.08.2020"",
            ""SAAT : 15:27"",
            ""FIS NO : 0082"",
            ""54491250"",
            ""2 ADx 2,75"",
            ""CC.COCA COLA CAM 200 08 *5,50"",
            ""2701084"",
            ""1,566 KGx 1,99"",
            ""MANAV DOMATES PETEME *3,32"",
            ""2701076"",
            ""0,358 KGx 4,99"",
            ""MANAV BIBER CARLISTO 08 *1,79"",
            ""4"",
            ""EKMEK CIFTLI 01 *1,25"",
            ""TOPKDV *0,80"",
            ""TOPLAM *11,86"",
            ""NAKIT *20,00"",
            ""KDV KDV TUTARI KDV'LI TOPLAM"",
            ""01 *0,01 *1,25"",
            ""08 *0,79 *10,61"",
            ""KASIYER : SUPERVISOR"",
            ""00 0001/020/000084/1//82/"",
            ""PARA USTU *8,14"",
            ""KASIYER: 1"",
            ""2 NO:1330 EKU NO:0001"",
            ""MF YAB 15017876""
          ],
            ""coordinates"": [
            10,
            20,
            30,
            40,
            50,
            60,
            70,
            80,
            90,
            100,
            110,
            120,
            130,
            140,
            150,
            160,
            170,
            180,
            190,
            200,
            210,
            220,
            230,
            240,
            250,
            260,
            270,
            280,
            290,
            300,
            310
          ]
        }";

        try
        {
            JObject json = JObject.Parse(jsonResponse);

            JArray descriptions = (JArray)json["description"];
            JArray coordinates = (JArray)json["coordinates"];

            // Dizilerin boyutlarini eslestirme
            int itemCount = Math.Min(descriptions.Count, coordinates.Count);
            descriptions = new JArray(descriptions.Take(itemCount));
            coordinates = new JArray(coordinates.Take(itemCount));

            // Metinleri koordinatlara gore siralama
            var sortedItems = new SortedDictionary<int, string>();
            for (int i = 0; i < descriptions.Count; i++)
            {
                string description = descriptions[i].ToString();
                int coordinate = (int)coordinates[i];
                sortedItems.Add(coordinate, description);
            }

            // Metinleri sirayla kaydetme
            foreach (var item in sortedItems)
            {
                Console.WriteLine(item.Value);
                //Console.ReadKey();
            }
        }
        catch (JsonException e)
        {
            Console.WriteLine("Gecersiz JSON formati. Hata mesaji: " + e.Message);
            Console.ReadLine();
        }
        catch (Exception e)
        {
            Console.WriteLine("Hata: " + e.Message);
            Console.ReadLine();
        }
    }
}
