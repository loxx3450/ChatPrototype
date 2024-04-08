using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatCore.PayloadTypes
{
    public class AuthRequestPayload : JsonPayload
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public AuthRequestPayload(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
