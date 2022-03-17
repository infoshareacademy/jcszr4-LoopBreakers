using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoopBreakers.DAL.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ActivityEvents
    {
        unknown,
        logging,
        logout,
        registering,
        transfering
    }
}
