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


function validateLogin() {
    let loginUsername = $("#login-username").val();
    let loginPassword = $("#login-password").val();

    if (loginUsername == "") {
        $("#login-alert").show();
        $("#login-username").addClass("errorClass");
        return;
    } else if (loginPassword == "") {
        $("#login-alert").show();
        $("#login-username").removeClass("errorClass");
        $("#login-password").addClass("errorClass");
        return;
    }
    else {
        $("#login-username").removeClass("errorClass");
        $("#login-password").removeClass("errorClass");           
    }

    var userData = { UserName: loginUsername, Email: loginUsername, Password: loginPassword };

    $.ajax({
        type: 'POST',
        url: "LogOn/SingIn",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(userData),
        async: true,
        dataType: "json",
        success: function successCallback(response) {
            $("#login-success").show();  
        },
        errror: function errorCallback(err) {
        }
    });

}
