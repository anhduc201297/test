<template>
  <div class="grid-list-data">
    <!-- Thực hiện hàng loạt -->
    <div class="batch-exection" :class="{ disable: listIdChecked.length == 0 }">
      <div>
        Đã chọn
        <span v-if="selectedAllPages">tất cả </span>
        <b v-if="selectedAllPages">{{ totalEmpl }}</b>
        <b v-if="!selectedAllPages">{{ listIdChecked.length }}</b>
      </div>
      <a
        class="btn-unchecked"
        v-if="listIdChecked.length > 1"
        style="margin-left: 16px; color: red; cursor: pointer"
        @click="clearAllChecked"
        >Bỏ chọn</a
      >
      <a
        class="btn-selected-all-pages"
        v-if="listIdChecked.length == dataTable.length && !selectedAllPages"
        style="margin-left: 16px; color: #1565c0; cursor: pointer"
        @click="selectedAllPages = true"
        >Chọn tất cả các trang</a
      >
      <m-button
        style="margin-left: 25px"
        class="m-btn-secondary m-btn-icon"
        :hasIcon="true"
        classIcon="mi-delete"
        @click="deleteEmployeeChecked"
        >Xóa</m-button
      >
    </div>
    <!-- Các hành động khác -->
    <div class="other-action">
      <!-- textfield search -->
      <m-text-field
        class="m-textfield-icon-action"
        :haveLabel="false"
        :placeholderTextField="this.$resource['vi-VN'].textfield.placeholder"
      ></m-text-field>
      <!-- xuất file excel -->
      <a
        class="m-btn-for-icon"
        style="margin-right: 20px"
        :title="this.$resource['vi-VN'].buttonIcon.titleExcel"
      >
        <div class="mi mi-24 mi-excel"></div>
      </a>
      <!-- refresh -->
      <a
        class="m-btn-for-icon"
        :title="this.$resource['vi-VN'].buttonIcon.titleRefresh"
        @click="this.$emitter.emit('refreshData')"
      >
        <div class="mi mi-24 mi-refresh"></div>
      </a>
    </div>
  </div>
  <div class="grid-model-control" :class="{ disable: selectedAllPages }">
    <div class="ms-grid-viewer scroller">
      <table class="ms-table-viewer">
        <thead>
          <tr>
            <th class="width-col-1">
              <m-checkbox
                :hasLabel="false"
                :value="checkboxAll || dataTable.length == listIdChecked.length"
                @Click="checkboxAllClick"
              ></m-checkbox>
            </th>
            <th class="width-col-2">MÃ NHÂN VIÊN</th>
            <th class="width-col-3">TÊN NHÂN VIÊN</th>
            <th class="width-col-4">GIỚI TÍNH</th>
            <th class="width-col-5 col-date">NGÀY SINH</th>
            <th class="width-col-6" title="Số chứng minh nhân dân">SỐ CMND</th>
            <th class="width-col-7">CHỨC DANH</th>
            <th class="width-col-8">TÊN ĐƠN VỊ</th>
            <!-- <th class="width-col-9">SỐ TÀI KHOẢN</th>
            <th class="width-col-10">TÊN NGÂN HÀNG</th>
            <th class="width-col-11" title="Chi nhánh tài khoản ngân hàng">
              CHI NHÁNH TK NGÂN HÀNG
            </th> -->
            <th class="width-col-12 fixed-column">CHỨC NĂNG</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(item, index) in dataTable"
            :key="item.EmployeeCode"
            :class="{
              'row-selected': index == RowClicked,
              'row-checked': listIdChecked.includes(item.EmployeeCode),
            }"
            @click="RowClicked = index"
            v-on:dblclick="editEmployee(item)"
          >
            <td class="width-col-1">
              <m-checkbox
                :hasLabel="false"
                :value="listIdChecked.includes(item.EmployeeCode)"
                @checkboxChangeValue="
                  checkboxChangeValue($event, item.EmployeeCode)
                "
              ></m-checkbox>
            </td>
            <!-- Mã nhân viên -->
            <td class="width-col-2" :title="item.EmployeeCode || ''">
              {{ item.EmployeeCode || "" }}
            </td>
            <!-- Họ tên -->
            <td class="width-col-3" :title="item.FullName || ''">
              {{ item.FullName || "" }}
            </td>
            <!-- Gioi tinh -->
            <td class="width-col-4">
              {{
                item.Gender === this.$enum.gender.MALE
                  ? this.$resource["vi-VN"].gender.MALE
                  : item.Gender === this.$enum.gender.FEMALE
                  ? this.$resource["vi-VN"].gender.FEMALE
                  : item.Gender === this.$enum.gender.ORTHER
                  ? this.$resource["vi-VN"].gender.ORTHER
                  : ""
              }}
            </td>
            <!-- Ngày sinh -->
            <td class="width-col-5 col-date" :title="item.DateOfBirth || ''">
              {{ $helper.formatDate(item.DateOfBirth) || "" }}
            </td>
            <!-- Số chứng minh nhân dân -->
            <td class="width-col-6" :title="item.IdentityNumber || ''">
              {{ item.IdentityNumber || "" }}
            </td>
            <!-- Chức danh -->
            <td class="width-col-7" :title="item.PositionName || ''">
              {{ item.PositionName || "" }}
            </td>
            <!-- Đơn vị -->
            <td class="width-col-8" :title="item.DepartmentName || ''">
              {{ item.DepartmentName || "" }}
            </td>
            <!-- Số tài khoản -->
            <!-- <td class="width-col-9" :title="item.accountNumber || ''">
              {{ item.accountNumber || "" }}
            </td> -->
            <!-- Ngân hàng -->
            <!-- <td class="width-col-10" :title="item.bank || ''">
              {{ item.bank || "" }}
            </td> -->
            <!-- Chi nhánh tài khoản ngân hàng -->
            <!-- <td class="width-col-11" :title="item.bankBranch || ''">
              {{ item.bankBranch || "" }}
            </td> -->
            <td class="width-col-12 funtion fixed-column">
              <a
                class="m-btn-for-icon m-btn-open-popup"
                @click="editEmployee(item)"
                >Sửa</a
              >
              <a
                class="m-btn-for-icon"
                @click="isShowContextMenu = !isShowContextMenu"
              >
                <div class="mi mi-16 mi-arrow-up-blue"></div> </a
              ><MISAContextMenu
                v-if="isShowContextMenu && index == RowClicked"
                :items="['Xóa', 'Nhân bản', 'Sử dụng']"
                @contextMenuItemClick="contextMenuItemClick"
              ></MISAContextMenu>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
  <m-dialog
    iconClass="mi-warning"
    v-if="showMISADialog"
    :hasButtonSecondary="true"
    @closeDialog="showMISADialog = false"
    @confirmContinue="deleteEmployeeCheckedContinue"
  >
    <template v-slot:title>
      {{ this.$resource["vi-VN"].dialog.titleDeleteSelectedEmployee }}
    </template>
    <template v-slot:dialog-description>{{
      this.$resource["vi-VN"].dialog.contentDeleteSelectedEmployee
    }}</template>
    <template v-slot:btn-second-text>{{
      this.$resource["vi-VN"].button.textButtonCancel
    }}</template>
    <template v-slot:btn-continue-text>{{
      this.$resource["vi-VN"].button.textButtonDelete
    }}</template>
  </m-dialog>
  <m-loading v-if="showLoading" :isLoadingControl="false"></m-loading>
