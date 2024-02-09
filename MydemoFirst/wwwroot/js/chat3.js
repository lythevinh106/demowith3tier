"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/hub/meatHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();


let duckEl = document.getElementById("duck");
let cowEl = document.getElementById("cow");
let chickenEl = document.getElementById("chicken");
connection.on("UpdateFavoriteMeat", function (duck, cow, chicken) {

    console.log(duck)
    duckEl.innerText = duck.toString();
    cowEl.innerText = cow.toString();
    chickenEl.innerText = chicken.toString();
});

////doan nay xai  OnDisconnectedAsync nen k can ivoke
//connection.on("UpdateTotalUser", function (TotalUser) {
//    let CountUi2 = document.getElementById("countUi2");
//    CountUi2.innerText = TotalUser.toString();
//});

connection.start().then(function () {

    connection.invoke("GetRaceStatus").then((raceCounter) => {


        duckEl.innerText = raceCounter.duck.toString();
        cowEl.innerText = raceCounter.cow.toString();
        chickenEl.innerText = raceCounter.chicken.toString();
    });


    //connection.invoke("NewWindowLoaded", "Window da dc load lai").catch(function (err) {
    //    return console.error(err.toString());
    //});
}).catch(function (err) {
    return console.error(err.toString());
});

