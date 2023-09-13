window.onload = function () {
  //Tạo sự kiện button add()
  document.getElementById("btnAdd").addEventListener("click", btnAddOnClick);
  document
    .getElementById("showListTable")
    .addEventListener("click", showIconTable);
  //   document
  //     .getElementById("showListTable")
  //     .addEventListener("click", hideIconTable);
  var closeButtons = document.querySelectorAll(".dialog__close");
  for (const close of closeButtons) {
    close.addEventListener("click", btnCloseClickForm);
  }
  var buttonWarning = document.querySelector(".wrap__warning");
  if (buttonWarning) {
    document
      .getElementById("icon__help")
      .addEventListener("click", btnWarningsClick);
  }

  var modal = document.getElementById("showValue_edit");
  document.onlick = function (event) {
    if (event.target.id !== "showValue_edit") {
      modal.style.display = "none";
    }
  };
};

function btnAddOnClick() {
  document.getElementById("range").style.visibility = "visible";
  document.querySelector(".content__input input").focus();
}

function btnCloseClickForm() {
  document.getElementById("range").style.visibility = "hidden";
}

function btnWarningsClick() {
  document.getElementById("wrap__warning").style.visibility = "visible";
}

function btnCloseClickDialog() {
  document.getElementById("wrap__warning").style.visibility = "hidden";
}

function showIconTable() {
  console.log("run");
  document.getElementById("showValue_edit").style.visibility = "visible";
}

//ẩn showicontable
function hideIconTable() {
  console.log("run");
  document.getElementById("showValue_edit").style.visibility = "hidden";
}
