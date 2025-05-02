using EdCon.MiniGameTemplate.Saves;
using EdCon.MiniGameTemplate.UI;
using Newtonsoft.Json;
using UnityEngine;

namespace EdCon.MiniGameTemplate
{
    public class SaveManager
    {
        public static readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
            Converters =
            {
                new Vector3SaveConverter()
            }
        };

        public static void SetData(string key, object data)
        {
            PlayerPrefs.SetString(key, JsonConvert.SerializeObject(data, _jsonSerializerSettings));
        }

        public static T GetData<T>(string key, T type = null) where T : class, ISaveData
        {
            var prefs = PlayerPrefs.GetString(key);
            return JsonConvert.DeserializeObject<T>(prefs, _jsonSerializerSettings);
        }

        public static bool TryGetData<T>(string key, out T result) where T : class, ISaveData, new()
        {
            if (!PlayerPrefs.HasKey(key))
            {
                result = null;
                return false;
            }

            result = GetData<T>(key, new T());

            return result != null;
        }
    }
}