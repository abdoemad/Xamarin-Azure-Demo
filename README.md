# Xamarin-Azure-Demo
Xamarin.Forms project for learning purpose, still under development

## Pages
| Login | Product Catalog | Add to Cart | Cart |
|--|--|--|--|
|![alt text](https://github.com/abdoemad/Xamarin-Azure-Demo/blob/master/screenshots/login.png)|![alt text](https://github.com/abdoemad/Xamarin-Azure-Demo/blob/master/screenshots/product-catalog.png)|![alt text](https://github.com/abdoemad/Xamarin-Azure-Demo/blob/master/screenshots/add-to-cart.png)|![alt text](https://github.com/abdoemad/Xamarin-Azure-Demo/blob/master/screenshots/cart.png)|

## Menu
<img src="https://github.com/abdoemad/Xamarin-Azure-Demo/blob/master/screenshots/menu.jpeg" alt="alt text" width="300" height="200">

### Purchasing cycle Functionalities
| Buss Func | Desc|
|--|--|
| Login online / offline with input validation	| Online to get the user id. |
| List products  (offline sync)	| Get products from the API |
| Display product details to add to cart | |
| List cart items and submit to API| |

### 3rd parties:
-	MVVM Light
-	Automapper
-	Xam Plugin Connectivity
-	FontAwesome

### Projects
- Xamarin projects
- - Shared (.Net Standard 2.0)
- - Xamarin.Android 
- - Xamarin.IOS
- EShope Mobile backend: Web API project(.Net Framework 4.7.2)
- EShope Admins.Net Core(2.2) run Angular(7) with SignalR

### Menu
<img src="https://github.com/abdoemad/Xamarin-Azure-Demo/blob/master/screenshots/menu.jpeg" alt="alt text" width="300" height="200">

### Xamarin.Forms project
<img src="https://github.com/abdoemad/Xamarin-Azure-Demo/blob/master/design%20arch%20images/xamarin-skeleton.png">

### Pages design
<img src="https://github.com/abdoemad/Xamarin-Azure-Demo/blob/master/design%20arch%20images/pages%20design.png" width="600" height="500">

## View Model Layer & Validation design
<img src="https://github.com/abdoemad/Xamarin-Azure-Demo/blob/master/design%20arch%20images/vm_and_validation.png">

### Database design
SQL Server with Entity Framework (Code First with Migrations)

<img src="https://github.com/abdoemad/Xamarin-Azure-Demo/blob/master/design%20arch%20images/db-diagram.PNG" width="400" height="500">

### Mobile backend Endpoints
| Endpoint | Call |
|--|--|
| List Products | http://eshopemobile.azurewebsites.net/tables/product?ZUMO-API-VERSION=2.0.0 |
| List Users | https://eshopemobile.azurewebsites.net/api/users?ZUMO-API-VERSION=2.0.0 |
| User Orders | http://eshopemobile.azurewebsites.net/tables/order?ZUMO-API-VERSION=2.0.0&userId=32eb7603-8179-4774-8f02-133060e0196d |


