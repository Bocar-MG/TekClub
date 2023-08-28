// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

 "use strict";
var conn = new signalR.HubConnectionBuilder().withUrl("/ChatHub").build();

//start connection
conn.start().then(function () {

    console.log("start");

}).catch(
    function (err) {
        console.error(err.toString());
    }
);

//send msg to server
document.getElementById("sendmessage").addEventListener("click", function (evt) {
    var user = document.getElementById("displayname").value;

    var msg = document.getElementById("message");
    var messagetemp = msg.value;
    msg.value = "";
    
    conn.invoke("SendMessage", user, messagetemp).catch(function (err) {
        console.error(err.toString());
    });

}
);
//receive msg from server
//write function name
conn.on("ReceiveMessage", function (user, messagetemp) {
    var li = document.createElement("li");
    li.innerHTML = user + ": " + messagetemp;
    document.getElementById("discussion").appendChild(li);
   // console.log(user + ":" + msg);

});