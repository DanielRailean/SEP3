"use strict";
var connection;
var isAdmin;
var timeout;

async function start(){
    connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();
    await connection.start();
    clearTimeout(timeout);
    GetUpdates();
}

async function ConnectUser(userId,username){
    connection.invoke("ConnectUserHub",userId, username).catch(function (err) {
        return console.error(err.toString());
    });
    clearTimeout(timeout);
    GetUpdates();
    console.log("connected as "+userId+username);
}
async function ConnectAdmin(userId,username){
    connection.invoke("ConnectAdminHub",userId, username).catch(function (err) {
        return console.error(err.toString());
    });
    clearTimeout(timeout);
    GetUpdates();
    console.log("connected as "+userId+username);
}

async function sendMessage(userId,isAdmin,message,name){
    connection.invoke("SendMessage", userId,isAdmin,message,name).catch(function (err) {
        return console.error(err.toString());
    });
    clearTimeout(timeout);
    GetUpdates();
    console.log("sent message");
}
async function GetUpdates(){
    isAdmin = await getLocally("isAdmin")==="true";
    console.log(isAdmin+"update");
    // do whatever you like here
    connection.invoke("GetUpdates",isAdmin).catch(function (err) {
        return console.error(err.toString());
    });
    timeout = setTimeout(GetUpdates, 1000);
}

async function HelpNextUser(){
    connection.invoke("Match").catch(function (err) {
        return console.error(err.toString());
    });
    clearTimeout(timeout);
    GetUpdates();
    console.log("pressod on help");
}

async function saveLocally(key,value){
    localStorage.setItem(key, value);
}
async function getLocally(key){
    return localStorage.getItem(key);
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
    connection.on("SetConnection", function (message) {
        saveLocally("connectionC",message);
        console.log("connection");
    });
    connection.on("SetChatRoom", function (message) {
        saveLocally("chatRoom",message);
        console.log("room");
    });
    
    
    

}