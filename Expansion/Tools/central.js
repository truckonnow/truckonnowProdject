function Init() {
    if (location.href.includes("https://www.centraldispatch.com/protected/cargo/dispatched-to-me")) {
        let elm = document.getElementById("dispatch-table").getElementsByTagName("tbody")[0].getElementsByTagName("tr");
        for (let i = 0; i < elm.length; i++) {
            var button = document.createElement('button');
            button.onclick = GetOreder;
            button.style.marginTop = "50px";
            button.style.background = "orange";
            button.style.border = "none";
            button.style.color = "white";


            var br = document.createElement('br');
            button.textContent = "Export Order";
            let chil = elm[i].children[2];
            chil.appendChild(br);
            chil.appendChild(button);
        }
    }
    else {

        AddButon();
    }
}

function AddButon() {

    let elm = document.getElementsByClassName("pull-right col-xs-12 col-sm-5 col-md-4 text-right")[0];
    var button = document.createElement('button');
    button.onclick = GetOreder1;
    button.style.marginTop = "50px";
    button.style.background = "orange";
    button.style.border = "none";
    button.style.color = "white";


    var br = document.createElement('br');
    button.textContent = "Export Order";
    elm.appendChild(br);
    elm.appendChild(button);
}

function GetOreder(event) {
    let body = "linck=" + event.path[2].children[7].children[1].href;
    let xhr = new XMLHttpRequest();
    xhr.open('POST', 'https://truckonnow.com/New', true);
    xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
    xhr.setRequestHeader('Access-Control-Allow-Origin', '*');
    xhr.setRequestHeader('Access-Control-Allow-Credentials', true);
    xhr.send(body);
    xhr.onreadystatechange = function () {
        if (xhr.readyState !== 4) {
            return;
        }
        if (xhr.status === 200) {

            alert("Successfully");
        }
        else {
            alert("Unsuccessfully");
        }
    }
}

function GetOreder1(event) {
    let link = location.href.replace("https://www.centraldispatch.com", "");
    let body = "linck=" + "('" + link + "')";
    let xhr = new XMLHttpRequest();
    xhr.open('POST', 'https://truckonnow.com/New', true);
    xhr.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
    xhr.setRequestHeader('Access-Control-Allow-Origin', '*');
    xhr.setRequestHeader('Access-Control-Allow-Credentials', true);
    xhr.send(body);
    xhr.onreadystatechange = function () {
        if (xhr.readyState !== 4) {
            return;
        }
        if (xhr.status === 200) {

            alert("Successfully");
        }
        else {
            alert("Unsuccessfully");
        }
    }
}