<template>
  <!-- Form thông tin nhân viên -->
  <div class="form-view">
    <input type="text" v-model="isShowToastMsgOutput" />
    <input type="text" v-model="toastMsgContentOutput" />
    <div action="" class="form-window">
      <!-- Phần đầu form -->
      <div class="form-header">
        <!-- Tiêu đề form -->
        <div style="display: flex">
          <div class="form-title">Thông tin nhân viên</div>
        </div>

        <div style="display: flex">
          <m-icon
            :boxIconType="'box-icon-question'"
            :iconType="'icon-question'"
            @click="onShowDialog"
          ></m-icon>
          <m-icon
            :boxIconType="'box-icon-x_L'"
            :iconType="'icon-x_L'"
            @click="onCloseForm"
          ></m-icon>
        </div>
      </div>

      <!-- Phần thân form - nội dung chính -->
      <div class="form-body">
        <!-- Nhóm thứ nhất-->
        <div class="group-1" style="display: flex">
          <!-- Nhóm bên trái -->
          <div class="group-1_col-1">
            <div style="display: flex">
              <!-- Mã nhân viên-->
              <div class="emp-code">
                <label for="" class="label-required"
                  >Mã<span class="span-required">*</span></label
                >
                <m-input
                  :refValue="'EmployeeCode'"
                  v-model="dataEmployeeOutput.EmployeeCode"
                  :isUpdateData="isUpdateData"
                  :inputError="isEmptyCode"
                ></m-input>
                <div
                  class="input-error-mess"
                  :class="{ 'tag-hidden': !isEmptyCode }"
                >
                  Mã không được để trống
                </div>
              </div>
              <!-- Tên nhân viên-->
              <div class="emp-name">
                <label for="" class="label-required"
                  >Tên<span class="span-required">*</span></label
                >
                <m-input
                  v-model="dataEmployeeOutput.FullName"
                  :isUpdateData="isUpdateData"
                  :inputError="isEmptyName"
                ></m-input>
                <div
                  class="input-error-mess"
                  :class="{ 'tag-hidden': !isEmptyName }"
                >
                  Tên không được để trống
                </div>
              </div>
            </div>
            <!-- Đơn vị làm việc-->
            <div class="emp-unit">
              <label for="" class="label-required"
                >Đơn vị<span class="span-required">*</span></label
              >
              <!-- <m-input-icon
                :boxIconType="'box-icon-arrow-down'"
                :iconType="'icon-arrow-down'"
              ></m-input-icon> -->
              <m-combobox
                url="https://cukcuk.manhnv.net/api/v1/Departments"
                propText="DepartmentName"
                propValue="DepartmentId"
              ></m-combobox>
              <div class="input-error-mess tag-hidden">
                Đơn vị không được để trống
              </div>
            </div>
            <!-- Chức danh -->
            <div class="emp-title">
              <label for="">Chức danh</label>
              <m-input
                v-model="dataEmployeeOutput.PositionName"
                :isUpdateData="isUpdateData"
              ></m-input>
            </div>
          </div>

          <!-- Nhóm bên phải -->
          <div class="group-1_col-2">
            <div style="display: flex">
              <!-- Ngày sinh -->
              <div class="emp-birthday">
                <label for="">Ngày sinh</label>
                <m-input-icon
                  :placeholder="'DD/MM/YYYY'"
                  :boxIconType="'box-icon-calendar'"
                  :iconType="'icon-calendar'"
                  v-model="dateOfBirth"
                  :isUpdateData="isUpdateData"
                ></m-input-icon>
              </div>

              <!-- Giới tính -->
              <div class="emp-gender">
                <label for="">Giới tính</label>
                <div class="box-gender-select">
                  <m-input-radio
                    :dataGender="dataGender"
                    v-model="picked"
                    :isUpdateData="isUpdateData"
                  ></m-input-radio>
                </div>
              </div>
            </div>

            <div class="box-id-card">
              <!-- Số chứng minh nhân dân -->
              <div class="emp-id">
                <label for="" title="Số chứng minh nhân dân">Số CMND</label>
                <m-input
                  v-model="dataEmployeeOutput.IdentityNumber"
                  :isUpdateData="isUpdateData"
                ></m-input>
              </div>
              <!-- Ngày cấp -->
              <div class="emp-id-datepicker">
                <div class="date-picker">
                  <label for="">Ngày cấp</label>
                  <m-input-icon
                    :placeholder="'DD/MM/YYYY'"
                    :boxIconType="'box-icon-calendar'"
                    :iconType="'icon-calendar'"
                    v-model="identityDate"
                    :isUpdateData="isUpdateData"
                  ></m-input-icon>
                </div>
              </div>
            </div>

            <!-- Nơi cấp -->
            <div class="emp-id-place">
              <label for="">Nơi cấp</label>
              <m-input
                v-model="dataEmployeeOutput.IdentityPlace"
                :isUpdateData="isUpdateData"
              ></m-input>
            </div>
          </div>
        </div>

        <!-- Nhóm thứ hai - địa chỉ -->
        <div class="group-2">
          <div class="emp-address">
            <label for="">Địa chỉ</label>
            <m-input
              v-model="dataEmployeeOutput.Address"
              :isUpdateData="isUpdateData"
            ></m-input>
          </div>
        </div>

        <!-- Nhóm thứ ba -->
        <div class="group-3">
          <!-- Số điện thoại di động -->
          <div class="emp-mobile-phone">
            <label for="">ĐT di động</label>
            <m-input
              v-model="dataEmployeeOutput.PhoneNumber"
              :isUpdateData="isUpdateData"
            ></m-input>
          </div>
          <!-- Số điện thoại cố định -->
          <div class="emp-landline-phone">
            <label for="">ĐT cố định</label>
            <m-input></m-input>
          </div>
          <!-- Địa chỉ email -->
          <div class="emp-email">
            <label for="">Email</label>
            <m-input
              v-model="dataEmployeeOutput.Email"
              :isUpdateData="isUpdateData"
            ></m-input>
          </div>
        </div>
        <!-- Nhóm thứ tư-->
        <div class="group-4">
          <!-- Tài khoản ngân hàng -->
          <div class="emp-bank-account">
            <label for="">Tài khoản ngân hàng</label>
            <m-input
              v-model="dataEmployeeOutput.BankAccountNumber"
              :isUpdateData="isUpdateData"
            ></m-input>
          </div>
          <!-- Tên ngân hàng -->
          <div class="emp-bank-name">
            <label for="">Tên ngân hàng</label>
            <m-input
              v-model="dataEmployeeOutput.BankName"
              :isUpdateData="isUpdateData"
            ></m-input>
          </div>
          <!-- Chi nhánh ngân hàng -->
          <div class="emp-bank-branch">
            <label for="">Chi nhánh</label>
            <m-input
              v-model="dataEmployeeOutput.BankBranch"
              :isUpdateData="isUpdateData"
            ></m-input>
          </div>
        </div>
      </div>

      <!-- Phần chân form -->
      <div class="form-footer">
        <div class="left-btn">
          <m-button
            class="minor-button"
            :title="'Hủy'"
            @click.prevent="onCloseForm"
          ></m-button>
        </div>
        <div class="right-btn">
          <m-button
            class="minor-button"
            :title="'Cất'"
            @click.prevent="onSaveEmployee"
          ></m-button>
          <m-button class="main-button" :title="'Cất và thêm'"></m-button>
        </div>
      </div>
    </div>
  </div>
  <m-dialog v-if="showDialog" :closeFunction="onHideDialog"></m-dialog>
  <!-- <m-toast-message
    v-if="showToastMessageSuccess"
    :boxIconType="'box-icon-success'"
    :iconType="'icon-success'"
    :closeFunction="onHideToastMessage"
  ></m-toast-message> -->
  <!-- <m-toast-message
    v-if="showToastMessage"
    :boxIconType="'box-icon-error'"
    :iconType="'icon-x_M-white'"
    :closeFunction="onHideToastMessage"
  ></m-toast-message> -->
