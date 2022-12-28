var count = 0;

function load() {
    alert("Hello World");
    setInterval(tick, 1000);
}

function tick() {
    count++;
}


function send() {
    var username = document.getElementById("UsernameBox").value;
    var message = document.getElementById("MessageBox").value;

    //adding new p tag to div
    var node = document.createElement("p");
    var innerNode = document.createTextNode(username + " : " + message);
    node.appendChild(innerNode);

    document.getElementById("message").appendChild(node);
}

console.log("Hello World");
alert("Hello there");