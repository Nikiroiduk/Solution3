using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Solution3BL.Models
{
    public class User
    {
        [XmlElement("fio")]
        public string Fio { get; set; }
        [XmlElement("email")]
        public string Email { get; set; }

        [XmlIgnore]
        private string[] fio
        {
            get => Fio.Split();
        }
        [XmlIgnore]
        public string FirstName
        {
            get => fio[1];
        }
        [XmlIgnore]
        public string LastName
        {
            get => fio[0];
        }

        public override string ToString()
        {
            return $"Fio: {Fio}\n" +
                   $"Email: {Email}";
        }
    }
}
