<template>
  <div class="main-content-top">
    <div class="main-content-title">Nhân viên</div>
    <m-button @click="openEmployeeDetail">Thêm mới nhân viên</m-button>
  </div>
  <div class="contents-layout">
    <!-- Table cũ truyền array obj nhân viên -->
    <MISATable
      :dataTable="dataTable"
      :total-empl="dataEmployees.length"
      @editEmployee="editEmployee"
      @selectedAllPages="selectedAllPages"
      @deleteEmployeeChecked="deleteEmployeeChecked"
      @deleteAllEmployees="deleteAllEmployees"
    ></MISATable>
    <!-- Table mới data từ call api theo pageCurrent, pageSize -->
    <!-- <MISATable
      :totalEmpl="dataEmployees.length"
      :pageCurrent="pageCurrent"
      :pageSize="pageSize"
    ></MISATable> -->

    <MISAPagination
      :totalRecord="dataEmployees.length"
      :isDisabled="paginationDisabled"
      @changePageSize="changePageSize"
      @changePageCurrent="changePageCurrent"
    ></MISAPagination>
  </div>

  <EmployeeDetail
    v-if="showEmployeeDetail"
    :propEmployee="dataEmployeeDetail"
    :isFormAdd="isAddEmployee"
    @closeForm="closeEmployeeDetail"
  ></EmployeeDetail>
