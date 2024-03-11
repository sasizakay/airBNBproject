var apiUsers = "https://proj.ruppin.ac.il/cgroup58/test2/tar1/api/Users";
$(document).ready(function () {
  $("#regForm").submit(regFormSubmit);
});



function regFormSubmit() {
  addUser();
  return false;
}

function addUser() {
  newUser = {
    firstName: $("#firstName").val(),
    familyName: $("#familyName").val(),
    email: $("#email").val(),
    password: $("#password").val(),
  };
    ajaxCall("POST", apiUsers, JSON.stringify(newUser), userPostSCB, userPostECB);
}

//A success callback function to the POST ajax call.
function userPostSCB(isSuccess) {
    if (isSuccess) {
        alert("User added successfully");
    } else {
        alert("Adding failed");
    }
}

//An error callback function to the POST ajax call.
function userPostECB(err) {
    alert(err.statusText);
}

