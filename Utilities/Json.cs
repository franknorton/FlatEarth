using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FlatEarth.Utilities
{
    public class Json
    {
        public static T Load<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Could not load Json file at " + filePath);

            var jsonText = File.ReadAllText(filePath);
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonText);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException("Unable to parse file " + filePath + " into type " + typeof(T).ToString(), ex);
            }
        }
    }
}
