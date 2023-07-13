namespace Console.Exceptions.Files.Disposable;

using System;
using System.IO;

public class FileService : IDisposable
{
    private bool _disposed = false;

    private readonly StreamWriter _streamWriter;

    public FileService(string path)
    {
        _streamWriter= new StreamWriter(path);
    }

    ~FileService()
    {
        Console.WriteLine("Finalizer of FileService");
        Dispose(false);
    }

    public void CreateFileWithContent(string path, string content)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        using (var _ = File.Create(path)) { }
        File.WriteAllText(path, content);
    }

    public void WriteInfo(string content)
    {
        _streamWriter.WriteLine("|===============|");
        _streamWriter.WriteLine(content);
        _streamWriter.WriteLine("|===============|");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _streamWriter.Dispose();
        }

        _disposed = true;
    }
}
