function AJAXSubmit(oFormElement) {
    $("#spinnerContainer").show();
    var response = fetch('Home/UploadFiles', {
        method: 'POST',
        body: new FormData(oFormElement)
    })    
    .then(data => {
        if (data.status !== 200) {
            $("#launchModal").trigger("click");
        }
        removeFileAttached();
        $("#spinnerContainer").hide();
    })
    .catch((error) => {
        removeFileAttached();
        $("#spinnerContainer").hide();
        alert("Some error in the server. Kindly contact admin.");
    });
}

function getFile() {
    var fileId = $('#getFileId').val();
    window.open(window.location.origin + "/Home/GetFile?fileId=" + fileId); 
}

function removeFileAttached() {
    var $el = $('#files');
    $el.wrap('<form>').closest('form').get(0).reset();
    $el.unwrap();
}

function signIn(loginUsername, loginPassword)
{
    var userData = { UserName: loginUsername, Email: loginUsername, Password: loginPassword };
    var signInSuccess = function successCallback(response) {
        if (response.status == true) {
            $("#login-alert").hide();
            $("#login-danger-alert").hide();
            $("#login-success-alert").show();
        }
        else {
            $("#login-alert").hide();
            $("#login-danger-alert").show();
        }
        //location.href = window.location.origin + "/Home/Dashboard.cshtml";
    };
    var signInError = function errorCallback(err) {
        $("#login-alert").hide();
        $("#login-danger-alert").show();
        $("#login-success-alert").hide();
    }

    ajaxRequest("LogOn/SingIn", signInSuccess, signInError, userData);
}

function ajaxRequest(url, successCallback, errorCallback, data)
{
    $.ajax({
        type: 'POST',
        url: url,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(data),
        async: true,
        dataType: "json",
        success: successCallback,
        errror: errorCallback
    });
}


function signUp()
{
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
            Password: regPassword            
        };
        var signInSuccess = function successCallback(response) {
            $("#spinnerContainer").hide();
            if (response.status == true) {
                $("#register-danger-alert").hide();
                $("#register-success-alert").show();
            }
            else {
                $("#register-danger-alert").show();
            }
            
        };
        var signInError = function errorCallback(err) {
            $("#spinnerContainer").hide();
        }

        ajaxRequest("LogOn/SingUp", signInSuccess, signInError, userData); 
        $("#spinnerContainer").show();
    }

   
}