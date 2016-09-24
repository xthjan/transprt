using System.Configuration;

namespace Transprt.Utils {
    public class UtilGral {
        public const string ERROR_FROM_CONTROLLER = "modelErrorCustomized";
        public static string GetConfiguration(string setting, string stringDefault) {
            return ConfigurationManager.AppSettings[setting]
                ?? stringDefault;
        }

        public static string GetPageName(string name) {
            return "Transprt | " + name;
        }
    }
}
