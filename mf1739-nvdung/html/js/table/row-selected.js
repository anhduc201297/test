// Người dùng click chọn một hàng bất kì => thêm class selected vào hàng đó

/**
 * Thêm sự kiện click cho items để xử lý row click selected
 * Author: NVDung (16/08/2023)
 */
export const rowSelected = () => {
  try {
    // Lấy tất cả các ô trong table
    var cells = document.querySelectorAll("table td");
    for (var i = 0; i < cells.length; i++) {
      // Thêm xử lý sự kiện cho từng ô
      cells[i].addEventListener("click", function () {
        // xóa class row-selected của tất cả các rows
        var rows = document.querySelectorAll("table tr");
        for (var j = 0; j < rows.length; j++) {
          rows[j].classList.remove("row-selected");
        }
        // Thêm class row-selected cho row được click
        this.parentNode.classList.add("row-selected");
      });
    }
  } catch (e) {
    console.log(e);
  }
};
