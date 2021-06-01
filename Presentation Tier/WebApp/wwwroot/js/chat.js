"use strict";
var connection = null;

async function EstablishConnection(){
    if(connection==null){
        console.log("connect");
        connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();
        await connection.start();
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
function ConnectToRoomJS(roomId){
    if(connection!=null){
        connection.invoke("ConnectToRoom", roomId).catch(function (err) {
            return console.error(err.toString());
        });
        
    }
}
function CloseChatRoomJS(){
    if(connection!=null){
        connection.invoke("CloseChatRoom").catch(function (err) {
            return console.error(err.toString());
        });
        
    }
}
function ExitRoomJS(){
    if(connection!=null){
        connection.invoke("ExitRoom").catch(function (err) {
            return console.error(err.toString());
        });
        
    }
}

async function SendMessageJS(message){
    if(connection!=null){
        connection.invoke("SendMessage",message).catch(function (err) {
            return console.error(err.toString());
        });
        console.log("sent message");
    }
}

async function ReconnectToChatJS(userId){
    if(connection!=null){
        connection.invoke("ReconnectToChat",userId).catch(function (err) {
            return console.error(err.toString());
        });
        console.log("reconnect");
    }
}

function ClearChatJS(){
    document.getElementById("messagesList").innerHTML="";
}

async function DisconnectJS(userId){
    console.log("null connection");
    // do whatever you like here
    if(connection!=null){
        connection.invoke("Disconnect",userId).catch(function (err) {
            return console.error(err.toString());
        });
        console.log("disconnect");
    }
    return true;
}

async function HelpNextUserJS(){
    if(connection!=null){
        connection.invoke("HelpNextUser").catch(function (err) {
            return console.error(err.toString());
        });
        console.log("pressod on help");
    }
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
    }
}