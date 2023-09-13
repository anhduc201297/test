const MISAHelper = {
  /**
   * Get property from value in Object
   * <value>: String
   * <object>: Object
   * Created by: mf1733-gtnghia - 30/8/2023
   */
  getPropertyFromValue(value, object) {
    for (const prop in object) {
      if (object[prop] === value) {
        return prop;
      }
    }
    return null;
  },
  /**
   * format date from Date Object to `dd/mm/yyyy`
   * <date>: Date
   * Created by: mf1733-gtnghia - 24/8/2023
   */
  formatDate(date) {
    date = new Date(date);
    let d = date.getDate();
    let m = date.getMonth() + 1;
    let y = date.getFullYear();
    return `${d}/${m < 9 ? "0" + m : m}/${y}`;
  },
  /**
   * format Gender from number to String
   * <gender>: Number
   * Created by: mf1733-gtnghia - 24/8/2023
   */
  formatGender(gender) {
    if (gender === 0) {
      return "Nữ";
    } else if (gender === 1) {
      return "Nam";
    } else {
      return "Khác";
    }
  },
  /**
   * handle response when call API
   * <error>: error response from server
   * Created by: mf1733-gtnghia - 31/8/2023
   */
  handleErrorResponse(error) {
    console.log(error);
    const errorCode = error.response.status;
    const errorMsg = error.response.data.userMsg;
    switch (errorCode) {
      case 500:
        return {
          status: "error",
          title: "Thất bại",
          message: errorMsg,
        };
      case 400:
        return {
          status: "error",
          title: "Thất bại",
          message: errorMsg,
        };
      case 401:
        return {
          status: "error",
          title: "Thất bại",
          message: errorMsg,
        };
      case 403:
        return {
          status: "error",
          title: "Thất bại",
          message: errorMsg,
        };
      case 404:
        return {
          status: "error",
          title: "Thất bại",
          message: errorMsg,
        };
      default:
        break;
    }
  },
};

export default MISAHelper;
