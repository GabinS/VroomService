﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
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
        VroomServiceModel model = new VroomServiceModel();

        #region Service

        // TODO Connexion (enregistrement du token)
        [WebMethod]
        public string Authentication()
        {
            return null;
        }
        
        // TODO Inscription
        [WebMethod]
        public string Registration()
        {
            return null;
        }

        // TODO Modifier un compte client
        [WebMethod]
        public string EditAccount()
        {
            return null;
        }

        // TODO Récupérer les infos d'un compte (par id)
        [WebMethod]
        public User GetAccount()
        {
            return null;
        }

        // TODO Récupérer la liste des voitures disponible
        [WebMethod]
        public List<Car> GetListCar()
        {
            return null;
        }

        // TODO Récupérer les infos d'une voiture (par id)
        [WebMethod]
        public Car GetCarById(int id)
        {
            return null;
        }

        // TODO Réserver une voiture
        [WebMethod]
        public string BookCar()
        {
            return null;
        }

        // TODO Récuprer la liste des réservations d'un compte
        [WebMethod]
        public List<Booking> GetListBooking(int user_id)
        {
            return null;
        }

        // TODO Récuprer la détails d'une réservation (par id)
        [WebMethod]
        public Booking GetBookingById(int id)
        {
            return null;
        }

        // TODO Annuler une réservation (par id)
        [WebMethod]
        public string CancelBookingById(int id)
        {
            return null;
        }

        #endregion

        #region Method

        // TODO Methode de rejeu du token


        #endregion

    }
}
