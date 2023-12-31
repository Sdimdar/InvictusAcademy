﻿using System.Text.Json;
using UserGateway.Domain.Entities;

namespace UserGateway.API.Extensions;

public static class SessionExtensions
{
    public static void SetData(this ISession session, string key, SessionData value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static SessionData GetData(this ISession session, string key)
    {
        var value = session.GetString(key);
        if (value == null) throw new UnauthorizedAccessException("User is not Authorized");
        return JsonSerializer.Deserialize<SessionData>(value)!;
    }
}
