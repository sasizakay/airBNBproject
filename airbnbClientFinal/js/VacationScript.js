var apiVacations = "https://proj.ruppin.ac.il/cgroup58/test2/tar1/api/Vacations";

$(document).ready(function () {
    $("#vForm").submit(vacationFormSubmit)
    getVacationIDandUserID();
    $("#vacationEndDate").on("change", datesValidation)
})

//Gets called when the user sbumits the vacation form.
function vacationFormSubmit() {
    addVacation();
    return false;
}

//This function validates that the dates input by the user are according to the rules:
//  1) The vacation's end date input must be greater than the start date input.
//  2) A vacation cannot exceed 20 days.
function datesValidation() {
    startDate = Date.parse($("#vacationStartDate").val());
    endDate = Date.parse($("#vacationEndDate").val());
    let msDifference = endDate - startDate;                 //The Date values are in mili-seconds
    let daysDifference = msDifference / (1000*3600*24);    //This line converts the difference between the selected dates from mili-seconds to days
    if (endDate < startDate) {
        this.validity.valid = false;
        this.setCustomValidity('Please chose a valid date!');
    } else if (daysDifference > 20) {
        this.validity.valid = false;
        this.setCustomValidity('Vacation cannot exceed 20 days!');
    }
    else {
        this.validity.valid = true;
        this.setCustomValidity('');
    }
}

//Gets the flat id from the page's URL in order to book a vacation in that specific flat.
function getVacationIDandUserID() {
    let url = new URL(window.location.href);
    let searchParams = new URLSearchParams(url.search);
    let flatId = searchParams.get('flatId');
    document.getElementById("vacationFlatID").value = flatId;
    let userid = sessionStorage["user"];
    document.getElementById("vacationUserID").value = userid;
}

//Creates a new vacation and sends it to the server.
function addVacation() {
    newVacation = {
        Id: $("#vacationID").val(),
        UserId: $("#vacationUserID").val(),
        FlatId: $("#vacationFlatID").val(),
        StartDate: $("#vacationStartDate").val(),
        EndDate: $("#vacationEndDate").val()
    }
    ajaxCall("POST", apiVacations, JSON.stringify(newVacation), vacationsPostSCB, vacationsPostECB);
}

//A success callback function to the POST ajax call.
function vacationsPostSCB(isSuccess) {
    if (isSuccess) {
        alert("Vacation added successfully");
    } else {
        alert("Adding failed");
    }
}
//An error callback function to the POST ajax call.
function vacationsPostECB(err) {
    alert(err);
}

function renderVacations() {
    ajaxCall("GET", apiVacations, "", vacationsGetSCB, vacationsGetECB);
}

//A success callback function to the GET ajax call.
function vacationsGetSCB(vacations) {
    let userid = document.getElementById("vacationUserID").value;
    let str = "";
    for (let i = 0; i < vacations.length; i++) {
        if (vacations[i].userId == userid) {
            str +=
                `<div class="vacation">
                    <div class="card-body">
                        <h5 class="card-title"><span class="subtitle">Vacation ID:</span>${vacations[i].id}</h5>
                        <p class="card-text"><span class="subtitle">User ID:</span> ${vacations[i].userId}</p>
                        <p class="card-text"><span class="subtitle">Flat ID:</span> ${vacations[i].flatId}</p>
                        <p class="card-text"><span class="subtitle">Start Date:</br></span> ${vacations[i].startDate.split('T')[0]}</p>
                        <p class="card-text"><span class="subtitle">End Date:</br></span>${vacations[i].endDate.split('T')[0]}</p>
                    </div>
                 </div>`
        }
    }
    document.getElementById("showVacations").innerHTML = str;
}

//An error callback function to the GET ajax call.
function vacationsGetECB(err) {
    alert(err.statusText);
}
