$(document).ready(function () {
    // Load initial data grid right away
    loadPeople();

    // Save Button Logic
    $('#btnSave').click(function () {
        var personData = {
            FullName: $('#fullName').val(),
            PhoneNumber: $('#phoneNumber').val(),
            EntryType: $('#entryType').val(),
            InvitedBy: $('#invitedBy').val(),
            CurrentStatus: $('#currentStatus').val(),
            SystemStage: $('#systemStage').val(),
            LastContactDate: $('#lastContactDate').val() ? $('#lastContactDate').val() : null,
            Notes: $('#notes').val()
        };

        // Validate required fields including Dropdowns
        if (!personData.FullName || !personData.PhoneNumber || !personData.EntryType || !personData.CurrentStatus || !personData.SystemStage) {
            alert("Please fill in all required fields and select options from the dropdowns.");
            return;
        }

        $.ajax({
            url: '/api/PeopleRegistry/Save',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(personData),
            success: function (response) {
                alert(response.message);
                $('#personForm')[0].reset(); // clear form
                loadPeople(); // Reload the data table dynamically
            },
            error: function (xhr, status, error) {
                alert("Error saving data. Make sure backend is running properly.");
                console.error(error);
            }
        });
    });
});

function loadPeople() {
    $.ajax({
        url: '/api/PeopleRegistry/Get',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var tbody = $('#peopleTable tbody');
            tbody.empty(); // Clear existing rows

            $.each(data, function (index, person) {
                var lastContact = person.lastContactDate ? person.lastContactDate.split('T')[0] : '';
                var row = `<tr>
                    <td>${person.id}</td><td>${person.fullName}</td><td>${person.phoneNumber}</td>
                    <td>${person.entryType}</td><td>${person.invitedBy || ''}</td><td>${person.currentStatus}</td>
                    <td>${person.systemStage}</td><td>${lastContact}</td><td>${person.notes || ''}</td>
                </tr>`;
                tbody.append(row);
            });
        },
        error: function (xhr, status, error) {
            console.error("Error loading data: ", error);
        }
    });
}