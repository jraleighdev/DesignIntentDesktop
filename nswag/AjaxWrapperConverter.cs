using DesignIntentDesktop.ServiceProxies;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignIntentDesktop.nswag
{
    public class AjaxWrapperConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Read about this problem here:
            // https://stackoverflow.com/questions/45777820/asp-net-boilerplate-generating-c-sharp-swagger-client-using-swagger-codegen-to

            var token = JToken.Load(reader);
            var tokenResult = token.First.First;
            var result = tokenResult.ToObject(objectType);

            return result;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CloudFileDto);
        }
    }
}
