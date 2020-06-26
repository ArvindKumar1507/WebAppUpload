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