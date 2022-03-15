using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LoopBreakers.DAL.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Currency
    {
        PLN = 0,
        USD = 1,
        EUR = 2,
        GBP = 3,
        CHF = 4,
        CHK = 5,
        HUF = 6,
        CZK = 7
    }
}