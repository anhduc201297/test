const helper = {
  /**
   * Author: Minh Đăng 27/08/2023
   * Feat: Định dạng lại ngày tháng năm từ datetime sang dd/mm/yyyy
   * @param {*} date
   * @returns
   */
  formatDate(date) {
    if (!date) {
      return "Không có";
    }
    const dateUTC = new Date(date);
    if (isNaN(dateUTC.getDate())) {
      return "";
    }
    let day = dateUTC.getDate();
    let month = dateUTC.getMonth() + 1;
    let year = dateUTC.getFullYear();
    if (day < 10) {
      day = "0" + day;
    }
    if (month < 10) {
      month = "0" + month;
    }
    return `${day}/${month}/${year}`;
  },

  /**
   * Author: Minh Đăng 28/03/2023
   * Feat: Chuyển ngày dạng dd/mm/yyyy sang yyyy-mm-ddT00:00:00
   */
  convertDateToDateTime(date) {
    let day = date.slice(0, 2);
    let month = date.slice(3, 5);
    let year = date.slice(6, 8);
    return `${year}-${month}-${day}T00:00:00`;
  },
};

export default helper;
