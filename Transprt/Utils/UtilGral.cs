using System.Configuration;

namespace Transprt.UtilAutUtils {
    public class UtilGral {
        public static string GetConfiguration(string setting, string stringDefault) {
            return ConfigurationManager.AppSettings[setting]
                ?? stringDefault;
        }

        public static string GetPageName(string name) {
            return "Transprt | " + name;
        }
    }
}
