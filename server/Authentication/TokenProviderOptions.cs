using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace CarTest.Authentication
{
    public class TokenProviderOptions
    {
        public static string Audience { get; } = "CarTestAudience";
        public static string Issuer { get; } = "CarTest";
        public static SymmetricSecurityKey Key { get; } = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("CarTestSecretSecurityKeyCarTest"));
        public static TimeSpan Expiration { get; } = TimeSpan.FromMinutes(5);
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
    }
}
