$(document).ready(function () {
    if (localStorage.getItem('userName'))
        $('#user-name').html(localStorage.getItem('userName'));
});

function ajaxRequest(url, successCallback, errorCallback, data, type, contentType = "application/json; charset=utf-8") {
    $.ajax({
        type: type,
        url: url,
        contentType: contentType,
        data: JSON.stringify(data),
        async: true,
        dataType: "json",
        success: successCallback,
        errror: errorCallback
    });
}

function UploadFiles() {
    $("#spinnerContainer").show();
    var data = new FormData();
    if (!$('#files')[0].files || !$('#files')[0].files.length)
    {
        alert('Please choose a file to upload');
        $("#spinnerContainer").hide();
        return;
    }
    $.each($('#files')[0].files, function (i, file) {
        data.append('file-' + i, file);
        data.append('passwd', $('#newfileID').val());
    });
    var success = function (data) {
        removeFileAttached();
        $("#spinnerContainer").hide();
        alert(data.message);
    };
    var error = function (error) {
        if (error.status == 401)
        {
            //show message to use as unauthorized
        }
        removeFileAttached();
        $("#spinnerContainer").hide();
        alert("Some error in the server. Kindly contact admin.");
    };
    $.ajax({
        url: '/Home/UploadFiles',
        data: data,
        contentType: false,
        cache: false,
        type: 'POST',
        method: 'POST',
        processData: false,
        success: success,
        error: error
    });
}

function getFile() {
    var fileId = $('#getFileId').val();
    window.open(window.location.origin + "/Home/GetFile?fileId=" + fileId);
}

function getFiles() {
    var success = function (response) {
        $("#listFiles").show();
        var trHTML = '';
        $.each(response, function (i, item) {
            trHTML += '<tr><td>' + item.fileId + '</td><td>' + item.fileName + '</td><td>' + new Date(item.createdTime) + '</td></tr>';
        });
        $('#listFiles').append(trHTML);
        $('#getFiles').css("cursor", "not-allowed").prop("disabled", "true");
    };
    var error = function () {
        $("#listFiles").hide();
    }
    ajaxRequest("/Home/GetFiles", success, error, null, 'GET');
}

function removeFileAttached() {
    var $el = $('#files');
    $el.wrap('<form>').closest('form').get(0).reset();
    $el.unwrap();
}

function signIn(loginUsername, loginPassword) {
    var userData = { UserName: loginUsername, Email: loginUsername, Password: loginPassword };
    var signInSuccess = function successCallback(response) {
        if (response && response.statusResponse) {
            $("#spinnerContainer").hide();
            $("#login-alert").hide();
            $("#login-danger-alert").hide();
            $("#login-success-alert").show();
            localStorage.setItem('userName', response.userName);
            location.href = window.location.origin + "/Home/Dashboard";
        }
        else {
            $("#spinnerContainer").hide();
            $("#login-alert").hide();
            $("#login-danger-alert").show();
        }
    };
    var signInError = function errorCallback(err) {
        $("#spinnerContainer").hide();
        $("#login-alert").hide();
        $("#login-danger-alert").show();
        $("#login-success-alert").hide();
    }
    ajaxRequest("/LogOn/SingIn", signInSuccess, signInError, userData, 'POST');
    $("#spinnerContainer").show();
}


function signUp() {
    var regUsername = $("#regUsername").val();
    var regPassword = $("#regPassword").val();
    var regEmail = $("#regEmail").val();
    var regContact = $("#regContact").val();
    var rePass = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])\w{6,}$/;
    var isPasswordValid = rePass.test(regPassword);
    var reEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
    var isEmailValid = reEmail.test(regEmail);
    var reContact = /^\d{10}$/;
    var isContactValid = reContact.test(regContact);
    if (regUsername == "") {
        $("#register-alert").show();
    }
    else if (!isEmailValid) {
        $("#register-alert").hide();
        $("#email-alert").show();
    }
    else if (!isPasswordValid) {
        $("#register-alert").hide();
        $("#email-alert").hide();
        $("#password-alert").show();
    }
    else if (!isContactValid) {
        $("#register-alert").hide();
        $("#email-alert").hide();
        $("#password-alert").hide();
        $("#mobile-alert").show();
    }
    else if (regUsername != "" && regPassword != "" && regEmail != "" && regContact != "") {
        $("#password-alert").hide();
        $("#register-alert").hide();
        $("#email-alert").hide();
        $("#mobile-alert").hide();
        var userData =
        {
            UserName: regUsername,
            Email: regEmail,
            Password: regPassword,
            PhoneNumber: Number(regContact)
        };
        var signInSuccess = function successCallback(response) {
            $("#spinnerContainer").hide();
            if (response.status == true) {
                $("#register-danger-alert").hide();
                $("#register-success-alert").show();
            }
            else {
                $("#spinnerContainer").hide();
                $("#register-danger-alert").show();
            }

        };
        var signInError = function errorCallback(err) {
            $("#spinnerContainer").hide();
        }

        ajaxRequest("/LogOn/SingUp", signInSuccess, signInError, userData, 'POST');
        $("#spinnerContainer").show();
    }



}

function logOut() {
    ajaxRequest('/LogOn/SignOut', function (res) {
        if (res && res.status) {
            localStorage.clear();
            location.href = window.location.origin;
        }
    }, function (err) { console.log(err) }, null, 'GET');
}


