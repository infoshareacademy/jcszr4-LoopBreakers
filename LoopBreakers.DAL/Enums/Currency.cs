using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LoopBreakers.DAL.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Currency
    {
        PLN = 0,
        USD = 1,
        EUR = 2
    }
}