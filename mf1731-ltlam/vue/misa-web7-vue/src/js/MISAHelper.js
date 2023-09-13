const MISAHelper = {
  // Ham dinh danh ngay thang nam
  formatDate(date) {
    try {
      date = new Date(date);
      let dateValue = date.getDate();
      let monthValue = date.getMonth() + 1;
      let year = date.getFullYear();
      return `${dateValue}/${monthValue}/${year}`;
    } catch (error) {
      console.error(error);
      return "";
    }
  },
  // onShowErrorHadle(error) {
  //   console.log("Hello");
  //   const code = error.response.status;
  //   const msg = error.response.data.userMsg;
  //   switch (code) {
  //     case 500:
  //       this.onShowError(code, msg);

  //       break;
  //     case 400:
  //       this.onShowError(code, msg);

  //       break;
  //     case 300:
  //       this.onShowError(code, msg);

  //       break;
  //     case 200:
  //       this.onShowError(code, msg);
  //       break;
  //     default:
  //       // Xử lý khi có lỗi khác
  //       this.onShowError(code, msg);
  //       break;
  //   }
  //   // this.contentDialog = error;
  //   console.log("error: ", error);
  // },
};

export default MISAHelper;
