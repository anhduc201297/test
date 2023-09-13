const helper = {

    /**
     * Định dàng ngày tháng năm
     * @param {*} date 
     * CreatedByL VTLam (24/08/2023)
     */
    formatDate(date){
        try {
            date = new Date(date);
            let dateValue = date.getDate();
            let monthValue = date.getMonth()+1;
            let yearValue = date.getFullYear();
            return `${dateValue}/${monthValue}/${yearValue}`;
        } catch (error) {
            console.error(error);
            return "";
        }
    },

    /**
    * Xử lí lỗi
    * @param {*} error
    * CreatedBy: VTLam (30/08/2023)
    */
    handleException(error){
        console.log(error.response.status);
        console.log(error.response.data.devMsg);
        return error.response.data.userMsg;
    }
}

export default helper