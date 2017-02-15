using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osio5
{
    /************************************************
    * Olio-ohjelmointi, kevät 2017                  *
    * Olli Suutari | Opiskelijanro:                 *
    * Osio 5, LodgingApp luokka                     *
    ************************************************/
    public class LodgingApp
    {
        // Siirrä traveller-olio sen attribuutiksi
        private Traveller traveller;

        // Tee pääsilmukan läpikäyntimuuttujasta LodingApp-olion ominaisuus
        // Staattisuus mahdollistaa arvon käyttämisen ja syöttämisen muissa luokissa.
        private static bool is_exit_code_true;
        // Aksessorit ja mutaattorit
        public Traveller Traveller { get { return traveller; } set { traveller = value; } }
        public static bool Is_exit_code_true { get { return is_exit_code_true; } set { is_exit_code_true = value; } }
        static void Main(string[] args)
        {
            // Luodaan cottageSim olio ja käynnistetään pääsilmukka "Run"-metodia hyödyntäen.
            LodgingApp cottageSim = new LodgingApp();
            cottageSim.Run();
        }
        // MuodostinOpiskelijanro:
        public LodgingApp()             
        {
        }
        //Hajotin
        ~LodgingApp()                  
        {
        }

        public void AskServices(Traveller traveller)
        {
            Services services = new Services();
            WirelessInternet internet = new WirelessInternet();
            string str_input;
            string filename;
            Console.Write("Lisätäänkö majoitus? (iso/pieni/ei): ");
            filename = Console.ReadLine();
            if (filename != "ei")
            {
                try
                {
                    services.Read_from_file(filename + ".txt");
                }
                catch (Exception Error_class)
                {
                    // Tulostetaan virheilmoitus
                    Console.Write("Tiedostoa ei voitu lukea, virhe: \n" + Error_class + "\n\n");
                    filename = "ei";
                }
            }
            // Wlan (k/e)
            Console.Write("Wifi (k/e): ");
            str_input = Console.ReadLine();
            while (!str_input.Equals("k") && !str_input.Equals("e"))
            {
                Console.Write("Wifi (k/e): ");
                str_input = Console.ReadLine();
            }
            if (str_input.Equals("k"))
            {
                // Luetaan wlanin tiedot tiedostosta wlan.txt
                internet.Read_from_file("wlan.txt");
            }
            else
            {
                internet = null;
            }
            Services[] objects = { new Room(), new WirelessInternet() };

            foreach (Services o in objects)
            {
                o.Service_summary();
            }

        }

        // Run metodi
        public void Run()
        {
            // Määritellään tarvittavat muuttujat
            Traveller = new Traveller();
            bool has_asked_for_customer = false;
            string str_input ="";
            Console.WriteLine("Tervetuloa Majoitussimulaattoriin!\n");
            // Jos syöte ei ole "lopeta" tai palveltavien asikkaiden määrä = 0, toistetaan silmukkaaa.
            while (Is_exit_code_true == false)
            {
                if (has_asked_for_customer == false || str_input == "uusi")
                {
                    has_asked_for_customer = true;
                    // Kysytään haluaako käyttäjä hakea tiedot tiedostosta.
                    Console.Write("Luetaanko matkustajan tiedot vai kirjoitetaanko itse? (lue | kirjoita): ");
                    str_input = Console.ReadLine();
                    while (str_input != "lue" && str_input != "kirjoita")
                    {
                        Console.Write("Yritä uudestaan syöttämällä 'lue' tai 'kirjoita': ");
                        str_input = Console.ReadLine();
                    }
                    if (str_input == "lue")
                    {
                        Console.Write("Anna tiedoston nimi: (lari | mari | pate): ");
                        str_input = Console.ReadLine() + ".txt";
                        Traveller.ReadFromFile(str_input, traveller);
                    }
                    else
                    {
                        // Kysytään tälle tiedot Traveller luokan AskInfoa hyödyntäen.
                        Traveller.AskInfo(traveller);
                    }
                    // Tulostetaan luodun traveller olion tiedot PrintSummaryn avulla.
                    Traveller.PrintSummary(traveller);
                }
                // Suoritus alkaa tästä jos asiakastietoja ollaan kysytty jo kertaalleen.
                Console.WriteLine("Komennot: 'uusi' = uusi asiakas | 'seuraava!' = asiakkaan palvelu. | 'lopeta'"); 
                Console.Write("Syötä haluttu komento: ");
                str_input = Console.ReadLine();
                if (str_input == "uusi")
                {
                    // Uusien tietojen kysely suoritetaan while lauseen alussa, emme kirjoita sitä uudestaan tähän.
                }
                else if (str_input == "seuraava!")
                {
                    // Käynnistetään NextTraveller metodi
                    Traveller.NextTraveller(traveller);
                }
                else if (str_input == "lopeta")
                {
                    Is_exit_code_true = true;
                }
                else
                {
                    Console.WriteLine("Anteeksi, nyt en ymmärtänyt?");
                }
            }
            // Jos poistumisen syynä on asikkaiden loppuminen
            if(TravellerQueue.Customers_left == 0)
            {
                Console.WriteLine("\nHuh, olipas päivä!");
                Console.ReadLine();
            }
            // Syötteenä: "lopeta"
            else
            {
                Console.WriteLine("\nPoistun, heippa!");
            }
        }
    }
}
