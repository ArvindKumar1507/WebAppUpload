
$(document).ready(function () {
    var validEmail;
    $('#viewFiles').prop('disabled', true);
    $('#fileId').keydown(function () {
        if ($(this).val().length > 4) {
            $('#viewFiles').prop('disabled', false);
        }
        else {
            $('#viewFiles').prop('disabled', true);
        }
    });
    $('#files').change(function () {
        var newFile = $("#newfileID").val();
        if ($(this).val() && newFile.length > 4) {
            $('#uploadFiles').attr('disabled', false);
        }
    });
    $('#newfileID').keydown(function () {
        if ($(this).val().length > 4 && $("#files").val()) {
            $('#uploadFiles').attr('disabled', false);
        }
        else {
            $('#uploadFiles').prop('disabled', true);
        }
    });
    $('#btn-signup').click(function () {
        var regUsername = $("#regUsername").val();
        var regPassword = $("#regPassword").val();
        var regEmail = $("#regEmail").val();
        var regContact = $("#regContact").val();
        if (regUsername != "" && regPassword != "" && regEmail != "" && regContact != "" && validEmail == true) {
            $("#register-alert").hide();
            alert("Regster");
        }
        else {
            $("#register-alert").show();
        }
    });
    $('#regEmail').blur(function () {
        var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
        if (testEmail.test(this.value)) {
            validEmail = true;
            $("#email-alert").hide();
        }
        else {
            validEmail = false;
            $("#email-alert").show();
        }
    });
});

function validateLogin() {
    let loginUsername = $("#login-username").val();
    let loginPassword = $("#login-password").val();
    if (loginUsername == "") {
        $("#login-alert").show();
        $("#login-username").addClass("errorClass");
        event.preventDefault();
    } else if (loginPassword == "") {
        $("#login-alert").show();
        $("#login-username").removeClass("errorClass");
        $("#login-password").addClass("errorClass");
        event.preventDefault();
    }
    else {
        $("#login-username").removeClass("errorClass");
        $("#login-password").removeClass("errorClass");
        $("#login-success").show();
        window.location = "dashboard.html";
    }
    // if(loginUsername == "" || loginPassword == "" || !loginPassword || !loginUsername){
    //     $("#login-alert").show();
    //     event.preventDefault();
    // }
    // else{
    //     if(loginUsername == "abc" || loginPassword == "abc"){
    //         $("#login-success").show();
    //         window.location = "dashboard.html";
    //         event.preventDefault();
    //     }
    //      else{
    //         $("#login-alert").show();
    //         event.preventDefault();
    //     }
    // }


}
