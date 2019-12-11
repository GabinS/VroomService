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

        #region User

        /// <summary>
        /// Enregistrement du token lors de la connexion.
        /// </summary>
        /// <param name="username">Pseudonyme de l'utilisateur.</param>
        /// <param name="password">Mot de passe de l'utilisateur.</param>
        /// <returns></returns>
        [WebMethod]
        public string Authentication(string username, string password)
        {
            try
            {
                password = EncodePassword(password);

                this.user = db.Users.FirstOrDefault(u => u.Username.Equals(username) && u.Password.Equals(password));
                if (user == null)
                {
                    return "Identifiants incorrects";
                }
                string token = GenerateToken();
                this.user.Token = token;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return $"Erreur de connexion : {ex.ToString()}";
            }

            return "Connexion réussie";

        }

        /// <summary>
        /// Inscription d'un utilisateur.
        /// </summary>
        /// <param name="username">Pseudonyme de l'utilisateur.</param>
        /// <param name="password">Mot de passe de l'utilisateur.</param>
        /// <param name="firstname">Prénom de l'utilisateur.</param>
        /// <param name="lastname">Nom de famille de l'utilisateur</param>
        /// <returns></returns>
        [WebMethod]
        [SoapHeader("unknownHeaders", Required = false)]
        public string Registration(string username, string password, string firstname, string lastname)
        {
            try
            {
                string token = GenerateToken();

                this.user = new User();
                this.user.Username = username;
                this.user.Password = EncodePassword(password);
                this.user.Firstname = firstname;
                this.user.Lastname = lastname;
                this.user.Token = token;
                registerTokenCache(token);

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
        /// <param name="username">Pseudonyme de l'utilisateur.</param>
        /// <param name="firstname">Prénom de l'utilisateur.</param>
        /// <param name="lastname">Nom de famille de l'utilisateur</param>
        /// <param name="password">Mot de passe de l'utilisateur.</param>
        /// <returns></returns>
        [WebMethod]
        public string EditAccount(int userId, string username, string firstname, string lastname, string password)
        {
            try
            {
                User user = db.Users.FirstOrDefault(u => u.Id == userId);

                user.Username = username;
                user.Firstname = firstname;
                user.Lastname = lastname;

                if (password != "")
                {
                    user.Password = EncodePassword(password);
                }

                db.SaveChanges();
                return "Modification enregistrée";
            }
            catch (Exception ex)
            {
                return $"Erreur de modification du compte : {ex.ToString()}";
            }

        }

        /// <summary>
        /// Récupère les informations d'un compte utilisateur.
        /// </summary>
        /// <param name="id">Numéro d'identification de l'utilisateur.</param>
        /// <returns>L'utilisateur</returns>
        [WebMethod]
        public User GetAccount(int id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        #endregion

        #region Cars

        /// <summary>
        /// Récupère la liste des voitures.
        /// </summary>
        /// <returns>La liste des véhicules.</returns>
        [WebMethod]
        public List<Car> GetListCar()
        {
            return db.Cars.Include("Brand").ToList();
        }

        /// <summary>
        /// Récupère les informations pour une voiture.
        /// </summary>
        /// <param name="id">Numéro d'identification de la voiture.</param>
        /// <returns>La voiture</returns>
        [WebMethod]
        public Car GetCarById(int id)
        {
            return db.Cars.Include("Brand").FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Récupère le numéro d'identification de l'utilisateur.
        /// </summary>
        /// <param name="id">Numéro d'identification de l'utilisateur</param>
        /// <returns>L'utilisateur</returns>
        [WebMethod]
        public User GetUserById(int id)
        {
            return db.Users.FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Réservation d'un voiture pour un utilisateur.
        /// </summary>
        /// <param name="startdate">Date de retrait de la voiture.</param>
        /// <param name="enddate">Date de retour de la voiture.</param>
        /// <param name="car_id">Numéro d'identification de la voiture.</param>
        /// <param name="user_id">Numéro d'identification de l'utilisateur.</param>
        /// <returns></returns>
        [WebMethod]
        public List<Brand> GetListBrand()
        {
            return db.Brands.ToList();
        }

        [WebMethod]
        public List<int> GetListNbPlace()
        {
            List<int> nbPlaceList = new List<int>();
            db.Cars.ToList().ForEach(c => {
                if (c.PlaceNb != null && !nbPlaceList.Contains((int)c.PlaceNb))
                    nbPlaceList.Add((int)c.PlaceNb);
            });
            return nbPlaceList;
        }

        #endregion

        #region Bookings

        /// <summary>
        /// Réserve une voiture
        /// </summary>
        /// <param name="startdate">Date de début</param>
        /// <param name="enddate">Date de fin</param>
        /// <param name="car_id">identifiant d'une voiture</param>
        /// <param name="user_id">identifiant de l'utilisateur</param>
        /// <returns></returns>
        [WebMethod]
        public string BookCar(DateTime startdate, DateTime enddate, int car_id, int user_id)
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
                return $"Erreur lors de la réservation de la voiture : {ex.ToString()}";
            }
            return "Réservation réussie";
        }

        /// <summary>
        /// Récupère la liste des réservations pour un utilisateur.
        /// </summary>
        /// <param name="user_id">Numéro d'identification de l'utilisateur.</param>
        /// <returns></returns>
        [WebMethod]
        public List<Booking> GetListBooking(int user_id)
        {
            // Date du jour.
            DateTime dateJour = DateTime.Now;

            // Liste les réservations en cours pour l'utilisateur.
            List<Booking> bookings = db.Bookings.Where(b => b.State == "En cours" && (b.EndDate == dateJour || b.EndDate < dateJour) && b.User_Id == user_id).ToList();

            foreach (Booking book in bookings)
            {
                book.State = "Terminé";
            }

            db.SaveChanges();

            return db.Bookings.Include("Car").Include("User").Where(b => b.User_Id == user_id).ToList();
        }

        /// <summary>
        /// Récupère les détails d'une réservation.
        /// </summary>
        /// <param name="id">Numéro d'identification de la réservation.</param>
        /// <returns>Les informations d'un réservation.</returns>
        [WebMethod]
        public Booking GetBookingById(int id)
        {
            return db.Bookings.Include("Car").Include("User").FirstOrDefault(b => b.Id == id);
        }

        /// <summary>
        /// Annule une réservation en cours.
        /// </summary>
        /// <param name="id">Numéro d'identification de la réservation.</param>
        /// <returns></returns>
        [WebMethod]
        public string CancelBookingById(int id)
        {
            Booking booking = db.Bookings.FirstOrDefault(b => b.Id == id && b.State == "En cours");
            booking.State = "Annulé";

            db.SaveChanges();

            return "Réservation annulée";
        }

        #endregion

        #endregion

        #region Method

        /// <summary>
        /// Génère un token
        /// </summary>
        /// <returns>Token</returns>
        private string GenerateToken()
        {
            string token = Guid.NewGuid().ToString();
            registerTokenCache(token);
            return token;
        }

        /// <summary>
        /// Enregistre le token dans le cache serveur
        /// </summary>
        /// <param name="token">Token</param>
        private void registerTokenCache(string token)
        {
            HttpRuntime.Cache.Add(
                token,
                this.user.Username,
                null,
                Cache.NoAbsoluteExpiration,
                TimeSpan.FromMinutes(30),
                CacheItemPriority.NotRemovable,
                null
            );
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
        /// Encode un mot de passe donné
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncodePassword(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

        #endregion

    }
}
