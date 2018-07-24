namespace GoodPractices.Logging
{
    using System;
    using System.IO;

    using Newtonsoft.Json;

    public class FileLogger
    {
        private const string FolderName = "ErrorFolder";

        private readonly ILog Logger = new Logger();
        private readonly JsonSerializer jsonSerializer = new JsonSerializer()
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public void Write<T>(T obj, string file, bool appendLine = false, bool hasError = false)
        {
            try
            {
                using (var fs = File.Open(file, FileMode.Append))
                using (var sw = new StreamWriter(fs))
                using (var jw = new JsonTextWriter(sw))
                {
                    if (appendLine)
                    {
                        sw.WriteLine();
                    }

                    this.jsonSerializer.Serialize(jw, obj);
                }

                // If there is an error, move the file to the errors folder
                if (hasError)
                {
                    var directoryName = Path.GetDirectoryName(file);
                    var errorFileName = Path.GetFileName(file);
                    var errorsFolder = Path.Combine(directoryName, FolderName);
                    if (!Directory.Exists(errorsFolder))
                        Directory.CreateDirectory(errorsFolder);

                    var fileErrorPath = Path.Combine(directoryName, FolderName, errorFileName);
                    File.Move(file, fileErrorPath);
                }
            }
            catch (Exception)
            {
                // TODO: Log error of logging
            }
        }
    }

    internal class Logger : ILog
    {
    }

    internal interface ILog
    {
    }
}
