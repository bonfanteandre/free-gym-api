using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeGym.Data.Security
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public int Expiration { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
