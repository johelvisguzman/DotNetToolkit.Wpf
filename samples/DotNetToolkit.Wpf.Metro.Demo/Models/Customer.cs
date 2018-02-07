namespace DotNetToolkit.Wpf.Metro.Demo.Models
{
    using System.Collections.Generic;

    public class Customer
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public Customer(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public static IEnumerable<Customer> SampleCustomers
        {
            get
            {
                return new List<Customer>
                {
                    new Customer("Elena Parker", "17 Division Street, Natchez, MS 39120"),
                    new Customer("Charlee Oconnor", "260 N. Ivy St. Lemont, IL 60439"),
                    new Customer("Essence Ritter", "556 Holly Court, Greensboro, NC 27405"),
                    new Customer("Leonel Douglas", "179 Theatre Court, Millville, NJ 08332"),
                    new Customer("Will Brewer", "568 Poplar Lane, Chardon, OH 44024"),
                    new Customer("Brayan Blackburn", "161 Colonial Dr, Ontario, CA 91762"),
                    new Customer("Sarah Wise", "733 Vernon St, Flowery Branch, GA 30542"),
                    new Customer("Sariah Randolph", "293 Brandywine Street, New Kensington, PA 15068"),
                    new Customer("Paityn Sharp", "380 Tower Court, Independence, KY 41051"),
                    new Customer("Bradley Krueger", "24 Devon Drive, Marysville, OH 43040"),
                    new Customer("Lindsey Steele", "9137 Lake View Road, Charlottesville, VA 22901"),
                    new Customer("Kaeden Newton", "639 Albany Street, Brockton, MA 02301"),
                    new Customer("Emiliano Boone", "23 N. College Lane, Knoxville, TN 37918"),
                    new Customer("Agustin Rollins", "680 Stonybrook Dr, Bethlehem, PA 18015"),
                    new Customer("Lexie Parrish", "6 Squaw Creek Rd, South Portland, ME 04106"),
                    new Customer("Sasha Meyers", "8515 Rock Maple St, Point Pleasant Beach, NJ 08742"),
                    new Customer("Angeline Burnett", "50 Homestead Circle, North Kingstown, RI 02852"),
                    new Customer("Remington Lindsey", "34 John Court, Muskego, WI 53150"),
                    new Customer("Brian Castaneda", "648 East Elizabeth St, Ambler, PA 19002"),
                    new Customer("Keagan Cox", "7137 North Peachtree Drive, Saint Johns, FL 32259"),
                    new Customer("Jeffery Mills", "589 Hartford Lane, Marlborough, MA 01752"),
                    new Customer("Naima Cook", "39 Big Rock Cove Street, Paramus, NJ 07652"),
                    new Customer("Graham Benitez", "89 N. Princess Street, Beaver Falls, PA 15010"),
                    new Customer("Corey Kelly", "71 Tarkiln Hill Street, Farmington, MI 48331"),
                    new Customer("Lyric Gill", "4 Manor Station Ave, Fullerton, CA 92831"),
                    new Customer("Todd Cabrera", "139 Brandywine Lane, Palm Bay, FL 32907"),
                    new Customer("Russell Chen", "104 Columbia Ave, Hartford, CT 06106"),
                    new Customer("Nyla Villa", "517 Edgewater Ave, North Andover, MA 01845"),
                    new Customer("Skylar Logan", "8174 San Juan Street, Huntley, IL 60142"),
                    new Customer("Callie Lara", "50 W. Marvon Ave, De Pere, WI 54115")
                };
            }
        }
    }
}
