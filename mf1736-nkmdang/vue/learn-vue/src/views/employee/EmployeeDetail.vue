<template>
  <div>
    <m-form :title="empFormTitle" :closeForm="onCloseEmpDetail">
      <template v-slot:MFormBody>
        <div class="flex" style="margin-top: 20px">
          <div class="mform--half" style="margin-right: 10px">
            <div class="mform--half__item flex">
              <div style="width: 40%; margin-right: 10px">
                <m-input-text
                  label="Mã"
                  required="true"
                  :focus="true"
                  v-model="empFormData.EmployeeCode"
                  :missingInput="missingInput.EmployeeCode"
                ></m-input-text>
              </div>
              <div style="width: 60%">
                <m-input-text
                  label="Tên"
                  required="true"
                  v-model="empFormData.FullName"
                  :missingInput="missingInput.FullName"
                ></m-input-text>
              </div>
            </div>
            <div class="mform--half__item">
              <!-- <div style="margin-bottom: 5px; font-weight: bold" class="flex">
                Đơn vị
                <div class="icon__required">*</div>
              </div> -->
              <MComboBox
                v-model="empFormData.DepartmentName"
                label="Đơn vị"
                :required="true"
                :missingInput="missingInput.DepartmentName"
              ></MComboBox>
            </div>
            <div class="mform--half__item">
              <m-input-text
                label="Chức danh"
                v-model="empFormData.PositionName"
              ></m-input-text>
            </div>
          </div>
          <div class="mform--half" style="margin-left: 10px">
            <div class="mform--half__item flex">
              <div>
                <div style="margin-bottom: 8px; font-weight: bold">
                  Ngày sinh
                </div>
                <m-date-picker
                  v-model="empFormData.DateOfBirth"
                ></m-date-picker>
              </div>
              <div style="margin-left: 10px">
                <div style="font-weight: bold; margin-bottom: 8px">
                  Giới tính
                </div>

                <div
                  class="radio-container flex align-center"
                  style="margin-left: 0"
                >
                  <MInputRadio
                    :tabindex="6"
                    name="gender"
                    value="0"
                    v-model="empFormData.Gender"
                  >
                  </MInputRadio>
                  <span class="gender">Nam</span>
                  <MInputRadio
                    name="gender"
                    value="1"
                    v-model="empFormData.Gender"
                  ></MInputRadio>
                  <span class="gender">Nữ</span>

                  <MInputRadio
                    name="gender"
                    value="2"
                    v-model="empFormData.Gender"
                  ></MInputRadio>
                  <span class="gender">Khác</span>
                </div>
              </div>
            </div>
            <div class="mform--half__item flex">
              <div style="width: calc(100% - 150px)">
                <m-input-text
                  label="Số CMND"
                  tooltip="Số chứng minh thư nhân dân"
                  v-model="empFormData.CMND"
                ></m-input-text>
              </div>
              <div style="margin-left: 10px">
                <div class="datepicker--label">Ngày cấp</div>
                <m-date-picker
                  v-model="empFormData.DateRangeCMND"
                ></m-date-picker>
              </div>
            </div>
            <div class="mform--half__item">
              <div>
                <m-input-text label="Nơi cấp"></m-input-text>
              </div>
            </div>
          </div>
        </div>
        <div style="margin-top: 45px">
          <!-- ĐT cố định, ĐT di động, Email -->
          <div style="margin-top: 15px">
            <m-input-text
              label="Địa chỉ"
              v-model="empFormData.Address"
            ></m-input-text>
          </div>

          <div style="margin-top: 15px">
            <div class="flex">
              <div class="mfrom--col3__item">
                <m-input-text
                  label="ĐT cố định"
                  tooltip="Điện thoại cố định"
                  v-model="empFormData.LandlinePhone"
                ></m-input-text>
              </div>
              <div class="mfrom--col3__item">
                <m-input-text
                  label="ĐT di động"
                  tooltip="Điện thoại di động"
                  v-model="empFormData.PhoneNumber"
                ></m-input-text>
              </div>
              <div class="mfrom--col3__item">
                <m-input-text
                  label="Email"
                  v-model="empFormData.Email"
                ></m-input-text>
              </div>
            </div>
          </div>

          <!-- TK ngân hàng, tên ngân hàng, chi nhánh -->
          <div style="margin-top: 15px">
            <div class="flex">
              <div class="mfrom--col3__item">
                <m-input-text
                  label="Tài khoản ngân hàng"
                  v-model="empFormData.BankAccountNumber"
                ></m-input-text>
              </div>
              <div class="mfrom--col3__item">
                <m-input-text
                  label="Tên ngân hàng"
                  v-model="empFormData.BankName"
                ></m-input-text>
              </div>
              <div class="mfrom--col3__item">
                <m-input-text
                  label="Chi nhánh"
                  v-model="empFormData.BankBranchName"
                ></m-input-text>
              </div>
            </div>
          </div>
        </div>
      </template>
      <template v-slot:MFormFooter>
        <div class="mform--footer">
          <div>
            <MButton class="bg-white">Hủy</MButton>
          </div>
          <div class="flex">
            <div style="margin-right: 10px">
              <m-button class="bg-white" @click="clickSave">Cất</m-button>
            </div>
            <div>
              <m-button @click="createOneEmpAndSaveNew">Cất và thêm</m-button>
            </div>
          </div>
        </div>
      </template>
    </m-form>
  </div>
