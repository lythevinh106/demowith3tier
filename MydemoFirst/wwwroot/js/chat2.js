"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/hub/userHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

//Disable the send button until connection is established.

connection.on("UpdateTotalView", function (TotalView) {

    let CountUi = document.getElementById("countUi");
    CountUi.innerText = TotalView.toString();

});



//doan nay xai  OnDisconnectedAsync nen k can ivoke
connection.on("UpdateTotalUser", function (TotalUser) {

    let CountUi2 = document.getElementById("countUi2");
    CountUi2.innerText = TotalUser.toString();

});


connection.start().then(function () {

    connection.invoke("NewWindowLoaded", "Window da dc load lai").catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});




document.getElementById("btn").addEventListener("click", function (event) {


    connection.invoke("NewWindowLoaded", "dang thuc thi load tu nguoi dung").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});