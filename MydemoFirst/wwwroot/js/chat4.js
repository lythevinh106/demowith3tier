
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hub/groupSample", { accessTokenFactory: () => "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJmaXJzdE5hbWUiOiJkYXNkYWQiLCJsYXN0TmFtZSI6ImFkc2FkIiwic2V4IjoiMSIsInVzZXJJZCI6ImM4MDVjNDM1LThiOTItNGRkZC04ZWJhLTNhZDlkNTZjYWUzYiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Imx5dGhldmluaDEwNkBnbWFpbC5jb20iLCJqdGkiOiIyMzZmZTUxYy1mOTk0LTQ0MDYtYTQ3My1iNDFiNDA2MzQ0N2EiLCJleHAiOjE3MDU4NDI4MzksImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjcwOTEiLCJhdWQiOiJVc2VyIn0.5tolSy_Rjs8iZWK45hXHM7Keyp8Fz7EwrFDI9Ebra4ce8gThZWvaT-GsZZfFtLlytu9tZTz1bi45cO0EOcG6NQ" })
    .build();
connection.on("Send", function (message) {
    var li = document.createElement("li");
    li.textContent = message;

    document.getElementById("messagesList").appendChild(li);
});
document.getElementById("groupmsg").addEventListener("click", async (event) => {

    var groupName = document.getElementById("group-name").value;

    var groupMsg = document.getElementById("group-message-text").value;

    try {
        await connection.invoke("SendMessageToGroup", groupName, groupMsg);
    }
    catch (e) {
        console.error(e.toString());
    }
    event.preventDefault();
});
document.getElementById("join-group").addEventListener("click", async (event) => {
    var groupName = document.getElementById("group-name").value;
    try {
        await connection.invoke("AddToGroup", groupName);
    }
    catch (e) {
        console.error(e.toString());
    }
    event.preventDefault();
});
document.getElementById("leave-group").addEventListener("click", async (event) => {
    var groupName = document.getElementById("group-name").value;
    try {
        await connection.invoke("RemoveFromGroup", groupName);
    }
    catch (e) {
        console.error(e.toString());
    }
    event.preventDefault();
});
// We need an async function in order to use await, but we want this code to run immediately,
// so we use an "immediately-executed async function"
(async () => {
    try {
        await connection.start();
    }
    catch (e) {
        console.error(e.toString());
    }
})();
