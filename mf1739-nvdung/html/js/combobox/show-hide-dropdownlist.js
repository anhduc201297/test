// Khi click vào nút a.icon thì dropdown sẽ hiển/ẩn
/**
 * Ẩn / hiện dropdownlist khi click vào nút a.icon
 * Author: NVDung (16/08/2023)
 */
export function cboShowHideDropdownlist(cboId) {
  try {
    // Lấy cbo-container để khoanh vùng truy vấn
    var cboContainer = document.getElementById("m-cbo-" + cboId);

    // Lấy button a.icon
    let button = cboContainer.querySelector("#cbo_btn_" + cboId);

    // Lấy thẻ div.icon để chuyển đổi class
    let icon = button.querySelector(".mi.mi-arrow-dropdown");

    // Lấy dropdownlist
    let ddl = cboContainer.querySelector("#cbo_ddl_" + cboId);

    // Thêm sự kiện click
    button.addEventListener("click", function () {
      // Chuyển đổi class .show
      ddl.classList.toggle("show");
      // có class .show
      if (ddl.classList.contains("show")) {
        icon.classList.add("mi-arrow-dropdown--open");
        icon.classList.remove("mi-arrow-dropdown--close");
      } else {
        // không có class .show
        icon.classList.add("mi-arrow-dropdown--close");
        icon.classList.remove("mi-arrow-dropdown--open");
      }
    });
  } catch (e) {
    console.log(e);
  }
}
