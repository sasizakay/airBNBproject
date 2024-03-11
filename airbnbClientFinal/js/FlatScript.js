var apiFlats = "https://proj.ruppin.ac.il/cgroup58/test2/tar1/api/Flats";

$(document).ready(function () {
    $("#flatForm").submit(flatFormSubmit);
    $("#loginForm").submit(loginFormSubmit);
    $("#Cities").on("blur", validateCityInput)
    ajaxCall("GET", apiFlats, "", flatsGetSCB, flatsGetECB);
    hideButtons();
    let loginPassword = document.getElementById("password");
    passwordHide(loginPassword);
})

function passwordHide(password) {
    if (password) {
        password.addEventListener("mouseover", function () {
            this.type = "text";
        });
        password.addEventListener("mouseout", function () {
            this.type = "password";
        });
    }
}

//Gets called when the user sbumits the flat form.
function flatFormSubmit() {
    addFlat();
    ajaxCall("GET", apiFlats, "", flatsGetSCB, flatsGetECB);
    return false;
}

function hideButtons() {
    orderButtons = document.getElementsByClassName("orderBTN");
    if (sessionStorage.length) { //user is connected
        document.getElementById("signup").style.display = "none";
        document.getElementById("login").style.display = "none";
        document.getElementById("update").style.display = "block";
        document.getElementById("logout").style.display = "block";
        
    } else {
        document.getElementById("signup").style.display = "block";
        document.getElementById("login").style.display = "block";
        document.getElementById("update").style.display = "none";
        document.getElementById("logout").style.display = "none";
    }
}

function clearSessionStorage() {
    sessionStorage.clear();
    hideButtons();
}

function loginFormSubmit() {
    let email = $("#email").val();
    let password = $("#password").val();
    let apiUsersEmail = "https://proj.ruppin.ac.il/cgroup58/test2/tar1/api/Users/email/" + email;
    ajaxCall("POST", apiUsersEmail, JSON.stringify(password), loginPostSCB, loginPostECB);
    return false;
}
//Creates a new flat and sends it to the server.
function addFlat() {
    newFlat = {
        Id : $("#flatID").val(),
        City : $("#Cities").val(),
        Address : $("#flatAdress").val(),
        NumberOfRooms : $("#flatNumOfRooms").val(),
        Price : $("#flatPrice").val()
    }
    ajaxCall("POST", apiFlats, JSON.stringify(newFlat), flatsPostSCB, flatsPostECB);
}

//A success callback function to the POST ajax call.
function flatsPostSCB(isSuccess) {
    if (isSuccess) {
        alert("Flat added successfully");
    } else {
        alert("Adding failed");
    }
}

//An error callback function to the POST ajax call.
function flatsPostECB(err) {
    alert(err.statusText);
}

//Called when the user is adding the city field.
//Validates if the city matches matches the values from the cities list.
function validateCityInput() {
    let selectedCity = this.value;
    let cityOptions = $("#flatCities").children();
    let isValid = false;
    for (let i = 0; i < cityOptions.length; i++) {
        if (cityOptions[i].value == selectedCity) {
            isValid = true;
            break;
        }
    }
    if (isValid) {
        this.validity.valid = true;
        this.setCustomValidity('');
    } else {
        this.validity.valid = false;
        this.setCustomValidity('Please chose a city from the list!');
    }
}

//Gets a list of flats from the server adn rendering all the flats from the list to the page.
function rednerFlats(flatsList) {
    let str = "";
    for (let i = 0; i < flatsList.length; i++) {
        str += `<div class="flat">
            <div class="card-body">
                <h5 class="card-title"><span class="subtitle">Flat ID:</span> ${flatsList[i].id}</h5>
                <p class="card-text"><span class="subtitle">City:</span> ${flatsList[i].city}</p>
                <p class="card-text"><span class="subtitle address">Address:</span> ${flatsList[i].address}</p>
                <p class="card-text"><span class="subtitle">Price:</span> ${parseFloat(flatsList[i].price.toFixed(2))}</p>
                <p class="card-text"><span class="subtitle">Rooms:</span> ${flatsList[i].numberOfRooms}</p>
                <a onclick="goToVacationsPage('${flatsList[i].id}')" class="btn btn-primary orderBTN">Order</a>
            </div>
        </div>`
    }
    document.getElementById("showFlats").innerHTML = str;

}
function goToVacationsPage(flatID) {
    
    if (sessionStorage.length) {
        window.location.href = `Vacations.html?flatId=${flatID}`
    } else {
        alert("You must log in to order a vacation!");
    }
}
//A success callback function to the GET ajax call.
function flatsGetSCB(data) {
    rednerFlats(data);
}

//An error callback function to the GET ajax call.
function flatsGetECB(err) {
    console.log(err.statusText);
}

//A success callback function to the Login POST ajax call.
function loginPostSCB(user) {
    if (user) {
        if (user.isActive) {
            userConnected = $("#email").val();
            sessionStorage.setItem('user', userConnected);
            if (user.isAdmin) {
                window.location.href = "admin.html";
            } else {
                window.location.href = "Flats.html";
            }
        } else {
            alert("this user is not active");
        }
    } else {
        alert("Log in failed");
    }
}

//An error callback function to the Login POST ajax call.
function loginPostECB(err) {
    alert("user not found");
}

