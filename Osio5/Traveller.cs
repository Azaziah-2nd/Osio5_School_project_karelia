using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osio5
{
    /************************************************
    * Olio-ohjelmointi, kevät 2017                  *
    * Olli Suutari | Opiskelijanro:         *
    * Osio 5, Traveller luokka                      *
    ************************************************/

    public partial class Traveller
    {
        // Alustetaan tarvittavat ominaisuudet.
        private string name;
        private int age;
        private string gender;
        private int experience;
        private double money;
        // Alustetaan asiakaslistojen listat.
        private List<int> customer_number = new List<int>();
        private List<string> name_list = new List<string>();
        private List<int> age_list = new List<int>();
        private List<string> gender_list = new List<string>();
        private List<int> experience_list = new List<int>();
        private List<double> money_list = new List<double>();
        private List<int> served_customers = new List<int>();
        // Alustetaan kokemuspisteiden määräksi "0" muodostinta käyttäen.
        public Traveller()
        {
            experience = 0;
        }

        public Traveller(string Name, int Age, string Gender, double Money, int Experience)
        {
            this.name = Name;
            this.age = Age;
            this.gender = Gender;
            this.money = Money;
            this.experience = Experience;
        }

        // Hajotin
        ~Traveller()
        {
        }
        // Tietojen luku tekstitiedostosta.
        public void ReadFromFile(string filename, Traveller traveller)
        {

            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    traveller.Name = sr.ReadLine();
                    traveller.Age = int.Parse(sr.ReadLine());
                    traveller.Gender = sr.ReadLine();
                    traveller.Money = double.Parse(sr.ReadLine());
                    traveller.Experience = int.Parse(sr.ReadLine());
                    // Lisätään tiedot myös listoihin, näitä hyödynnetään TravellerQueue luokassa.
                    Name_list.Add(traveller.name);
                    Age_list.Add(traveller.age);
                    Gender_list.Add(traveller.gender);
                    Money_list.Add(traveller.money);
                    Experience_list.Add(traveller.experience);
                    // Tarkistetaan asiakkaiden lukumäärä ja tallennetaan uuden asiakkaan numeroksi määrä + 1
                    int customers_count = Customer_number.Count;
                    Customer_number.Add(customers_count ++);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nVirhe!\n" + e.Message);
                Console.WriteLine("Tiedostoa ei pystytty avaamaan.");
            }
        }
        // AskInfo metodi. Kysytään tiedot käyttäjältä ja tallennetaan 
        // ne "Traveller" olioon parametrin välityksellä.
        public void AskInfo(Traveller traveller)
        {
            // Alustetaan hyväksytyt sukupuolisyötteet ja str_input apumuuttuja.
            string[] gender_female = { "Nainen", "nainen", "Female", "female", "N", "n", "F", "f" };
            string[] gender_male = { "Mies", "mies", "Male", "male", "M", "m" };
            string str_input;
            // Lähdetään kysymymään tietoja ja tarkistetaan niiden oikeellisuudet.
            Console.WriteLine("\nOle hyvä ja syötä matkailijan tiedot: \n");
            Console.Write("Nimi: ");
            str_input = Console.ReadLine();
            while (str_input.Length == 0)
            {
                Console.Write("Nimesi ei voi olla tyhjä, yritä uudelleen: ");
                str_input = Console.ReadLine();
            }
            traveller.name = str_input;
            Console.Write("Ikä: ");
            str_input = Console.ReadLine();
            while (!int.TryParse(str_input, out traveller.age))
            {
                Console.Write("Numerosi on virheellinen, yritä uudestaan: ");
                str_input = Console.ReadLine();
            }
            Console.Write("Sukupuoli (mies/nainen): ");
            str_input = Console.ReadLine();
            while (!(gender_male.Contains(str_input) || gender_female.Contains(str_input)))
            {
                Console.WriteLine("Syötit arvon: " + str_input + " - Anna sukupuoleksi mies tai nainen.");
                str_input = Console.ReadLine();
            }
            // Jos syötetty sukupuoli oli naispuolinen, asetetaan sukupuoliarvoksi "Nainen".
            // Näin sukupuolia on kaksi vaikka ne voidaan syöttää eritavoin.
            if (gender_female.Contains(str_input))
            {
                traveller.gender = "Nainen";
            }
            // Muuten sukupuoli on "Mies".
            else
            {
                traveller.gender = "Mies";
            }
            // Lisätään tiedot myös listoihin, näitä hyödynnetään TravellerQueue luokassa.
            Name_list.Add(traveller.name);
            Age_list.Add(traveller.age);
            Gender_list.Add(traveller.gender);
            Money_list.Add(traveller.money);
            Experience_list.Add(traveller.experience);
            // Tarkistetaan asiakkaiden lukumäärä ja tallennetaan uuden asiakkaan numeroksi määrä + 1
            int customers_count = Customer_number.Count;
            Customer_number.Add(customers_count++);
        }

        // PrintSummary metodi.
        // Tulostetaan luodun "traveller" olion tiedot konsoliin.
        public void PrintSummary(Traveller traveller)
        {
            Console.WriteLine("\nMatkailijan yhteenveto:\n");
            Console.WriteLine("Nimi: {0}",  traveller.name);
            Console.WriteLine("Ikä: " + traveller.age);
            Console.WriteLine("Sukupuoli: " + traveller.gender);
            Console.WriteLine("Raha: " + traveller.money);
            Console.WriteLine("Kokemus: " + traveller.experience + "\n");
            Console.WriteLine("Tästä se lähtee.\n");
        }
        // seuraava!
        // Arpoo palveltavan henkilön ja kysyy tilattavien palveluiden tiedot.
        public void NextTraveller(Traveller traveller)
        {
            // Käytetään TravellerQueue luokkaa
            TravellerQueue traveller_queue = new TravellerQueue();
            // Kutsutaan "Next" metodia.
            traveller_queue.Next(traveller);
            // Jos Next palauttaa kahviarvon epätodeksi.
            if (traveller_queue.Cofee_break == false)
            {
                // Palveltavan asiakkaan nimi varataan sattumanvaraisesti Next metodissa.
                Console.WriteLine("Palvelen asiakasta nimeltä: " + traveller_queue.Name_from_list + "\n");
                // Kysytään ja tulostetaan asiakkaan huoneen tiedot.
                Room_service room = null;
                room = new Room_service();
                room.Room_service_AskInfo();
                Console.WriteLine();
                room.Room_service_PrintSummary();
                // Lisätään palveltun asiakkaan numero palveltujen asiakkaiden listaan ja tarkistetaan, onko asiakkaita vielä palveltavana.
                Served_customers.Add(traveller_queue.Customer_number_from_list);
                traveller_queue.Check_customers_left(traveller);
            }
            else
            {
                // Jos ollaan tauolla.
                Console.WriteLine("KAHVITAUKO, YRITÄ HETKEN PÄÄSTÄ UUDESTAAN!\n");
            }
        }
    }
}
