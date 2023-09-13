import MISAEnum from './MISAEnum'
// import MISAResource from './MISAResource'

const helper = {
    /**
     * author: ttkien
     * 29/08/2023
     * định dạng gới tính
     */
    formatGender(value) {
        if (value === MISAEnum.genderValue.female) return `Nữ`
        else if (value === MISAEnum.genderValue.male) return `Nam`
        else if (value === MISAEnum.genderValue.other) return `Khác`
        else return null
    },
    /**
     * author: ttkien
     * 29/08/2023
     * định dạng dữ liệu ngày tháng năm
     */
    formatDate(date) {
        if (date === null) return null
        else {
            date = new Date(date)
            let dateValue = date.getDate()
            dateValue = dateValue < 10 ? `0${dateValue}` : dateValue
            let monthValue = date.getMonth() + 1
            monthValue = monthValue < 10 ? `0${monthValue}` : monthValue
            let yearValue = date.getFullYear()
            return `${dateValue}/${monthValue}/${yearValue}`
        }
    },
    /**
     * author: ttkien
     * 31/08/2023
     * chuyển đổi ngày tháng năm thành date time
     */
    convertToDateTime(date) {
        if (date === null) return null
        else {
            let dateValue = date.slice(0, 2)
            let monthValue = date.slice(3, 5)
            let yearValue = date.slice(6, 10)
            return `${yearValue}-${monthValue}-${dateValue}T00:00:00`
        }
    },
    /**
     * author: ttkien
     * 31/08/2023
     * hàm lấy dữ liệu nhân viên
     */
    loadData() {
        this.$axios.get("https://cukcuk.manhnv.net/api/v1/Employees")
            .then(res => {
                return res.data
            }).catch(this.$helper.showError)
    },
    /**
     * author: ttkien
     * 31/08/2023
     * hàm xử lí lỗi
     */
    showError(error) {
        let status = error.response.status;
        let userMsg = error.response.data.userMsg;
        console.log("error: ", error);
        switch (status) {
            case 400:
                if (userMsg === undefined) {
                    userMsg = "Thông tin không hợp lệ. Vui lòng kiểm tra lại."
                }
                console.log("Nội dung lỗi:", userMsg)
                break;
            case 500:
                console.log("Nội dung lỗi:", userMsg)
                break;
            default:
                break;
        }
    }
}

export default helper