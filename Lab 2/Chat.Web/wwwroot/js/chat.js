"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();
document.getElementById("sendButton").disabled = true;

connection.on("GetMessage", function(user, message, timestamp) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMessage = fromServer(timestamp) + "\t" + user + ": " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMessage;
    document.getElementById("messageList").appendChild(li);
});

connection.start().then(function() {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function(event) {
    var user = document.getElementById("usernameInput").value;
    var messageInput = document.getElementById("messageInput");
    var message = messageInput.value;

    if (user.trim().length === 0) {
        alert("You must enter your username.");
        event.preventDefault();
        return;
    }

    if (message.trim().length === 0) {
        alert("You must enter a message.");
        event.preventDefault();
        return;
    }

    connection.invoke("BroadcastMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
    messageInput.value = "";
    messageInput.focus();
})