/**
 * Xử lý sự kiện click cho items trong dropdownlist để hiện lên input trong combobox: item-selected
 * Author: NVDung (16/08/2023)
 */
export function itemSelected(cboId) {
  try {
    // Lấy tất cả các item trong dropdownlist
    let items = document.querySelectorAll("#cbo_ddl_" + cboId + " a");

    // Thêm sự kiện click cho tất cả các item
    for (let i = 0; i < items.length; i++) {
      items[i].addEventListener("click", function () {
        // Lấy cbo-container
        let cboContainer = document.getElementById("m-cbo-" + cboId);
        // Lấy thẻ input để thay đổi value khi chọn
        let input = cboContainer.querySelector("#m-cbo-" + cboId + " input");
        // Lấy tất cả các item trong dropdownlist
        let itemsNew = cboContainer.querySelectorAll(
          "#cbo_ddl_" + cboId + " a"
        );

        // Xóa hết selected ở tất cả các items
        for (let j = 0; j < itemsNew.length; j++) {
          if (itemsNew[j].classList.contains("selected")) {
            itemsNew[j].classList.remove("selected");
            // Xóa div icon-tick
            itemsNew[j].lastElementChild.remove();
            break;
          }
        }

        // Thêm class selected cho item được click
        this.classList.add("selected");
        // Thêm icon-tick cho item được click
        const div = document.createElement("div");
        div.className = "mi mi-24 mi-tick";
        this.appendChild(div);

        // Chuyển đổi value input
        input.value = this.textContent;
      });
    }
  } catch (e) {
    console.log(e);
  }
}
