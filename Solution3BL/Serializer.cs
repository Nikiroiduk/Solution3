using Solution3BL.Models;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;

[assembly: InternalsVisibleTo("UnitTests")]

namespace Solution3BL
{
    internal static class Serializer
    {
        internal static List<Order> SerializeXmlToOrders(string path)
        {
            var serializer = new XmlSerializer(typeof(Orders));

            try
            {
                using (var reader = new StreamReader(path))
                {
                    var orders = (Orders)serializer.Deserialize(reader);
                    return orders?.OrderList ?? new List<Order>();
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("File not found", ex);
            }
            catch (XmlException ex)
            {
                throw new XmlException("XML error", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("Deserialization of the XML file failed", ex);
            }
            catch (Exception ex)
            {
                throw new FileNotFoundException("An unexpected error occurred", ex);
            }
        }
    }
}
