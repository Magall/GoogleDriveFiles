using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Permutante.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Permutante.Services.IService
{
    public interface IGoogleDriveService
    {
        public  Task<IRestResponse> InserirDocumento(IFormFile file);
        public string Autenticar();
        public IRestResponse ApagarDocumento(string id);
    }
}
