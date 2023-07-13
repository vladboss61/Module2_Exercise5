using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Console.Exceptions.Files;

public sealed class MedicalData
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool IsActive { get; set; }

    public AdditionalInfo Info { get; set; }

    public AdditionalInfo Empty { get; set; }
}

public sealed class AdditionalInfo
{
    public string Table { get; set; }

    public string[] Docs { get; set; }
}

internal static class JsonSerializationExample
{
    public static void ExampleJson()
    {
        var data = new MedicalData
        {
            Id = new Random().Next(),
            Description = "Medical Description",
            IsActive = true,
            Name = "Medicine",
            Info = new AdditionalInfo
            {
                Table = "Medical Table",
                Docs = new[] { "str1 content", "str2 content" }
            },
            Empty = null
        };

        var now = DateTime.Now;
        string path = $"medical_{now.ToString("MM-dd-yyyy-hh-mm-ss")}.json";
        string strData = JsonSerializer.Serialize(data);
        
        using (var _ = File.Create(path)) { }

        File.WriteAllText(path, strData);
        var deserializedMedicalData = JsonSerializer.Deserialize<MedicalData>(strData);

        System.Console.WriteLine(deserializedMedicalData.Name);
        System.Console.WriteLine(deserializedMedicalData.Id);

        var jsonTextContent = File.ReadAllText(path);
        var deserializedMedicalDataFromFile = JsonSerializer.Deserialize<MedicalData>(jsonTextContent);

        System.Console.WriteLine(deserializedMedicalDataFromFile.Name);
        System.Console.WriteLine(deserializedMedicalDataFromFile.Id);
    }
}
