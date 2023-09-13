const helper = {
  /**
   * Fomat dữ liệu ngày tháng
   * Author: NVDUNG (20/08/2023)
   */
  formatDate(date) {
    try {
      if (!date) return "";

      var d = new Date(date),
        month = "" + (d.getMonth() + 1),
        day = "" + d.getDate(),
        year = d.getFullYear();
      return day.padStart(2, "0") + "/" + month.padStart(2, "0") + "/" + year;
    } catch (error) {
      console.log(error);
    }
  },

  /**
   * Format dữ liệu yyyy-mm-dd để set value cho input
   * Author: NVDUNG (20/08/2023)
   */
  formatDateForInput(date) {
    try {
      if (!date) return "";

      var d = new Date(date),
        month = "" + (d.getMonth() + 1),
        day = "" + d.getDate(),
        year = d.getFullYear();
      return year + "-" + month.padStart(2, "0") + "-" + day.padStart(2, "0");
    } catch (error) {
      console.log(error);
    }
  },
};

export default helper;
