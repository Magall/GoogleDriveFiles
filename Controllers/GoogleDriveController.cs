using System;
using System.IO;
using System.Text;
using System.Threading;
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

        [HttpPut]
        [Consumes("multipart/form-data")]
        public GoogleDriveInsertFilesResponse InserirDocumento([FromForm]  IFormFile file)
        {
            return _drive.InserirDocumento(file);    
        }
        [HttpDelete]
        public IRestResponse RemoverDocumento([FromForm]string Id)
        {
            return _drive.ApagarDocumento(Id);
        }
        


    } 
}
