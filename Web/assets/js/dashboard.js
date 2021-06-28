$(document).ready(function(){
    MainSchedulePage();
    MainAvailabilityPage();
    MainProfilePage();
});

function MainSchedulePage(){
    let scheduleTable = document.getElementById('schedule-overview');
    
    if (scheduleTable != null)
    {
        WelcomeCalendarView();
        $('#btn-schedule-previous').click(BackToPreviousMonth);
        $('#btn-schedule-next').click(GoToNextMonth);
    }
}

function MainAvailabilityPage(){
    let availabilityTable = document.getElementById('availability-table');

    if (availabilityTable != null)
    {
        ShowAvailabilityTable();
        $('#edit-availability-form').submit(SubmitEditAvailabilityForm);
    }
}

function MainProfilePage() {
    UpdateProfileView();
    $('#btn-edit-profile').click(EditProfile);
}