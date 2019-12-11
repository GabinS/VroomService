# Vroom Service

## I. Définition et connexion
### A. Définition
Vroom Service est un service web permettant de louer des véhicules.

### B. Accès au web service
Pour accéder au web service, il faut se relier au réseau de l'Institut d'Informatique Appliquée (IIA) et se connecter à l'adresse suivante **http://192.168.214.64/:52066/VroomService.asmx**.  

Vous arrivez sur la page d'accueil du service web. Pour accéder au service de réservation de voitures, il faudra cliquer sur ***Vroom Service***.

On vous demandera de vous identifier avec un pseudonyme et un mot de passe.

 ## II. Méthodes
| Opération      | Description |
| ----------- | ----------- |
| Authentification      | Fonction utilisée lors de la connexion de l'utilisateur.       |
| AuthentificationResponse   | Fonction qui retourne le résultat de l'authentification.        |
| Registration   | Fonction qui enregistre l'utilisateur.        |
| RegistrationResponse   | Fonction qui retourne le résultat de l'enregistrement.       |
| EditAccount   | Fonction qui modifie le compte d'un utilisateur.       |
| EditAccountResponse   | Fonction qui retourne le résultat de la modification du compte utilisateur.       |
| GetAccount   | Fonction qui récupère un compte utilisateur.      |
| GetAccountResponse   | Fonction qui retourne le résultat de la récupération du compte utilisateur.       |
| GetListCar   | Fonction appellée pour récupérer la liste des voitures.       |
| GetListCarResponse   | Fonction qui retourne une liste de voitures sous la forme d'un tableau.       |
| GetCarById   | Fonction qui récupère la voiture avec son numéro d'identification.       |
| GetCarByIdResponse   | Fonction qui retourne le résultat de la voiture.      |
| BookCar   | Fonction qui associe une voiture à une réservation.       |
| BookCarResponse   | Fonction qui retourne le résultat de l'association d'un véhicule avec une réservation.       |
| GetListBooking   | Fonction qui liste les réservations.       |
| GetListBookingResponse   | Fonction qui retourne une liste de réservations sous la forme d'un tableau.      |
| GetBookingById   | Fonction qui récupère la réservation avec son numéro d'identification.       |
| GetBookingByIdResponse   | Fonction qui retourne le résultat de la réservation.       |
| CancelBookingById   | Fonction qui annule la réservation d'un véhicule.       |
| CancelBookingByIdResponse   | Fonction qui retourne le résultat de l'annulation de la réservation.     |

   
## II. Compte utilisateur  
---

### A. Authentification
Fonction utilisée lors de la connexion de l'utilisateur. Deux chaînes de caractère seront nécessaires pour permettre à l'utilisateur d'accèder au service web : un **pseudonyme** et un **mot de passe**. 

| Nom      | Type | Description |
| ----------- | ----------- | ----------- |
| username | string | Pseudonyme de l'utilisateur |
| password | string | Mot de passe de l'utilisateur |


### B. AuthentificationResponse
Fonction qui retourne le résultat de l'authentification sous la forme d'une chaîne de caractère.

### C. Registration 
Fonction qui enregistre la connexion de l'utilisateur. Deux chaînes de caractère seront nécessaires pour permettre à l'utilisateur d'accèder au service web : un **pseudonyme** et un **mot de passe**. 
| Nom      | Type | Description |
| ----------- | ----------- | ----------- |
| username | string | Pseudonyme de l'utilisateur |
| password | string | Mot de passe de l'utilisateur |

### D. RegistrationResponse
Fonction qui retourne le résultat de l'enregistrement sous la forme d'une chaîne de caractère. 

### E. EditAccount
Fonction qui modifie le compte d'un utilisateur. Plusieurs données seront attendues pour modifier les données existants d'un utilisateur : un **numéro d'identification**, un **pseudonyme**, un **prénom**, un **nom** et un **mot de passe**.
| Nom      | Type | Description |
| ----------- | ----------- | ----------- |
| userId | int | Numéro d'identification l'utilisateur |
| username | string | Pseudonyme de l'utilisateur |
| firstName | string | Prénom de l'utilisateur |
| lastName | string | Nom de famille de l'utilisateur |
| password | string | Mot de passe de l'utilisateur |


### F. EditAccountResponse
Fonction qui retourne le résultat de la création du compte utilisateur.       |

### H. GetAccount
Fonction qui récupère un compte utilisateur. 

| Nom      | Type | Description |
| ----------- | ----------- | ----------- |
| id | int | Numéro d'identification l'utilisateur |

### I. GetAccountResponse
Fonction qui retourne le résultat de la récupération du compte utilisateur. 

| Nom      | Type | Description |
| ----------- | ----------- | ----------- |
| User | string | Pseudonyme de l'utilisateur |

## III. Gestion des voitures

### A. GetListCar
Appelle la méthode pour récupérer la liste des voitures.

### B. GetListCarResponse
Fonction qui retourne une liste de voitures sous la forme d'un tableau.

### C. GetCarById
Fonction qui récupère le numéro d'identification de la voiture : **id**.
| Nom      | Type | Description |
| ----------- | ----------- | ----------- |
| id | int | Numéro d'identification de la voiture |

### D. GetCarByIdResponse
Fonction qui retourne le résultat de la récupération du numéro d'identification de la voiture.

### E. BookCar
Fonction de réservation de voitures. Quatre données seront nécessaires pour enregistrer la location : une **date de retrait**, une **date de retour**, le **numéro d'identification de la voiture**, le **numéro d'identification de l'utilisateur**.

| Nom      | Type | Description |
| ----------- | ----------- | ----------- |
| startdate | dateTime | Date de retrait de la voiture |
| enddate | dateTime | Date de retour de la voiture |
| car_id | int | Numéro d'identification de la voiture |
| user_id | int | Numéro d'identification de l'utilisateur |


### F. BookCarResponse
Fonction qui retourne le résultat de la réservation de la voiture.


## IV. Gestion des réservations
### A. GetListBooking
Fonction qui récupère la liste des réservations pour un utilisateur grâce à son numéro d'identification : **id**.
| Nom      | Type | Description |
| ----------- | ----------- | ----------- |
| user_id | int | Numéro d'identification de l'utilisateur |

### B. GetListBookingResponse
Fonction qui retourne le résultat de la liste des réservations.

### C. GetBookingById
Fonction qui récupère les informations d'une réservation grâce à son numéro d'identification. 

| Nom      | Type | Description |
| ----------- | ----------- | ----------- |
| id | int | Numéro d'identification de la réservation |
    id : Numéro d'identification de la réservation

### D. GetBookingByIdResponse
Fonction qui retourne le résultat de la réservation.

### E. CancelBookingById
Fonction qui annule une réservation. Elle nécessite de connaître le numéro d'identification de la réservation.

| Nom      | Type | Description |
| ----------- | ----------- | ----------- |
| id | int | Numéro d'identification de la réservation |
