namespace HotelClient
{
    public static class Config
    {
        private static string _baseUrl;

        static Config()
        {
            _baseUrl = "https://localhost:101";

            LoadFromFile();
        }

        private static void LoadFromFile()
        {
            try
            {
                var assembly = typeof(Config).Assembly;

                var resourceName = "HotelClient.appsettings.json";
                using var stream = assembly.GetManifestResourceStream(resourceName);

                if (stream != null)
                {
                    using var reader = new StreamReader(stream);
                    var json = reader.ReadToEnd();

                    var lines = json.Split('\n');
                    foreach (var line in lines)
                    {
                        if (line.Contains("BaseUrl"))
                        {
                            var parts = line.Split('"');
                            if (parts.Length >= 4)
                            {
                                _baseUrl = parts[3].Trim();
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Не удалось загрузить конфиг, используем значение по умолчанию");
            }
        }

        public static string BaseUrl => _baseUrl;
    }
}