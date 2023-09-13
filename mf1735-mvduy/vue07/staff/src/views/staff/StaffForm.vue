<template>
  <div v-if="showForm" class="d-form-popup">
    <div class="d-detail-form">
      <div class="header-form">
        <div class="left-header">
          <span style="font-size: 24px; font-weight: 700"
            >Thông tin nhân viên</span
          >
          <!-- <m-checkbox content="Là khách hàng"></m-checkbox>
          <m-checkbox content="Là nhà cung cấp"></m-checkbox> -->
        </div>
        <div class="right-header">
          <div class="icon help-icon"></div>
          <div class="icon close-icon" @click="onCloseForm"></div>
        </div>
      </div>
      <div class="body-form">
        <div class="top-body-form">
          <div class="left-part">
            <div class="row1">
              <div class="d-wrapper">
                <label>Mã <span class="asterisk">*</span></label>
                <m-input
                  v-model="employee.employeeCode"
                  :content="this.$resourceJS['VN'].CodeNotEmpty"
                ></m-input>
              </div>
              <div class="d-wrapper">
                <label>Tên <span class="asterisk">*</span></label>
                <m-input
                  v-model="employee.fullName"
                  :content="this.$resourceJS['VN'].NameNotEmpty"
                ></m-input>
              </div>
            </div>
            <div class="row2">
              <div class="d-wrapper">
                <label>Đơn vị <span class="asterisk">*</span></label>
                <m-combobox
                  @getValue="handleGetValue"
                  class="custom-combobox"
                  url="https://cukcuk.manhnv.net/api/v1/Departments"
                  propText="DepartmentName"
                  propValue="DepartmentId"
                  :options="customOptions"
                  :content="this.$resourceJS['VN'].DepartmentNotEmpty"
                ></m-combobox>
              </div>
            </div>
            <div class="row3">
              <div class="d-wrapper">
                <label for="">Chức danh</label>
                <m-input :inputValue="''"></m-input>
              </div>
            </div>
          </div>
          <div class="right-part">
            <div class="row1">
              <div class="d-wrapper">
                <label for="">Ngày sinh</label>
                <input class="d-date-picker" type="date" />
              </div>
              <div class="d-wrapper">
                <label for="">Giới tính</label>
                <div class="radio-group">
                  <m-radio content="Nam"></m-radio>
                  <m-radio content="Nữ"></m-radio>
                  <m-radio content="Khác"></m-radio>
                </div>
              </div>
            </div>
            <div class="row2">
              <div class="d-wrapper">
                <label for="" title="Số chứng minh nhân dân">Số CMND</label>
                <m-input></m-input>
              </div>
              <div class="d-wrapper">
                <label for="">Ngày cấp</label>
                <input class="d-date-picker" type="date" />
              </div>
            </div>
            <div class="row3">
              <div class="d-wrapper">
                <label for="">Nơi cấp</label>
                <m-input></m-input>
              </div>
            </div>
          </div>
        </div>
        <div class="bottom-body-form">
          <div class="row1">
            <div class="d-wrapper">
              <label for="">Địa chỉ</label>
              <m-input></m-input>
            </div>
          </div>
          <div class="row2">
            <div class="d-wrapper">
              <label for="">ĐT di động</label>
              <m-input v-model="employee.phoneNumber"></m-input>
              <div>{{ employee.phoneNumber }}</div>
            </div>
            <div class="d-wrapper">
              <label for="">Đt cố định</label>
              <m-input></m-input>
            </div>
            <div class="d-wrapper">
              <label for="">Email</label>
              <m-input v-model="employee.email"></m-input>
            </div>
          </div>
          <div class="row3">
            <div class="d-wrapper">
              <label for="">Tài khoản ngân hàng</label>
              <m-input></m-input>
            </div>
            <div class="d-wrapper">
              <label for="">Tên ngân hàng</label>
              <m-input></m-input>
            </div>
            <div class="d-wrapper">
              <label for="">Chi nhánh</label>
              <m-input></m-input>
            </div>
          </div>
        </div>
      </div>

      <div class="footer-form">
        <button class="btn-normal" @click="onCloseForm">Hủy</button>
        <div class="right-footer">
          <m-button-notext @click="onCloseSave();">Cất</m-button-notext>
          <m-button @click="onSave()">Cất và thêm</m-button>
        </div>
      </div>
    </div>
  </div>
  <m-toast
      v-show="showToast"
      :title="titleMsg"
      :type="typeMsg"
      :content="errorMsg">
</m-toast>
</template>


<script>
export default {
  name: "StaffForm",
  props:{
    onCloseForm: Function,
    showForm: Boolean,
  },
  methods: {

     /*
     * Lưu và đóng form
     * Author: mf1735-duy (30/08/2023)
     */
      onCloseSave(){
      this.$axios
        .post("https://cukcuk.manhnv.net/api/v1/Employees", this.employee)
        .then((res) => {
          console.log(res);
          this.onCloseForm();
          this.$emit("onShowToast");
          
        })
        .catch((error) => {
          const code = error.response.status;
          console.log(code);
          const msg = error.response.data.userMsg;
          this.onShowToast(msg,this.$enumJS.Toast.Error,this.$resourceJS['VN'].Fail);
        });
    },
      
    
    /*
     * Click bật toast trong 3s
     * Author: mf1735-duy (24/08/2023)
     */
    onShowToast(msg,type,title) {
      this.errorMsg = msg;
      this.showToast = true;
      this.typeMsg = type;
      this.titleMsg=title;
     
      setTimeout(() => {
        this.showToast = false;
        this.errorMsg = '';
      }, 3000);
    },
    /*
     * Gọi Api để lưu giá trị và sử lí thông báo
     * Author: mf1735-duy (30/08/2023)
     */
    onSave() {
      this.$axios
        .post("https://cukcuk.manhnv.net/api/v1/Employees", this.employee)
        .then((res) => {
          console.log(res);
          this.onShowToast( this.$resourceJS['VN'].AddUserSuccess,this.$enumJS.Toast.Success,this.$resourceJS['VN'].Success);


        })
        .catch((error) => {
          const code = error.response.status;
          console.log(code);
          const msg = error.response.data.userMsg;
          this.onShowToast(msg,this.$enumJS.Toast.Error,this.$resourceJS['VN'].Fail);

        });
    },

    /*
     * Nhận dữ liệu từ combobox
     * Author: mf1735-duy (30/08/2023)
     */
    handleGetValue(value) {
      this.employee.departmentId = value;
    },
  },
  data() {
    return {
      customOptions: ["Phòng nhân sự", "Phòng điều hành"],
      employee: {
        employeeCode: "",
        fullName: "",
        phoneNumber: "",
        email: "",
        departmentId: "",
      },
      errorMsg: "",
      typeMsg:"",
      titleMsg:"",
    };
  },
};
</script>

<style scoped>
@import url(../../css/component/detail-form.css);
</style>