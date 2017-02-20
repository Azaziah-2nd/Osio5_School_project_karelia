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
    * Olli Suutari | Opiskelijanro:                     *
    * Osio 5, Services luokka ja sen alaiset palvelut   *
    *****************************************************/
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

        // Monimuotoistetaan Read_from_file metodi.
        public virtual void Read_from_file(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                Service_name = reader.ReadLine();
                Service_description = reader.ReadLine();
                Service_price = double.Parse(reader.ReadLine());
            }
        }
        public virtual string Service_summary()
        {
            return "\n" + Service_name + " - Hinta: " + Service_price + " euroa\nKuvaus: " + Service_description  + "\n";
        }
    }
    public class Room : Services
    {
        private DateTime accomodation_starts;
        private DateTime accomodation_ends;
        public DateTime Accomodation_starts { get { return accomodation_starts; } set { accomodation_starts = value; } }
        public DateTime Accomodation_ends { get { return accomodation_ends; } set { accomodation_ends = value; } }
        private int numPersons;
        private WirelessInternet internet = new WirelessInternet();
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
        public override string Service_summary()
        {
            // Tulostetaan huoneen yhteenveto.
            string tmp = base.Service_summary() + "Huoneessa voi majoittua " + NumPersons + " ihmistä. ";
            if (Internet != null)
            {
                tmp += "\nHuoneeseen kuuluu lisäpalveluna: " + Internet.Service_summary();
            }
            return tmp;
        }
    }
        public class WirelessInternet : Services
        {
            private int bandwidth;
            public int Bandwidth
            { get { return bandwidth; } set { bandwidth = value; } }
            private string password;
            public string Password { get { return password; } set { password = value; } }
            public override string Service_summary()
            {
                // Tulostetaan huoneen yhteenveto.
                return base.Service_summary() + "Yhteyden nopeus on " + Bandwidth + ", salasana: " + Password;
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

        public override void Read_from_file(string file)
        {
            try
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    Service_name = reader.ReadLine();
                    Service_description = reader.ReadLine();
                    Service_price = double.Parse(reader.ReadLine());
                    NumDogs = int.Parse(reader.ReadLine());
                }
            }
            catch (Exception error_message)
            {
                throw new Error_class("Tiedostoa ei pystytty lukemaan: " + error_message.Message);
            }
        }
        public override string Service_summary()
        {
            // Tulostetaan huoneen yhteenveto.
            return base.Service_summary() + "Siihen kuuluu " + numDogs + " koiraa. ";
        }
    }
}