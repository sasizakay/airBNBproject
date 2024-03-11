var apiUsers = "https://proj.ruppin.ac.il/cgroup58/test2/tar1/api/Users";
$(document).ready(function () {
    let updatingPassword = document.getElementById("updatePassword");
    passwordHide(updatingPassword);
    let email = sessionStorage["user"];
    $("#update").click(getUserDetailsToUpdate(email));
    $("#updateForm").submit(updateFormSubmit);
});

function passwordHide(passwordInputBox) {
    if (passwordInputBox) {
        passwordInputBox.addEventListener("mouseover", function () {
            this.type = "text";
        });
        passwordInputBox.addEventListener("mouseout", function () {
            this.type = "password";
        });
    }
}

function getUserDetailsToUpdate(email) {
    let address = apiUsers + `/email/${email}`;
    ajaxCall("GET", address,"", userGetSCB, userGetECB);
}

function updateFormSubmit() {
    updateUser();
    return false;
}


function updateUser() {
    let password = $("#updatePassword").val();
    let firstName = $("#updateFirstName").val();
    let familyName = $("#updateFamilyName").val();
    let email = sessionStorage["user"];
    let address = apiUsers + `/firstName/${firstName}/familyName/${familyName}/email/${email}`;
    ajaxCall("PUT", address, JSON.stringify(password), userUpdatePostSCB, userUpdatePostECB);
}

//A success callback function to the POST ajax call.
function userUpdatePostSCB(isSuccess) {
    if (isSuccess) {
        alert("User details updated successfully");
    } else {
        alert("Update failed");
    }
}

//An error callback function to the POST ajax call.
function userUpdatePostECB(err) {
    alert(err.statusText);
}

function userGetSCB(user) {
    console.log("User details returned successfully");
    $("#updateFirstName").val(user.firstName);
    $("#updateFamilyName").val(user.familyName);
    $("#updatePassword").val(user.password);
}

function userGetECB(err) {
    alert(err.statusText);
}
