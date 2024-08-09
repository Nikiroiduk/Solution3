﻿using Solution3BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class XmlToDbTests
    {
        [Fact]
        public void IncorrectConnectionString()
        {
            string xml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>

						<orders>
							<order>
								<no>1</no>
								<reg_date>2012.12.19</reg_date>
								<sum>234022.25</sum>
								<product>
									<quantity>2</quantity>
									<name>LG 1755</name>
									<price>12000.75</price>
								</product>
								<product>
									<quantity>5</quantity>
									<name>Xiomi 12X</name>
									<price>42000.75</price>
								</product>
								<product>
									<quantity>10</quantity>
									<name>Noname 14232</name>
									<price>1.7</price>
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
									<fio>Петров Виктор Семенович</fio>
									<email>xyz@email.com</email>
								</user>
							</order>
						</orders>";
            string tempFilePath = Path.GetTempFileName();

            try
            {
                File.WriteAllText(tempFilePath, xml);

				Action act = () => XmlToDataBase.SaveXmlObjectsInDb(tempFilePath, null);

				Exception exception = Assert.Throws<Exception>(act);

				Assert.StartsWith($"Error processing user Иван Иванов:", exception.Message);
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
