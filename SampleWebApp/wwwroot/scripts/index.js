function AJAXSubmit(oFormElement) {
    var response = fetch('Home/UploadFiles', {
        method: 'POST',
        body: new FormData(oFormElement)
    });
    if (response.ok) {
        console.log("Success");
    }
}