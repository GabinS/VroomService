# Vroom Service

## I. Définition et connexion
### A. Définition
Vroom Service est un service web permettant de louer des véhicules.

### B. Accès au web service
Pour accéder au web service, il faut se relier au réseau de l'Institut d'Informatique Appliquée (IIA) et se connecter au port **52066**.  

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

   
## II. Compte utilisateur  

### A. Authentification
Fonction utilisée lors de la connexion de l'utilisateur.  

| Nom      | Type | Description |
| ----------- | ----------- | ----------- |
| username | string | Pseudonyme de l'utilisateur |
| password | string | Mot de passe de l'utilisateur |


### B. AuthentificationResponse
Fonction qui retourne le résultat de l'authentification sous la forme d'une chaîne de caractère.

### C. Registration 
Fonction qui enregistre l'utilisateur. 
| Nom      | Type | Description |
| ----------- | ----------- | ----------- |
| username | string | Pseudonyme de l'utilisateur |
| password | string | Mot de passe de l'utilisateur |

### D. RegistrationResponse
Fonction qui retourne le résultat de l'enregistrement sous la forme d'une chaîne de caractère. 

### E. EditAccount
Fonction qui modifie le compte d'un utilisateur.
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


  