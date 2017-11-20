using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrApp.Helpers
{
    public class AppSettingsHelper
    {
        Windows.Storage.ApplicationDataContainer localSettings =
            Windows.Storage.ApplicationData.Current.LocalSettings;

        public void AddSetting(string settingKey, object settingValue)
        {
            localSettings.Values[settingKey] = settingValue;
        }

        public object RetrieveSetting(string settingKey)
        {
            Object value = localSettings.Values[settingKey];
            return value;
        }

        public void RemoveSetting(string settingKey)
        {
            localSettings.Values.Remove(settingKey);
        }
    }
}
