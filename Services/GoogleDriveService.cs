using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Permutante.Models;
using Permutante.Services.IService;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Permutante.Services
{
    public class GoogleDriveService :IGoogleDriveService
    {
        static string[] Scopes = { DriveService.Scope.DriveReadonly, DriveService.Scope.DriveFile, DriveService.Scope.Drive };
        static string ApplicationName = "Permuta API";
       
      

        public GoogleDriveInsertFilesResponse InserirDocumento(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();

               // string s = Convert.ToBase64String(fileBytes);
                // act on the Base64 data
            //string path = @"C:\CurriculumPT.pdf";     
            var client = new RestClient("https://www.googleapis.com/upload/drive/v2/files");
            //client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            var accesToken = Autenticar();
             request.AddHeader("Authorization", "Bearer " + accesToken);
            //var bytes = File.ReadAllBytes(path);
            var content = new { title = "tetet.pdf", description = "mypdf.pdf", parents = new[] { new { id = "1ogWGSIIlXk7XK_Hquyv_ozTOPOw5-2uy" } }, mimeType = "application/pdf" };
            var data = JsonConvert.SerializeObject(content);
            request.AddFile("content", Encoding.UTF8.GetBytes(data), "content", "application/json; charset=utf-8");
            request.AddFile("mypdf.pdf", fileBytes, "mypdf.pdf", "application/pdf");
            var response = client.Execute(request);
            var responseData = new GoogleDriveInsertFilesResponse();
            using JsonDocument doc = JsonDocument.Parse(response.Content);
            JsonElement root = doc.RootElement;
           
            responseData.Id = root.GetProperty("id").ToString();
            responseData.Title = root.GetProperty("title").ToString();

            return responseData;

            }
        }
        public string Autenticar()
        {
            UserCredential credential;

            string path = @"C:\CurriculumPT.pdf";
            var bytesData = File.ReadAllBytes(path);
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "rafitogm2@gmail.com",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }
            return credential.GetAccessTokenForRequestAsync().Result;
        }

        public IRestResponse ApagarDocumento(string id)
        {
            var client = new RestClient("https://www.googleapis.com/drive/v3/files/"+id);
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            IRestResponse response = client.Execute(request);
            return response;

        }
    }
}
