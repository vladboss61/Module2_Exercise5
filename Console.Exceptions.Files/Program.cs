namespace Console.Exceptions.Files;

using Console.Exceptions.Files.Disposable;
using System;
using System.IO;
using System.Text;

internal class Program
{
    public static void Main(string[] args)
    {
        JsonSerializationExample.ExampleJson();

        UsingExample();

        ExceptionExample();

        FolderExample();

        FileExample();
    }

    public static void FolderExample()
    {
        var drivers = DriveInfo.GetDrives();

        foreach (DriveInfo driver in drivers)
        {
            Console.WriteLine(driver.Name);
        }

        Directory.CreateDirectory("D://MyNewFolder");
        Directory.CreateDirectory("MyNewFolderNearExe");

        if (Directory.Exists("D://MyNewFolder123"))
        {
            Console.WriteLine("MyNewFolder is exist");
        }

        var info = new DirectoryInfo("D://MyNewFolder");
        var files = info.GetFiles();

        for (int i = 0; i < files.Length; i++)
        {
            Console.WriteLine(files[i].FullName);
        }
    }

    public static void FileExample()
    {
        var fileInfo = new FileInfo("file.txt");
        fileInfo.Create();

        using (var fileStream = File.Create("created.txt"))
        using (var writer = new StreamWriter(fileStream))
        {
            for (int i = 0; i < 20; i++)
            {
                writer.WriteLine(i + " - Hello фівфівфів");
            }

            writer.WriteLine("End");
            writer.Flush();
            writer.BaseStream.Seek(0, SeekOrigin.Begin);
            writer.WriteLine("Seek to Begin");
        }

        using (var fileStream = File.Open("created.txt", FileMode.Open))
        using (var reader = new StreamReader(fileStream))
        {
            string line1 = reader.ReadLine();
            string line2 = reader.ReadLine();
            string line3 = reader.ReadLine();
        }


        byte[] UTF32 = Encoding.UTF32.GetBytes("Hello World");

        byte[] ASCII = Encoding.ASCII.GetBytes("Hello World");
        byte[] UTF7 = Encoding.UTF7.GetBytes("Hello World");
        byte[] Unicode = Encoding.Unicode.GetBytes("Hello World");

        using (var fileStream = File.Create("created.UTF32.txt"))
        {
            fileStream.Write(UTF32);
        }

        var bytes = new byte[UTF32.Length];
        using (var fileStream = File.Open("created.UTF32.txt", FileMode.Open))
        {
            fileStream.Read(bytes, 0, bytes.Length);
        }

        var helloStr = Encoding.Unicode.GetString(bytes);
        Console.WriteLine(helloStr);
        Console.WriteLine();
    }

    public static void UsingExample()
    {
        //using (var stream1 = new FileStream("text.txt", FileMode.OpenOrCreate))
        //{
        //    stream1.Write(new byte[] { 47, 48, 49, 50 });
        //}

        // ===

        FileStream stream2 = null;
        try
        {
            stream2 = new FileStream("text.txt", FileMode.OpenOrCreate);
        }
        finally
        {
            if (stream2 != null)
            {
                stream2.Dispose();
            }
        }
        Console.WriteLine();
    }

    public static void ExceptionExample()
    {
        var example = new ExampleExceptionThrow();

        try
        {
            example.Logic();
        }
        catch (MyException ex)
        {
            Console.WriteLine($"I am executing always. {ex.Message}");
        }

        try
        {
            example.LogicInvalid();
            example.LogicInvalid();
            example.LogicInvalid();
            example.LogicInvalid();
            example.LogicInvalid();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Catch Exceptions InvalidOperationException: {ex.Message}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"Catch Exceptions DivideByZeroException: {ex.Message}");
        }
        catch (ExecutionEngineException ex)
        {
            Console.WriteLine($"Catch Exceptions ExecutionEngineException: {ex.Message}");
        }
        catch (MyException ex)
        {
            Console.WriteLine("I am executing always.");
        }

        GC.Collect();
        Console.WriteLine("Hello, World!");
    }

    private static void ExampleFileSerivce()
    {
        using (var fileService = new FileService("text_file.txt"))
        {
            fileService.WriteInfo("something content");
        }

        // The same code.

        FileService fileService2 = null;
        try
        {
            fileService2 = new FileService("text.txt");
        }
        finally
        {
            if (fileService2 != null)
            {
                fileService2.Dispose();
            }
        }
    }
}
