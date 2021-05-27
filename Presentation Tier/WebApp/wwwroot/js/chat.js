"use strict";
var connection;

function start(){
    connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();
    document.getElementById("sendButton").disabled = true;
}
//Disable send button until connection is established



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

    await connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
    }).catch(function (err) {
        return console.error(err.toString());

    });
    connection.on("Notify", function (message) {
        document.getElementById("notification").textContent=message;
        // We can assign user-supplied strings to an element's textContent because it
        // is not interpreted as markup. If you're assigning in any other way, you 
        // should be aware of possible script injection concerns.
        console.log("notify");
    });
    document.getElementById("sendButton").addEventListener("click", function (event) {
        console.log("clicked");
        var user = parseInt(document.getElementById("userInput").value,10);
        var message = document.getElementById("messageInput").value;
        connection.invoke("SendMessage", user, message).catch(function (err) {
            return console.error(err.toString());
        });

        event.preventDefault();
    });
    document.getElementById("connectAsAdmin").addEventListener("click", function (event) {
        console.log("clicked admin");
        connection.invoke("getConnection", 2, "Admin").catch(function (err) {
            return console.error(err.toString());
        });
        connection.invoke("NotifyServer","Admin connected").catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });
    document.getElementById("connectAsUser").addEventListener("click", function (event) {
        console.log("clicked user");
        connection.invoke("getConnection", 1, "USER").catch(function (err) {
            return console.error(err.toString());
        });
        connection.invoke("NotifyServer","User connected").catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });
}