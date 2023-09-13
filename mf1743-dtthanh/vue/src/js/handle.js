import emitter from 'tiny-emitter/instance'
import resources from './resources';

const handle = {
    /**
     * Hiển thị Dialog với thông tin trả về từ api
     * AUTHOR: DTTHANH (30/08/2023)
     */
    showDialog(title, errorMsg, type) {
        emitter.emit("showDialog", title, errorMsg, type);
    },
  

    /**
     * Xử lý lỗi api trả về
     * AUTHOR: DTTHANH (30/08/2023)
     */
    handleAPIError(res) {
        if (res.response) {
            const statusCode = res.response.status;
            const msg = res.response.data.userMsg;
            switch (statusCode) {
                case 500:
                    this.showDialog(
                        resources['VN'].Dialog.Notify,
                        [msg],
                        3
                    );
                    break;
                case 400:
                    this.showDialog(
                        resources['VN'].Dialog.Invalid,
                        [msg],
                        3
                    );
                    break;
                default:
                    break;
            }
        }
    },
};

export default handle;
