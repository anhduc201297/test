import axios from 'axios';
const employeeApi= {
    apiUrl: 'https://cukcuk.manhnv.net/api/v1/Employees',

    /**
     * Get employee
     * @returns response
     * AUTHOR: DTTHANH (06/09/2023)
     */
    async getEmployee() {
        const response = await axios.get(this.apiUrl);
        return response;
    },

    /**
     * Add employee
     * AUTHOR: DTTHANH (06/09/2023)
     * @param {object} employee
     * @returns response
     */
    async postEmployee(employee) {
        const response = await axios.post(this.apiUrl, employee);
        return response;
    },

    /**
     * Update employee
     * AUTHOR: DTTHANH (06/09/2023)
     * @param {string} id
     * @param {object} employee
     * @returns response
     */
    async putEmployee(id, employee) {
        const response = await axios.post(`${this.apiUrl}/${id}`, employee);
        return response;
    },

    /**
     * Delete employee
     * AUTHOR: DTTHANH (06/09/2023)
     * @param {String} id 
     * @returns response
     */
    async deleteEmployee(id) {
        const response = await axios.delete(`${this.apiUrl}/${id}`)
        return response
    }
};

export default employeeApi;
