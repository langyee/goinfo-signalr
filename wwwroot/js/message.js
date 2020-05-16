"use strict";

var connection = new signalR.HubConnectionBuilder()
                    .withUrl("/messages")
                    .build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function(message) {

    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/</g, "&gt;");
    var div = document.createElement("div");
    div.innerHTML = msg + "<hr/>";
    document.getElementById("messages").appendChild(div);
});

connection.on("UserConnected", function(connectionId) {
    var groupElement = document.getElementById("group");
    var option = document.createElement("option");
    option.text = connectionId;
    option.value = connectionId;
    groupElement.add(option);
});

connection.on("userDisconnected", function(connectionId) {
    var groupElement = document.getElementById("group");
    for(var i = 0; i < groupElement.clientHeight; i++) {
        if (groupElement.options[i].value == connectionId) {
            groupElement.remove(i);
        }
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function(event) {
    var message = document.getElementById("message").value;
    var groupElement = document.getElementById("group");
    var groupValue = groupElement.options[groupElement.selectedIndex].value;
    
    var method = "SendMessageToAll";
    if (groupValue === "Myself") {
        method = "SendMessageToCaller";
    }

    connection.invoke(method, message).catch(function (err) {
        return console.error(err.ToString());
    });

    event.preventDefault();
})