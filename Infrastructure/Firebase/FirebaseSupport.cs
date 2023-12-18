using Application.Interface;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Firebase
{
    public class FirebaseSupport : IFirebaseSupport
    {
        private readonly IConfiguration _configuration;

        public FirebaseSupport(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> getEmailFromToken(string firebaseToken)
        {
            try
            {
                FirebaseToken decodeToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(firebaseToken);
                return decodeToken.Claims.GetValueOrDefault("email").ToString();
            }
            catch (Exception ex)
            {
                return "Firebase Exception: "+ex.Message;
            }
        }

        public void initFirebase()
        {
            if (FirebaseApp.DefaultInstance == null)
            {
                string path = _configuration["Firebase:CridentialPath"];
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(path),
                    ServiceAccountId = _configuration["Firebase:ServiceAccountId"]
                });
            }
        }
    }
}
