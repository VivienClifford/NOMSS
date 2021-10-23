using Newtonsoft.Json;
using Service.Models;

namespace Service.Helper
{
    public class JsonDeserializer
    {
        public static OrderProduct GetData() {
            var data = System.IO.File.ReadAllText(Config.DataFile);

            var orderProduct = JsonConvert.DeserializeObject<OrderProduct>(data);
            return orderProduct;
        }
    }
}
