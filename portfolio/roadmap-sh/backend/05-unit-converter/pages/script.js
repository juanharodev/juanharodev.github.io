document.addEventListener("DOMContentLoaded", initialize)
// Update to your local api port
const localApiPort = "localhost:5010"
const apiURL = `http://${localApiPort}/api/UnitConverter/`

function initialize(){  

    // Tabs for conversion
    const lengthTab = document.querySelector("#length-tab");
    const weightTab = document.querySelector("#weight-tab");
    const temperatureTab = document.querySelector("#temperature-tab");    
    
    const lengthButton = document.querySelector("#length-button");
    const weightButton = document.querySelector("#weight-button");
    const temperatureButton = document.querySelector("#temperature-button");
    
    lengthButton.addEventListener("click",()=>showTab(1));
    weightButton.addEventListener("click",()=>showTab(2));
    temperatureButton.addEventListener("click",()=>showTab(3));
    showTab(1);    
    
    function showTab(tab){
        lengthTab.style.display = 'none';
        weightTab.style.display = 'none';
        temperatureTab.style.display = 'none';

        lengthButton.classList.remove("active-tab");
        weightButton.classList.remove("active-tab");
        temperatureButton.classList.remove("active-tab");
        
        if(tab === 1){
            lengthTab.style.display = 'block';
            lengthButton.classList.add("active-tab");
        }else if(tab === 2){
            weightTab.style.display = 'block';
            weightButton.classList.add("active-tab");
        }else{
            temperatureTab.style.display = 'block';
            temperatureButton.classList.add("active-tab");
        }
    } 
    
    const lengthInput = document.querySelector("#length-input");
    const weightInput = document.querySelector("#weight-input");
    const temperatureInput = document.querySelector("#temperature-input");
    
    const lengthOutput = document.querySelector("#length-output");
    const weightOutput = document.querySelector("#weight-output");
    const temperatureOutput = document.querySelector("#temperature-output");

    lengthOutput.style.display = "none";
    weightOutput.style.display = "none";
    temperatureOutput.style.display = "none";

    const lengthResult = document.querySelector("#length-result");
    const weightResult = document.querySelector("#weight-result");
    const temperatureResult = document.querySelector("#temperature-result");

    async function getData(formInput, resultOutput, apiEndpoint) {
        const formData = new FormData(formInput);
        const url = apiURL + apiEndpoint + "?" + new URLSearchParams(formData);

        try{   
            const response = await fetch(url);
            const json = await response.json();
            resultOutput.innerHTML = `
            <h4>Resultado de la conversión:</h4>
            <p>${json.originalValue} ${json.from} = ${json.convertedValue} ${json.to}</p>
            `;            
        }catch(e){
            console.error(e);
            resultOutput.innerHTML = `
            <h4>Ocurrió un error: </h4>
            <p>${e.message}</p>
            `;
        }
    }

    function fetchResult(formInput, resultTab, resultOutput, apiEndpoint){
        formInput.addEventListener("submit", (event)=>{
        event.preventDefault();
        formInput.style.display = "none";
        resultTab.style.display = "block"
        getData(formInput,resultOutput, apiEndpoint);
        });
    }

    fetchResult(lengthInput,lengthOutput, lengthResult, "length-converter");
    fetchResult(weightInput,weightOutput, weightResult, "weight-converter");
    fetchResult(temperatureInput,temperatureOutput, temperatureResult, "temperature-converter");

    const lengthReset = document.querySelector("#length-reset");
    const weightReset = document.querySelector("#weight-reset");
    const temperatureReset = document.querySelector("#temperature-reset");

    function showHide(show, hide){
        show.style.display = "block";
        hide.style.display = "none";
    }

    lengthReset.addEventListener("click",()=>{showHide(lengthInput,lengthOutput);});
    weightReset.addEventListener("click", ()=>{ showHide(weightInput,weightOutput);});
    temperatureReset.addEventListener("click",()=>{showHide(temperatureInput,temperatureOutput);});    
}