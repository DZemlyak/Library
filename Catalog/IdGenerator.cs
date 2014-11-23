using System;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace Library.Catalog
{
    public static class IdGenerator
    {
        private static int _maxId;
        private const int MinId = 100000;

        public static void SetId(Catalog catalog) {
            try {
                _maxId = catalog.CatalogItems.Max(m => m.Id) + 1;
            }
            catch (Exception) {
                _maxId = MinId;
            }
            ConfigurationSettings.AppSettings["Id"] = _maxId.ToString(CultureInfo.InvariantCulture);
        }

        public static int GetId(string value = null) {
            int id;
            if (!string.IsNullOrEmpty(value)) {
                return int.TryParse(value, out id) ? id : _maxId;
            }
            return int.TryParse(ConfigurationSettings.AppSettings["Id"], out id) ? id : _maxId;
        }

        public static void IncrementId()
        {
            var value = ConfigurationSettings.AppSettings["Id"];
            ConfigurationSettings.AppSettings["Id"] = (GetId(value) + 1).ToString();
        }
    }
}
