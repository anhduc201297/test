/**
 * Hiện thông báo lỗi khi thẻ input trong combobox không có giá trị
 * Author: NVDung (16/08/2023)
 */
export function cboError(cboId) {
  try {
    // Lấy cbo-container
    var cboContainer = document.getElementById("m-cbo-" + cboId);

    /* ===== active khi input:focus ===== */
    // Lấy input
    let input = document.querySelector("#m-cbo-" + cboId + " input");

    input.addEventListener("blur", function () {
      // Kiểm tra input value rỗng
      if (this.value.trim() === "")
        cboContainer.classList.add("m-combobox-error");
      else cboContainer.classList.remove("m-combobox-error");
    });
  } catch (e) {
    console.log(e);
  }
}
