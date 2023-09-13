export default {
    /**
     * Kiểm tra điều kiện required
     * @Author NTVu - MF1742 (20/08/2023)
     * @param {string?} value
     * @returns {boolean}
     */
    required: (value) => !!value && !!value.trim(),
    /**
     * Kiểm tra điều kiện minLength
     * @Author NTVu - MF1742 (20/08/2023)
     * @param {string} value
     * @param {number, string} minLength
     * @returns {boolean}
     */
    min: (value, minLength) =>
        value && minLength && value.length >= parseInt(minLength),
    /**
     * Kiểm tra điều kiện maxLength
     * @Author NTVu - MF1742 (20/08/2023)
     * @param {string} value
     * @param {number, string} maxLength
     * @returns {boolean}
     */
    max: (value, maxLength) =>
        value && maxLength && value.length <= parseInt(maxLength),
    /**
     * @description Kiểm tra format dạng email, number
     * @description Nếu là dạng khác thì phải tự định nghĩa dạng muốn format
     * @Author NTVu - MF1742 (20/08/2023)
     * @param {string} value
     * @param {string} formatType
     * @returns {boolean}
     */
    format: (value, formatType) => {
        if (formatType === 'email') {
            const emailRegex = /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/;
            return emailRegex.test(value);
        } else if (formatType === 'number') {
            const phoneRegex = /^[0-9]{10}$/;
            return phoneRegex.test(value);
        } else if (formatType === 'endWithNum') {
            const phoneRegex = /\d$/;
            return phoneRegex.test(value);
        }
        return value && formatType && formatType.test(value);
    },
    /**
     * Kiểm tra value có nằm trong khoảng giá trị đã cho
     *   @Author NTVu - MF1742 (20/08/2023)
     * @param {string} value
     * @param {Array} options
     * @returns {boolean}
     */
    inOptions: (value, options) => value && options && options.includes(value),
    /**
     * So sánh ngày input có sớm hơn ngày target không
     * @author NTVu - 02/09/2023
     * @param {Date} dateInput
     * @param {Date} dateTarget
     * @returns {boolean}
     */
    earlier: (dateInput, dateTarget) => {
        const date1 = new Date(dateInput);
        const date2 = new Date(dateTarget);
        if (date1 > date2) {
            return false;
        }
        return true;
    },
    /**
     * So sánh ngày input có muộn hơn ngày target không
     * @author NTVu - 02/09/2023
     * @param {Date} dateInput
     * @param {Date} dateTarget
     * @returns {boolean}
     */
    later: (dateInput, dateTarget) => {
        const date1 = new Date(dateInput);
        const date2 = new Date(dateTarget);
        if (date1 > date2) {
            return true;
        }
        return false;
    }
};
