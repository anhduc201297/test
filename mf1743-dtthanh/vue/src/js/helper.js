const helper = {
    /**
     * Hàm định dạng ngày tháng năm
     * @param {*} date 
     * @returns 
     * AUTHOR: DTTHANH (24/08/2023)
     */
    formatDate(date) {
        try {
            date = new Date(date);
            let dateValue = String(date.getDate()).padStart(2,'0');
            let monthValue = String(date.getMonth() + 1).padStart(2,'0');
            let yearValue = date.getFullYear();
            return `${dateValue}/${monthValue}/${yearValue}`;
        } catch (error) {
            console.log(error);
            return "";
        }
    },

    formatGender(gender) {
        if (gender == 1) return "Nam"
        else if (gender == 2) return "Nữ"
        else return ""
    },

}

export default helper;