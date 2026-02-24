using System.Text.Json;

namespace SsoOAuth.Helpers
{
    public static class FileManagerHelper
    {
        private static readonly string DataFolderPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        private static string GetFullPath(string fileName)
        {
            var fullPath = Path.Combine(DataFolderPath, fileName);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"File not found: {fullPath}");

            return fullPath;
        }
        
        public static Dictionary<string, string> ReadJson(string fileName)
        {
            var fullPath = GetFullPath(fileName);
            var json = File.ReadAllText(fullPath);

            return JsonSerializer.Deserialize<Dictionary<string, string>>(json)
                   ?? throw new Exception("Failed to deserialize JSON.");
        }
        
        public static List<Dictionary<string, string>> ReadCsv(string fileName)
        {
            var fullPath = GetFullPath(fileName);

            var lines = File.ReadAllLines(fullPath);

            if (lines.Length == 0)
                return new List<Dictionary<string, string>>();

            var headers = lines[0].Split(',');

            var result = new List<Dictionary<string, string>>();

            foreach (var line in lines.Skip(1))
            {
                var values = line.Split(',');

                var row = new Dictionary<string, string>();

                for (int i = 0; i < headers.Length; i++)
                {
                    row[headers[i]] = i < values.Length ? values[i] : string.Empty;
                }

                result.Add(row);
            }

            return result;
        }
    }
}