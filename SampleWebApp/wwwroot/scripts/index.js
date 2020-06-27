function AJAXSubmit(oFormElement) {
    $("#spinnerContainer").show();
    var response = fetch('Home/UploadFiles', {
        method: 'POST',
        body: new FormData(oFormElement)
    })    
    .then(data => {
        $("#spinnerContainer").hide();
        alert(data);
    })
    .catch((error) => {
        $("#spinnerContainer").hide();
        alert("Some error in the server. Kindly contact admin.");
    });
}
function getFile() {
    var fileId = $('#getFileId').val();
    window.open(window.location.origin + "/Home/GetFile?fileId=" + fileId); 
}


function signIn(loginUsername, loginPassword)
{
    var userData = { UserName: loginUsername, Email: loginUsername, Password: loginPassword };
    var signInSuccess = function successCallback(response) {
        $("#login-success").show();
    };
    var signInError = function errorCallback(err) {
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
    if (regUsername != "" && regPassword != "" && regEmail != "" && regContact != "" && validEmail == true) {
        $("#register-alert").hide();
        var userData =
        {
            UserName: regUsername,
            Email: regEmail,
            Password: regPassword            
        };
        var signInSuccess = function successCallback(response) {
            $("#login-success").show();
        };
        var signInError = function errorCallback(err) {
        }

        ajaxRequest("LogOn/SingUp", signInSuccess, signInError, userData);        
    }
    else {
        $("#register-alert").show();
    }

   
}