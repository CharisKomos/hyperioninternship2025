using System;
using System.IO;

public class FileLogger
{
    private readonly string _logFilePath;

    public FileLogger(string logFilePath = "SimLog.log")
    {
        _logFilePath = logFilePath;
    }

    public void Log(string message, bool breakBefore = true)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(_logFilePath, append: true))
            {
                if (breakBefore)
                    writer.WriteLine(); // Blank line before the message

                writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Logging failed: " + ex.Message);
        }
    }
}
