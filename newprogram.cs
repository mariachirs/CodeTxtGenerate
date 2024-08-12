using System.Collections.Generic;
using System.IO;

public class TextFileGenerator
{
    public static void GenerateTextFile(string configFilePath, string outputFilePath)
    {
        var headers = new List<Header>();
        var infos = new List<Information>();

        for (int i = 1; i <= 5; i++)
        {
            headers.Add(new Header(configFilePath)
            {
                Name = $"HeaderName{i}",
                Email = $"header{i}@example.com",
                Age = $"{30 + i}",
                Address = $"123 Header St, City {i}",
                Obs = $"Header observation {i}",
                MoneyAvailable = $"{i * 1000}"
            });

            infos.Add(new Information(configFilePath)
            {
                Name = $"InfoName{i}",
                Email = $"info{i}@example.com",
                Age = $"{20 + i}",
                Address = $"456 Info Rd, Town {i}",
                Obs = $"Info observation {i}",
                MoneyAvailable = $"{i * 2000}"
            });
        }

        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            for (int i = 0; i < headers.Count; i++)
            {
                writer.WriteLine(headers[i].FormatHeader());
                writer.WriteLine(infos[i].FormatInformation());
            }
        }
    }
}

public class FieldFormattingRule
{
    public string Name { get; set; }
    public int MaxLength { get; set; }
    public char PaddingChar { get; set; }
    public string Align { get; set; }
}

public abstract class FormattableSection
{
    protected Dictionary<string, FieldFormattingRule> _formattingRules;

    public FormattableSection(string configFilePath, string sectionName)
    {
        _formattingRules = LoadFormattingRules(configFilePath, sectionName);
    }

    protected Dictionary<string, FieldFormattingRule> LoadFormattingRules(string configFilePath, string sectionName)
    {
        return System.Xml.Linq.XDocument.Load(configFilePath)
                        .Element("FormattingRules")
                        .Element(sectionName)
                        .Elements("Field")
                        .ToDictionary(
                            e => e.Attribute("name").Value,
                            e => new FieldFormattingRule
                            {
                                Name = e.Attribute("name").Value,
                                MaxLength = int.Parse(e.Attribute("maxLength").Value),
                                PaddingChar = e.Attribute("paddingChar").Value[0],
                                Align = e.Attribute("align").Value
                            });
    }

    protected string FormatField(string fieldName, string fieldValue)
    {
        var rule = _formattingRules[fieldName];
        if (rule.Align == "Right")
        {
            return fieldValue.PadRight(rule.MaxLength, rule.PaddingChar);
        }
        else
        {
            return fieldValue.PadLeft(rule.MaxLength, rule.PaddingChar);
        }
    }
}

public class Header : FormattableSection
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Age { get; set; }
    public string Address { get; set; }
    public string Obs { get; set; }
    public string MoneyAvailable { get; set; }

    public Header(string configFilePath) : base(configFilePath, "Header") { }

    public string FormatHeader()
    {
        return FormatField("Name", Name) +
               FormatField("Email", Email) +
               FormatField("Age", Age) +
               FormatField("Address", Address) +
               FormatField("Obs", Obs) +
               FormatField("MoneyAvailable", MoneyAvailable);
    }
}

public class Information : FormattableSection
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Age { get; set; }
    public string Address { get; set; }
    public string Obs { get; set; }
    public string MoneyAvailable { get; set; }

    public Information(string configFilePath) : base(configFilePath, "Information") { }

    public string FormatInformation()
    {
        return FormatField("Name", Name) +
               FormatField("Email", Email) +
               FormatField("Age", Age) +
               FormatField("Address", Address) +
               FormatField("Obs", Obs) +
               FormatField("MoneyAvailable", MoneyAvailable);
    }
}

namespace TextFileGeneratorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Define paths for the configuration file and the output file
                string configFilePath = "C:\\teste\\config.xml";
                string outputFilePath = "C:\\teste\\output.txt";

                // Call the GenerateTextFile method to generate the file
                TextFileGenerator.GenerateTextFile(configFilePath, outputFilePath);

                // Inform the user that the file has been generated
                Console.WriteLine("Text file has been generated successfully at: " + outputFilePath);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur and display an error message
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
