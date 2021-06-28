let months = ['January', 'February', 'March', 'April', 'May', 'June',
              'July', 'August', 'September', 'October', 'November', 'December'];

let currentMonthView;
let currentYearView;

function WelcomeCalendarView() {
    let currentDate = new Date();
    currentMonthView = currentDate.getMonth();
    currentYearView = currentDate.getFullYear();
    showCalendar(currentMonthView, currentYearView);
    ShowShiftsToCalendar();
}

function showCalendar(month, year) {
    let startOfMonth = new Date(year, month).getDay();
    let numOfDays = 32 - new Date(year, month, 32).getDate();
    let renderDay = 1;
    let today = new Date();

    $('#schedule-heading').text(months[month] + ', ' + year);
    let scheduleTable = $('#schedule-body');
    scheduleTable.html('');
    
    for (row = 0; row < 6; row++)
    {
        let rowDate = document.createElement('tr');
        let isThisRowNeedToBeAppend = false;

        for (day = 0; day < 7; day++)
        {
            let cellDate = document.createElement('td');
            cellDate.className = "schedule-date-cell";

            if (row == 0 && day < startOfMonth) {
                // Empty cell, may be brown cells
                cellDate.classList.add('out-of-range-month');
            }
            else if (renderDay > numOfDays) {
                // Empty cell, may be brown cells
                cellDate.classList.add('out-of-range-month');
            }
            else {
                isThisRowNeedToBeAppend = true;
                spanDate = document.createElement('span');
                spanDate.append(renderDay);
                if (currentYearView == today.getFullYear() 
                && currentMonthView == today.getMonth() 
                && renderDay == today.getDate()){
                    spanDate.className = "circle-today";
                }
                cellDate.append(spanDate);
                cellDate.id = CreateIDBasedOnDate(renderDay);
                renderDay++;
            }
            rowDate.appendChild(cellDate);
        }
        if (isThisRowNeedToBeAppend)
        {
            scheduleTable.append(rowDate);
        }
    }
}

function CreateIDBasedOnDate(renderDay){
    let id = renderDay.toString();
    if (id.length < 2){
        id = "0" + id;
    }
    return id;
}

function GoToNextMonth() {
    if (currentMonthView == 11) currentYearView++;
    currentMonthView = (currentMonthView + 1) % 12;
    showCalendar(currentMonthView, currentYearView);
    ShowShiftsToCalendar();
}

function BackToPreviousMonth() {
    if (currentMonthView == 0) currentYearView--;
    currentMonthView = (currentMonthView - 1 + 12) % 12;
    showCalendar(currentMonthView, currentYearView);
    ShowShiftsToCalendar();
}

function ShowShiftsToCalendar(){
    $.ajax({
        method : 'POST',
        url    : 'php/schedule.php',
        data   : {
            'Month' : currentMonthView + 1,
            'Year'  : currentYearView
        },
        success: function(response){
            let myObj = JSON.parse(response);
            console.log(myObj);
            if (myObj.error != "") {
                alert(myObj.error);
                return false;
            }
            let listOfShifts = myObj.shifts;
            for (date in listOfShifts)
            {
                let shift = listOfShifts[date];
                let listShiftGroup = CreateListShiftGroup(shift);
                let parts = date.split('-');
                let key = "#" + parts[2];
                $(key).append(listShiftGroup);
            }
            return true;
        },
        error: function(jqXHR, textStatus, errorThrown){
            alert("Error = " + jqXHR.status + ", status = " + textStatus + ", " + "error thrown: " + errorThrown);
            return false;
        }
    });
}

function CreateListShiftGroup(shift){
    let listShiftGroup = document.createElement('ul');
    listShiftGroup.className = "list-group schedule-session-group";

    for (session in shift) {
        let employees = shift[session];
        let listShiftGroupItem = CreateSessionListGroupItem(session, employees);
        listShiftGroup.appendChild(listShiftGroupItem);
    }
    return listShiftGroup;
}

function CreateSessionListGroupItem(session, employees){
    let sessionListGroupItem = document.createElement('li');
    let spanSession          = document.createElement('span');
    let sessionName          = document.createTextNode(session);
    let employeesDropdownMenu = CreateEmployeeesDropdownMenuForShift(employees);
    sessionListGroupItem.className = "list-group-item text-left d-xl-flex justify-content-xl-start schedule-list-item";
    spanSession.className          = "align-left span-session";

    if (session == "MORNING"){
        sessionListGroupItem.classList.add("morning-notification");
    }
    else if (session == "AFTERNOON"){
        sessionListGroupItem.classList.add("afternoon-notification");
    }
    else if (session == "EVENING"){
        sessionListGroupItem.classList.add("evening-notification");
    }
    spanSession.appendChild(sessionName);
    sessionListGroupItem.appendChild(spanSession);
    sessionListGroupItem.appendChild(employeesDropdownMenu);
    return sessionListGroupItem;
}

function CreateEmployeeesDropdownMenuForShift(employees){
    let dropdownDiv  = document.createElement('div');
    let buttonShow   = document.createElement('button');
    let dropdownMenu = document.createElement('div');

    dropdownDiv.className = "dropdown";
    buttonShow.className  = "btn btn-primary dropdown-toggle align-right schedule-btn-detail";
    dropdownMenu.className = "dropdown-menu";

    let attAriaExpanded = document.createAttribute('aria-expanded');
    let attDataToggle   = document.createAttribute('data-toggle');
    let attType         = document.createAttribute('type');
    let buttonName      = document.createTextNode("Show");
    attAriaExpanded.value = "false";
    attDataToggle.value   = "dropdown";
    attType.value         = "button";
    buttonShow.setAttributeNode(attAriaExpanded);
    buttonShow.setAttributeNode(attDataToggle);
    buttonShow.setAttributeNode(attType);
    buttonShow.appendChild(buttonName);

    for (i in employees) {
        let employeeName = document.createTextNode(employees[i]);
        let spanDropdownItem = document.createElement('span');
        spanDropdownItem.className = "dropdown-item-text";
        spanDropdownItem.appendChild(employeeName);
        dropdownMenu.appendChild(spanDropdownItem);
    }
    dropdownDiv.appendChild(buttonShow);
    dropdownDiv.appendChild(dropdownMenu);
    return dropdownDiv;
}