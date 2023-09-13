class Common {
    /**
     * Lưu các contains của trang web
     * Author: NTVu - MF1742
     * Modified: NTVu - 16/08/2023
     */
    static fieldLabels = {
        genders: [
            { label: 'Nam', value: 'male' },
            { label: 'Nữ', value: 'female' },
            { label: 'Khác', value: 'other' },
        ],
    };

    /**
     * Convert date to String
     * Author: NTVu - MF1742
     * Modified: NTVu - 15/08/2023
     * @param {string} dateString
     * @returns {string}
     */
    static convertDate(dateString) {
        const date = new Date(dateString);

        const day = date.getDay() > 9 ? date.getDay() : `0${date.getDay() + 1}`;
        const month =
            date.getMonth() > 8
                ? date.getMonth() + 1
                : `0${date.getMonth() + 1}`;

        return `${date.getFullYear()}-${month}-${day}`;
    }

    /**
     * Hiển thị loading
     * Author: NTVu - MF1742
     * Modified: NTVu - 15/08/2023
     */
    static onLoading() {
        $('#loading').css('visibility', 'visible');
    }
    /**
     * Hidden loading
     * Author: NTVu - MF1742
     * Modified: NTVu - 15/08/2023
     */
    static endLoading() {
        $('#loading').css('visibility', 'hidden');
    }

    /**
     * Loại bỏ dấu tiếng Việt
     * Author: NTVu - MF1742
     * Modified: NTVu - 16/08/2023
     * @param {string} str
     * @returns {string}
     */
    static convertToUnaccented(str) {
        return str
            .normalize('NFD') // Tách ra thành ký tự base và dấu
            .replace(/[\u0300-\u036f]/g, '') // Loại bỏ dấu
            .toLowerCase(); // Chuyển thành chữ thường
    }
}
