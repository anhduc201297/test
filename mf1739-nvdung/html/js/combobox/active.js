/**
 * active và inactive combobox: Combobox active khi input:focus hoặc a.icon:click
 * Author: NVDung (16/08/2023)
 */

export function cboActive(cboId) {
  try {
    // Lấy cbo-container
    var cboContainer = document.getElementById("m-cbo-" + cboId);

    /* ===== active khi input:focus ===== */
    // Lấy input
    let input = document.querySelector("#m-cbo-" + cboId + " input");
    input.addEventListener("focus", function () {
      cboContainer.classList.add("m-combobox-active");
    });
    input.addEventListener("blur", function () {
      cboContainer.classList.remove("m-combobox-active");
    });
    /* ===== active khi a.icon:click ===== */
    let button = document.getElementById("cbo_btn_" + cboId);
    button.addEventListener("click", function () {
      cboContainer.classList.add("m-combobox-active");
    });
  } catch (e) {
    console.log(e);
  }
}
