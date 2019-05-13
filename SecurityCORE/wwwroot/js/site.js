$(".custom-file-input").on("change",
    function() {
        var fileName = $(this).val().split("\\").pop();
        $(this).siblings(".custom-file-label").addClass("selected").html(fileName);
    });

async function readFileAsDataURL(file) {
    return new Promise(resolve => {
        const reader = new FileReader();
        reader.onload = () => resolve(reader.result);
        reader.readAsDataURL(file);
    });
}

async function UploadFile() {
    const files = document.getElementById("customFile").files;
    if (!files.length) {
        alert("Please select a file!");
        return;
    }
    const fileData = await readFileAsDataURL(files[0]);
    const fromUserId = $("#from-user").val();


    let jsonPayload = JSON.stringify({
        ToUserId: $("#to-user").find(':selected').data('id'),
        FromUserId: $("#from-user").find(':selected').data('id'),
        Base64Data: fileData,
        FileName: files[0].name
    });

    await fetch('UploadFiles',
        {
            headers: {
                "Content-Type": "application/json"
            },
            method: 'Post',
            body: jsonPayload
        }).then(async function (response) {
            if (response.ok) {
                console.log("Data Send!")
                console.log();
                console.log();
                console.log();
            console.log(fileData);
        }
    });

}

let tester;

async function Download() {
    document.location = 'Download/myUser/' + $('#iam-user').find(':selected').data('id') + '/file/' + $('#retrieve').find(':selected').data('id'); 
}