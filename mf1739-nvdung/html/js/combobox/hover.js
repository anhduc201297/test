/**
 * Xử lý sự kiện hover combox: Combobox hover khi input hover hoặc a.icon hover
 * Author: NVDung (16/08/2023)
 */
export function cboHover(cboId) {
  try {
    // Lấy cbo-container
    var cboContainer = document.getElementById("m-cbo-" + cboId);

    // Lấy input
    let input = cboContainer.querySelector("#m-cbo-" + cboId + " input");

    // Lấy button a.icon
    let button = document.getElementById("cbo_btn_" + cboId);

    // input hover
    input.addEventListener("mouseover", function () {
      cboContainer.classList.add("m-combobox-hover");
    });
    input.onmouseleave = function () {
      cboContainer.classList.remove("m-combobox-hover");
    };
    // button hover
    button.onmouseover = function () {
      cboContainer.classList.add("m-combobox-hover");
    };
    button.onmouseleave = function () {
      cboContainer.classList.remove("m-combobox-hover");
    };
  } catch (e) {
    console.log(e);
  }
}

// 
/**
 * function hover một phần tử trong dropdown, khi dropdown trong combobox mở ra
 * Author: NVDung (16/08/2023)
 */
export function cboDropdownItemHover() {}
