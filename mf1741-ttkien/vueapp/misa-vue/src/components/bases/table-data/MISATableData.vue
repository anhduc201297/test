<template>
  <div class="table-content">
    <!-- Công cụ điều khiển bảng -->
    <div class="table-control">
      <div
        class="left-control"
        v-show="this.checkBoxArray.length || this.checkBoxArray === true"
      >
        <div class="checked-number">
          Đã chọn
          <b>{{
            this.checkBoxArray === true
              ? this.employees.length
              : this.checkBoxArray.length
          }}</b>
        </div>
        <div class="clear-check" @click="onCancelChecked">Bỏ chọn</div>
        <div
          class="action-tbl"
          @click="onDeleteEmployee"
          v-show="this.checkBoxArray.length > 1 || this.checkBoxArray === true"
        >
          <m-icon
            :boxIconType="'box-icon-trash'"
            :iconType="'icon-trash'"
          ></m-icon>
          <div class="action-content">Xóa tất cả</div>
        </div>
      </div>
      <div class="right-control">
        <m-input-icon
          :placeholder="'Tìm theo mã, tên nhân viên'"
          :boxIconType="'box-icon-search'"
          :iconType="'icon-search'"
        ></m-input-icon>
        <m-icon
          :boxIconType="'box-icon-excel'"
          :iconType="'icon-excel'"
          :tooltip="'Xuất ra Excel'"
        ></m-icon>
        <m-icon
          :boxIconType="'box-icon-refresh'"
          :iconType="'icon-refresh'"
          :tooltip="'Lấy lại dữ liệu'"
        ></m-icon>
      </div>
    </div>

    <!-- Dữ liệu của bảng -->
    <div class="table-data">
      <table class="table-data-content">
        <thead>
          <tr>
            <th>
              <m-input-checkbox
                :thead="true"
                @click="onCheckAll"
                v-model="checkBoxArray"
              ></m-input-checkbox>
              <!-- <input type="checkbox" @click="onCheckAll" v-model="checkBoxArray" /> -->
            </th>
            <th class="emp-code">Mã nhân viên</th>
            <th class="emp-full_name">Tên nhân viên</th>
            <th class="emp-gender">Giới tính</th>
            <th class="emp-date_of_birth">Ngày sinh</th>
            <th class="emp-id_number" title="Số chứng minh nhân dân">
              Số CMND
            </th>
            <th class="emp-position_name">Chức danh</th>
            <th class="emp-department_name">Tên đơn vị</th>
            <th class="emp-bank_acc_number">Số tài khoản</th>
            <th class="emp-bank_name">Tên ngân hàng</th>
            <th class="emp-bank_branch">Chi nhánh TK ngân hàng</th>
            <th class="table-function">Chức năng</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(employee, index) in employees"
            :key="employee.EmployeeId"
            @dblclick="changeEmployee"
            :id="index"
          >
            <td @dblclick.stop="">
              <input
                type="checkbox"
                :value="employee.EmployeeId"
                v-model="checkBoxArray"
              />
            </td>
            <td class="emp-code">{{ employee.EmployeeCode }}</td>
            <td class="emp-full_name">{{ employee.FullName }}</td>
            <td class="emp-gender">
              {{ $helper.formatGender(employee.Gender) }}
            </td>
            <td class="emp-date_of_birth">
              {{ $helper.formatDate(employee.DateOfBirth) }}
            </td>
            <td class="emp-id_number">{{ employee.IdentityNumber }}</td>
            <td class="emp-position_name">{{ employee.PositionName }}</td>
            <td class="emp-department_name">{{ employee.DepartmentName }}</td>
            <td class="emp-bank_acc_number">
              {{ employee.BankAccountNumber }}
            </td>
            <td class="emp-bank_name">{{ employee.BankName }}</td>
            <td class="emp-bank_branch">{{ employee.BankBranch }}</td>
            <td class="table-function" @dblclick.stop="">
              <div class="option-func" :id="index">
                <div class="fix-data" @click="changeEmployee">Sửa</div>
                <m-icon
                  :id="employee.EmployeeId"
                  :class="{
                    'box-icon_checked':
                      showDroplistFunc === employee.EmployeeId,
                  }"
                  :boxIconType="'box-icon-arrow-down_blue'"
                  :iconType="'more-option icon-arrow-down_blue'"
                  @click.stop="onShowDroplistFunc"
                ></m-icon>
              </div>
            </td>
            <m-droplist
              :id="employee.EmployeeId"
              :showDropList="showDroplistFunc"
              :droplistType="'droplist-func'"
              :items="optionFunction"
            ></m-droplist>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Điều hướng trang -->
    <div class="table-pagination">
      <!-- Số bản ghi -->
      <div class="left-pagination">
        <div class="total-record">
          Tổng:
          <b>{{ this.employees === null ? "0" : this.employees.length }}</b>
        </div>
      </div>
      <!-- Số bản ghi trên 1 trang -->
      <div class="right-pagination" style="display: flex; position: relative">
        <div class="record-in-page">
          <div class="record-value">10 bản ghi trên 1 trang</div>
          <div class="record-more-option">
            <m-icon
              :boxIconType="'box-icon-arrow-down'"
              :iconType="{
                'icon-arrow-down': !showDroplistRecord,
                'icon-arrow-up': showDroplistRecord,
              }"
              @click.stop="onShowDroplistRecord"
            ></m-icon>
          </div>
        </div>
        <m-droplist
          :droplistType="'droplist-record'"
          :items="recordInPage"
          v-show="showDroplistRecord"
        ></m-droplist>
        <div class="page-number">
          <div class="page-number-control pre-page" style="margin-right: 8px">
            Trước
          </div>
          <div class="page-index">
            <div
              class="page-index-item"
              style="border: 1px solid #e6e6e6; font-weight: bold"
            >
              1
            </div>
            <div class="page-index-item">2</div>
            <div class="page-index-item">3</div>
            <div class="page-index-item">...</div>
            <div class="page-index-item">8</div>
          </div>
          <div class="page-number-control after-page" style="margin-left: 8px">
            Sau
          </div>
        </div>
      </div>
    </div>

    <!-- disable bảng dữ liệu -->
    <div class="table-disable" v-show="this.checkBoxArray === true"></div>
  </div>
