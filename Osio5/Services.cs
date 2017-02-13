﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Osio5
{
    /****************************************************
    * Olio-ohjelmointi, kevät 2017                      *
    * Olli Suutari | Opiskelijanro:                     *
    * Osio 5, Services luokka ja sen alaiset palvelut   *
    *****************************************************/
    //
    public class Services
    {
        private string service_name;

        public string Service_name
        {
            get { return service_name; }
            set { service_name = value; }
        }
        private string service_description;

        public string Service_description { get { return service_description; } set { service_description = value; } }
        private double service_price;

        public double Service_price { get { return service_price; } set { service_price = value; } }

        public void Service_base_details()
        {
            Console.WriteLine("\nPalvelu: {0} | Hinta: {1}\nKuvaus: {2}", Service_name, Service_price, Service_description);
        }
        public virtual void Read_from_file(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                Service_name = reader.ReadLine();
                Service_description = reader.ReadLine();
                Service_price = double.Parse(reader.ReadLine());
            }
        }
    }
        public class Room : Services
        {
            private DateTime accomodation_starts;
            private DateTime accomodation_ends;
            public DateTime Accomodation_starts { get { return accomodation_starts; } set { accomodation_starts = value; } }
            public DateTime Accomodation_ends { get { return accomodation_ends; } set { accomodation_ends = value; } }
            private int numPersons;
            private WirelessInternet internet = null;
            public WirelessInternet Internet { get { return internet; } set { internet = value; } }
            public int NumPersons { get { return numPersons; } set { numPersons = value; } }

            // Ylikirjoitetaan Read_from_file metodi
            public override void Read_from_file(string filename)
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    Service_name = reader.ReadLine();
                    Service_description = reader.ReadLine();
                    Service_price = double.Parse(reader.ReadLine());
                    NumPersons = int.Parse(reader.ReadLine());
                }
            }
            public void Room_service_AskInfo()
            {
                WirelessInternet wlan = new WirelessInternet();
                string str_input;
                string filename;
                Console.Write("Luetaanko pienen vai ison mökin tiedot? (pieni/iso): ");
                filename = Console.ReadLine();
                try
                {
                    Read_from_file(filename + ".txt");
                }
                catch (Exception error)
                {
                    // Tulostetaan virheilmoitus
                    Console.Write("Tiedostoa ei voitu lukea: " + error.Message);
                    Console.Write("\nYritä uudestaan syöttämällä 'iso' tai pieni: ");
                    filename = Console.ReadLine();
                    // Toistetaan kunnes käyttäjä syöttää oikean tiedostonimen tai peruu syöttämisen.
                    while (filename != "iso" && filename != "pieni")
                    {
                        Console.Write("Yritä uudestaan syöttämällä 'iso' tai 'pieni': ");
                        filename = Console.ReadLine();
                    }
                    // Luetaan tiedot tiedostosta.
                    StreamReader reader = new StreamReader(filename + ".txt");
                    Service_name = reader.ReadLine();
                    Service_description = reader.ReadLine();
                    Service_price = double.Parse(reader.ReadLine());
                    NumPersons = int.Parse(reader.ReadLine());
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
                    // Luetaan wlanin tiedot tiedostosta wlan.txt
                    wlan.Read_from_file("wlan.txt");
                }
                // Tulostetaan huoneen yhteenveto.
                base.Service_base_details();
                Console.WriteLine("Huoneessa voi majoittua " + NumPersons + " ihmistä.");
                if (wlan != null)
                {
                    wlan.Internet_Details();
                }
            }
        }
        public class WirelessInternet : Services
        {
            private int bandwidth;

            public int Bandwidth
            { get { return bandwidth; } set { bandwidth = value; } }
            private string password;
            public string Password { get { return password; } set { password = value; } }
            public void Internet_Details()
            {
                base.Service_base_details();
                Console.WriteLine("Yhteyden nopeus on " + Bandwidth + ", salasana: \"" + Password);
            }
            public override void Read_from_file(string filename)
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    Service_name = reader.ReadLine();
                    Service_description = reader.ReadLine();
                    Service_price = double.Parse(reader.ReadLine());
                    Bandwidth = int.Parse(reader.ReadLine());
                    Password = reader.ReadLine();
                }
            }
        }
        public class DogSafari : Services
        {
            private int numDogs;
            public int NumDogs { get { return numDogs; } set { numDogs = value; } }
            public void DogSafariDetails()
            {
                base.Service_base_details();
                Console.WriteLine(" Siihen kuuluu " + numDogs + " koiraa. ");
            }
        }
}