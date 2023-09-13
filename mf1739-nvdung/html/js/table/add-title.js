// Thêm title cho các ô không hiển thị hết nội dung trong table
// Nếu width của ô nhỏ hơn nội dung -> hiện thị ba chấm bằng css

/**
 * Thêm title cho các ô trong table
 * Author: NVDung (16/08/2023)
 */
export function setTitleAllCells() {
  try {
    // Lấy tất cả các ô trong table
    var cells = document.querySelectorAll("table td");
    // Set title cho các ô đó
    for (var i = 0; i < cells.length; i++) {
      // Kiểm tra kích thước ô nhỏ hơn kích thước thanh cuộn nội dung trong ô
      if (cells[i].offsetWidth < cells[i].scrollWidth) {
        cells[i].setAttribute("title", cells[i].textContent);
      }
    }
  } catch (e) {
    console.log(e);
  }
}
