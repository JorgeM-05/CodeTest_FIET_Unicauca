using Newtonsoft.Json;
using System;

namespace OrqUtilities
{
    public static class Utilities
    {
        public static T Deserializacion<T>(this string jsonTexto) where T : class
        {
            jsonTexto = jsonTexto.TrimStart('\"');
            jsonTexto = jsonTexto.TrimEnd('\"');
            return JsonConvert.DeserializeObject<T>(jsonTexto);
        }

    }
}
