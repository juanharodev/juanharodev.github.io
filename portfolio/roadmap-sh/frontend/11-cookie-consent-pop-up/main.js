let showConsent = false;
const COOKIE_CONSENT_NAME = "consent";

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
    if(showConsent === false){
        setTimeout(() => {
            const btnCookie = document.querySelector("#cookie");
            btnCookie.style.transform = "scale(1)";
        }, 500);
    }
}

function agree(){
    setCookie(COOKIE_CONSENT_NAME,"true",1);
    showConsent = true;
    popDown();
}

function reject(){
    setCookie(COOKIE_CONSENT_NAME,"false",1);
    showConsent = true;
    popDown();
}

function setCookie(cookieName,cookieValue, expirationDays){
    const date = new Date();
    date.setTime(date.getTime() + (expirationDays*24*60*60*1000));
    let expires = "expires" + date.toUTCString();
    document.cookie = cookieName + "=" + cookieValue + ";" + expires;
}

function getCookie(cookieName){
    let name = cookieName + "=";
    let decodedCookie = decodeURI(document.cookie);
    let ca = decodedCookie.split(";");
    for(let i = 0; i<ca.length; i++){
        let c = ca[i];
        while(c.charAt(0) == ' '){
            c = c.substring(1);
        }
        if(c.startsWith(name)){
            return c.substring(name.length,);
        }
    }

    return "";
}

document.addEventListener("DOMContentLoaded",()=>{

    showConsent = getCookie(COOKIE_CONSENT_NAME) !== "";

    if(showConsent){ return; }

    setTimeout(popUp, 2000);

    let btnClose = document.querySelector("#close-cookie");
    let btnAccept = document.querySelector("#accept-cookie");
    let btnReject = document.querySelector("#reject-cookie");


    btnClose.addEventListener("click",popDown);
    btnAccept.addEventListener("click",agree);
    btnReject.addEventListener("click",reject);

    let btnCookie = document.querySelector("#cookie")
    btnCookie.addEventListener("click",popUp);

});