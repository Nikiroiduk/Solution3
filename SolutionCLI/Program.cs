using Solution3BL;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static void Main(string[] args)
    {

        if (args.Length == 0)
        {
            Console.WriteLine("No arguments were passed. Usage: -xml pathToXmlFile");
            return;
        }

        string xmlFilePath = null;

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-xml")
            {
                if (i + 1 < args.Length)
                {
                    xmlFilePath = args[i + 1];
                }
                else
                {
                    Console.WriteLine("Error: Path to XML file is missing.");
                    return;
                }
            }
        }

        if (xmlFilePath != null)
        {
            Console.WriteLine($"Processing XML file at: {xmlFilePath}");
        }
        else
        {
            Console.WriteLine("Error: -xml argument was not provided.");
        }


        string projectDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\.."));

        var configuration = new ConfigurationBuilder()
            .SetBasePath(projectDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string connectionString = configuration.GetConnectionString("DefaultConnection");
        XmlToDataBase.SaveXmlObjectsInDb(xmlFilePath, connectionString);
        //XmlToDataBase.SaveXmlObjectsInDb($"{projectDirectory}\\example.xml", connectionString);       
    }
}