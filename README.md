# Hair Salon Website

#### A website that allows user to keep track of stylists and clients. 9/21/18

#### By **Chris Crow**

## Description

A website that allows user to keep track of stylists and clients. It allows stylist names to be created and deleted as well as client names under each stylist.

### Specs
| # | Spec | Input | Output |
| :-------------     | :-------------     | :------------- | :------------- |
| 1 | The website can display a list of all stylists.  | View website | List of all stylists |
| 2 | The website allows user to select a stylist and see their clients.  | Stylist 1 clicked | Stylist 1's clients |
| 3 | The website allows user to add new stylist | New Stylist Info | List of all stylists including new stylist |
| 4 | The website allows user to add new clients under a specific stylist | New Client | Clients of Selected stylist including new client |


## Setup/Installation Requirements

1. Clone this repository.
2. Create a database using the following SQL commands:
  > CREATE DATABASE to_do;

  > USE to_do;

  > CREATE TABLE categories (id serial PRIMARY KEY, name VARCHAR(255));

  > CREATE TABLE tasks (id serial PRIMARY KEY, description VARCHAR(255));

3. Edit the Startup.cs file to reference your database.

4. Navigate to the HairSalon folder in command shell and use the following commands:
  > dotnet restore

  > dotnet run

## Known Bugs
* No known bugs at this time.

## Technologies Used
* C#
* MAMP
* SQL
* Bootstrap

## Support and Contact Details

_Email chrismcrow@gmail.com._

### License

*none*

Copyright (c) 2018 **_Chris Crow_**
