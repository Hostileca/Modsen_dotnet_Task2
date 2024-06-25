using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dtos.Users
{
    public class AuthenticationResponseDto
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
