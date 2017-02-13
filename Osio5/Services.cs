using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Osio5
{
    /****************************************************
    * Olio-ohjelmointi, kevät 2017                      *
    * Olli Suutari | Opiskelijanro:             *
    * Osio 5, Services luokka ja sen alaiset palvelut   *
    *****************************************************/
    //
    class Services
    {
        protected string service_name;

        public string Name_from_list { get { return service_name; } set { service_name = value; } }
        public Services(string name)
        {
            this.service_name = name;
        }
        public void PrintSummary()
        {
            // Nimi, hinta ja kuvaus määritellään palveluiden aliluokissa.
            Console.WriteLine("Palvelu: " + service_name);
        }
    }

    partial class Room_service : Services
    {
        public Room_service()
            : base("Majoitus")
        {
        }
        // Määritellään muuttujat, aksessorit ja mutaattorit ovat GetSet tiedostossa.
        private string accomodation_description;
        private double accomodation_price;
        private int accomadation_max_visitors;
        private DateTime accomodation_starts;
        private DateTime accomodation_ends;
        private Wlan_service wlan;
        // Tietojen kysely joko tiedostosta tai käsin.
        public void Room_service_AskInfo()
        {
            string str_input;
            string filename;
            Console.Write("Jos haluat syöttää tiedot käsin kirjoita 'k', muuten tiedot luetaan tiedostosta: ");
            str_input = Console.ReadLine();
            if (str_input != "k")
            {
                Console.Write("Luetaanko pienen vai ison mökin tiedot? (pieni/iso): ");
                filename = Console.ReadLine();
                try
                {
                    // Yritetään hakea tiedostosta, tallennetaan tiedot muuttujiin.
                    // Tiedoston nimi = teksti + .txt.
                    StreamReader str = new StreamReader(filename + ".txt");
                    Accomodation_description = str.ReadLine();
                    Accomodation_price = float.Parse(str.ReadLine());
                    Accomadation_max_visitors = int.Parse(str.ReadLine());
                    // Tiedoston luku ei onnistunut.
                }
                catch (Exception error)
                {
                    // Tulostetaan virheilmoitus
                    Console.Write("Tiedostoa ei voitu lukea: " + error.Message);
                    Console.Write("\nYritä uudestaan syöttämällä 'iso' tai pieni tai peruta tiedoston luku syöttämällä 'peru': ");
                    filename = Console.ReadLine();
                    // Toistetaan kunnes käyttäjä syöttää oikean tiedostonimen tai peruu syöttämisen.
                    while (filename != "iso" && filename != "pieni" && filename != "peru")
                    {
                        Console.Write("Yritä uudestaan syöttämällä 'iso' tai pieni tai peruta tiedoston luku syöttämällä 'peru': ");
                        filename = Console.ReadLine();
                    }
                    // Käyttäjä peruu syöttämisen, asetetaan str_input syötteeksi "k" ja kysytään tietoja yksitellen.
                    if (filename == "peru")
                    {
                        str_input = "k";
                    }
                    // Luetaan tiedot tiedostosta.
                    else
                    {
                        StreamReader str = new StreamReader(filename + ".txt");
                        Accomodation_description = str.ReadLine();
                        Accomodation_price = float.Parse(str.ReadLine());
                        Accomadation_max_visitors = int.Parse(str.ReadLine());
                    }
                }
            }
            // Jos valittiin tietojen syöttö käsin tai peruttiiin tietojen lukeminen tiedostosta.
            if (str_input == "k")
            {
                Console.WriteLine("\nOle hyvä ja syötä majoituspaikan tiedot.");
                Console.Write("Millainen huone? (iso / pieni): ");
                str_input = Console.ReadLine();
                while (!(str_input == "iso" || str_input == "pieni"))
                {
                    Console.Write("Syötä arvoksi 'iso' tai 'pieni': ");
                    str_input = Console.ReadLine();
                }
                if (str_input == "pieni")
                {
                    Accomodation_price = 59.9;
                    Accomadation_max_visitors = 1;
                    Accomodation_description = "Pikkumökki: Toimiva perusmajoituskohde.";
                }
                else
                {
                    Accomodation_price = 3450.9;
                    Accomadation_max_visitors = 30;
                    Accomodation_description = "Maalaiskartano: Näyttävä ja tilava majoituskohde.";
                }
            }
            // Kysytään majoituksen alkamis ja päättymispäivät.
            Console.Write("Majoituksen aloituspäivämäärä: (pp/kk/vvvv): ");
            str_input = Console.ReadLine() + " 15:00:00";
            Accomodation_starts = Convert.ToDateTime(str_input);
            Console.Write("Majoituksen loppuminen: (pp/kk/vvvv): ");
            str_input = Console.ReadLine() + " 12:00:00";
            Accomodation_ends = Convert.ToDateTime(str_input);
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
                wlan = new Wlan_service();
                wlan.Wlan_Read_from_file();
            }
        }

        public void Room_service_PrintSummary()
        {
            // Tulostetaan palvelun perustiedot (nimi)
            base.PrintSummary();
            // Tulostetaan muut tiedot.
            Console.WriteLine(accomodation_description);
            // Pyöristetään hinta kahden desimaalin tarkkuuteen.
            Console.WriteLine("Hinta: " + Math.Round(accomodation_price, 2) + " euroa | Vieraiden enimäismäärä: " + accomadation_max_visitors);
            Console.WriteLine("Majoituksen alkaminen: " + accomodation_starts.ToString());
            Console.WriteLine("Majoituksen päättyminen: " + accomodation_ends.ToString());
            // Jos wlan on totta, tulostetaan sen tiedot.
            if (wlan != null)
            {
                Console.WriteLine();
                wlan.Wlan_PrintSummary();
            }
            Console.WriteLine();
        }
    }
    class Wlan_service : Services
    {

        public Wlan_service()
            : base("Wlan")
        {
        }
        protected string wlan_name;
        protected string wlan_password;

        public string Wlan_name { get  { return wlan_name; }  set { wlan_name = value; } }
        public string Wlan_password { get { return wlan_password; } set { wlan_password= value; } }
        // Luetaan wlanin tiedot tiedostosta.
        public void Wlan_Read_from_file()
        {
            StreamReader str = new StreamReader("wlan.txt");
            Wlan_name = str.ReadLine();
            Wlan_password = str.ReadLine();
        }
        // Wlanin tietojen tulostus.
        public void Wlan_PrintSummary()
        {
            base.PrintSummary();
            Console.WriteLine("Huoneeseen kuuluu lisäpalveluna MökkiNet: Langaton verkkoyhteys.");
            Console.WriteLine("Hinta: 0 euroa" + " | Verkon nimi: " + Wlan_name + " | Salasana: " + Wlan_password);
        }
    }
    // Koiravaljakko
    class DogRide_service : Services
    {
        private DateTime dogRide_time;
        private int dogRide_max_riders;
        public DateTime DogRide_time { get { return dogRide_time; } set { dogRide_time = value; } }
        public int DogRide_max_riders { get { return dogRide_max_riders; } set { dogRide_max_riders = value; } }
        public DogRide_service(DateTime Time, int Max_riders)
            : base ("Dog ride")
        {
            DogRide_time = Time;
            DogRide_max_riders = Max_riders;
        }
        public void DogRide_AskInfo()
        {
            string date;
            string time;
            string str_input;
            Console.Write("Ajelun päivämäärä: (pp/kk/vvvv): ");
            date = Console.ReadLine();
            Console.Write("Syötä kellon aika (hh:mm)");
            time = Console.ReadLine() + ":00";
            str_input = date + time;
            DogRide_time = Convert.ToDateTime(str_input);
            Console.Write("Matkustajien määrä: ");
            str_input = Console.ReadLine();
            DogRide_max_riders = int.Parse(str_input);
        }
        public void DogRide_Service_PrintSummary()
        {
            base.PrintSummary();
            Console.WriteLine("Ajelu koiravaljakolla.");
            Console.WriteLine("Hinta: 100 euroa" + " | Aika: " + DogRide_time.ToString() + " | Matkustajat: " + DogRide_max_riders);
        }
    }
}
