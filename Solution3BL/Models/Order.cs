using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Solution3BL.Models
{
    [XmlRoot("orders")]
    public class Orders
    {
        [XmlElement("order")]
        public List<Order> OrderList { get; set; } = [];
    }

    public class Order
    {
        [XmlElement("no")]
        public int No { get; set; }

        [XmlElement("reg_date")]
        public string RegDate { get; set; }

        [XmlElement("sum")]
        public decimal Sum { get; set; }

        [XmlElement("product")]
        public List<Product> Products { get; set; } = [];

        [XmlElement("user")]
        public User User { get; set; }

        [XmlIgnore]
        public DateTime RegistrationDate
        {
            get => DateTime.ParseExact(RegDate, "yyyy.MM.dd", CultureInfo.InvariantCulture);
            set => RegDate = value.ToString("yyyy.MM.dd");
        }

        public override string ToString()
        {
            string result = $"No: {No}\n" +
                   $"Reg_date: {RegistrationDate}\n" +
                   $"Sum: {Sum}\n" +
                   $"Products\n";

            foreach (var product in Products)
            {
                result += product;
            }

            result += User;

            return result;
        }
    }
}
