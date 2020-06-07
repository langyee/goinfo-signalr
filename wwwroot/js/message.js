"use strict";

class User {
    constructor(id, username, connectedAt, connectionId) {
        this.id = id;
        this.username = username;
        this.connectedAt = connectedAt;
        this.connectionId = connectionId;
    }
}

class CustomMessage {
    constructor(sender, timestamp, message) {
        this.Sender = sender;
        this.Timestamp = timestamp;
        this.Message = message;
    }
}

let activeUsers = [];

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
    console.warn("====== new user logged in ======");
    activeUsers = users;
    
    $("#activeUsers").empty();
    const activeUserElement = document.getElementById("activeUsers");
    
    for(let i = 0; i < users.length; i++) {
        let user = users[i]; 
        console.log(`${i}: ${user.username} logged in at ${user.connectedAt} for connection ${user.connectionId}`);
        
        const option = document.createElement("option");
        option.text = user.username;
        option.value = user.connectionId;
        activeUserElement.add(option);
        console.log("adding option", user.username);
    }

    console.warn("====== active user list kept in js ======");
    console.log(activeUsers);
});

connection.on("NotifyUserLogout", (users) => {
    console.warn("====== user logged out ======");
    for(let i = 0; i < users.length; i++) {
        let user = users[i]; 
        console.log(`${i}: ${user.username} logged in at ${user.connectedAt} for connection ${user.connectionId}`);
    }
});

connection.on("UserDisconnected", (users) => {
    console.warn("====== user disconnected ======");
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

// connection.on("UserDisconnected", function(connectionId) {
//     var groupElement = document.getElementById("group");
//     for(var i = 0; i < groupElement.length; i++) {
//         if (groupElement.options[i].value == connectionId) {
//             groupElement.remove(i);
//         }
//     }
// });

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
            return console.error(err.toString());
        });
    } else if(groupValue === "PrivateGroup") {
        connection.invoke("SendMessageToGroup", "PrivateGroup", message)
            .catch(function(err) {
                return console.error(err.toString());
            });
    } else {
        connection.invoke("SendMessageToUser", groupValue, message).catch(function(err) {
            return console.error(err.toString());
        })
    }
    
    event.preventDefault();
});

document.getElementById("joinGroup").addEventListener("click", function(event) {
    connection.invoke("JoinGroup", "PrivateGroup").catch(function(err) {
        return console.error(err.toString());
    });

    event.preventDefault();
});

// Demo Custom Message Sending and Receiving

document.getElementById("sendCustomMessageButton").addEventListener("click", event => {

    const activeUserElement = document.getElementById("activeUsers");
    const connectionId = activeUserElement.options[activeUserElement.selectedIndex].value;

    connection
        .invoke("SendCustomMessageToUser", 
            connectionId, 
            "demo sender",
            "demo message")
        .catch(err => console.error(err.toString()));
});

connection.on("ReceiveCustomMessage", message => {
    console.warn("====== Custom Message Received ======");
    console.log(message);
});