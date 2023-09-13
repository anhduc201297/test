import axiosRequest from "@/utils/axiosRequest";

/**
 * lấy danh sách nhân viên
 * Author: dktuan (30/08/2023)
 */
export const addEmployees = async (data) => {
    const res = await axiosRequest.post('/Employees', data)
    return res;
}

/**
 * lấy danh sách nhân viên
 * Author: dktuan (30/08/2023)
 */
export const getEmployees = async (size, page) => {
    // const res = await axiosRequest.get('/Employees/employeeFilter?pageSize=1&pageNumber=1&employeeFilter=2')
    const res = await axiosRequest.get(`/Employees?size=${size}&page=${page}`)
    return res;
}