</template>

<script>
export default {
  name: "MISAForm",
  props: [
    "closeFunction",
    "dataEmployeeInput",
    "formMode",
    "isShowToastMsg",
    "toastMsgContent",
  ],
  data() {
    return {
      showDialog: false,
      showToastMessageSuccess: false,
      isShowToastMsgOutput: false,
      toastMsgContentOutput: "",
      dataEmployeeOutput: {},
      dataNewEmployee: {
        EmployeeCode: "",
        FullName: null,
        PositionName: null,
        DateOfBirth: null,
        Gender: 1,
        IdentityNumber: null,
        IdentityDate: null,
        IdentityPlace: null,
        Address: null,
        PhoneNumber: null,
        Email: null,
        BankAccountNumber: null,
        BankName: null,
        BankBranch: null,
      },
      isUpdateData: false,
      dateOfBirth: this.$helper.formatDate(this.dataEmployeeInput.DateOfBirth),
      identityDate: this.$helper.formatDate(
        this.dataEmployeeInput.IdentityDate
      ),
      picked: this.$helper.formatGender(this.dataEmployeeInput.Gender),
      dataGender: ["Nam", "Nữ", "Khác"],
      employeeId: null,
      isEmptyCode: false,
      isEmptyName: false,
      // isEmptyDepartment: false,
    };
  },
  watch: {
    // showToastMsgOutput(newValue) {},
    // toastMsgContentOutput(newValue) {},
  },
  methods: {
    /**
     * author: ttkien
     * 23/08/2023
     * đóng form
     */
    onCloseForm() {
      this.isUpdateData = false;
      this.closeFunction();
    },
    /**
     * author: ttkien
     * 23/08/2023
     * hiển thị thông báo
     */
    onShowDialog() {
      this.showDialog = true;
    },
    /**
     * author: ttkien
     * 23/08/2023
     * ẩn thông báo
     */
    onHideDialog() {
      this.showDialog = false;
    },
    /**
     * author: ttkien
     * 24/08/2023
     * Hiển thị toast message
     * */
    onShowToastMessage() {
      this.showToastMessage = true;
    },
    /**
     * author: ttkien
     * 24/08/2023
     * Ẩn toast message
     * */
    onHideToastMessage() {
      this.showToastMessage = false;
    },
    /**
     * author: ttkien
     * 27/08/20232
     * cất dữ liệu nhân viên
     */
    onSaveEmployee() {
      this.isUpdateData = true;
    },
  },
  created() {
    this.dataEmployeeOutput = this.dataEmployeeInput;
    if (this.formMode === this.$MISAEnum.formMode.changeEmployee) {
      this.employeeId = this.dataEmployeeOutput.EmployeeId;
    }
    // this.showToastMsgOutput = this.showToastMsg;
    // this.toastMsgContentOutput = this.toastMsgContent;
    // this.picked = this.picked !== undefined ? this.picked : "Nam";
  },
  /**
   * author: ttkien
   * 30/08/2023
   * update/không update dữ liệu
   */
  updated() {
    if (this.isUpdateData) {
      this.isUpdateData = false;
      if (this.dataEmployeeOutput.EmployeeCode === "") this.isEmptyCode = true;
      else this.isEmptyCode = false;
      if (this.dataEmployeeOutput.FullName === "") this.isEmptyName = true;
      else this.isEmptyName = false;

      if (!this.isEmptyCode && !this.isEmptyName) {
        // chuyển dữ liệu giới tính về dạng lưu trữ trong database
        if (this.picked === "Nữ") this.dataEmployeeOutput.Gender = 0;
        else if (this.picked === "Nam") this.dataEmployeeOutput.Gender = 1;
        else if (this.picked === "Khác") this.dataEmployeeOutput.Gender = 2;

        // chuyển dữ liệu ngày tháng năm về date time
        this.dataEmployeeOutput.DateOfBirth = this.$helper.convertToDateTime(
          this.dateOfBirth
        );
        this.dataEmployeeOutput.IdentityDate = this.$helper.convertToDateTime(
          this.identityDate
        );

        if (this.formMode === this.$MISAEnum.formMode.changeEmployee) {
          this.$axios
            .put(
              `https://cukcuk.manhnv.net/api/v1/Employees/${this.employeeId}`,
              this.dataEmployeeOutput
            )
            .then((res) => {
              console.log(res.status);
              this.isShowToastMsgOutput = true;
              this.toastMsgContentOutput =
                this.$MISAResource["VN"].toastMessage.changeEmployeeSuccessfull;
              this.$emit("update:isShowToastMsg", this.isShowToastMsgOutput);
              this.$emit("update:toastMsgContent", this.toastMsgContentOutput);
              // console.log(this.showToastMsgOutput);
              // console.log(this.toastMsgContentOutput);
              this.closeFunction();
            })
            .catch(this.$helper.showError);
        } else {
          this.$axios
            .post(
              "https://cukcuk.manhnv.net/api/v1/Employees",
              this.dataEmployeeOutput
            )
            .then((res) => {
              console.log(res.status);
              // this.$emit("update:dataEmployeeInput", this.dataEmployeeOutput);
              // console.log("form:", this.dataEmployeeOutput);
              this.closeFunction();
            })
            .catch(this.$helper.showError);
        }
      }
    }
  },
  beforeUnmount() {
    this.dataEmployeeOutput = this.dataNewEmployee;
    this.$emit("update:dataEmployeeInput", this.dataEmployeeOutput);
  },
};
</script>

<style scoped>
@import url(./MISAForm.css);
</style>