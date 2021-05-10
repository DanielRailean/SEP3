"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/orderHub").build();

connection.on('register', 
    function (Email, Password, FirstName, LastName, PhoneNumber, Address, Postal) {
    
    
});