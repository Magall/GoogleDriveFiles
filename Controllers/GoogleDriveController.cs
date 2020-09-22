using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Permutante.Models;
using Permutante.Services;
using Permutante.Services.IService;
using RestSharp;

namespace Permutante.Controllers
{
   
    [ApiController]
    [Route("[controller]")]
    public class GoogleDriveController : ControllerBase
    {

        static string[] Scopes = { DriveService.Scope.DriveReadonly, DriveService.Scope.DriveFile, DriveService.Scope.Drive };
        static string ApplicationName = "Permuta API";
        private readonly IGoogleDriveService _drive;
        private readonly ILogger<GoogleDriveController> _logger;

        public GoogleDriveController(ILogger<GoogleDriveController> logger, IGoogleDriveService drive )
        {
            _logger = logger;
            _drive = drive;
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> InserirDocumento( IFormFile file)
        {
           var resp = await _drive.InserirDocumento(file);
            if (resp.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return Ok(resp.Content);
            }
            else 
                return StatusCode(500);
        }
        [HttpDelete]
        public IRestResponse RemoverDocumento([FromForm]string Id)
        {
            return _drive.ApagarDocumento(Id);
        }
        


    } 
}
