class EmployeeListHandler {
  constructor() {}
  /**
   * Author: Minh Đăng
   * Function: Đóng form  thêm mới nhân viên 20/8/2023
   * */
  hideEmpDetail() {
    this.isShowEmpDetail = false;
    this.isEmpSelectFuncId = false;
    // this.$router.push("/employees");
  }
  /**
   * Author: Minh Đăng
   * Function: Mở form  thêm mới nhân viên 20/8/2023
   * */
  showEmpDetail() {
    // this.$router.push("/employees/form");
    this.isShowEmpDetail = true;
    this.empDetail = {};
    this.empFormType = "add";
  }

  /**
   * Author: Minh Đăng
   * Function: Mở form  thêm mới nhân viên 20/8/2023
   * */
  showChangeEmpDetail(emp) {
    this.empFormType = "change";
    this.empDetail = emp;
    this.isShowEmpDetail = true;
    this.empDetailTitle = "Thông tin nhân viên";
    // this.indexEmpToChange = index;
    // this.$router.push(`/employees/form/${emp.EmployeeId}`);
    // this.$emitter.emit("empDetailChanged", this.empDetail);
  }

  /**
   * Author: Minh Đăng
   * Function: Mở phần chọn chức năng với nhân viên 20/8/2023
   * */
  clickEmpSelectFunc(index) {
    // e.stopPropagation();
    // console.log(index);
    const prevValue = this.isShowEmpSelectFuncArray[index];
    this.isShowEmpSelectFuncArray = new Array(
      this.isShowEmpSelectFuncArray.length
    ).fill(false);
    this.isShowEmpSelectFuncArray[index] = prevValue;
    this.isShowEmpSelectFuncArray[index] =
      !this.isShowEmpSelectFuncArray[index];
    this.empSelectFuncId = index;
  }

  /**
   * Author: Minh Đăng
   * Function: Ẩn phần chọn chức năng với nhân viên  20/8/2023
   * */
  hideEmpSelectFunc(empSelectFuncId) {
    // console.log(empSelectFuncId);
    this.isShowEmpSelectFuncArray[empSelectFuncId] = false;
    empSelectFuncId = -1;
  }
  /**
   * Author: Minh Đăng
   * Function: Mở phần chọn số nhân viên trong bảng 20/8/2023
   * */
  clickEmpPerPageList() {
    this.isShowEmpPerPageList = !this.isShowEmpPerPageList;
  }
  /**
   * Author: Minh Đăng 30/08/2023
   * Feat: Xử lý error khi request axios bị lỗi (400, 500)
   * @param {*} error
   */
  axiosEmployeeErrorHandler(error) {
    this.isLoading = false;
    console.log(error);
    switch (error.response.status) {
      case 500:
        this.isShowNotificationDialog = true;
        this.dialogContent =
          this.$resource[
            this.$languageCode
          ].DialogContent.EmployeeForm.ErrorInput.InvalidField;
        this.dialogContent.Message = error.response.data.userMsg;
        // console.log(this.dialogContent)
        break;
      case 400:
        this.isShowNotificationDialog = true;
        this.dialogContent =
          this.$resource[
            this.$languageCode
          ].DialogContent.EmployeeForm.ErrorInput.InvalidField;
        this.dialogContent.Message = error.response.data.userMsg;
        // console.log(this.dialogContent)
        break;
      case 404:
        this.isShowNotificationDialog = true;
        this.dialogContent =
          this.$resource[
            this.$languageCode
          ].DialogContent.EmployeeForm.NotFound;
        break;
      case 401:
        this.isShowNotificationDialog = true;
        this.dialogContent =
          this.$resource[this.$languageCode].DialogContent.InvalidSignup;
        break;
    }
  }
  /**
   * Author: Minh Đăng
   * Function: Xóa 1 nhân viên theo id 23/8/2023
   * */
  async deleteEmpById(id) {
    try {
      this.isLoading = true;
      await this.$axios.delete(
        `https://cukcuk.manhnv.net/api/v1/employees/${id}`
      );
      this.isLoading = false;
      this.isShowToastMessage = true;
      this.toastMesContent =
        this.$resource[this.$languageCode].ToastMesContent.Success;
      setTimeout(this.closeToastMessage, 5000);
    } catch (error) {
      this.axiosEmployeeErrorHandler(error);
    }
  }

  /**
   * Author: Minh Đăng
   * Function: Chọn và bỏ chọn tất cả nhân viên trong bảng 24/8/2023
   * */
  selectAllEmp() {
    this.numEmpSelected = this.numEmpsDataShow;
    this.isEmpSelected = new Array(this.numEmpsDataShow).fill(true);
    this.isSelectAllEmp = true;
    this.checkAllEmpClass = "tickv-circle";
  }

  /**
   * Author: Minh Đăng
   * Function: Chọn hoặc bỏ chọn 1 nhân viên 24/8/2023
   * @param(id)
   * */
  selectOneEmp(index) {
    console.log(index);
    this.isEmpSelected[index] = !this.isEmpSelected[index];
    // Đếm số nhân viên đang được chọn
    this.numEmpSelected = this.isEmpSelected.reduce(function (
      prevNum,
      currentValue
    ) {
      return currentValue ? prevNum + 1 : prevNum;
    },
    0);

    // Nếu tất cả nhân viên được chọn
    if (this.numEmpSelected === this.numEmpsDataShow) {
      this.isSelectAllEmp = true;
      this.checkAllEmpClass = "tickv-circle";
    }
    if (this.numEmpSelected < this.numEmpsDataShow) {
      this.isSelectAllEmp = false;
    }
    if (this.numEmpSelected >= 2 && !this.isSelectAllEmp) {
      this.isEmpBatchExe = true;
      this.checkAllEmpClass = "ti-minus";
      // this.isSelectAllEmpCheckboxChecked = true;
    } else if (this.numEmpSelected <= 1) {
      this.isEmpBatchExe = false;
      this.isSelectAllEmp = false;
      this.checkAllEmpClass = "";
    }
  }

  /**
   * Author: Minh Đăng 24/8/2023
   * Feat: Thay đổi số nhân viên trong bảng
   * @param {num}
   */
  changeNumEmpsDataShowed(num) {
    this.numEmpsDataShow = num;
    this.empData = this.displayEmpByNum();
    // console.log(num);
  }

  /**
   * Author: Minh Đăng 24/08/2023
   * Feat: Bỏ chọn tất cả các nhân viên đang được chọn
   */
  unselectedAllEmp() {
    this.isSelectAllEmp = false;
    this.isEmpBatchExe = false;
    this.isEmpSelected = new Array(this.isEmpSelected.length).fill(false);
    this.numEmpSelected = 0;
  }

  /**
   * Author: Minh Đăng 24/08/2023
   * Feat: Thông báo xác nhận xóa tất cả nhân viên được chọn
   */
  deleteAllEmpSelected() {
    this.isShowAcceptDialog = true;
    this.dialogContent.Title = "Xóa tất cả nhân viên đã chọn?";
    this.dialogContent.Message = "Thao tác này không thể khôi phục được.";
    this.dialogContent.Icon = "icon__bigwarning";
    this.dialogContent.Cancel = "Hủy";
    this.dialogContent.Accept = "Đồng ý";
  }

  /**
   * Author: Minh Đăng 24/08/2023
   * Feat: Đóng thông báo dialog
   */
  closeDialog() {
    this.isShowDialog = false;
  }
}
module.exports = new EmployeeListHandler();