</template>

<script>
export default {
  name: "MISATableData",
  props: ["employees", "changeEmployee"],
  data() {
    return {
      employeesOutput: null,
      checkedAll: false,
      checkedApart: false,
      showDroplistFunc: null,
      optionFunction: ["Nhân bản", "Xóa", "Ngừng sử dụng"],
      checkBoxArray: [],
      showDroplistRecord: false,
      recordInPage: [
        "10 bản ghi trên 1 trang",
        "20 bản ghi trên 1 trang",
        "30 bản ghi trên 1 trang",
        "50 bản ghi trên 1 trang",
        "100 bản ghi trên 1 trang",
      ],
    };
  },
  watch: {
    employeesOutput(newValue) {
      this.$emit("update:employees", newValue);
    },
  },
  methods: {
    /**
     * author: ttkien
     * 24/08/2023
     * chọn tất cả checkbox
     */
    onCheckAll() {
      if (this.checkBoxArray !== true) {
        this.checkBoxArray = true;
      } else this.checkBoxArray = [];
    },
    /**
     * author: ttkien
     * 24/08/2023
     * hiển thị danh sách chức năng 1 hàng
     */
    onShowDroplistFunc(e) {
      if (this.showDroplistFunc !== e.target.id) {
        this.showDroplistFunc = e.target.id;
      } else this.showDroplistFunc = null;
      // this.showDroplistRecord = false;
    },
    /**
     * author: ttkien
     * 24/08/2023
     * ẩn danh sách chức năng 1 hàng
     */
    onHideDroplistFunc() {
      this.showDroplistFunc = null;
    },
    /**
     * author: ttkien
     * 23/08/2023
     * ẩn hiện lựa chọn số bản trên 1 trang
     */
    onShowDroplistRecord() {
      this.showDroplistRecord = !this.showDroplistRecord;
      // this.showDroplistFunc = false;
    },
    /**
     * author: ttkien
     * 23/08/2023
     * ẩn tất cả droplist đang hiện
     */
    hideDroplists() {
      // this.showDroplistFunc = false;
      this.showDroplistRecord = false;
    },
    /**
     * author: ttkien
     * 29/08/2023
     * Hàm bỏ chọn tất cả checkbox
     */
    onCancelChecked() {
      this.checkBoxArray = [];
    },
    /**
     * author: ttkien
     * 30/08/2023
     * xóa dữ liệu nhân viên
     */
    onDeleteEmployee() {
      for (const employeeId of this.checkBoxArray) {
        this.$axios
          .delete(`https://cukcuk.manhnv.net/api/v1/Employees/${employeeId}`)
          .then((res) => {
            console.log(res.status);
            this.employeesOutput = this.employeesOutput.filter(
              (element) => element.EmployeeId !== employeeId
            );
            this.checkBoxArray = this.checkBoxArray.filter(
              (element) => element !== employeeId
            );
            this.showToastMessageSuccess = true;
            this.toastMsgContent =
              this.$MISAResource["VN"].toastMessage.deleteEmployeeSuccessfull;
          })
          .catch((error) => {
            console.log(error);
          });
      }
    },
  },
  updated() {
    this.employeesOutput = this.employees;
    // this.checkBoxArray = this.modelValue;
    // console.log(this.checkBoxArray);
  },
};
</script>

<style scoped>
@import url(./MISATableData.css);
</style>