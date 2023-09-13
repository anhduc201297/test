/**
 * Thông báo lỗi cho các textfield bắt buộc nhập
 * Author: NVDung (16/08/2023)
 */
export function textfieldError(id) {
  try {
    let element = document.getElementById(id);
    let input = element.querySelector("input");

    // Thêm sự kiện blur cho textfield
    input.addEventListener("blur", function () {
      if (input.value.trim() === "")
        element.classList.add("m-textfield-danger");
      else {
        element.classList.remove("m-textfield-danger");
      }
    });
  } catch (e) {
    console.log(e);
  }
}