</template>
<script>
// import axios from '@/js/axios';
import EmployeeApi from "./employeeapi";
export default {
  props: ["onCloseEmpDetail", "type", "empDetail", "employeeBaseUrlApi"],
  data() {
    return {
      empFormData: {
        EmployeeCode: "",
        FullName: "",
        Gender: "",
        GenderName: "",
        DateOfBirth: "",
        PositionName: "",
        PhoneNumber: "",
        Email: "",
        Address: "",
        DepartmentName: "",
        CMND: "",
        DateRangeCMND: "",
        LandlinePhone: "",
        Mobile: "",
        BankName: "",
        BankBranchName: "",
        BankAccountNumber: "",
        // departmentId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
      },
      missingInput: {
        EmployeeCode: false,
        FullName: false,
        DepartmentName: false,
      },
      departmentInformation: "",
      empFormTitle: "Thêm mới nhân viên",
    };
  },
  methods: {
    /**
     * Author: Minh Đăng
     */
    validEmpFormData() {
      if (
        !this.empFormData.EmployeeCode ||
        !this.empFormData.DepartmentName ||
        !this.empFormData.FullName
      ) {
        return false;
      } else {
        return true;
      }
    },
    createOneEmp: EmployeeApi.createOneEmp,
    clickSave: EmployeeApi.clickSave,
    createOneEmpAndSaveNew: EmployeeApi.createOneEmpAndSaveNew,
    changeOneEmp: EmployeeApi.changeOneEmp,
  },
  watch: {},
  async created() {
    if (this.type == "add") {
      this.empFormTitle = "Thêm mới nhân viên";
      try {
        this.$emitter.emit("onShowLoading");
        const response = await this.$axios.GET(
          "https://cukcuk.manhnv.net/api/v1/Employees/NewEmployeeCode"
        );
        this.$emitter.emit("onHideLoading");
        // if (response.status == 200) {
        console.log(response.data);
        this.empFormData.EmployeeCode = response.data;
        // }
      } catch (error) {
        this.$emitter.emit("onHideLoading");
        this.$emitter.emit(
          "setToastMessageContent",
          this.$resource[this.$languageCode].ToastMessageContent.EmployeeForm
            .CannotGetNewEmployeeCode
        );
        this.$emitter.emit("onShowToastMessage");
        console.log(error);
      }
    } else {
      this.empFormTitle = "Thông tin nhân viên";
      if (this.empDetail) {
        console.log(this.empDetail);
        this.empFormData = this.empDetail;
      }
    }
  },
};
</script>
<style scoped>
.mform--half {
  width: 50%;
}
.mform--half__item {
  margin-top: 15px;
}
.mfrom--col3__item {
  margin-right: 10px;
}
.datepicker--label {
  font-weight: bold;
  margin-bottom: 8px;
}

.mform--footer {
  margin-top: 45px;
  display: flex;
  justify-content: space-between;
}
</style>
