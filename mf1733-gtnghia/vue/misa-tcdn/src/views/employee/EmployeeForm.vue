<template>
  <m-modal
    :title="'Thông tin nhân viên'"
    :titleSubmitBtn="'Cất'"
    :titleSubmitAndNewBtn="'Cất và Thêm mới'"
    :method="'POST'"
    :data="employee"
    :URL="'https://cukcuk.manhnv.net/api/v1/employees'"
    :msgSuccess="this.$MISAResource.VN.CreateEmployeeSuccess"
    @onSubmitAndNewForm="onClickStoreAndNew"
    @onSubmitForm="onClickStore"
    @onCloseForm="onCloseForm"
    @onResetForm="onResetForm"
  >
    <template #modal__body>
      <div class="row">
        <div class="col-2 mr-24">
          <div class="row">
            <div class="col-2/5 mr-8">
              <m-input
                id="employeeCode"
                :isRequired="true"
                :label="'Mã'"
                v-model="employee.employeeCode"
              ></m-input>
            </div>
            <div class="col-3/5">
              <m-input
                :isRequired="true"
                :label="'Họ và tên'"
                v-model="employee.fullName"
              ></m-input>
            </div>
          </div>
          <div class="row">
            <div class="input-container">
              <div class="label-box">
                <label name="department" for="department"
                  >Đơn vị <span class="text-red">*</span></label
                >
              </div>
              <div class="input-box">
                <select class="select-box" name="">
                  <option value="">-- Không chọn --</option>
                  <option value="">Hành Chính Nhân Sự</option>
                  <option value="">Tài Chính Doanh Nghiệp</option>
                </select>
              </div>
            </div>
          </div>
          <div class="row">
            <m-input
              :label="'Chức danh'"
              :name="'job-title'"
              v-model="employee.positionName"
            />
          </div>
        </div>
        <div class="col-2">
          <div class="row">
            <div class="col-2/5 mr-8">
              <m-input
                :label="'Ngày sinh'"
                :name="'birth'"
                :type="'date'"
                v-model="employee.dateOfBirth"
              />
            </div>
            <div class="col-3/5">
              <m-radio
                :name="'gender'"
                :label="'Giới tính'"
                :options="genderOptions"
                v-model="employee.gender"
              />
            </div>
          </div>
          <div class="row">
            <div class="col-3/5 mr-8">
              <m-input
                v-model="employee.identityNumber"
                :label="'Số CCCD/CMTND'"
                :name="'identity'"
                :tooltips="'Số Căn Cước Công Dân/Chứng Minh Thư Nhân Dân'"
              />
            </div>
            <div class="col-2/5">
              <m-input
                v-model="employee.identityDate"
                :label="'Ngày cấp'"
                :name="'lisence-date'"
                :type="'date'"
              />
            </div>
          </div>
          <div class="row">
            <m-input
              v-model="employee.identityPlace"
              :label="'Nơi cấp'"
              :name="'license-place'"
            />
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col">
          <div class="row">
            <m-input :label="'Địa chỉ'" :name="'address'" />
          </div>
          <div class="row">
            <div class="col-3 mr-8">
              <m-input
                v-model="employee.phoneNumber"
                :label="'ĐT di động'"
                :name="'mobile'"
                :tooltips="'Điện thoại di động'"
              />
            </div>
            <div class="col-3 mr-8">
              <m-input
                v-model="employee.landline"
                :label="'ĐT cố định'"
                :name="'landline'"
                :tooltips="'Điện thoại cố định'"
              />
            </div>
            <div class="col-3">
              <m-input
                v-model="employee.email"
                :label="'Email'"
                :type="'email'"
                :placeholder="'nghia2905.per@gmail.com'"
              ></m-input>
            </div>
          </div>
          <div class="row">
            <div class="col-3 mr-8">
              <m-input
                v-model="employee.bankAccount"
                :label="'Tài khoản ngân hàng'"
                :name="'bank-account'"
              />
            </div>
            <div class="col-3 mr-8">
              <m-input
                v-model="employee.bankName"
                :label="'Tên ngân hàng'"
                :name="'bank-name'"
              />
            </div>
            <div class="col-3">
              <m-input
                v-model="employee.bankBranch"
                :label="'Chi nhánh ngân hàng'"
                :name="'bank-branch'"
              />
            </div>
          </div>
        </div>
      </div>
    </template>
  </m-modal>
</template>

<script>
export default {
  name: "EmployeeForm",
  props: [],
  data() {
    return {
      genderOptions: [
        {
          value: this.$MISAEnum.Gender.male,
          label: "Nam",
        },
        {
          value: this.$MISAEnum.Gender.female,
          label: "Nữ",
        },
        {
          value: this.$MISAEnum.Gender.other,
          label: "Khác",
        },
      ],
      employee: {
        employeeCode: "",
        fullName: "",
        dateOfBirth: "",
        departmentCode: "",
        positionName: "",
        gender: "",
        identityNumber: "",
        identityDate: "",
        identityPlace: "",
        phoneNumber: "",
        landline: "",
        email: "",
        bankAccount: "",
        bankName: "",
        bankBranch: "",
      },
    };
  },
  mounted() {
    this.getNewEmloyeeCode();
  },
  methods: {
    resetInputField() {},
    /**
     * On Reset Form input button
     * Created by: mf1733-gtnghia - 21/8/2023
     */
    onResetForm() {
      console.log("on reset form");
      // Reset in object
      Object.keys(this.employee).forEach((key) => {
        this.employee[key] = "";
      });

      const modalBody = document.querySelector(".modal__body");
      const inputBoxForm = Array.from(modalBody.querySelectorAll("input"));
      // Reset in input field
      this.$nextTick(() => {
        inputBoxForm.forEach((e) => {
          e.value = "";
        });
        this.getNewEmloyeeCode();
      });
    },
    /**
     * On close form button - redirect to Customer List
     * Created by: mf1733-gtnghia - 21/8/2023
     */
    onCloseForm() {
      this.$router.push("/employee/list");
    },
    /**
     * On click Store button
     * Redirect to Customer List
     * Created by: mf1733-gtnghia - 21/8/2023
     */
    onClickStore() {
      this.onCloseForm();
      this.$emit("reloadData");
    },
    /**
     * On click Store and New button
     * Redirect to Customer List
     * Created by: mf1733-gtnghia - 21/8/2023
     */
    onClickStoreAndNew() {
      this.onResetForm();
      this.$emit("reloadData");
    },
    /**
     * Get new emloyee code from database
     * Created by: mf1733-nghia - 5/9/2023
     */
    async getNewEmloyeeCode() {
      const employeeCodeInput = document.getElementById("employeeCode");
      const newEmployeeCode = await this.$sendRequest(
        "https://cukcuk.manhnv.net/api/v1/Employees/NewEmployeeCode"
      );
      this.employee.employeeCode = newEmployeeCode;
      this.$nextTick(() => {
        employeeCodeInput.querySelector("input").value = newEmployeeCode;
      });
    },
  },
};
</script>

<style scoped>
@import url(../../components/base/modal/modal.css);
</style>
