// import các file sử dụng trong form chi tiết
import * as cboHover from "../combobox/hover.js";
import * as cboActive from "../combobox/active.js";
import * as cboError from "../combobox/error.js";
import * as cboShowHideDropdownlist from "../combobox/show-hide-dropdownlist.js";
import * as cboItemSelected from "../combobox/item-selected.js";
import * as textfieldError from "../textfield/error.js";
import * as rdoSelectedItem from "../radio-button/selected-item.js";

/**
 * Render view form details vào index.html khi lần đầu click nút sửa
 * Author: NVDung (16/08/2023)
 */
async function renderPopup() {
  /* ===== Render view ===== */
  try {
    // Tạo request để lấy nội dung render từ view
    const response = await fetch("../../views/form-detail.html");
    // Lấy nội dung của response
    const data = await response.text();
    // Khai báo đối tượng DOM Parser để phân tích cú pháp HTML
    const mDocument = new DOMParser().parseFromString(data, "text/html");
    // Lấy nội dung trong thẻ <body> của data
    const bodyContent = mDocument.body.innerHTML;

    // Gắn nội dung popup vào index.html
    document.querySelector("#render-form-chi-tiet").innerHTML = bodyContent;
  } catch (e) {
    console.log(e);
  }
  // Chạy các funtion thực hiện sau khi render
  try {
    funcsAfterRender();
  } catch (e) {
    console.log(e);
  }
}

/**
 * Đóng popup form chi tiết
 * Author: NVDung (16/08/2023)
 */
function closePopup() {
  /* ========= Đóng bằng nút close trong form chi tiết ========= */
  try {
    // Lấy nút close trong form chi tiết để đóng
    let btnClose = document.getElementById("btn-popup-close");
    // Thêm sự kiện click vào button close trong popup để đóng
    btnClose.addEventListener("click", function () {
      removeClassPopupShow();
    });
  } catch (e) {
    console.log(e);
  }

  /* ========= Đóng bằng phím Escape ========= */
  try {
    window.addEventListener("keydown", function (event) {
      if (event.key === "Escape") {
        removeClassPopupShow();
      }
    });
  } catch (e) {
    console.log(e);
  }
}

/**
 * Xóa class "popup-show" để đóng popup form chi tiết
 * Author: NVDung (16/08/2023)
 */
function removeClassPopupShow() {
  try {
    document
      .querySelector("#render-form-chi-tiet")
      .classList.remove("popup-show");
  } catch (e) {
    console.log(e);
  }
}

// open popup khi click vào nút sửa
/**
 * Mở popup form chi tiết
 * Author: NVDung (16/08/2023)
 */
export function openPopup() {
  /* ===== Thêm sự kiện click cho các nút sửa ở table ===== */
  try {
    let buttons = document.querySelectorAll(".m-btn-open-popup");
    for (let i = 0; i < buttons.length; i++) {
      buttons[i].addEventListener("click", function () {
        /* ===== render popup ===== */
        renderPopup();
        /* ===== show popup ===== */
        document
          .querySelector("#render-form-chi-tiet")
          .classList.add("popup-show");
      });
    }
  } catch (e) {
    console.log(e);
  }
}
/**
 * Các funtion thực hiện sau khi render popup form chi tiết để đảm bảo DOM đã hoạt động
 * Author: NVDung (16/08/2023)
 */
function funcsAfterRender() {
  try {
  // Cài đặt nút đóng
  closePopup();

  // js\combobox
  cboHover.cboHover("donvi");
  cboActive.cboActive("donvi");
  cboError.cboError("donvi");
  cboShowHideDropdownlist.cboShowHideDropdownlist("donvi");
  cboItemSelected.itemSelected("donvi");
  // js\textfield

  textfieldError.textfieldError("m-tf-ma");
  textfieldError.textfieldError("m-tf-ten");
  // js\radio-button
  rdoSelectedItem.selectedItem("m-rdo-gioitinh");
  }
  catch (e) {
    console.log(e);
  }
}
