using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osio5
{
    /********************************************************
    * Olio-ohjelmointi, kevät 2017                          *
    * Olli Suutari | Opiskelijanro:                         *
    * Osio 5, GetSet (aksessorit ja mutaattorit             *
    *********************************************************/
    public partial class Traveller
    {
        // Määritellään aksessorit ja mutaattorit Traveller luokan  muuttujille.
        public string Name { get { return name; } set { name = value; } }
        public int Age { get { return age; } set { age = value; } }
        public string Gender { get { return gender; } set { gender = value; } }
        public double Money { get { return money; } set { money = value; } }
        public int Experience { get { return experience; } set { experience = value; } }
        public List<int> Customer_number { get { return customer_number; } set { customer_number = value; } }
        public List<string> Name_list { get { return name_list; } set { name_list = value; } }
        public List<int> Age_list { get { return age_list; } set { age_list = value; } }
        public List<string> Gender_list { get { return gender_list; } set { gender_list = value; } }
        public List<int> Experience_list { get { return experience_list; } set { experience_list = value; } }
        public List<double> Money_list { get { return money_list; } set { money_list = value; } }
        public List<int> Served_customers { get { return served_customers; } set { served_customers = value; } }
    }
    public partial class TravellerQueue
    {
        // Määritellään aksessorit ja mutaattorit TravellerQueue luokan  muuttujille.
        public int Customer_number_from_list { get { return customer_number_from_list; } set { customer_number_from_list = value; } }
        public string Name_from_list { get { return name_from_list; } set { name_from_list = value; } }
        public int Age_from_list { get { return age_from_list; } set { age_from_list = value; } }
        public string Gender_from_list { get { return gender_from_list; } set { gender_from_list = value; } }
        public double Money_from_list { get { return money_from_list; } set { money_from_list = value; } }
        public int Experience_from_list { get { return experience_from_list; } set { experience_from_list = value; } }
        public bool Cofee_break { get { return cofee_break; } set { cofee_break = value; } }
        public static int Customers_left { get { return customers_left; } set { customers_left = value; } }
    }
    partial class Room_service : Services
    {
        public string Accomodation_description { get { return accomodation_description; } set { accomodation_description = value; } }
        public double Accomodation_price { get { return accomodation_price; } set { accomodation_price = value; } }
        public int Accomadation_max_visitors { get { return accomadation_max_visitors; } set { accomadation_max_visitors = value; } }
        public DateTime Accomodation_starts { get { return accomodation_starts; } set { accomodation_starts = value; } }
        public DateTime Accomodation_ends { get { return accomodation_ends; } set { accomodation_ends = value; } }
        public Wlan_service Wlan { get { return wlan; } set { wlan = value; } }
    }
}