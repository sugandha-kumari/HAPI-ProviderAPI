using Microsoft.AspNetCore.Mvc;
using Provider.Structures.Entities;
using Provider.Structures.Interfaces;

namespace HAPI_ProviderAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelBedsController : ControllerBase
    {

        ICredentialAuthService _credentialAuthService;
        private readonly ILogger<HotelBedsController> _logger;

        public HotelBedsController(ILogger<HotelBedsController> logger, ICredentialAuthService credentialAuthService)
        {
            _logger = logger;
            _credentialAuthService= credentialAuthService;
        }

        [HttpGet(Name = "GetStatus")]
        public async Task<CredentialAuthRS> Get()
        {
            return await _credentialAuthService.GetStatus();
        }
    }
}