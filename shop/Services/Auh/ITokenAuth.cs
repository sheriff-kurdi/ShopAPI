using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shop.Services.Auth
{
    public interface ITokenAuth
    {
        public string GenerateToken();
    }
}
