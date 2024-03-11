var apiUsers = "https://proj.ruppin.ac.il/cgroup58/test2/tar1/api/Users";
var apiVacations = "https://proj.ruppin.ac.il/cgroup58/test2/tar1/api/Vacations";
$(document).ready(function () {
    
})

function getUsersDataTable() {
    ajaxCall("GET", apiUsers, "", usersTableGetSCB, usersTableGetECB);
    document.getElementById("usersTable").style.visibility = "visible";
}

function usersTableGetSCB(usersList) {
    users = usersList; // keep the cars array in a global variable;
    try {
        tbl = $('#usersTable').DataTable({
            data: usersList,
            pageLength: 5,
            columns: [
                { data: "firstName" },
                { data: "familyName" },
                { data: "password" },
                { data: "email" },                
                {
                    data: "isActive",
                    render: function (data, type, row, meta) {
                        if (data == true)
                            return '<input type="checkbox" checked onclick="changeUserStatus(this)"/>';
                        else
                            return '<input type="checkbox" onclick="changeUserStatus(this)"/>';
                    }
                },
            ],
        });
    }

    catch (err) {
        alert(err);
    }
}

function usersTableGetECB(err) {
    alert("Error: " + err);
}

function changeUserStatus(user) {
    userEmail = user.parentElement.parentElement.children[3].innerHTML;
    if (user.checked) {
        newStatus = true;
    } else {
        newStatus = false;
    }
    let address = apiUsers + `/email/${userEmail}/newStatus/${newStatus}`;
    ajaxCall("POST", address,"", usersTablePostSCB, usersTablePostECB);
}
function usersTablePostSCB(answer) {
    alert(answer);
}
function usersTablePostECB(err) {
    alert("Error: " + err);
}

function getReport() {
    let selectedMonth = document.getElementById("month").value;
    let address = apiVacations + `/${selectedMonth}`;
    ajaxCall("GET", address, "", getReportSCB, getReportECB);
}

function getReportSCB(objectList) {
    let str = `<table border="1" id="usersTable" class="display nowrap" style="width:100%" > <thead>
                    <tr>
                        <th>City</th>
                        <th>Average Price</th>
                    </tr>
                    </thead>`;
    for (let i = 0; i < objectList.length; i++) {
        str += `<tr>
                    <td>${objectList[i]["city"]}</td>
                    <td>${objectList[i]["avg"]}</td>
                </tr>`
    }
    str += '</table>';
    document.getElementById("report").innerHTML = str;
}
function getReportECB(err) {
    alert("Error: " + err);
}