</template>
<script>
import MISAContextMenu from "../contextmenu/MISAContextMenu.vue";
export default {
  name: "MISATable",
  emits: [
    "editEmployee",
    "selectedAllPages",
    "deleteAllEmployees",
    "deleteEmployeeChecked",
  ],
  components: {
    MISAContextMenu,
  },
  data() {
    return {
      RowClicked: -1,
      checkboxAll: false,
      listIdChecked: [] /* danh sách id các nhân viên đã chọn */,
      isShowContextMenu: false,
      selectedAllPages: false /* chọn tất cả các trang */,
      showMISADialog: false /* Điều khiển dialog xóa nhân viên */,
      showLoading: false,
    };
  },
  props: {
    dataTable: {
      type: Array,
      required: true,
    },
    totalEmpl: {
      type: Number,
      required: true,
    },

    // pageCurrent: { // Dùng cho table mới
    //   type: Number,
    //   default: 1,
    //   required: false,
    // },
    // pageSize: { // Dùng cho table mới
    //   type: Number,
    //   default: 20,
    //   required: false,
    // },
  },

  // computed: {
  //   dataTable() {
  //     return this.loadDataEmployees(this.pageCurrent, this.pageSize);
  //   },
  // },

  methods: {
    /* ======================== Load data ========================= */
    /**
     * Load data theo pageCurrent, pageSize
     * Author: NVDUNG (22/08/2023)
     */
    // loadDataEmployees(pageCurrent, pageSize) {
    //   try {
    //     // Hiện loading khi gọi API
    //     this.showLoading = true;
    //     // Gọi api
    //     var data = [];
    //     this.$axios
    //       .get(
    //         "https://cukcuk.manhnv.net/api/v1/Employees/Filter?pageSize=" +
    //           pageSize +
    //           "&pageNumber=" +
    //           pageCurrent
    //       )
    //       .then((response) => {
    //         data = response.data;
    //         console.log(response);
    //       })
    //       .catch((error) => {
    //         console.log(error);
    //       });
    //     // Tắt loading
    //     this.showLoading = false;
    //     return data;
    //   } catch (error) {
    //     console.log(error);
    //   }
    // },

    /* ============== Xử lý checkbox ================= */
    /**
     * Cập nhật tất cả checkbox theo giá trị của checkboxAll
     * Author: NVDUNG (22/08/2023)
     */
    checkboxAllClick() {
      // toggle checkboxAll
      this.checkboxAll = !this.checkboxAll;
      // Thêm tất cả id nhân viên vào listIdChecked để tự động checked checkboxs
      if (this.checkboxAll)
        this.listIdChecked = this.dataTable.map((item) => item.EmployeeCode);
      // Xóa tất cả id nhân viên trong listIdChecked
      else this.listIdChecked = [];
    },

    /**
     * Thêm/xóa id nhân viên trong listIdChecked
     * Author: NVDUNG (22/08/2023)
     */
    checkboxChangeValue(newValue, EmployeeCode) {
      try {
        // Thêm id nhân viên vào listIdChecked
        if (newValue) {
          this.listIdChecked.push(EmployeeCode);
        }
        // Xóa id nhân viên khỏi listIdChecked
        else {
          // Lấy index của mã nhân viên trong listIdChecked
          const index = this.listIdChecked.indexOf(EmployeeCode);
          if (index !== -1) {
            // Xóa nhân viên khỏi list
            this.listIdChecked.splice(index, 1);
          }
        }
      } catch (error) {
        console.log(error);
      }
    },

    /* ============== Xử lý button bỏ chọn ================= */
    /**
     * Xóa tất cả id nhân viên trong listIdChecked
     * Author: NVDUNG (22/08/2023)
     */
    clearAllChecked() {
      // clear listIdChecked
      this.listIdChecked.splice(0, this.listIdChecked.length);
      // unchecked checkboxAll
      this.checkboxAll = false;
      // xóa chọn tất cả các trang
      this.selectedAllPages = false;
    },

    /* ============== Xử lý ContextMenu ================= */

    /**
     * Chọn item trong context menu
     * Author: NVDUNG (22/08/2023)
     */
    contextMenuItemClick(itemValue) {
      if (itemValue === this.$resource["vi-VN"].button.textButtonDelete) {
        // Xóa data nhân viên
        this.deleteEmployee(itemValue);
      }
      // Đóng context menu
      this.isShowContextMenu = false;
    },

    /* ============== Xử lý xóa data nhân viên ================= */
    /**
     * Xóa hàng loạt nhân viên được chọn
     * Author: NVDUNG (22/08/2023)
     */
    deleteEmployeeChecked() {
      try {
        // this.listIdChecked.forEach((id) => {
        //   this.deleteEmployeeChecked(id);
        // });
        this.showMISADialog = true;
      } catch (e) {
        console.log(e);
      }
    },
    /* ============== Xử lý sửa data nhân viên ================= */
    /**
     * Phát sự kiện sửa nhân viên và truyền data nhân viên đến EmployeeList
     * Author: NVDUNG (22/08/2023)
     */
    editEmployee(epmData) {
      this.$emit("editEmployee", epmData);
    },

    /* ============== Xử lý Dialog xóa nhân viên ================= */
    /**
     * Phát sự kiện xóa nhiều nhân viên và truyền dữ liệu đến EmployeeList
     * Author: NVDUNG (22/08/2023)
     */
    deleteEmployeeCheckedContinue() {
      // Đóng dialog
      this.showMISADialog = false;

      // Gửi sự kiện xóa nhân viên
      // Xóa tất cả
      if (this.selectedAllPages) this.$emit("deleteAllEmployees");
      // Xóa nhân viên trong listIdChecked
      else this.$emit("deleteEmployeeChecked", this.listIdChecked);

      // reset listIdChecked
      this.listIdChecked = [];
    },
  },
  watch: {
    selectedAllPages(newValue) {
      // Tạo sự kiện để truyền sang component EmployeeList, điều khiển disabled component panigation
      this.$emit("selectedAllPages", newValue);
    },
  },
};
</script>
<style scoped>
@import url(./table.css);
</style>
