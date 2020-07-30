$(document).ready(function () {
    $('#fetchAsync').on('click', function () {
        $.ajax({
            url: '/api/UsersListApi',
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                $('#usersTable tbody tr').detach();
                $.each(data, function (index, item) {
                    $('#usersTable tbody').append('<tr><td>' + item.UserData.FirstName + '</td><td>' + item.UserData.LastName + '</td><td>' + item.UserData.Age + '</td><td>' + item.FetchMethod + '</td></tr>');
                });
            }
        });
    });
});