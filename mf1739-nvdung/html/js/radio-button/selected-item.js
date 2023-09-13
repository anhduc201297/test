/* Selected-item khi click vào một item trong radio-button */
/**
 * Chọn item trong radio-button
 * Author: NVDung (16/08/2023)
 */
export function selectedItem(rdoId) {
  try {
    // Lấy radio-button
    let rdo = document.getElementById(rdoId);

    // Lấy tất cả các item trong radio-button
    let items = rdo.querySelectorAll(".m-rdo-content .item");

    // Thêm sự kiện click cho tất cả các item
    for (let i = 0; i < items.length; i++) {
      items[i].addEventListener("click", function () {
        // Xóa class selected
        let itemsNew = rdo.querySelectorAll(
          "#" + rdoId + " .m-rdo-content .item"
        );
        for (let j = 0; j < itemsNew.length; j++) {
          itemsNew[j].classList.remove("m-rdo-selected");
        }

        // Thêm class selected cho item được click
        this.classList.add("m-rdo-selected");
      });
    }
  } catch (e) {
    console.log(e);
  }
}
