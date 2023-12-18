using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IFirebaseSupport
    {
        void initFirebase();
        Task<string> getEmailFromToken(string firebaseToken);
    }
}
