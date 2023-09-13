const helper = {

  /**
   * Hàm định dạng ngày/tháng/năm
   * @param {date}
   * CreateBy : NTKMY (24/08/2023)
   */
  formatDate(date) {
    try {
      date = new Date(date);
      let dateValue = date.getDate();
      let monthValue = date.getMonth() + 1;
      let yearValue = date.getFullYear(); 
      return `${dateValue}/${monthValue}/${yearValue}`;
    } catch (error) {
      console.error(error); 
      return "";
    }
  },
};

export default helper;