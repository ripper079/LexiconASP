/*
//Verify that js file is read
console.log("External java script file for AJAX calls code working [Daniel]");

//Code for verifying that jQuery is working 
$(document).ready(function () {
    if (jQuery) {
        alert("jQuery is loaded and working correctly!");
    } else {
        alert("jQuery Does NOT Work");
    }
});

*/


//Will execute after the page has been loaded
//JQuery
$(document).ready(function () {
    $('#buttonpeople').on('click', function () {                    //When menu button People clicked
        alert("Hello button people" + " from Ajax");                //Code block to execute
        console.log("Button |buttonpeople| clicked");               //Code block to execute
    });
});


$(document).ready(function () {
    $('#buttondetails').on('click', function () {                   //When menu button Detail clicked
        $.get("/Ajax/MyNameIsCool", function (data) {
            alert(data);
            console.log(data);
        });
    });
});



$(document).ready(function () {
    $('#buttondelete').on('click', function () {                   //When menu button Delete clicked

        $.ajax({
            type: "GET",
            url: "/Ajax/GetAllPersons",
            success: function (response) {
                alert("jQuery, Within Ajax. The response is: |" + response + "|");
            }
        });

    });
});


$(document).ready(function () {
    $('#buttonjson').on('click', function () {                   //When menu button JSon clicked

        $.ajax({
            type: "GET",
            url: "/Ajax/MyJson",
            success: function (response) {
                alert("jQuery, Within Ajax. The response is: |" + response + "|");
                console.log(response);
                console.log("The name is: " + response.fullName);
            }
        });

    });
});













/*

////People button - This should use ajax to fetch the Partialview for all people
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
            console.log("Status is:" + this.status + " [Daniel]");
            console.log("Message is:" + this.responseText);//This returns the whole page/JSON

            $.ajax({
                url: URL,
                type: 'GET',
                contentType: this.responseText
            });

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

*/