"use strict";

class User {
    constructor(id, username, connectedAt) {
        this.id = id;
        this.username = username;
        this.connectedAt = connectedAt;
    }
}

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

connection.on("NotifyNewUserLogin", (users) => {
    for(let i = 0; i < users.length; i++) {
        let user = users[i]; 
        console.log(`${i}: ${user.username} logged in at ${user.connectedAt} for connection ${user.connectionId}`);
    }
});

connection.on("UserConnected", function(connectionId) {
    var groupElement = document.getElementById("group");
    var option = document.createElement("option");
    option.text = connectionId;
    option.value = connectionId;
    groupElement.add(option);
});

connection.on("UserDisconnected", function(connectionId) {
    var groupElement = document.getElementById("group");
    for(var i = 0; i < groupElement.length; i++) {
        if (groupElement.options[i].value == connectionId) {
            groupElement.remove(i);
        }
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    connection.invoke("NotifyRandomUserLogin")
        .catch(err => console.error(err.toString()) );
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function(event) {
    var message = document.getElementById("message").value;
    var groupElement = document.getElementById("group");
    var groupValue = groupElement.options[groupElement.selectedIndex].value;
    
    if (groupValue === "All" || groupValue === "Myself") {
        var method = groupValue === "All" ? "SendMessageToAll" : "SendMessageToCaller";

        connection.invoke(method, message).catch(function (err) {
            return console.error(err.ToString());
        });
    } else if(groupValue === "PrivateGroup") {
        connection.invoke("SendMessageToGroup", "PrivateGroup", message)
            .catch(function(err) {
                return console.error(err.ToString());
            });
    } else {
        connection.invoke("SendMessageToUser", groupValue, message).catch(function(err) {
            return console.error(err.ToString());
        })
    }
    
    event.preventDefault();
});

document.getElementById("joinGroup").addEventListener("click", function(event) {
    connection.invoke("JoinGroup", "PrivateGroup").catch(function(err) {
        return console.error(err.ToString());
    });

    event.preventDefault();
});