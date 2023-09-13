// import axios from "@/js/axios";
class EmployeeApi {
  constructor() {
    // this.employeeBaseUrlApi = "https://cukcuk.manhnv.net/api/v1/Employees";
  }

  /**
   * Author: Minh Đăng 07/09/2023
   * Feat: Lấy toàn bộ dữ liệu nhân viên
   */
  async getAllEmployees() {
    try {
      this.$emitter.emit("onShowLoading");
      const respone = await this.$axios.get(this.employeeBaseUrlApi);
      this.allEmpDatas = respone.data;
      // console.log(this.allEmpDatas);
      this.empDataShow = this.allEmpDatas.slice(0, this.numEmpsDataShow);
      this.isShowEmpSelectFuncArray = new Array(this.empDataShow.length).fill(
        false
      );
      this.isEmpSelected = new Array(this.empDataShow.length).fill(false);
      this.isLoading = false;
      this.$emitter.emit("onHideLoading");
    } catch (error) {
      this.$emitter.emit("onHideLoading");
      console.log(error);
      // console.log(error.response.data);
      let dialogContent =
        this.$resource[this.$languageCode].DialogContent.Error;
      dialogContent.Message =
        error.response?.data?.userMsg || dialogContent.Message;
      dialogContent.Icon = "icon__error";
      this.$emitter.emit("setDialogContent", dialogContent);
      this.$emitter.emit("onShowNotificationDialog");
    }
  }

  async getDepartmentInformation() {}

  /**
   * Author: Minh Đăng 4/9/2023
   * Feat: Thêm mới 1 nhân viên (kiểm tra sơ bộ dữ liệu đầu vào, post lên server,... )
   */
  async createOneEmp() {
    console.log(this.empFormData);
    if (
      !this.empFormData.EmployeeCode ||
      !this.empFormData.DepartmentName ||
      !this.empFormData.FullName
    ) {
      if (!this.empFormData.EmployeeCode) {
        this.missingInput.EmployeeCode = true;
      }
      if (!this.empFormData.DepartmentName) {
        this.missingInput.DepartmentName = true;
      }
      if (!this.empFormData.FullName) {
        this.missingInput.FullName = true;
      }
      // console.log(this.missingInput);
      this.empFormData.GenderName =
        this.$baseEnum[this.$languageCode][this.empFormData.Gender];
      this.$emitter.emit(
        "setDialogContent",
        this.$resource[this.$languageCode].DialogContent.MissingInput
      );
      this.$emitter.emit("onShowNotificationDialog");
      return;
    }
    try {
      this.$emitter.emit("onShowLoading");

      const response = await this.$axios.post(
        this.employeeBaseUrlApi,
        this.empFormData
      );
      console.log(response);
      this.$emitter.emit("onSaveOneEmployeeSuccess");

      this.onCloseEmpDetail();
      this.$emitter.emit("onHideLoading");
      if (response.status == 201) {
        // this.$emitter.emit(
        //   "setToastMessageContent",
        //   this.$resource[this.$languageCode].ToastMessageContent.EmployeeForm
        //     .AddSuccess
        // );
        this.$emitter.emit(
          "createNewToastMessage",
          this.$resource[this.$languageCode].ToastMessageContent.EmployeeForm
            .AddSuccess
        );
        // this.$emitter.emit("onShowToastMessage");
      }
    } catch (error) {
      this.$emitter.emit("onHideLoading");
      console.log(error);
      let dialogContent =
        this.$resource[this.$languageCode].DialogContent.Error;
      dialogContent.Message =
        error.response?.data?.userMsg || dialogContent.Message;
      dialogContent.Icon = "icon__error";
      this.$emitter.emit("setDialogContent", dialogContent);
      this.$emitter.emit("onShowNotificationDialog");
    }
  }
  /**
   * Author: Minh Đăng 4/9/2023
   * Feat: Thêm mới 1 nhân viên (kiểm tra sơ bộ dữ liệu đầu vào, post lên server,... )
   */
  async createOneEmpAndSaveNew() {
    console.log(this.empFormData);
    if (
      !this.empFormData.EmployeeCode ||
      !this.empFormData.DepartmentName ||
      !this.empFormData.FullName
    ) {
      if (!this.empFormData.EmployeeCode) {
        this.missingInput.EmployeeCode = true;
      }
      if (!this.empFormData.DepartmentName) {
        this.missingInput.DepartmentName = true;
      }
      if (!this.empFormData.FullName) {
        this.missingInput.FullName = true;
      }
      console.log(this.missingInput);
      this.$emitter.emit(
        "setDialogContent",
        this.$resource[this.$languageCode].DialogContent.MissingInput
      );
      this.$emitter.emit("onShowNotificationDialog");
      return;
    }
    try {
      this.$emitter.emit("onShowLoading");
      const respone = await this.$axios.post(
        // this.employeeBaseUrlApi,
        "https://localhost:7214/api/v1/Employees",
        this.empFormData
      );
      this.$emitter.emit("onHideLoading");
      if (respone.status == 201) {
        this.$emitter.emit(
          "setToastMessageContent",
          this.$resource[this.$languageCode].ToastMessageContent.EmployeeForm
            .AddSuccess
        );
        this.$emitter.emit("onShowToastMessage");
        this.empFormData = {};
      }
    } catch (error) {
      this.$emitter.emit("onHideLoading");
      console.log(error);
      let dialogContent =
        this.$resource[this.$languageCode].DialogContent.Error;
      dialogContent.Message =
        error.response?.data?.userMsg || dialogContent.Message;
      dialogContent.Icon = "icon__error";
      this.$emitter.emit("setDialogContent", dialogContent);
      this.$emitter.emit("onShowNotificationDialog");
    }
  }
  /**
   * Author: Minh Đăng 07/09/2023
   * Feat: Thay đổi một nhân viên
   */
  changeOneEmp() {
    this.$emitter.emit("createNewToastMessage", {
      Status: "Thành công!",
      Desc: "Thêm mới một nhân viên thành công.",
      TextClass: "text-50b83c",
      OptionAction: "Hoàn tác",
      Icon: "icon__success",
    });
  }

  /**
   * Author: Minh Đăng 07/09/2023
   * Feat: Bấm nút cất chung cho form, type = "add" => thêm mới nhân viên,
   */
  clickSave() {
    if (this.type === "add") {
      this.createOneEmp();
    } else this.changeOneEmp();
  }
}

export default new EmployeeApi();
