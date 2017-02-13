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
    * Osio 5, TravellerQueue luokka                 *
    ************************************************/
    public partial class TravellerQueue
    {
        // Ominaisuudet sattumanvaraisen matkaajan tietoja varten. Aksessorit ja mutaattorit löytyvät GetSet tiedostosta.
        private int customer_number_from_list;
        private string name_from_list;
        private int age_from_list;
        private string gender_from_list;
        private double money_from_list;
        private int experience_from_list;
        // Kahvitauko ja palveltujen asiakkaiden lista.
        private bool cofee_break;
        // Customers_left täytyy olla staattinen, että sen arvoa voidaan kutsua sellaisenaan LodgingApp luokassa.
        private static int customers_left;
        // Apumuuttujat.
        protected int random_list_item;
        protected int customer_count;
        // Check_customers_left tarkistaa jäljellä olevien asiakkaiden määrän.
        public void Check_customers_left(Traveller traveller)
        {
            // Asiakasnumeroiden määrä - palveltujen asiakkaiden määrä.
            Customers_left = traveller.Customer_number.Count() - traveller.Served_customers.Count();
            if (Customers_left == 0)
            {
                // Asetetaan pääsilmukan läpikäyntimuuttujan arvoksi "true".
                LodgingApp.Is_exit_code_true = true;
            }
        }
        // Next arpoo seuraavan palveltavan asiakkaan jos asiakkaita on jonossa ja kokoaa palveltavan asiakkaan tiedot.
        public void Next(Traveller traveller)
        {
            // Määritellään kahvitauko oletuksena todeksi.
            Cofee_break = true;
            Random random = new Random();
            // Arvotaan random  numero 0 ja 1000 välillä.
            int random_number = random.Next(1000);
            // Tulostetaan jäljellä olevien asiakkaiden määrä.
            Customers_left = traveller.Customer_number.Count() - traveller.Served_customers.Count();
            Console.WriteLine("\nPalveltavia asiakkaita jonossa: " + Customers_left + " kpl");
            // 513 = 51,3 %  tuhannesta eli 51,3 % todenäköisyys.
            if (random_number < 513)
            {
                // Palvellaan seuraavaa asiakasta, cofee break = false.
                Cofee_break = false;
                // Arvotaan uusi palveltava asiakas, jos asiakasta on palveltu jo aiemmin arvotaan uusi asiakas.
                random_list_item = random.Next(traveller.Customer_number.Count);
                while (traveller.Served_customers.Contains(random_list_item))
                {
                    random_list_item = random.Next(traveller.Customer_number.Count);
                }
                // Haetaan traveller olion asiakkaan numero x tiedot
                Customer_number_from_list = traveller.Customer_number[random_list_item];
                Name_from_list = traveller.Name_list[random_list_item];
                Age_from_list = traveller.Age_list[random_list_item];
                Gender_from_list = traveller.Name_list[random_list_item];
                Money_from_list = traveller.Money_list[random_list_item];
                Experience_from_list = traveller.Experience_list[random_list_item];
            }
        }
    }
}
