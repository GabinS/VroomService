using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;
using VroomService.Models;

namespace VroomService
{
    /// <summary>
    /// Description résumée de VroomService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante. 
    // [System.Web.Script.Services.ScriptService]
    public class VroomService : System.Web.Services.WebService
    {
        /// <summary>
        /// Connection à la base de données.
        /// </summary>
        VroomServiceModel db = new VroomServiceModel();

        /// <summary>
        /// Utilisateur connecté
        /// </summary>
        User user;

        // Receive all SOAP headers besides the MyHeader SOAP header.
        public SoapUnknownHeader[] unknownHeaders;

        #region Service

        // Connexion (enregistrement du token)
        [WebMethod]
        [SoapHeader("unknownHeaders", Required = false)]
        public string Authentication(string username, string password)
        {
            try
            {
                this.user = db.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
                this.user.Token = GenerateToken();
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return $"Erreur d'enregistrement : {ex.ToString()}";
            }

            return this.user.ToString();
        }

        // Inscription
        [WebMethod]
        [SoapHeader("unknownHeaders", Required = false)]
        public string Registration(string username, string password)
        {
            try
            {
                this.user = new User();
                this.user.Username = username;
                this.user.Password = Hashpassword(password);
                this.user.Token = GenerateToken();
                db.Users.Add(this.user);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return $"Erreur d'enregistrement : {ex.ToString()}";
            }

            return "Enregistrement réussi";
        }

        /// <summary>
        /// Modification d'un compte utilisateur existant.
        /// </summary>
        /// <param name="userId">Numéro d'identification de l'utilisateur.</param>
        /// <param name="userName">Pseudonyme de l'utilisateur.</param>
        /// <param name="password">Mot de passe de l'utilisateur.</param>
        /// <param name="firstName">Prénom de l'utilisateur.</param>
        /// <param name="lastName">Nom de famille de l'utilisateur</param>
        /// <param name="password">Mot de passe de l'utilisateur.</param>
        /// <returns></returns>
        [WebMethod]
        public string EditAccount(int userId, string userName, string firstName, string lastName, string password)
        {
            try
            {
                User user = db.Users.Where(u => u.Id == userId).FirstOrDefault();

                user.Username = userName;
                user.Password = password;
                user.Firstname = firstName;
                user.Lastname = lastName;

                db.SaveChanges();
                return "Modification enregistrée";
            }
            catch (Exception)
            {
                return "La modification du compte a échoué.";
            }

        }

        // Récupérer les infos d'un compte (par id)
        [WebMethod]
        [return: XmlElement("User", typeof(User))]
        public User GetAccount(int id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        // Récupérer la liste des voitures disponible
        [WebMethod]
        public List<Car> GetListCar()
        {
            return db.Cars.ToList();
        }

        // Récupérer les infos d'une voiture (par id)
        [WebMethod]
        public Car GetCarById(int id)
        {
            return db.Cars.Include("Brand").FirstOrDefault(c => c.Id == id);
        }
        
        // Réserver une voiture
        [WebMethod]
        public string BookCar(DateTime startdate, DateTime enddate , int car_id, int user_id)
        {
            try
            {
                Booking booking = new Booking();
                booking.StartDate = startdate;
                booking.EndDate = enddate;
                booking.State = "En cours";
                booking.Car = db.Cars.FirstOrDefault(b => b.Id == car_id);
                booking.User = GetAccount(user_id);

                db.Bookings.Add(booking);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return "Erreur lors de la réservation";
            }
            return "Réservation réussie";
        }

        // TODO Récuprer la liste des réservations d'un compte
        [WebMethod]
        public List<Booking> GetListBooking(int user_id)
        {
            // Date du jour.
            DateTime dateJour = DateTime.Now;

            // Liste les réservations en cours de l'utilisateur.
            // si date du jour et si date inférieure (et heure).
            List<Booking> bookings = db.Bookings.Where(b => b.State == "En cours" && (b.EndDate == dateJour || b.EndDate < dateJour) && b.User_Id == user_id).ToList();

            foreach (Booking book in bookings)
            {
                book.State = "Terminé";              
            }

            db.SaveChanges();

            return db.Bookings.Include("Car").Include("User").Where(b => b.User_Id == user_id).ToList();
        }

        // Récuprer la détails d'une réservation (par id)
        [WebMethod]
        public Booking GetBookingById(int id)
        {
            return db.Bookings.Include("Car").Include("User").FirstOrDefault(b => b.Id == id);
        }

        // Annuler une réservation (par id)
        [WebMethod]
        public string CancelBookingById(int id)
        {
            Booking booking = db.Bookings.Where(b => b.Id == id).FirstOrDefault();
            booking.State = "Annulé"; // changer le statut de la réservation = annulé.

            db.SaveChanges();

            return "Réservation annulée";
        }

        #endregion

        #region Method

        /// <summary>
        /// Génère un token
        /// </summary>
        /// <returns>Token</returns>
        private string GenerateToken()
        {
            string token = Guid.NewGuid().ToString();
            HttpRuntime.Cache.Add(
                token,
                this.user.Username,
                null,
                Cache.NoAbsoluteExpiration,
                TimeSpan.FromMinutes(30),
                CacheItemPriority.NotRemovable,
                null
                );
            return token;
        }

        /// <summary>
        /// Vérifie que le token passé est le même que celui en cache
        /// </summary>
        /// <returns></returns>
        private bool CheckToken()
        {
            if (this.user != null && !string.IsNullOrEmpty(this.user.Token))
                return HttpRuntime.Cache[this.user.Token] != null;
            return false;
        }

        /// <summary>
        /// Hachage d'un mot de passe donné
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Mot de passe haché</returns>
        private string Hashpassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return null;
            if (password.Length < 1) return null;

            byte[] salt = new byte[20];
            byte[] key = new byte[20];
            byte[] ret = new byte[40];

            try
            {
                using (RNGCryptoServiceProvider randomBytes = new RNGCryptoServiceProvider())
                {
                    randomBytes.GetBytes(salt);

                    using (var hashBytes = new Rfc2898DeriveBytes(password, salt, 10000))
                    {
                        key = hashBytes.GetBytes(20);
                        Buffer.BlockCopy(salt, 0, ret, 0, 20);
                        Buffer.BlockCopy(key, 0, ret, 20, 20);
                    }
                }
                // returns salt/key pair
                return Convert.ToBase64String(ret);
            }
            finally
            {
                if (salt != null)
                    Array.Clear(salt, 0, salt.Length);
                if (key != null)
                    Array.Clear(key, 0, key.Length);
                if (ret != null)
                    Array.Clear(ret, 0, ret.Length);
            }
        }

        #endregion

    }
}
