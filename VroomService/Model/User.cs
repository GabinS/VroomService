using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace VroomService
{
    public class User : SoapHeader
    {
        public int id { get; set; }
        public string uersname { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public string fisrtname { get; set; }
        public string lastname { get; set; }

        // TODO vérifiaction de l'existance du user

        // TODO vérification du token du user

    }
}