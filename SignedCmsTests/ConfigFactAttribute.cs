using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Xunit;

namespace SignedCmsTests
{
    public sealed class ConfigFactAttribute : FactAttribute
    {
        public ConfigFactAttribute()
        {
            if (Config.Values == null)
            {
                Skip = "Local config not set up correctly. " +
                    "Please see the README for more information.";
            }
        }

        //Shadow the Skip as get only so it isn't set when an instance of the
        //attribute is declared
        public new string Skip {
            get => base.Skip;
            private set => base.Skip = value;
        }
    }

    public class Config
    {
        public static Config Values { get; }

        static Config()
        {
            try
            {
                var basePath = Path.GetDirectoryName(typeof(Config).GetTypeInfo().Assembly.Location);
                var credLocation = Path.Combine(basePath, @"private\config.json");
                var contents = File.ReadAllText(credLocation);
                Values = JsonConvert.DeserializeObject<Config>(contents);
            }
            catch 
            {
            }
        }

        public string Thumbprint { get; set; }
    }
}
