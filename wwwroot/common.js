var mainUrl = "/api/gargoyles";

var eTag = null; // will store E-Tag header

function getFunc() {
    var requestUrl = mainUrl;

    if (document.getElementById('name').value) {
        requestUrl = mainUrl + "/" + document.getElementById('name').value;

        $.ajax(requestUrl,
            {
                method: "GET",
                success: function (res, status, response) {
                    eTag = response.getResponseHeader("ETag");
                    eTagResponse(eTag);
                },
                error: simpleError
            });
    }
    else
    {
        eTagResponse("");
    }
    $.ajax(requestUrl,
        {
            method: "GET",
            success: simpleResponse,
            error: simpleError
        });
}

function eTagResponse(data) {
    document.getElementById('eTag').innerHTML = "ETag: " + data;
}

function simpleResponse(data) {
    document.getElementById('result').innerHTML = JSON.stringify(data);
}

function simpleError(response, status, error) {
    document.getElementById('result').innerHTML = response.status + ": " + error + " " + response.responseText;
}

function postFunc() {
    var dataToSend = {};

    dataToSend.name = document.getElementById("name").value;
    dataToSend.color = document.getElementById("color").value;
    dataToSend.size = document.getElementById("size").value;
    dataToSend.gender = document.getElementById("gender").value;

    $.ajax(mainUrl + "/" + name,
        {
            method: "POST",
            success: simpleResponse,
            error: simpleError,
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(dataToSend)
        }
    );
}

function putFunc() {
    var dataToSend = {};

    var name = document.getElementById("name").value;
    dataToSend.name = name;

    var color = document.getElementById("color").value;
    if (color) {
        dataToSend.color = color;
    }

    var size = document.getElementById("size").value;
    if (size) {
        dataToSend.size = size;
    }

    var gender = document.getElementById("gender").value;
    if (gender) {
        dataToSend.gender = gender;
    }

    $.ajax(mainUrl + "/" + name,
        {
            method: "PUT",
            success: simpleResponse,
            error: simpleError,
            headers: { "If-Match": eTag },
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(dataToSend)
        }
    );
}

function patchFunc() {
    var dataToSend = {};

    var name = document.getElementById("name").value;
    if (name) {
        dataToSend.name = name;
    }

    var color = document.getElementById("color").value;
    if (color) {
        dataToSend.color = color;
    }

    var size = document.getElementById("size").value;
    if (size) {
        dataToSend.size = size;
    }

    var gender = document.getElementById("gender").value;
    if (gender) {
        dataToSend.gender = gender;
    }

    $.ajax(mainUrl + "/" + name,
        {
            method: "PATCH",
            success: simpleResponse,
            error: simpleError,
            headers: {
                "If-Match": eTag
            },
            contentType: 'application/json',
            processData: false,
            data: JSON.stringify(dataToSend)
        }
    );
}

window.onload = function () {
    document.getElementById("getButton").onclick = getFunc;
    document.getElementById("postButton").onclick = postFunc;
    document.getElementById("putButton").onclick = putFunc;
    document.getElementById("patchButton").onclick = patchFunc;
}