using Provider.Structures.Entities;
using Provider.Structures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Services.HotelBeds
{
    public class CredentialAuthService: ICredentialAuthService
    {
        private readonly IGateway _gateway;
        private readonly string _apiKey;
        private readonly string _secret;
        public CredentialAuthService(IGateway gateway)
        {
            _gateway = gateway;
            _apiKey = "3da03f81d1aa07716583df7633048459";
            _secret = "88fdc2eaeb";
        }
        public async Task<CredentialAuthRS> GetStatus()
        {
            CredentialAuthRS rs = new CredentialAuthRS();

            rs.Signature = GetSignature(_apiKey, _secret);

            var heders = new Dictionary<string, string>();
            heders.Add("X-Signature", rs.Signature);
            heders.Add("Api-Key", _apiKey);
            heders.Add("Accept", "application/json");

            rs.Response = await _gateway.GetJsonAsync("https://api.test.hotelbeds.com/hotel-api/1.0/status", heders);
            
            return rs;
        }
        public  string GetSignature( string apiKey, string secret)
        {
            // Compute the signature to be used in the API call (combined key + secret + timestamp in seconds)

            using (SHA256 sha = SHA256.Create())
            {
                long ts = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds / 1000;
                Console.WriteLine("Timestamp: " + ts);
                byte[] computedHash = sha.ComputeHash(Encoding.UTF8.GetBytes(apiKey + secret + ts));
                return BitConverter.ToString(computedHash).Replace("-", "");
            }
        }

        
    }
}
