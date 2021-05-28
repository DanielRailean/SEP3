"use strict";
var connection;

async function start(){
    connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();
    await connection.start();
}

async function connectToChat(securityLevel,username){
    connection.invoke("GetConnection",securityLevel, username).catch(function (err) {
        return console.error(err.toString());
    });
    GetUpdates();
    console.log("connected as "+securityLevel+username);
    
}

async function sendMessage(securityLevel,message){
    connection.invoke("SendMessage", securityLevel, message).catch(function (err) {
        return console.error(err.toString());
    });
    console.log("sent message");
}
function GetUpdates(){
    // do whatever you like here
    connection.invoke("GetUpdates").catch(function (err) {
        return console.error(err.toString());
    });
    
    setTimeout(GetUpdates, 5000);
}

async function HelpNextUser(){
    connection.invoke("Match").catch(function (err) {
        return console.error(err.toString());
    });
    console.log("pressod on help");
}
async function initialise(){
    connection.on("ReceiveMessage", function (user, message) {
        var li = document.createElement("li");
        document.getElementById("messagesList").appendChild(li);
        li.textContent = `${user} says ${message}`;
        console.log("receive");
    });
    
    connection.on("Notify", function (message) {
        document.getElementById("notification").textContent=message;
        console.log("notify");
    });
    
    

}