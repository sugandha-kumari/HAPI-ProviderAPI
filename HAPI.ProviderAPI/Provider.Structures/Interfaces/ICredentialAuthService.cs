using Provider.Structures.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Structures.Interfaces
{
    public interface ICredentialAuthService
    {
        Task<CredentialAuthRS> GetStatus();
        string GetSignature(string apiKey, string secret);
    }
}
