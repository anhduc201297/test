<template>
  <div class="header-wrapper">
    <h1 class="title-main">Nhân viên</h1>
    <a href="#">
      <m-button
        content="Thêm mới nhân viên"
        @click="onShowEmployeeDetail"
      ></m-button>
    </a>
  </div>
  <div class="container-content">
    <div class="header-container-content">
      <div class="first-left-content">
        Đã chọn: <span class="text-boil">{{ selectedCount }}</span>

        <a
          href="#"
          v-if="showSelected"
          class="m-left-16 m-red-color m-a-style"
          @click="deleteAll"
          >Bỏ chọn</a
        >
        <a
          href="#"
          v-if="showSelected"
          class="m-left-16 m-blue-color m-a-style"
          @click="selectAll"
          >Chọn tất cả
        </a>
        <a href="#" class="delete-row" v-if="showSelected">
          <p class="icon-delete"></p>
          <p class="" @click="deleteSelected">Xóa</p>
        </a>
      </div>
      <div class="right-left-content">
        <m-search content="Tìm theo mã, tên nhân viên"></m-search>
        <i class="excel-icon"> </i>
        <i class="reload-icon" @click="handleLoading"> </i>
      </div>
    </div>
    <div class="table-content">
      <table class="employee-table">
        <thead>
          <tr>
            <th class="employee-table__header text-center size-checkbox">
              <input
                type="checkbox"
                id="myCheckbox"
                v-model="allSelected"
                @change="selectAlltableDatas"
              />
            </th>
            <th class="employee-table__header">Mã nhân viên</th>
            <th class="employee-table__header">Tên nhân viên</th>
            <th class="employee-table__header">Giới tính</th>
            <th class="employee-table__header text-center">Ngày sinh</th>
            <th class="employee-table__header">
              <label title="Số chứng minh nhân dân">Số CMND</label>
            </th>
            <th class="employee-table__header">Chức danh</th>
            <th class="employee-table__header">Tên đơn vị</th>
            <th class="employee-table__header text-center">Chức năng</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="customer in customers" :key="customer.CustomerId">
            <td class="employee-table__data text-center size-checkbox">
              <input
                type="checkbox"
                id="myCheckbox"
                :value="customer.CustomerId"
                v-model="selectedtableDatas"
                @change="updateSelectedCount"
              />
            </td>
            <td class="employee-table__data">{{ customer.CustomerCode }}</td>
            <td class="employee-table__data">{{ customer.FullName }}</td>
            <td class="employee-table__data">{{ customer.GenderName }}</td>
            <td class="employee-table__data text-center">
              {{ formatDate(customer.CreatedDate) }}
            </td>
            <td class="employee-table__data">{{ customer.PhoneNumber }}</td>
            <td class="employee-table__data">{{ customer.CompanyNames }}</td>
            <td class="employee-table__data">{{ customer.CompanyName }}</td>
            <td class="employee-table__data text-center">
              <div class="edit">
                <a href="#">Sửa</a>
                <a
                  href="#"
                  class="down-icon"
                  @click="toggleCombobox(customer.CustomerId)"
                >
                  <m-contextmenu
                    v-if="customer.showCombobox"
                    :menuItems="customItems2"
                    @onItemClick="
                      onContextMenuItemClick(customer.CustomerId, $event)
                    "
                    @onClose="onCloseContextmenu"
                  ></m-contextmenu>
                </a>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="table-end">
      <div class="table-end-left">
        Tổng số <strong>{{ customers.length }}</strong> bản ghi
      </div>
      <div class="table-end-right">
        <div>Bản ghi / Trang</div>
        <div class="search-bar">
          <input type="text" placeholder="" :value="customItems[0]" />
          <label
            for="search-box"
            class="down-icon-black"
            @click="onShowCombobox"
          ></label>
          <m-combobox
            v-if="showCombobox"
            :items="customItems"
            @onClose="onCloseCombobox"
          ></m-combobox>
        </div>

        <div class="page-container">
          <a href="" class="right-icon-black"></a>
          <a href="" class="left-icon-black"></a>
        </div>
      </div>
    </div>
  </div>
  <m-loading v-if="showLoading"></m-loading>
  <EmployeeDetail
    v-if="showEmployeeDetail"
    @saveSuccess="handleSaveSuccess"
  ></EmployeeDetail>
  <m-notify
    v-if="showNotify"
    :title="titleNotify"
    :type="iconNotify"
    :message="contentNotify"
  ></m-notify>
