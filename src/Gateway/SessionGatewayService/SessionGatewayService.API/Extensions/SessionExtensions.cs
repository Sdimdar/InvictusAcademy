using SessionGatewayService.Domain.Entities;
using System.Text.Json;

namespace SessionGatewayService.API.Extensions
{
    public static class SessionExtensions
    {
        public static void SetData(this ISession session, string key, SessionData value)
        {
            session.SetString(key, JsonSerializer.Serialize<SessionData>(value));
        }

        public static SessionData? GetData(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<SessionData>(value);
        }
    }
}
