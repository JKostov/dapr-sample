
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Products.Utils;

public static class Constants
{
    public const string DaprStoreNameVariable = "DAPR_STORE_NAME";

    public const string MongoStoreName = "statestore-mongo";

    public readonly static JsonSerializerOptions SerializerOptions = new JsonSerializerOptions();

    static Constants()
    {
        SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }
}