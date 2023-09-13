const helper = {
    /**
     * So sánh ngày được truyền vào với ngày hiện tại
     * @author NTVu - 02/09/2023
     * @param {Date} date
     * @returns true - Ngày hiện tại lớn hơn
     */
    compareDateWithNow: (date) => {
        const currentDate = new Date();
        const parsedInputDate = new Date(date);
        if (currentDate > parsedInputDate) {
            return true;
        } else {
            return false;
        }
    },

    /**
     * @description Format date về dạng YYYY-MM-DD với múi giờ Việt Nam (VN)
     * @Author NTVu - 20/08/2023
     * @param {Date} dateInput
     * @param {String} formatType
     * @returns
     */
    formatDate(dateInput, formatType) {
        const date = new Date(dateInput);
        const options = {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
            hour: 'numeric',
            minute: 'numeric',
            second: 'numeric',
            timeZoneName: 'short'
        };

        // Sử dụng toLocaleString() với các tùy chọn để định dạng và bảo tồn múi giờ Việt Nam (VN)
        const formattedDate = date.toLocaleString('vi-VN', options);

        // Lấy thông tin ngày, tháng, năm từ chuỗi định dạng
        const [, , dateOutput] = formattedDate.split(' ');
        const [day, month, year] = dateOutput.split('/');

        let dateStringOut = formatType.replace('dd', day);
        dateStringOut = dateStringOut.replace('yyyy', year);
        dateStringOut = dateStringOut.replace('MM', month);
        return dateStringOut;
    },
    /**
     * Chuyển một string sang dạng capitalize
     * @Author NTVu - MF1742 (21/08/2023)
     * @param {string} str
     * @returns {string}
     */
    capitalizeString: (str) =>
        str[0].toUpperCase() + str.slice(1).toLowerCase(),
    /**
     * Hàm thực hiện trì hoãn thực hiện func trong thời gian delay
     * @Author NTVu - MF1742 (21/08/2023)
     * @param {Function} func
     * @param {Number} delay
     * @returns {Function}
     */
    debounce(func, delay) {
        let timer;
        return function () {
            clearTimeout(timer);
            timer = setTimeout(() => {
                func.apply(this, arguments);
            }, delay);
        };
    },
    /**
     * Xóa dấu tiếng Việt
     * @Author NTVu - 24/08/2023
     * @param {string} str
     * @returns
     */
    removeVietnameseTone(str) {
        return str
            .normalize('NFD') // Tách ra thành ký tự base và dấu
            .replace(/[\u0300-\u036f]/g, '') // Loại bỏ dấu
            .toLowerCase(); // Chuyển thành chữ thường
    },
    /**
     * Hàm viết tắt chuỗi string
     * @author NTVu - 02/09/2023
     * @param {string} inputString
     * @returns
     */
    abbreviateString(inputString) {
        if (!inputString) return '';
        // Tách chuỗi đầu vào thành mảng các từ
        const words = inputString.split(' ');

        // Khởi tạo biến để lưu kết quả viết tắt
        let abbreviation = '';

        // Lặp qua từng từ và lấy ký tự đầu tiên của mỗi từ
        for (const word of words) {
            abbreviation += word.charAt(0).toUpperCase(); // Chuyển ký tự đầu tiên thành chữ hoa
        }

        return abbreviation;
    }
};

export default helper;
