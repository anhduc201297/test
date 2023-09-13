<template>
  <div class="container-form">
    <div class="m-form-background"></div>
    <!-- v-if="dataEmployee.EmployeeCode" -->
    <m-form
      :key="keyRenderForm"
      :formTitle="
        isFormAdd === this.$enum.formPopup.addEmployee
          ? this.$resource['vi-VN'].formPopup.employee.titleAddEmployee
          : this.$resource['vi-VN'].formPopup.employee.titleEditEmployee
      "
      @closeForm="closeForm"
    >
      <template v-slot:header>
        <m-checkbox
          :value="isCustomer"
          @checkboxChangeValue="isCustomer = !isCustomer"
          style="padding: 0 1.5rem"
          >Là khách hàng</m-checkbox
        >
        <m-checkbox
          :value="isProvider"
          @checkboxChangeValue="isProvider = !isProvider"
          style="padding: 0 1.5rem"
          >Là nhà cung cấp</m-checkbox
        >
      </template>
      <template v-slot:main>
        <div class="basic-info">
          <div class="basic-info-left">
            <div class="flex-row">
              <m-text-field
                :haveLabel="true"
                :textFieldRequired="true"
                style="width: 48%"
                :isAutoFocused="true"
                :tabIndex="1"
                :valueInput="dataEmployee.EmployeeCode || ''"
                @inputOnChange="textFieldOnChange($event, 'EmployeeCode')"
              >
                <template v-slot:lbl-content>Mã</template>
                <template v-slot:warning>Mã không được để trống</template>
              </m-text-field>

              <m-text-field
                :haveLabel="true"
                :textFieldRequired="true"
                style="width: 48%"
                :tabIndex="2"
                :valueInput="dataEmployee.FullName"
                @inputOnChange="textFieldOnChange($event, 'FullName')"
              >
                <template v-slot:lbl-content>Tên</template>
                <template v-slot:warning>Tên không được để trống</template>
              </m-text-field>
            </div>

            <m-combobox
              :valueInputCombobox="dataEmployee.DepartmentName"
              :dataDropdownlist="listDepartment.map((e) => e.DepartmentName)"
              :comboboxRequired="true"
              :tabIndexInput="3"
              :tabIndexButton="4"
              @comboboxChangeValue="
                comboboxChangeValue($event, 'DepartmentName')
              "
            >
              <template v-slot:lbl-content>Đơn vị</template>
              <template v-slot:warning>Đơn vị không được để trống</template>
            </m-combobox>

            <m-text-field
              style="padding-top: 8px"
              :tabIndex="5"
              :valueInput="dataEmployee.PositionName"
              @inputOnChange="textFieldOnChange($event, 'PositionName')"
            >
              <template v-slot:lbl-content>Chức danh</template>
            </m-text-field>
          </div>
          <div class="basic-info-right">
            <div class="flex-row">
              <m-text-field
                :haveLabel="true"
                inputType="date"
                style="width: 33%"
                :tabIndex="6"
                :valueInput="
                  this.$helper.formatDateForInput(dataEmployee.DateOfBirth)
                "
                @inputOnChange="textFieldOnChange($event, 'DateOfBirth')"
              >
                <template v-slot:lbl-content>Ngày sinh</template>
              </m-text-field>

              <m-radio
                :tabIndexRadio="7"
                :indexSelected="dataEmployee.Gender"
                :listOptions="[
                  this.$resource['vi-VN'].gender.FEMALE,
                  this.$resource['vi-VN'].gender.MALE,
                  this.$resource['vi-VN'].gender.ORTHER,
                ]"
                @changeValue="radioChangeValue($event, 'Gender')"
                style="width: 66%; padding-left: 8px"
                >Giới tính</m-radio
              >
            </div>
            <div class="flex-row">
              <m-text-field
                lblTooltip="Số chứng minh nhân dân"
                style="width: 48%"
                :tabIndex="10"
                :valueInput="dataEmployee.IdentityNumber"
                @inputOnChange="textFieldOnChange($event, 'IdentityNumber')"
              >
                <template v-slot:lbl-content>Số CMND</template>
              </m-text-field>
              <m-text-field
                inputType="date"
                style="width: 48%"
                :tabIndex="11"
                :valueInput="
                  this.$helper.formatDateForInput(dataEmployee.IdentityDate)
                "
                @inputOnChange="textFieldOnChange($event, 'IdentityDate')"
              >
                <template v-slot:lbl-content>Ngày cấp</template>
              </m-text-field>
            </div>
            <m-text-field
              :tabIndex="12"
              :valueInput="dataEmployee.IdentityPlace"
              @inputOnChange="textFieldOnChange($event, 'IdentityPlace')"
            >
              <template v-slot:lbl-content>Nơi cấp</template>
            </m-text-field>
          </div>
        </div>
        <div class="contact-info">
          <m-text-field
            style="padding-top: 8px"
            :tabIndex="13"
            :valueInput="dataEmployee.Address"
            @inputOnChange="textFieldOnChange($event, 'Address')"
          >
            <template v-slot:lbl-content>Địa chỉ</template>
          </m-text-field>

          <div class="flex-row flex-left">
            <m-text-field
              style="padding-top: 8px"
              :tabIndex="14"
              :valueInput="dataEmployee.PhoneNumber"
              @inputOnChange="textFieldOnChange($event, 'PhoneNumber')"
              lblTooltip="Điện thoại di động"
            >
              <template v-slot:lbl-content>ĐT di động</template>
            </m-text-field>
            <m-text-field
              style="padding-top: 8px; padding-left: 8px"
              :tabIndex="15"
              :valueInput="dataEmployee.landlineNumber"
              @inputOnChange="textFieldOnChange($event, 'landlineNumber')"
              lblTooltip="Điện thoại cố định"
            >
              <template v-slot:lbl-content>ĐT cố định</template>
            </m-text-field>

            <m-text-field
              style="padding-top: 8px; padding-left: 8px"
              :tabIndex="16"
              :valueInput="dataEmployee.Email"
              @inputOnChange="textFieldOnChange($event, 'Email')"
            >
              <template v-slot:lbl-content>Email</template>
            </m-text-field>
          </div>

          <div class="flex-row flex-left">
            <m-text-field
              style="padding-top: 8px"
              :tabIndex="17"
              :valueInput="dataEmployee.accountNumber"
              @inputOnChange="textFieldOnChange($event, 'accountNumber')"
            >
              <template v-slot:lbl-content>Tài khoản ngân hàng</template>
            </m-text-field>
            <m-text-field
              style="padding-top: 8px; padding-left: 8px"
              :tabIndex="18"
              :valueInput="dataEmployee.bank"
              @inputOnChange="textFieldOnChange($event, 'bank')"
            >
              <template v-slot:lbl-content>Tên ngân hàng</template>
            </m-text-field>
            <m-text-field
              style="padding-top: 8px; padding-left: 8px"
              :tabIndex="19"
              :valueInput="dataEmployee.bankBranch"
              @inputOnChange="textFieldOnChange($event, 'bankBranch')"
            >
              <template v-slot:lbl-content>Chi nhánh</template>
            </m-text-field>
          </div>
        </div>
      </template>
      <template v-slot:footer>
        <div class="flex-row">
          <div class="left">
            <m-button @click="closeForm" class="m-btn-secondary" tabindex="20"
              >Hủy</m-button
            >
          </div>
          <div class="right">
            <m-button
              class="m-btn-secondary"
              style="margin-right: 12px; /* zero */"
              tabindex="21"
              @Click="saveForm"
              >Cất</m-button
            >
            <m-button tabindex="22" @click="saveAndCreateForm"
              >Cất và thêm</m-button
            >
          </div>
        </div>
      </template>
    </m-form>
  </div>
