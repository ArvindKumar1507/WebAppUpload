function AJAXSubmit(oFormElement) {
    $("#spinnerContainer").show();
    var response = fetch('Home/UploadFiles', {
        method: 'POST',
        body: new FormData(oFormElement)
    })
    //.then(response => response.json())
    .then(data => {
        $("#spinnerContainer").hide();
        alert("File uploaded successfully!");
    })
    .catch((error) => {
        $("#spinnerContainer").hide();
        alert("File not uploaded successfully!");
    });
}
function getFile() {
    var fileId = $('#getFileId').val();
    window.open(window.location.origin + "/Home/GetFile?fileId=" + fileId); 
}

function validateLogin()
{
    var userData = { UserName: "Arvind", Email: "arvind.kumar@email.com", Password: "Baggie" };
    $.ajax({
        type: 'POST',   
        url: "LogOn/SingIn",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ userDetails: userData }),
        async: true,
        dataType: "json",
        success: function successCallback(response) {

        },
        errror:function errorCallback(err){
        }
    });

}