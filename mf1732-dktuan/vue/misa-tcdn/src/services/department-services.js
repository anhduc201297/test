import axiosRequest from "@/utils/axiosRequest";

/**
 * lấy danh sách đơn vị
 * Author: dktuan (30/08/2023)
 */
export const getDepartments = async () => {
    const res = await axiosRequest.get('/Departments')
    const departmentMap = {}
    res.forEach(element => {
        departmentMap[element.DepartmentName] = element.DepartmentCode
    });
    return departmentMap;
}