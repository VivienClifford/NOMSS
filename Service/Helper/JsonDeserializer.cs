using Newtonsoft.Json;
using Service.Models;
using System;
using System.IO;

namespace Service.Helper
{
    public class JsonDeserializer
    {
        public static OrderProduct GetData() {

            var relativePath = GetRelativePath();

            var textFileData = File.ReadAllText(relativePath + Config.DataFilePath);

            return JsonConvert.DeserializeObject<OrderProduct>(textFileData);
        }

        public static string GetRelativePath()
        {
            var baseDIR = AppDomain.CurrentDomain.BaseDirectory;

            string[] paths = baseDIR.Split('\\');
            var relativePath = "";

            foreach (var path in paths)
            {
                relativePath += $"{path}\\";

                if (path == "NOMSS")
                    break;
            }

            return relativePath;
        }
    }
}
