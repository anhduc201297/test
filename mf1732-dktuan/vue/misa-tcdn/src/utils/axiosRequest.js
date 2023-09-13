import axios from "axios"
import helper from "@/js/MISAHelper"

const axiosRequest = {
    /**
 * post method
 * Author: dktuan (30/08/2023)
 */
    post: async (path, data) => {
        try {
            const res = await axios.post(process.env.VUE_APP_BASE_URL + path, data)
            return res.data
        } catch (error) {
            return helper.handleErrorRes(path, error)
        }
    },
    /**
     * get method
     * Author: dktuan (30/08/2023)
     */
    get: async (path) => {
        try {
            const res = await axios.get(process.env.VUE_APP_BASE_URL + path)
            return res.data
        } catch (error) {
            return helper.handleErrorRes(path, error)
        }
    }
}

export default axiosRequest