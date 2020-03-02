"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();
document.getElementById("sendButton").disabled = true;

connection.on("GetMessage", function(user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMessage = user + ": " + msg;
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
    var message = document.getElementById("messageInput").value;

    connection.invoke("BroadcastMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });

    event.preventDefault();
})