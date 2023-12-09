using System.Text.Json.Serialization;
using System.Text.Json;

namespace server.Utils;

public static class Constants
{

    public const int MaxItemsPerPage = 2;
    public static JsonSerializerOptions _JsonSerializerOptions = new()
    {
        ReferenceHandler = ReferenceHandler.Preserve
    };
}