</template>
<script>
export default {
  name: "EmployeeDetail",
  emits: ["closeForm"],
  inject: ["toastSuccess200", "toastWarning", "showDialogError"],
  data() {
    return {
      isCustomer: false, // Là khách hàng
      isProvider: false, // Là nhà cung cấp

      // Dữ liệu nhân viên mẫu
      dataEmployee: {
        // EmployeeCode: "",
        // FullName: "",
        // Gender: -1,
        // DateOfBirth: "",
        // IdentityNumber: "",
        // IdentityDate: "",
        // IdentityPlace: "",
        // Address: "",
        // PositionName: "",
        // DepartmentName: "",
        // accountNumber: "",
        // bank: "",
        // bankBranch: "",
        // PhoneNumber: "",
        // landlineNumber: "",
        // Email: "",
      },

      // Danh sách tất cả đơn vị, phòng ban
      listDepartment: [],

      keyRenderForm: 0, // Đánh dấu key để re-render form
    };
  },
  props: {
    propEmployee: {
      // Truyền obj nhân viên
      type: Object,
      required: false,
    },
    // propEmployeeId: {
    //   // Truyền id nhân viên
    //   type: String,
    //   required: false,
    // },
    isFormAdd: {
      type: Boolean,
      required: true,
    },
  },

  created() {
    /* ======================== Sinh mã tự động nếu là thêm mới ========================= */
    if (this.isFormAdd) {
      this.generateEmployeeCode();
    }

    /* ======================== Load thông tin nhân viên ========================= */
    /**
     * Sao chép đối tượng được truyền vào
     * Author: NVDUNG (20/08/2023)
     */
    if (this.propEmployee) {
      this.dataEmployee = Object.assign({}, this.propEmployee);
      console.log(this.dataEmployee);
    }

    /**
     * Load dữ liệu nhân viên theo id
     * Author: NVDUNG (20/08/2023)
     */
    // if (this.propEmployeeId) {
    //   this.loadEmployeeById(this.propEmployeeId);
    //   console.log("Created ::::: ", this.dataEmployee);
    // }

    /* ======================== Load danh sách tất cả đơn vị, phòng ban ========================= */
    this.loadDepartment();
  },
  methods: {
    /* ======================== Load dữ liệu ========================= */
    /**
     * Load danh sách các đơn vị, phòng ban
     * Author: NVDUNG (20/08/2023)
     */
    async loadDepartment() {
      // Hiện loading
      this.$emitter.emit("showLoading", true);
      try {
        const url = "https://cukcuk.manhnv.net/api/v1/Departments";
        const method = "GET";
        const response = await this.$repository.callAPI(url, method);
        // console.log("response: ", response);
        // Truy cập dữ liệu thành công
        if (response.httpStatus === 200) {
          this.listDepartment = response.data;
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
     * Load nhân viên theo id
     * Author: NVDUNG (20/08/2023)
     */
    loadEmployeeById(id) {
      // Hiện loading
      this.$emitter.emit("showLoading", true);
      try {
        const url = "https://cukcuk.manhnv.net/api/v1/Employees/" + id;
        const method = "GET";
        const response = this.$repository.callAPI(url, method);
        // Truy cập dữ liệu thành công
        if (response.httpStatus === 200) {
          this.dataEmployee = response.data;
        }
        console.log("this.dataEmployee: ", this.dataEmployee);
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
    /* ======================== Tạo EmployeeCode ========================= */
    /**
     * Tạo EmployeeCode cho nhân viên khi thêm nhân viên
     * Author: NVDUNG (20/08/2023)
     */
    async generateEmployeeCode() {
      // Hiện loading
      this.$emitter.emit("showLoading", true);
      try {
        const url =
          "https://cukcuk.manhnv.net/api/v1/Employees/NewEmployeeCode";
        const method = "GET";
        const response = await this.$repository.callAPI(url, method);
        // Truy cập dữ liệu thành công
        if (response.httpStatus === 200) {
          console.log("MÃ mới:", response);
          this.dataEmployee.EmployeeCode = response.data;
          // Thay đổi keyRenderForm để re-render form
          if (this.keyRenderForm == 10) this.keyRenderForm = 0;
          else this.keyRenderForm++;
        }
      } catch (error) {
        // Xử lý lỗi
        // Tạo toast thông báo lỗi
        this.toastWarning("Lỗi sinh mã tự động.");
      }
      // Tắt loading
      this.$emitter.emit("showLoading", false);
    },

    /* ======================== Đóng form ========================= */
    /**
     * Gửi sự kiện closeForm cho component cha để đóng form
     * Author: NVDUNG (20/08/2023)
     */
    closeForm() {
      this.$emit("closeForm");
    },

    /* ======================== Binding data ========================= */

    /**
     * Nhận dữ liệu từ sự kiện inputOnChange để lưu vào data
     * Author: NVDUNG (20/08/2023)
     */
    textFieldOnChange(newValue, dataAttr) {
      try {
        this.dataEmployee[dataAttr] = newValue;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Cập nhật thông tin giới tính
     * Author: NVDUNG (20/08/2023)
     */
    radioChangeValue(newValue, dataAttr) {
      try {
        this.dataEmployee[dataAttr] = newValue;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Cập nhật thông tin department
     * Author: NVDUNG (20/08/2023)
     */
    comboboxChangeValue(newValue, dataAttr) {
      try {
        this.dataEmployee[dataAttr] = newValue;
        //console.log('Cập nhật thông tin department: ',this.dataEmployee[dataAttr]);
      } catch (error) {
        console.log(error);
      }
    },

    /* ======================= Lưu thông tin nhân viên ========================= */
    /**
     * Hàm post data nhân viên
     * Author: NVDUNG (20/08/2023)
     */
    async addEmployee() {
      // Hiện loading
      this.$emitter.emit("showLoading", true);
      try {
        const url = "https://cukcuk.manhnv.net/api/v1/Employees";
        const method = "POST";
        const data = {
          createdDate: new Date(),
          createdBy: "NVManh",
          employeeCode: this.dataEmployee.EmployeeCode,
          fullName: this.dataEmployee.FullName,
          gender: this.dataEmployee.Gender,
          dateOfBirth: new Date(this.dataEmployee.DateOfBirth),
          phoneNumber: this.dataEmployee.PhoneNumber,
          email: this.dataEmployee.Email,
          address: this.dataEmployee.Address,
          identityNumber: this.dataEmployee.IdentityNumber,
          identityDate: new Date(this.dataEmployee.IdentityDate),
          identityPlace: this.dataEmployee.IdentityPlace,
          joinDate: new Date(),
          martialStatus: 0,
          educationalBackground: 0,
          workStatus: 0,
          personalTaxCode: "string",
          salary: 0,
          positionName: this.dataEmployee.PositionName,
          departmentName: this.dataEmployee.DepartmentName,
        };
        const response = await this.$repository.callAPI(url, method, data);
        // console.log("response: ", response);
        // Truy cập dữ liệu thành công
        if (response.httpStatus === 200) {
          this.dataEmployees = response.data;
        }
      } catch (error) {
        // Xử lý lỗi
        console.error("Lỗi khi gọi API:", error);

        // Truy cập thông tin trạng thái HTTP và dữ liệu phản hồi trong trường hợp lỗi
        if (error.httpStatus === 201) {
          // Tạo thông báo toast
          this.toastSuccess200("Đã thêm một nhân viên mới.");
          // Đóng form
          this.closeForm();
        } else {
          // 400, 401, 402, 403, 404, 500
          // Tạo dialog thông báo
          //   console.error("Trạng thái HTTP:", error.httpStatus);
          //   console.error("Dữ liệu phản hồi:", error.data);
          this.showDialogError(
            error.userMsg != undefined
              ? error.userMsg
              : "Thông tin không hợp lệ, hãy kiểm tra lại."
          );
          console.error(error.devMsg);
        }
      }
      // Tắt loading
      this.$emitter.emit("showLoading", false);
    },
    /**
     * Hàm put data nhân viên
     * Author: NVDUNG (20/08/2023)
     */
    async updateEmployee(id) {
      // Hiện loading
      this.$emitter.emit("showLoading", true);
      try {
        const url = "https://cukcuk.manhnv.net/api/v1/Employees/" + id;
        const method = "PUT";
        const data = {
          createdDate: new Date(),
          createdBy: "NVManh",
          modifiedDate: new Date(),
          modifiedBy: "NVManh",
          employeeCode: this.dataEmployee.EmployeeCode,
          fullName: this.dataEmployee.FullName,
          gender: this.dataEmployee.Gender,
          dateOfBirth: new Date(this.dataEmployee.DateOfBirth),
          phoneNumber: this.dataEmployee.PhoneNumber,
          email: this.dataEmployee.Email,
          address: this.dataEmployee.Address,
          identityNumber: this.dataEmployee.IdentityNumber,
          identityDate: new Date(this.dataEmployee.IdentityDate),
          identityPlace: this.dataEmployee.IdentityPlace,
          joinDate: new Date(),
          martialStatus: 0,
          educationalBackground: 0,
          workStatus: 0,
          salary: 0,
          positionName: this.dataEmployee.PositionName,
          departmentName: this.dataEmployee.DepartmentName,
        };
        const response = await this.$repository.callAPI(url, method, data);
        // console.log("response: ", response);
        // Truy cập dữ liệu thành công
        if (response.httpStatus === 200) {
          // this.dataEmployees = response.data;
        }
        // Thông báo toast thành công
        this.toastSuccess200("Đã cập nhập nhân viên thành công.");
        // Đóng form
        this.closeForm();
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
          this.showDialogError(
            error.userMsg != undefined
              ? error.userMsg
              : "Thông tin không hợp lệ, hãy kiểm tra lại."
          );
          console.error(error.devMsg);
        }
      }
      // Tắt loading
      this.$emitter.emit("showLoading", false);
    },
    /**
     * Cất nhân viên
     */
    saveForm() {
      try {
        // Gọi api
        if (this.isFormAdd) this.addEmployee();
        else this.updateEmployee(this.dataEmployee.EmplyeeId);
      } catch (error) {
        console.error("Lỗi khi gọi API toang.................", error);
      }

      // Refresh data
      this.$emitter.emit("refreshData");
    },

    /**
     * Cất và thêm mới nhân viên
     */
    saveAndCreateForm() {
      try {
        // Gọi api
        if (this.isFormAdd) this.addEmployee();
        else this.updateEmployee(this.dataEmployee.EmplyeeId);
      } catch (error) {
        console.error("Lỗi khi gọi API toang:", error);
      }
    },
  },
};
</script>
<style scoped>
@import url(./employee-detail.css);
</style>
