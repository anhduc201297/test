const helper = {
     /**
     * định dạng ngày tháng
     * Author: dktuan (25/08/2023)
     */
    formatDate: (date) => {
        if(date)
        return date.substring(0,10)
        else return null;
    },
    /**
     * xử lý lỗi khi gửi request
     * Author: dktuan (30/08/2023)
     */
    handleErrorRes: (path, error) => {
        // const code = error.response.status;
        const title = error.response.data.userMsg;
        const msgObj = Object.values(error.response.data.data)[0];
        const msg = Array.isArray(msgObj)?msgObj[0]:msgObj ;
        const dialogState = {
            error: true,
            show: true,
            msg: msg,
            title: title
        }
        return dialogState;
    }
}

export default helper