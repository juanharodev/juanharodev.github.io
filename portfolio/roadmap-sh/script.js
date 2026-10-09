function initialize(){
    const webButton = document.querySelector("#basic-web");
    webButton.addEventListener("click",(e)=>{
        e.preventDefault();
        alert("¡Así fue como empezó, pero aquí estamos!")
    });
    
    const porfolioButton = document.querySelector("#portfolio-link");
    porfolioButton.addEventListener("click",(e)=>{
        e.preventDefault();
        alert("¡Que mejor que un proyecto viviente!")
    });
}

document.addEventListener("DOMContentLoaded",initialize);