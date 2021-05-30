"use strict";
var connection = null;

async function EstablishConnection(){
    if(connection==null){
        console.log("connect");
        connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();
        connection.start();
        await InitialiseMethods();
    }
}

async function GoOnlineJS(userId,isAdmin,name){
    if(connection!=null){
        connection.invoke("GoOnline", userId,isAdmin,name).catch(function (err) {
            return console.error(err.toString());
        });
        
    }
}

function AskQuestionJS(question){
    if(connection!=null){
        connection.invoke("AskQuestion", question).catch(function (err) {
            return console.error(err.toString());
        });
        
    }
}

async function ConnectUser(userId,username){
    if(connection!=null){
        connection.invoke("ConnectUserHub",userId, username).catch(function (err) {
            return console.error(err.toString());
        });
        console.log("connected as "+userId+username);
    }
}
async function ConnectAdmin(userId,username){
    if(connection!=null){
        connection.invoke("ConnectAdminHub",userId, username).catch(function (err) {
            return console.error(err.toString());
        });
        console.log("connected as "+userId+username);
    }
}

async function sendMessage(userId,isAdmin,message){
    if(connection!=null){
        connection.invoke("SendMessage", userId,isAdmin,message).catch(function (err) {
            return console.error(err.toString());
        });
        console.log("sent message");
    }
}

async function ReconnectJS(userId,isAdmin,name){
    if(connection!=null){
        connection.invoke("Reconnect", userId,isAdmin).catch(function (err) {
            return console.error(err.toString());
        });
        console.log("reconnect");
    }
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
    }
}

async function HelpNextUser(){
    if(connection!=null){
        connection.invoke("Match").catch(function (err) {
            return console.error(err.toString());
        });
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
async function InitialiseMethods(){
    if(connection!=null){
        connection.on("ReceiveMessage", function (user, message) {
            var li = document.createElement("li");
            li.className="list-group-item";
            document.getElementById("messagesList").appendChild(li);
            li.textContent = `${user} says ${message}`;
            console.log("receive");
        });
        /*connection.on("SetConnection", function (message) {
            saveSession("connectionC",message);
            console.log("connection");
        });
        connection.on("SetChatRoom", function (message) {
            saveLocal("chatRoom",message);
            console.log("room");
        });
        connection.on("ClearLocalKeys", function () {
            saveLocal("startedChat",false);
            /!*document.getElementById("connectButton").innerText=;*!/
            console.log("clear keys");
        });*/
    }
}