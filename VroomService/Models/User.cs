using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace VroomService.Models
{
    public class User : SoapHeader
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public string fisrtname { get; set; }
        public string lastname { get; set; }

        /// <summary>
        /// User constructor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="fisrtname"></param>
        /// <param name="lastname"></param>
        public User(string username, string password, string fisrtname, string lastname)
        {
            this.username = username;
            this.password = password;
            this.fisrtname = fisrtname;
            this.lastname = lastname;
        }

        // TODO vérifiaction de l'existance du user

        // TODO vérification du token du user

    }
}