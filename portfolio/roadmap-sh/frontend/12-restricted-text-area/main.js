document.addEventListener("DOMContentLoaded",()=>{
    const maxCommentSize = 250;
    const commentText = document.querySelector("#comment");
    commentText.maxLength = maxCommentSize;

    const counter = document.querySelector(".word-counter");
    counter.textContent = `${commentText.textLength}/${maxCommentSize}`;

    commentText.addEventListener("input", (event)=>{
        counter.textContent = `${commentText.textLength}/${maxCommentSize}`;
        if(commentText.textLength < maxCommentSize){
            commentText.classList.remove("full");
            counter.classList.remove("full");
        }
        else{
            commentText.classList.add("full");
            counter.classList.add("full");
        }
    })
});