</template>
<script>
import MISATable from "../../../components/base/table/MISATable.vue";
import MISAPagination from "../../../components/base/pagination/MISAPagination.vue";
import EmployeeDetail from "../employeeForm/EmployeeDetail.vue";
export default {
  name: "EmployeeList",
  emits: ["showLoading", "hideLoading", "toastSuccess"],
  inject: ["toastSuccess200", "showDialogError"],
  components: {
    MISATable,
    MISAPagination,
    EmployeeDetail,
  },
  data() {
    return {
      dataEmployees: [] /* danh sách nhân viên */,

      paginationDisabled: false /* Đánh dấu disabled pagination khi table chọn tất cả trang */,
      pageCurrent: 1 /* trang hien tai */,
      pageSize: 20 /* số bản ghi mỗi trang */,

      dataEmployeeDetail: {} /* obj nhân viên để truyền vào form */,
      // propEmployeeId: "",
      showEmployeeDetail: false /* Ẩn/hiện form chi tiết nhân viên */,

      isAddEmployee: true /* Đánh dấu thêm nhân viên mới */,
    };
  },

  computed: {
    /**
     * Cập nhật danh sách cho table khi thay đổi: pageSize, pageCurrent, dataEmployees
     * Author: NVDUNG (20/08/2023)
     *  */
    dataTable() {
      // index bản ghi đầu tiên của page
      var start = (this.pageCurrent - 1) * this.pageSize;
      // index bản ghi cuối cùng của page
      var end = start + this.pageSize - 1;
      // danh sách nhân viên có index từ start đến end
      var listEmployees = this.dataEmployees.slice(start, end + 1);
      return listEmployees;
    },
  },
  created() {
    // Load dữ liệu toàn bộ nhân viên
    // this.loadDataEmployees();
    this.fetchDataFromAPI();

    /**
     * Tạo lắng nghe sự kiện refreshData
     * Author: NVDUNG (20/08/2023)
     */
    this.$emitter.on("refreshData", () => {
      console.log("refreshData .............");
      this.fetchDataFromAPI();
    });
  },
  beforeUnmount() {
    this.$emitter.off("refreshData");
  },
  methods: {
    /* ======================== Load data ========================= */

    /**
     * Load data employees mới
     * Author: NVDUNG (20/08/2023)
     */
    async fetchDataFromAPI() {
      // Hiện loading
      this.$emitter.emit("showLoading", true);
      try {
        const url = "https://cukcuk.manhnv.net/api/v1/Employees";
        const method = "GET";
        const response = await this.$repository.callAPI(url, method);
        // console.log("response: ", response);
        // Truy cập dữ liệu thành công
        if (response.httpStatus === 200) {
          this.dataEmployees = response.data;
        }
        // Thông báo toast thành công
        // this.toastSuccess200("Lấy được danh sách rồi này.");
        // Test dialog
        // this.showDialogError("Lỗi rồi đây nè.");
      } catch (error) {
        // Xử lý lỗi
        console.error("Lỗi khi gọi API:", error);

        // Truy cập thông tin trạng thái HTTP và dữ liệu phản hồi trong trường hợp lỗi
        if (error.httpStatus === 201) {
          // Tạo thông báo toast
          this.toastSuccess200("Đã thêm một nhân viên mới.");
        } else {
          // 400, 401, 402, 403, 404, 500
          // Tạo dialog thông báo
          //   console.error("Trạng thái HTTP:", error.httpStatus);
          //   console.error("Dữ liệu phản hồi:", error.data);
        }
      }
      // Tắt loading
      this.$emitter.emit("showLoading", false);
    },

    /**
     * Load dữ liệu toàn bộ nhân viên
     * Author: NVDUNG (20/08/2023)
     */
    loadDataEmployees() {
      try {
        // Hiện loading khi gọi API
        this.$emitter.emit("showLoading", true);
        // Gọi api
        this.$axios
          .get("https://cukcuk.manhnv.net/api/v1/Employees")
          .then((response) => {
            this.dataEmployees = response.data;
            // console.log(this.dataEmployees);
          })
          .catch((error) => {
            console.log(error);
          });
        // Tắt loading
        this.$emitter.emit("showLoading", false);
      } catch (error) {
        console.log(error);
      }
    },

    /* ======================== Form Employee Detail ========================= */

    /**
     * Mở form EmployeeDetail
     * Author: NVDUNG (20/08/2023)
     */
    openEmployeeDetail() {
      this.showEmployeeDetail = true;
    },

    /**
     * Mở form sửa nhân viên
     * Author: NVDUNG (22/08/2023)
     */
    editEmployee(empData) {
      try {
        // Cập nhật đánh dấu sửa nhân viên
        this.isAddEmployee = false;
        // Truyền dữ liệu nhân viên
        this.dataEmployeeDetail = empData;
        // Hiển thị form
        this.showEmployeeDetail = true;
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * Đóng form employeeDetail
     * Author: NVDUNG (22/08/2023)
     */
    closeEmployeeDetail() {
      this.showEmployeeDetail = false;
      // reset dataEmployeeDetail
      this.dataEmployeeDetail = {};
      // reset isAddEmployee
      this.isAddEmployee = true;
    },

    /* ======================== Table ========================= */

    /**
     * Enabled / Disabled Pagination theo selectedAllPages
     * Author: NVDUNG (22/08/2023)
     */
    selectedAllPages(newValue) {
      this.paginationDisabled = newValue;
    },

    /* ==================== Pagination ==================== */

    /**
     * Thay đổi dataTable khi thay đổi pageSize
     * Author: NVDUNG (25/08/2023)
     */
    changePageSize(newPageSize) {
      this.pageCurrent = 1;
      this.pageSize = newPageSize;
    },

    /**
     * Thay đổi dataTable khi thay đổi pageCurrent
     * Author: NVDUNG (25/08/2023)
     */
    changePageCurrent(newPageCurrent) {
      this.pageCurrent = newPageCurrent;
    },

    /* =================== Xóa data nhân viên =================== */

    /**
     * Xóa data nhân viên theo id nhân viên
     * Author: NVDUNG (22/08/2023)
     */
    deleteEmployee(employeeCode) {
      try {
        // Lấy vị trí nhân viên cần xóa
        const pos = this.dataEmployees
          .map((e) => e.employeeCode)
          .indexOf(employeeCode);
        // Xóa nhân viên
        this.dataEmployees.splice(pos, 1);
      } catch (e) {
        console.log(e);
      }
    },

    /**
     * Xóa nhân viên được chọn
     * Author: NVDUNG (22/08/2023)
     */
    deleteEmployeeChecked(listIdChecked) {
      try {
        // Hiện loading
        this.$emit("showLoading");
        // Xóa nhiều nhân viên
        listIdChecked.forEach((id) => {
          this.deleteEmployee(id);
        });
        // Tắt loading
        this.$emit("hideLoading");
        // Thông báo toast thành công

        // Tạo đối tượng toast thành công
        var toast = {
          toastType: "success",
          message: this.$resource["vi-VN"].toast.messageSuccess,
          showButton: false,
          title: this.$resource["vi-VN"].toast.titleSuccess,
        };
        this.$emit("toastSuccess", toast);
      } catch (e) {
        console.log(e);
      }
    },

    /**
     * Xóa tất cả nhân viên
     * Author: NVDUNG (22/08/2023)
     */
    deleteAllEmployees() {
      try {
        // Hiện loading
        this.$emit("showLoading");
        // Xóa nhiều nhân viên
        this.dataEmployees = [];
        // Tắt loading
        this.$emit("hideLoading");
        // Thông báo toast thành công

        // Tạo đối tượng toast thành công
        var toast = {
          toastType: "success",
          message: this.$resource["vi-VN"].toast.messageSuccess,
          showButton: false,
          title: this.$resource["vi-VN"].toast.titleSuccess,
        };
        this.$emit("toastSuccess", toast);
      } catch (e) {
        console.log(e);
      }
    },
  },
};
</script>
<style scoped>
@import url(./employee-list.css);
</style>
