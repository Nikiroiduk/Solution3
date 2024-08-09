using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using Solution3BL;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static void Main(string[] args)
    {
        string projectDirectory = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\.."));

        var configuration = new ConfigurationBuilder()
            .SetBasePath(projectDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string connectionString = configuration.GetConnectionString("DefaultConnection");
        XmlToDataBase.SaveXmlObjectsInDb($"{projectDirectory}\\example.xml", connectionString);       
    }
}