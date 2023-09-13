/**
 * function này thực hiện các chức năng liên quan đến checkbox trong table:
    - Khi checkbox đầu tiên checked thì tất cả các checkboxes con đều checked và ngược lại
    - Khi một checkbox con bất kì unchecked thì checkbox đầu tiên unchecked
    - Khi checkbox con thay đổi trạng thái thì class row-checked được thêm/xóa để phù hợp hiển thị
 * Author: NVDung (16/08/2023)
 */
export const rowChecked = () => {
  try {
    // checkbox đầu tiên
    let selectAllCheckbox = document.querySelector("#selectAllCheckbox");
    // Lấy tất cả thẻ input[type="checkbox"] con
    let checkboxes = document.querySelectorAll(
      "table tbody tr td input[type=checkbox]"
    );

    // Khi checkbox đầu tiên checked thì tất cả các checkboxes con đều checked và ngược lại
    selectAllCheckbox.addEventListener("change", function () {
      for (var i = 0; i < checkboxes.length; i++) {
        checkboxes[i].checked = this.checked;
        // Gọi sự kiện change cho checkboxes con để cập nhật class
        checkboxes[i].dispatchEvent(new Event("change"));
      }
    });

    for (var i = 0; i < checkboxes.length; i++) {
      // Thêm sự kiện change cho các checkboxes
      checkboxes[i].addEventListener("change", function () {
        if (this.checked) {
          // closest() để tìm tổ tiên gần nhất phù hợp với selector, nếu không có trả về null
          this.closest("tr").classList.add("row-checked");
        } else {
          // unchecked
          this.closest("tr").classList.remove("row-checked");

          // Khi một checkbox con bất kì unchecked thì checkbox đầu tiên unchecked
          selectAllCheckbox.checked = false;
        }
      });
    }
  } catch (e) {
    console.log(e);
  }
};
