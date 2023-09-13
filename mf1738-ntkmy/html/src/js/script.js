const togglePopup = () => {
    var popupContainer = document.getElementById("popupContainer");
    popupContainer.style.display = (popupContainer.style.display === "flex") ? "none" : "flex";
}

window.onclick = function(event) {
    if (event.target == popupContainer) {
        popupContainer.style.display = "none";
    }
}

