// import combobox chạy trong form chi tiết
import * as cbo_hover from "../combobox/hover.js";
import * as cbo_active from "../combobox/active.js";
import * as cbo_error from "../combobox/error.js";
import * as cbo_show_hide_dropdownlist from "../combobox/show-hide-dropdownlist.js";
import * as cbo_item_selected from "../combobox/item-selected.js";
import * as textfield_error from "../textfield/error.js";
import * as rdo_selected_item from "../radio-button/selected-item.js";

// Render view form details vào index.html khi lần đầu click nút sửa
async function render_popup() {
  /* ===== Render view ===== */

  // Tạo request để lấy nội dung render từ view
  const response = await fetch("../../views/form-detail.html");
  // Lấy nội dung của response
  const data = await response.text();
  // Khai báo đối tượng DOM Parser để phân tích cú pháp HTML
  const m_document = new DOMParser().parseFromString(data, "text/html");
  // Lấy nội dung trong thẻ <body> của data
  const body_content = m_document.body.innerHTML;

  // Gắn nội dung popup vào index.html
  document.querySelector("#render-form-chi-tiet").innerHTML = body_content;

  // Chạy các funtion thực hiện sau khi render
  funcs_after_render();
}

// Sự kiện đóng popup
function closePopup() {
  /* ========= Đóng bằng nút close trong form chi tiết ========= */

  // Lấy nút close trong form chi tiết để đóng
  let btn_close = document.getElementById("btn-popup-close");
  // Thêm sự kiện click vào button close trong popup để đóng
  btn_close.addEventListener("click", function () {
    remove_class_popup_show();
  });

  /* ========= Đóng bằng phím Escape ========= */
  window.addEventListener("keydown", function (event) {
    if (event.key === "Escape") {
      remove_class_popup_show();
    }
  });
}
// Xóa class "popup-show" để đóng popup
function remove_class_popup_show() {
  document
    .querySelector("#render-form-chi-tiet")
    .classList.remove("popup-show");
}

// open popup khi click vào nút sửa
export function openPopup() {
  /* ===== Thêm sự kiện click cho các nút sửa ở table ===== */
  let buttons = document.querySelectorAll(".m-btn-open-popup");
  for (let i = 0; i < buttons.length; i++) {
    buttons[i].addEventListener("click", function () {
      /* ===== render popup ===== */
      render_popup();
      /* ===== show popup ===== */
      document
        .querySelector("#render-form-chi-tiet")
        .classList.add("popup-show");
    });
  }
}

function funcs_after_render() {
  // Cài đặt nút đóng
  closePopup();

  // js\combobox
  cbo_hover.cbo_hover("donvi");
  cbo_active.cbo_active("donvi");
  cbo_error.cbo_error("donvi");
  cbo_show_hide_dropdownlist.cbo_show_hide_dropdownlist("donvi");
  cbo_item_selected.item_selected("donvi");
  // js\textfield
  
  textfield_error.textfield_error("m-tf-ma");
  textfield_error.textfield_error("m-tf-ten");
  // js\radio-button
  rdo_selected_item.selected_item("m-rdo-gioitinh");
}
