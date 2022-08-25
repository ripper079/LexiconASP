//Verify that fileis read
console.log("External java script file for AJAX calls code working [Daniel]");



//People button - This should use ajax to fetch the Partialview for all people
document.getElementById("buttonpeople").addEventListener("click", loadPeople);
function loadPeople() {
    console.log("Button |buttonpeople| clicked. Function |loadPeople| invoked");

    //Create a XHR object
    var xhr = new XMLHttpRequest();
    //Path to file
    //const URL = "./res/FooText.txt";
    const URL = "/Ajax/Get";
    //Open the type
    xhr.open('GET', URL, true);

    xhr.onload = function () { //Callback function
        console.log("Callback function HIT!!!!");
        if (this.status == 200) {
            console.log("Status is" + this.status + " Daniel");
            console.log("Message is:" + this.responseText);
        }
        console.log(this);
    }
    //Send the resource [Respond the the resource and HTTP status]
    xhr.send();
        

}


//Detail button
document.getElementById("buttondetails").addEventListener("click", loadDetails);
function loadDetails() {
    console.log("Button |buttondetails| clicked. Function |loadDetails| invoked");
}


//Delete button
document.getElementById("buttondelete").addEventListener("click", loadDelete);
function loadDelete() {
    console.log("Button |buttondelete| clicked. Function |loadDelete| invoked");
}