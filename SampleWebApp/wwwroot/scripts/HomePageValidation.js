var validEmail;

$(document).ready(function () {
    $('#viewFiles').prop('disabled', true);
    $("#spinnerContainer").hide();
});

$(document).on("keyup", "#getFileId", function () {
    var fileIdLen = $(this).val().length;
    if (fileIdLen > 3) {
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

    signIn(loginUsername, loginPassword);

}