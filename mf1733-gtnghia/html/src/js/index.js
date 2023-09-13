const btnContextMenu = Array.from(
  document.querySelectorAll(".btn-context-menu")
);
const btnEditEmployee = Array.from(
  document.querySelectorAll(".btn-edit-employee")
);

const main = document.querySelector(".main");
const sidebar = document.querySelector(".sidebar");
const menuBar = document.querySelector(".menu-bar");
const popup = document.querySelector(".popup");
const contentTable = document.querySelector('.content__table')
const contentContainer = document.querySelector('.content-container')

const btnMenuOption = document.querySelector(".btn-menu-option");
const btnModalHelp = document.querySelector(".btn-modal-help");
const btnModalClose = document.querySelector(".btn-modal-close");
const modalInfoEmployee = document.querySelector(".modal-info-employee");
const modalInputId = document.querySelector("input[name='id']");
const btnAddEmployee = document.querySelector(".btn-add-employee");
const btnPopupClose = document.querySelector(".btn-popup-close");
// const 

btnMenuOption.addEventListener("click", function () {
  if (sidebar.classList.toggle("expand")) {
    // when remove expand
    menuBar.classList.remove("scrollbar-hidden");
    menuBar.classList.add("scrollbar");
    main.classList.remove("expand");
  } else {
    menuBar.classList.remove("scrollbar");
    menuBar.classList.add("scrollbar-hidden");
    main.classList.add("expand");
  }
});

btnContextMenu.forEach((btn) => {
  btn.addEventListener("click", function () {
    const contextMenuHTML = `
    <div class="context-menu">
      <div class="context-menu__row">
        <div class="btn-context__item">
          <div class="context-menu__icon icon-container">
            <i class="icon icon-duplicate"></i>
          </div>
          <p class="row-label body2">Nhân bản</p>
        </div>
      </div>
      <div class="context-menu__row">
        <div class="btn-context__item">
          <div class="context-menu__icon icon-container">
            <i class="icon icon-duplicate"></i>
          </div>
          <p class="row-label body2">Xóa</p>
        </div>
      </div>
      <div class="context-menu__row">
        <div class="btn-context__item">
          <div class="context-menu__icon icon-container">
            <i class="icon icon-duplicate"></i>
          </div>
          <p class="row-label body2">Ngừng sử dụng</p>
        </div>
      </div>
    </div>
    `;
    let contextMenuElement = document.createElement('div')
    contextMenuElement.innerHTML = contextMenuHTML
    const rect = btn.getBoundingClientRect();
    const rectContentContainer = contentTable.getBoundingClientRect();
    // Tọa độ x, y của phần tử so với góc trên cùng bên trái của màn hình
    
    contextMenuElement.firstElementChild.style.top = 
    `${rect.top - rectContentContainer.top + btn.clientHeight}px`
    console.log(rect.top + ' '+rectContentContainer.top + ' ' + btn.clientHeight);
    contextMenuElement.firstElementChild.classList.add('show')
    const right = contextMenuElement.firstElementChild.getBoundingClientRect().left
    console.log(rectContentContainer.right + ' ' + right);
    contextMenuElement.firstElementChild.addEventListener('scroll', (e) => {
      console.log(rectContentContainer.right - right);
      e.style.right = rectContentContainer.right - right
    })
    contentTable.append(contextMenuElement)
  });
});

btnEditEmployee.forEach((btn) => {
  btn.addEventListener("click", function () {
    modalInfoEmployee.classList.add("show");
    modalInputId.focus();
  });
});

btnModalClose.addEventListener("click", function () {
  modalInfoEmployee.classList.remove("show");
});

btnAddEmployee.addEventListener("click", function () {
  popup.classList.add("show");
});

btnPopupClose.addEventListener("click", function () {
  popup.classList.remove("show");
});

// TEST
