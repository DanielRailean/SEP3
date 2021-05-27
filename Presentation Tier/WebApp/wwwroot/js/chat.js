"use strict";
var connection;

async function start(){
    connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();
    await connection.start();
}
//Disable send button until connection is established

async function connectToChat(securityLevel,username){
    connection.invoke("getConnection",securityLevel, username).catch(function (err) {
        return console.error(err.toString());
    });
    console.log("connected as "+securityLevel+username);
}

async function sendMessage(securityLevel,message){
    connection.invoke("SendMessage", securityLevel, message).catch(function (err) {
        return console.error(err.toString());
    });
    console.log("sent message");
}

async function initialise(){
    connection.on("ReceiveMessage", function (user, message) {
        var li = document.createElement("li");
        document.getElementById("messagesList").appendChild(li);
        // We can assign user-supplied strings to an element's textContent because it
        // is not interpreted as markup. If you're assigning in any other way, you 
        // should be aware of possible script injection concerns.
        li.textContent = `${user} says ${message}`;
        console.log("receive");
    });
    
    connection.on("Notify", function (message) {
        document.getElementById("notification").textContent=message;
        // We can assign user-supplied strings to an element's textContent because it
        // is not interpreted as markup. If you're assigning in any other way, you 
        // should be aware of possible script injection concerns.
        console.log("notify");
    });
    
    

}