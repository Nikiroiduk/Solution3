using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Solution3BL.Models
{
    public class Product
    {
        [XmlElement("quantity")]
        public int Quantity { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("price")]
        public decimal Price { get; set; }
        public override string ToString()
        {
            return $"\tQuantity: {Quantity}\n" +
                   $"\tName: {Name}\n" +
                   $"\tPrice: {Price}\n";
        }
    }
}
