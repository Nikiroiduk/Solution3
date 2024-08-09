using Solution3BL;
using System.Xml;

namespace UnitTests
{
    public class SerializerTests
    {
        [Fact]
        public void EmptyXml()
        {
            string xml = "<root></root>";
            string tempFilePath = Path.GetTempFileName(); 

            try
            {
                File.WriteAllText(tempFilePath, xml);

                Assert.Throws<InvalidOperationException>(() => Serializer.SerializeXmlToOrders(tempFilePath));
            }
            finally
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath); 
                }
            }
        }

        [Fact]
        public void WrongPathToXml()
        {
            string path = Directory.GetCurrentDirectory();

            Assert.Throws<FileNotFoundException>(() => Serializer.SerializeXmlToOrders($"path\\meh.xml"));
        }

        [Fact]
        public void IncorrectXmlBody()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
                            <orders>
		                            <no>1</no>
		                            <reg_date>2012.12.19</reg_date>
		                            <product>
			                            <quantity>2</quantity>
			                            <price>12000.75</price>
		                            </product>
		                            <product>
			                            <quantity>5</quantity>
			                            <name>Xiomi 12X</name>
			                            <price>42000.75</price>
		                            </product>
		                            <product>
			                            <name>Noname 14232</name>
		                            </product>
		                            <user>
			                            <fio>Иванов Иван Иванович</fio>
			                            <email>abc@email.com</email>
		                            </user>
	                            </order>
	                            <order>
		                            <no>122</no>
		                            <reg_date>2018.01.09</reg_date>
		                            <sum>126065.05</sum>
		                            <product>
			                            <quantity>3</quantity>
			                            <name>Xiomi 12X</name>
			                            <price>42000.75</price>
		                            </product>
		                            <product>
			                            <quantity>20</quantity>
			                            <name>Noname 222</name>
			                            <price>3.14</price>
		                            </product>
		                            <user>
			                            <email>xyz@email.com</email>
		                            </user>
	                            </order>
                            </orders>";

            string tempFilePath = Path.GetTempFileName();

            try
            {
                File.WriteAllText(tempFilePath, xml);

                Assert.Throws<InvalidOperationException>(() => Serializer.SerializeXmlToOrders(tempFilePath));
            }
            finally
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
        }
    }
}