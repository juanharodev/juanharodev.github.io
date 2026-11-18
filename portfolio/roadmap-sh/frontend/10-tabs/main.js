function activateTab(tabCollection, tab, contentCollection){
    for(let i = 0; i< tabCollection.length; i++){
        if(tabCollection.item(i) === tab){
            tab.classList.remove("unselected"); 
            tab.classList.add("selected"); 
            contentCollection.item(i).classList.remove("unselected"); 
            contentCollection.item(i).classList.add("selected"); 
        }
        else{
            tabCollection.item(i).classList.remove("selected"); 
            tabCollection.item(i).classList.add("unselected"); 
            contentCollection.item(i).classList.remove("selected"); 
            contentCollection.item(i).classList.add("unselected"); 
        }
    }
}

function setTab(tabHeader, tabContent){
    for(let i = 0; i<tabHeader.children.length; i++){
        tabHeader.children.item(i).addEventListener("click",()=>{
            activateTab(tabHeader.children,tabHeader.children.item(i),tabContent.children);
        })
    }
    activateTab(tabHeader.children,tabHeader.children.item(0),tabContent.children);
}

function initialize(){
    
    // Set tabs
    const tabHeaders = document.querySelectorAll(".tab-header");
    const tabContents = document.querySelectorAll(".tab-content");

    for(let i = 0; i < tabHeaders.length; i++){
        setTab(tabHeaders[i],tabContents[i]);
    }
}

document.addEventListener("DOMContentLoaded",initialize);