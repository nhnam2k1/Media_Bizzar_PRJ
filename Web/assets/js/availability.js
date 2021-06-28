const daysOfWeek = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
const sessions = ["Morning", "Afternoon", "Evening"];

function ShowAvailabilityTable(){
    let availabilityTableBody = $("#availability-table-body");
    availabilityTableBody.html('');

    sessions.forEach(function(session){
        let dataRow = document.createElement('tr');

        daysOfWeek.forEach(function(dayInWeek){
            let dataCell = document.createElement('td');
            let checkboxCell = CreatingCheckBox(dayInWeek, session);
            dataCell.append(checkboxCell);
            dataRow.append(dataCell);
        });
        availabilityTableBody.append(dataRow);
    });

    InitializeAvailabilityTable();
}

function CreatingCheckBox(dayOfWeek, session){
    dayOfWeek = dayOfWeek.toString();
    session = session.toString();

    let container = document.createElement('div');
    container.className = "form-check";

    let checkbox = document.createElement('input');
    checkbox.className = "form-check-input";
    checkbox.type = "checkbox";
    checkbox.id = GetIndexInAvailability(dayOfWeek, session);
    
    let label = document.createElement('label');
    label.className = "form-check-label";
    label.htmlFor = checkbox.id;

    label.append(session);
    container.append(checkbox);
    container.append(label);

    return container;
}

function SubmitEditAvailabilityForm() 
{
    let request = GetTheAvailabilityTable();
    $.ajax({
        method: 'POST',
        url: 'php/availability.php',
        data: {
            "Sun" : request["Sun"],
            "Mon" : request["Mon"],
            "Tue" : request["Tue"],
            "Wed" : request["Wed"],
            "Thu" : request["Thu"],
            "Fri" : request["Fri"],
            "Sat" : request["Sat"]
        },
        success: function(response) {
            let myObj = JSON.parse(response);
            console.log(myObj);

            if (myObj.error != "") {
                alert(myObj.error);
                return false;
            }

            alert("Successful submit your availability");
            return true;
        }, 
        error: function(jqXHR, textStatus, errorThrown) {
            alert("Error = " + jqXHR.status + ", status = " + textStatus + ", " +
                "error thrown: " + errorThrown);
        }
    });
}

function GetTheAvailabilityTable()
{
    let result = {};
    daysOfWeek.forEach(function(day, posDay){
        let encode = 0;
        sessions.forEach(function(session, posSession) {
            let id = GetIndexInAvailability(day, session);
            let val = document.getElementById(id).checked;
            let bitmask = (1 << posSession);
            if (val == true)
            {
                encode = encode | bitmask;
            }
        });
        result[day] = encode;
    });
    return result;
}

function InitializeAvailabilityTable(){
    $.ajax({
        method: 'GET',
        url: 'php/availability.php',
        success: function(response) {
            let myObj = JSON.parse(response);
            console.log(myObj);

            if (myObj.error != "") {
                alert(myObj.error);
                return false;
            }

            let data = myObj.data[0];

            daysOfWeek.forEach(function(day, posDay){
                let value = data[day];
                
                sessions.forEach(function(session, posSession) {
                    let bitmask = (1 << posSession);
                    let cond = value & bitmask;
                    if (cond != 0) {
                        let id = GetIndexInAvailability(day, session);
                        let chk = document.getElementById(id);
                        chk.checked = true;
                    }
                });
            });
            return true;
        }, 
        error: function(jqXHR, textStatus, errorThrown) {
            alert("Error = " + jqXHR.status + ", status = " + textStatus + ", " +
                "error thrown: " + errorThrown);
        }
    });
}

function GetIndexInAvailability(dayOfWeek, session)
{
    return dayOfWeek + '-' + session;
}