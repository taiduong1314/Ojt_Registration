using Application.Interface;
using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.PdfSupport
{
    public class PdfFileSupport : IPdfFileSupport
    {
        private readonly IConfiguration _configuration;

        public PdfFileSupport(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadFileToFirebase(IFormFile file, string studentCode)
        {
            //parse file to stream
            var stream = file.OpenReadStream();

            //connect to firebase
            var auth = new FirebaseAuthProvider(new FirebaseConfig(_configuration["Firebase:apiKey"]));
            var a = await auth.SignInWithEmailAndPasswordAsync(_configuration["Firebase:AuthEmail"], _configuration["Firebase:AuthPassword"]);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(_configuration["Firebase:Bucket"], new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                ThrowOnCancel = true
            }).Child("CvFile")
            .Child(studentCode + "_CV.pdf")
            .PutAsync(stream, cancellation.Token);

            return await task;
        }
    }
}
