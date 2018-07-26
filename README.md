# CoffeeShop inventory tracker (C#)

A web app, built with C#, that allows user to store an inventory, create drinks, and remove small amounts of inventory when ordering a drink.

## Created
Epicodus Team Week of C# 07/26/2018
Austin Barr
Hisato Kawaminami
Eliot Charette
Curt Caldwell
Randy Weyer
## Installation

### Database Setup

1. Clone this repository.
2. Within a terminal running MySQL, run the following commands:

 ```
 CREATE DATABASE coffeeshop;
 ```

 ```
 USE coffeeshop;
 ```

 ```
 CREATE TABLE ingredients (id serial PRIMARY KEY, drink_id INT, inventory_id INT, amount INT);
 ```

 ```
 CREATE TABLE drinks (id serial PRIMARY KEY, name VARCHAR(255));
 ```

 ```
 CREATE TABLE inventories (id serial PRIMARY KEY, item VARCHAR(255), item_amount INT);
 ```


3. Optional - to allow for testing, run the following SQL commands as well:

 ```
 CREATE DATABASE coffeeshop_tests;
 ```

 ```
 USE coffeeshop_tests;
 ```

 ```
 CREATE TABLE ingredients (id serial PRIMARY KEY, drink_id INT, inventory_id INT, amount INT);
 ```

 ```
 CREATE TABLE drinks (id serial PRIMARY KEY, name VARCHAR(255));
 ```

 ```
 CREATE TABLE inventories (id serial PRIMARY KEY, item VARCHAR(255), item_amount INT);
 ```



### Web App Setup

1. Follow the steps above to set up your database/s.
2. Install .NET, if not already present on your local machine.

3. In your preferred shell, navigate to the HairSalon folder and run the following commands:

 ```
 $ dotnet restore
 ```

 ```
 $ dotnet run
 ```

4. Navigate to localhost:5000 in your preferred browser.

## Specifications

1. App routes users to the home page, which displays form to enter username and password.

2. When clicking "Add to inventory", user is routed to a form through which the user can add a new inventory item and amount which would be added to database.

3. When clicking "Add a new Drink", user is routed to a form through which the new drink name and will be added to the database.

4. When clicking "Create a drink", user is routed to a form through which the will be able to add ingredients to their drink with the amount.

5. When clicking "View Drink names", user can see what drinks are present and has a link to view which ingredients are in drink.

6. When clicking "View inventory", user can see what inventory items and item amount are present and has a link to restock item or delete.

7. When clicking "New Order", user can see what drinks are available to order.

8. When clicking a specified drink on the order page, amount of ingredients in drink will remove from inventory

### Technologies Used

* C#
* HTML
* MAMP
* MySql
* MVC Architecture

### Support and Contact Details
If you encounter any bugs or would like to make suggestions regarding this project, please feel free to contact austinbarr@protonmail.com, hisatokawaminami@gmail.com.



### License

This project is distributed under the MIT License
