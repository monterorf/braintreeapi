using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPITest
{
    internal static class MessageHelper
    {
        internal static string GetMessage(this EnumResponseStatus status)
        {
            var dictionary = GetDictionary();
            if(!dictionary.ContainsKey(status))
            {
                return "Mensaje default (No se encontro el mensaje de acuerdo al error)";
            }
            return dictionary[status];
        }

        private static Dictionary<EnumResponseStatus, string> GetDictionary()
        {
            var dictionary = new Dictionary<EnumResponseStatus, string>();
            dictionary.Add(EnumResponseStatus.Error, "El servidor no pudo procesar la peticion");            
            return dictionary;
        }
    }
}