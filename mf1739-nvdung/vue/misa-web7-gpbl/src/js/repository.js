import axios from "axios";
const repository = {
  /**
   * Hàm chung để gọi API theo chuẩn RESTful trả về promise
   *
   * @param {string} url - URL của API.
   * @param {string} method - Phương thức HTTP (GET, POST, PUT, DELETE).
   * @param {object} data - Dữ liệu gửi lên (chỉ dùng cho POST và PUT).
   * @returns {Promise} - Trả về một Promise với kết quả của API call.
   */
  callAPI(url, method, data = null) {
    return new Promise((resolve, reject) => {
      axios({
        method: method,
        url: url,
        data: data,
      })
        .then((response) => {
          console.log(response);
          // Kiểm tra trạng thái HTTP
          if (response.status === 200)
            // callback resolved
            resolve({ httpStatus: response.status, data: response.data });
          else {
            // Trả về trạng thái HTTP và dữ liệu phản hồi
            const errorObject = {
              httpStatus: response.status,
              messageData: response.data,
            };
            // callback reject
            reject(errorObject);
          }
        })
        .catch((error) => {
          console.log(error);
          const errorObject = {
            httpStatus: error.response.status,
            devMsg: error.response.data.devMsg,
            userMsg: error.response.data.userMsg,
          }
          reject(errorObject);
        });
    });
  },
};

export default repository;
