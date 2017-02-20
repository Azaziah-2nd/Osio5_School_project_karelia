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
        /*
         * Metodi kysyy asiakkaalta haluaako hän huoneen (ison / pienen), langattoman internetin tai koiravaljakkoajelun, 
         * ja luo palveluoliot sen mukaisesti. Asiakkaan ei ole pakko valita huonetta, mutta hän voi silti valita langattoman 
         * internetin tai koiravaljakkoajelun.
         * Palveluoliot metodi tallentaa listaan, ja palauttaa listan paluuarvona. 
         */
        public List<Services> Serve()
        {
            List<Services> service_list = new List<Services>();

            Console.Write("Minkälaisen huoneen haluat? (iso/pieni/ei): ");
            string input = Console.ReadLine();

            Room room = null;
            if (input == "iso")
            {
                room = new Room();
                room.Read_from_file("iso.txt");
            }
            else if (input == "pieni")
            {
                room = new Room();
                room.Read_from_file("pieni.txt");
            }

            if (room != null)
            {
                service_list.Add(room);
            }
            Console.Write("Haluatko internetin? (k/e): ");
            WirelessInternet wifi = null;
            if (Console.ReadLine() == "k")
            {
                wifi = new WirelessInternet();
                wifi.Read_from_file("wlan.txt");
            }
            if (room != null && wifi != null)
            {
                room.Internet = wifi;
            }
            else if (wifi != null && room == null)
            {
                service_list.Add(wifi);
            }
            Console.Write("Haluatko koiravaljakkoajelulle? (k/e): ");
            DogSafari safari = null;
            if (Console.ReadLine() == "k")
            {
                safari = new DogSafari();
                safari.Read_from_file("safari.txt");
            }
            if (safari != null)
            {
                service_list.Add(safari);
            }
            return service_list;
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
                        try
                        {
                            Traveller.ReadFromFile(str_input, traveller);
                            
                        }
                        catch (Error_class error_details)
                        {
                            Console.WriteLine("Hups! Tapahtui virhe: " + error_details.Message);
                        }
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
                    try
                    {
                        Traveller.NextTraveller(traveller);
                    }
                    catch (Error_class e)
                    {
                        Console.WriteLine("Hups! Tapahtui virhe: " + e.Message);
                    }

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
