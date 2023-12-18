using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IHasingSupport
    {
        string parseEndDate(string endDate);
        string encriptSHA256(string password);
    }
}
