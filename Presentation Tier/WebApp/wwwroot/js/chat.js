"use strict";
var connection = null;
var isAdmin;
var timeout;

async function start(){
    if(connection==null){
        console.log("connect");
        connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();
        await connection.start();
        clearTimeout(timeout);
        GetUpdates();
        initialise();
    }
}

async function GoOnlineJS(userId,isAdmin,name,connectionId){
    if(connection!=null){
        connection.invoke("GoOnline", userId,isAdmin,name,connectionId).catch(function (err) {
            return console.error(err.toString());
        });
    }
}

async function ConnectUser(userId,username){
    if(connection!=null){
        connection.invoke("ConnectUserHub",userId, username).catch(function (err) {
            return console.error(err.toString());
        });
        clearTimeout(timeout);
        GetUpdates();
        console.log("connected as "+userId+username);
    }
}
async function ConnectAdmin(userId,username){
    if(connection!=null){
        connection.invoke("ConnectAdminHub",userId, username).catch(function (err) {
            return console.error(err.toString());
        });
        clearTimeout(timeout);
        GetUpdates();
        console.log("connected as "+userId+username);
    }
}

async function sendMessage(userId,isAdmin,message,name){
    if(connection!=null){
        connection.invoke("SendMessage", userId,isAdmin,message,name).catch(function (err) {
            return console.error(err.toString());
        });
        clearTimeout(timeout);
        GetUpdates();
        console.log("sent message");
    }
}

async function GetUpdates(){
    isAdmin = await getSession("isAdmin")==="true";
    console.log(isAdmin+"update");
    // do whatever you like here
    if(connection!=null){
        connection.invoke("GetUpdates",isAdmin).catch(function (err) {
            return console.error(err.toString());
        });
    }
    timeout = setTimeout(GetUpdates, 1000);
}


async function DisconnectJS(userId){
    console.log("null connection");
    // do whatever you like here
    if(connection!=null){
        connection.invoke("Disconnect",userId).catch(function (err) {
            return console.error(err.toString());
        });
        document.getElementById("messagesList").innerHTML="";
        console.log("disconnect");
        clearTimeout(timeout);
    }
    
}
async function HelpNextUser(){
    if(connection!=null){
        connection.invoke("Match").catch(function (err) {
            return console.error(err.toString());
        });
        clearTimeout(timeout);
        GetUpdates();
        console.log("pressod on help");
    }
}

async function saveSession(key,value){
    sessionStorage.setItem(key, value);
}
async function getSession(key){
    return sessionStorage.getItem(key);
}
async function saveLocal(key,value){
    localStorage.setItem(key, value);
}
async function getLocal(key){
    return localStorage.getItem(key);
}
async function initialise(){
    if(connection!=null){
        connection.on("ReceiveMessage", function (user, message) {
            var li = document.createElement("li");
            document.getElementById("messagesList").appendChild(li);
            li.textContent = `${user} says ${message}`;
            console.log("receive");
        });

        connection.on("Notify", function (message) {
            document.getElementById("notification").textContent=message;
            console.log("notify "+message);
        });
        connection.on("SetConnection", function (message) {
            saveSession("connectionC",message);
            console.log("connection");
        });
        connection.on("SetChatRoom", function (message) {
            saveLocal("chatRoom",message);
            console.log("room");
        });
        connection.on("ClearLocalKeys", function () {
            // saveLocal("startedChat",false);
            /*document.getElementById("connectButton").innerText=;*/
            console.log("clear keys");
        });
    }
}