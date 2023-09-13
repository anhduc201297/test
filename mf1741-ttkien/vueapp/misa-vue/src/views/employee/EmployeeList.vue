<template>
  <div>
    <!-- Phần header -->
    <div class="container-header">
      <div style="display: flex; align-items: center">
        <m-icon
          :boxIconType="'header-icon'"
          :iconType="'icon-three-stripes'"
        ></m-icon>
        <div class="header-branch">
          <div class="header-branch-title">
            Công ty TNHH sản suất - thương mại - dịch vụ Qui Phúc
          </div>
          <m-icon :iconType="'header-branch-icon icon-chevron-down_L'"></m-icon>
        </div>
      </div>

      <div style="display: flex; align-items: center">
        <div class="header-tooltip">
          <m-icon
            :iconType="'header-tooltip-icon icon-bell'"
            :tooltip="'Thông báo'"
          ></m-icon>
        </div>
        <div class="account-info">
          <div class="account-info-btn">
            <div class="btn-info-div">
              <m-icon
                :boxIconType="'user-avatar'"
                :iconType="'icon-user-avatar'"
              ></m-icon>
              <div class="user-name">Trần Trung Kiên</div>
              <m-icon :iconType="'icon-chevron-down_S'"></m-icon>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- Nội dung chính -->
    <div class="container-content">
      <!-- Tiêu đề của bảng -->
      <div class="table-title">
        <div class="table-name">Nhân viên</div>
        <m-button
          class="main-button"
          title="Thêm mới nhân viên"
          @click="onAddNewEmployee"
        ></m-button>
      </div>

      <!-- Nội dung bảng -->
      <m-table-data
        v-model:employees="dataEmployees"
        :changeEmployee="onChangeEmployee"
      ></m-table-data>
      <!-- Kết thúc nội dung bảng -->
    </div>
  </div>
  <!-- form thông tin nhân viên -->
  <router-view
    v-model:dataEmployeeInput="dataEmployeeInput"
    :closeFunction="onHideForm"
    :formMode="formMode"
    v-model:isShowToastMsg="isShowToastMsg"
    v-model:toastMsgContent="toastMsgContent"
  ></router-view>
  <!-- loading... -->
  <m-loading :spinnerType="'normal'" v-if="showTableLoading"></m-loading>
  <m-toast-message
    v-if="isShowToastMsg"
    :boxIconType="'box-icon-success'"
    :iconType="'icon-success'"
    :closeFunction="onHideToastMessage"
    :toastMsgContent="toastMsgContent"
  ></m-toast-message>
</template>

<script>
export default {
  name: "EmployeeList",
  data() {
    return {
      showTableLoading: false,
      showForm: false,
      formMode: this.$MISAEnum.formMode.addNewEmployee,
      isShowToastMsg: false,
      toastMsgContent: "",
      toastMsgType: "",
      toastMsgIcon: "",
      dataEmployees: null,
      dataEmployeeInput: {
        EmployeeCode: "",
        FullName: "",
        PositionName: null,
        DateOfBirth: null,
        Gender: null,
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
      dataNewEmployee: {
        EmployeeCode: "",
        FullName: "",
        PositionName: null,
        DateOfBirth: null,
        Gender: null,
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
    };
  },
  methods: {
    /**
     * author: ttkien
     * 23/08/2023
     * hiển thị form thông tin để thêm mới nhân viên
     */
    onAddNewEmployee() {
      this.formMode = this.$MISAEnum.formMode.addNewEmployee;
      // this.dataEmployeeInput = this.dataNewEmployee;
      this.$router.push("/employeelist/employee-infomation");
    },
    /**
     * author: ttkien
     * 23/08/2023
     * ẩn form thông tin
     */
    onHideForm() {
      // this.showForm = false;
      this.$router.push("/employeelist");
    },
    /**
     * author: ttkien
     * 29/08/2023
     * double click vào hàng để hiển thị form sửa thông tin nhân viên
     */
    onChangeEmployee(e) {
      this.formMode = this.$MISAEnum.formMode.changeEmployee;
      this.dataEmployeeInput = this.dataEmployees[e.target.parentElement.id];
      this.$router.push("/employeelist/employee-infomation");
    },
    /**
     * author: ttkien
     * 24/08/2023
     * Ẩn toast message
     * */
    onHideToastMessage() {
      this.isShowToastMsg = false;
    },
  },
  /**
   * author:ttkien
   * 30/08/2023
   * lấy dữ liệu
   */
  created() {
    this.$axios
      .get("https://cukcuk.manhnv.net/api/v1/Employees")
      .then((res) => {
        // console.log(res);
        this.dataEmployees = res.data;
      })
      .catch(this.$helper.showError);
    if (this.dataEmployees === null) this.showTableLoading = true;
  },
  /**
   * author: ttkien
   * 31/08/2023
   * lấy mã nhân viên mới và load lại dữ liệu sau khi thêm mới nhân viên
   */
  updated() {
    if (this.dataEmployees !== null) this.showTableLoading = false;
    if (this.dataEmployeeInput.EmployeeCode === "") {
      this.$axios
        .get("https://cukcuk.manhnv.net/api/v1/Employees/NewEmployeeCode")
        .then((res) => {
          // console.log(res.data);
          this.dataEmployeeInput.EmployeeCode = res.data;
        })
        .catch(this.$helper.showError);
      this.$axios
        .get("https://cukcuk.manhnv.net/api/v1/Employees")
        .then((res) => {
          // console.log(res.data.length);
          this.dataEmployees = res.data;
        })
        .catch(this.$helper.showError);
      if (this.dataEmployees === null) this.showTableLoading = true;
    }
  },
};
</script>

<style scoped>
@import url(./EmployeeList.css);
</style>