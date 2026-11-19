let accepted = false;

function popUp(){
    const cookie = document.querySelector("#cookie");
    cookie.style.transform = "scale(0)";
    setTimeout(()=>{
        const cookieConsent = document.querySelector("#cookie-consent");
        cookieConsent.style.transform = "scale(1)";
    },500);
}

function popDown(){
    const cookieConsent = document.querySelector("#cookie-consent");
    cookieConsent.style.transform = "scale(0)";
    if(accepted === false){
        setTimeout(() => {
            const btnCookie = document.querySelector("#cookie");
            btnCookie.style.transform = "scale(1)";
        }, 500);
    }
}

function agree(){
    accepted = true;
    popDown();
}

function reject(){
    accepted = true;
    popDown();
}


document.addEventListener("DOMContentLoaded",()=>{

    setTimeout(popUp, 2000);

    let btnClose = document.querySelector("#close-cookie");
    let btnAccept = document.querySelector("#accept-cookie");
    let btnReject = document.querySelector("#reject-cookie");


    btnClose.addEventListener("click",popDown);
    btnAccept.addEventListener("click",agree);
    btnReject.addEventListener("click",reject);

    let x = document.cookie;
    console.log(x);

    let btnCookie = document.querySelector("#cookie")
    btnCookie.addEventListener("click",popUp);
});