</template>
<script>
import EmployeeDetail from "./EmployeeDetail.vue";
export default {
  name: "TheEmployeeList",
  props: [""],
  components: {
    EmployeeDetail,
  },

  created() {
    // Đăng ký sự kiện "onClose" và gán hàm xử lý onCloseEmployeeDetail
    this.$emitter.on("onClose", this.onCloseEmployeeDetail);
    // Lấy dữ liệu từ API
    this.$axios
      .get("https://cukcuk.manhnv.net/api/v1/customers")
      .then((response) => {
        this.customers = response.data;
        console.log("Lấy dữ liệu thành công");
      })
      .catch((error) => {
        console.log("Lấy dữ liệu thất bại");
        console.error(error);
      });
  },
  computed: {
    // Trả về số checkbox đã được chọn
    selectedCount() {
      return this.selectedtableDatas.length;
    },
  },

  methods: {
    // Xử lý hiển thị và ẩn loading khi lấy dữ liệu
    handleLoading() {
      this.showLoading = true; // Hiển thị loading
      // Đặt thời gian chờ 1 giây rồi ẩn loading
      setTimeout(() => {
        this.showLoading = false; // Ẩn loading sau 1 giây
      }, 1000);
      this.fetchData(); // Gọi phương thức fetchData để lấy dữ liệu
    },

    // Lấy dữ liệu từ API
    fetchData() {
      // Gọi API để lấy dữ liệu khách hàng
      fetch("https://cukcuk.manhnv.net/api/v1/customers")
        .then((res) => res.json())
        .then((data) => {
          this.customers = data;
          console.log("Lấy dữ liệu thành công");
        })
        .catch((error) => {
          console.log("Lấy dữ liệu thất bại");
          console.error(error);
        });
    },
    // Đóng form và thông báo Cất thành công
    handleSaveSuccess() {
      this.fetchData();
      // Thực hiện đóng form
      this.onCloseEmployeeDetail(); // Gọi phương thức để đóng form
      // Hiển thị thông báo thành công
      this.contentNotify = this.$MISAResource["VN"].FormEdited; // Nội dung thông báo thành công
      this.titleNotify = this.$MISAEnum["MISANotify"].Success.title; // Tiêu đề thông báo thành công
      this.iconNotify = this.$MISAEnum.MISANotify.Success.id; // ID của biểu tượng thông báo thành công
      this.showNotify = true; // Hiển thị thông báo
      setTimeout(() => {
        this.showNotify = false; // Tự động ẩn thông báo sau 3 giây
      }, 3000);
    },
    //
    /**
     * Xử lý sự kiện khi một mục menu ngữ cảnh được nhấp vào.
     * @param {number} CustomerId - ID của khách hàng.
     * @param {number} index - Chỉ số của mục menu được nhấp vào.
     *  Created by Lê Trường Lam ngày 5/9
     */
    onContextMenuItemClick(CustomerId, index) {
      const selectedMenuItem = this.customItems2[index];
      if (selectedMenuItem === "Xóa") {
        this.pickId(CustomerId);
      } else {
        console.log(selectedMenuItem);
      }
    },
    /**
     * Chọn nhân viên cần xóa dựa trên mã ID.
     * @param {string} CustomerId - Mã ID của khách hàng.
     *  Created by Lê Trường Lam ngày 5/9
     */
    pickId(CustomerId) {
      const customer = this.customers.find(
        (customer) => customer.CustomerId === CustomerId
      );
      if (customer) {
        console.log(CustomerId);
        // this.deleteData(CustomerId);
        this.contentNotify = "Đã xóa thành công"; // Nội dung thông báo xóa thành công
        this.titleNotify = this.$MISAEnum["MISANotify"].Success.title; // Tiêu đề thông báo thành công
        this.iconNotify = this.$MISAEnum["MISANotify"].Success.id; // ID của biểu tượng thông báo thành công
        this.showNotify = true; // Hiển thị thông báo
        setTimeout(() => {
          this.showNotify = false;
        }, 3000);
      } else {
        console.log("Không tìm thấy khách hàng với ID:", CustomerId);
      }
    },
    /**
     * Xóa dữ liệu từ Id đã chọn
     * @param {string} CustomerId - Mã ID của khách hàng.
     *  Created by Lê Trường Lam ngày 5/9
     */
    deleteData(CustomerId) {
      this.$axios
        .delete(`https://cukcuk.manhnv.net/api/v1/customers/${CustomerId}`)
        .then((res) => {
          console.log("OK: ", res);
          this.fetchData(); // Thực hiện gọi fetchData để lấy dữ liệu
        })
        .catch((error) => {
          console.log("Lỗi khi xóa khách hàng:", error);
        });
    },
    // Mở form nhân viên
    onShowEmployeeDetail() {
      this.showEmployeeDetail = true;
    },
    // Đóng thông tin nhân viên

    onCloseEmployeeDetail() {
      this.showEmployeeDetail = false;
    },
    /**
     * Chuyển đổi trạng thái hiển thị của combobox cho một mục tableData cụ thể.
     * @param {number} CustomerId - Id của mục tableData.
     *  Created by Lê Trường Lam ngày 5/9
     */
    toggleCombobox(CustomerId) {
      // Lặp qua từng phần tử trong mảng customers
      this.customers.forEach((customer) => {
        // Nếu Id của customer trùng với CustomerId đầu vào
        if (customer.CustomerId === CustomerId) {
          // Đảo ngược trạng thái showCombobox của customer
          customer.showCombobox = !customer.showCombobox;
        } else {
          // Nếu không, đặt trạng thái showCombobox của customer thành false
          customer.showCombobox = false;
        }
      });
    },
    /**
     * Chuyển đổi đối tượng date thành chuỗi định dạng ngày/tháng/năm.
     * @param {Date} date - Đối tượng date cần chuyển đổi. Có thể là đối tượng Date
     * @throws {Error} - Nếu không thể chuyển đổi đối tượng date thành đối tượng Date hoặc xảy ra lỗi trong quá trình chuyển đổi.
     *  Created by Lê Trường Lam ngày 5/9
     *
     */
    formatDate(date) {
      try {
        // Chuyển đổi đối tượng date thành một đối tượng Date
        date = new Date(date);
        // Lấy giá trị ngày
        let dateValue = date.getDate();
        // Lấy giá trị tháng (cộng thêm 1 vì tháng trong JavaScript bắt đầu từ 0)
        let monthValue = date.getMonth() + 1;
        // Lấy giá trị năm
        let year = date.getFullYear();
        // Trả về chuỗi định dạng ngày/tháng/năm
        return `${dateValue}/${monthValue}/${year}`;
      } catch (error) {
        // Xử lý lỗi nếu có
        console.error(error);
        // Trả về chuỗi rỗng nếu xảy ra lỗi
        return "";
      }
    },
    // Hiện Combobox
    onShowCombobox() {
      this.showCombobox = true;
    },
    // Đóng Combobox
    onCloseCombobox() {
      this.showCombobox = false;
    },
    // Hiện Contextmenu

    onShowContextmenu() {
      this.showContextMenu = true;
    },
    // Đóng Contextmenu

    onCloseContextmenu() {
      this.showContextMenu = false;
    },
    /**
     * Chọn tất cả checkbox trong bảng.
     * Created by Lê Trường Lam ngày 5/9
     */
    selectAlltableDatas() {
      if (this.allSelected) {
        this.selectedtableDatas = this.customers.map(
          (customer) => customer.CustomerId
        );
      } else {
        this.selectedtableDatas = [];
      }
    },
    /**
     * Xóa những data đã được chọn
     * Created by Lê Trường Lam ngày 7/9
     */

    deleteSelected() {
      // console.log(this.selectedtableDatas)
      for (let i = 0; i < this.selectedtableDatas.length; i++) {
        this.$axios
          .delete(
            `https://cukcuk.manhnv.net/api/v1/customers/${this.selectedtableDatas[i]}`
          )
          .then((res) => {
            console.log("OK: ", res);
            this.fetchData(); // Thực hiện gọi fetchData để lấy dữ liệu
          })
          .catch((error) => {
            console.log("Lỗi khi xóa khách hàng:", error);
          });
      }
    },

    /**
     * Cập nhật số checkbox trong bảng.
     * Created by Lê Trường Lam ngày 5/9
     */
    updateSelectedCount() {
      if (
        this.allSelected &&
        this.selectedtableDatas.length !== this.customers.length
      ) {
        this.allSelected = false;
      }
      if (this.selectedtableDatas.length > 0) {
        this.showSelected = true;
      } else {
        this.showSelected = false;
      }
    },

    // Xóa tất cả
    deleteAll() {
      // Đặt lại selectedtableDatas về mảng rỗng
      this.selectedtableDatas = [];
      this.allSelected = false;
    },
    // Chọn tất cả
    selectAll() {
      this.allSelected = true;
      // Đặt giá trị selectedtableDatas thành danh sách các id của tất cả các mục trong tableDatas
      this.selectedtableDatas = this.customers.map(
        (customer) => customer.CustomerId
      );
    },
  },
  data() {
    return {
      showLoading: false,
      showNotify: false,
      showCombobox: false,
      showDialog: false,
      showContextMenu: false,
      showSelected: false,
      customItems: [10, 20, 30, 40, 50], // Mảng của combobox
      customItems2: ["Nhân bản", "Xóa", "Ngừng sử dụng"], // Mảng contextmenu
      allSelected: false,
      selectedtableDatas: [],
      customers: [],
      showEmployeeDetail: false,
    };
  },
};
</script>

<style>
.first-left-content {
  display: flex;
  align-items: center;
}
.delete-row {
  text-decoration: none;
  display: flex;
  flex-direction: center;
  align-items: center;
  height: 36px;
  background-color: #eeeeee;
  padding: 0 12px;
  margin-left: 12px;
  border-radius: 4px;
  border: 1px solid #e0e0e0;
  width: max-content;
}
.delete-row > p:first-child {
  margin-right: 8px;
}
</style>
