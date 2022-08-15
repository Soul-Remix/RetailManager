using System.Security.Claims;
using System.Text.Json;

namespace Portal.Blazor.Authentication;

public static class JwtParser
{
    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split(".")[1];

        var jsonBytes = Parse64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        ExtractCRolesFromJwt(claims, keyValuePairs);

        claims.AddRange(keyValuePairs.Select(s => new Claim(s.Key, s.Value.ToString())));

        return claims;
    }

    private static void ExtractCRolesFromJwt(List<Claim> claims, Dictionary<string, object> keyValuePair)
    {
        keyValuePair.TryGetValue(ClaimTypes.Role, out object roles);

        if (roles != null)
        {
            var parsedRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(",");

            if (parsedRoles.Length > 1)
            {
                foreach (var role in parsedRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Trim('"')));
                }
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));
            }

            keyValuePair.Remove(ClaimTypes.Role);
        }
    }

    private static byte[] Parse64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}