import { defineStore } from 'pinia';
import { ref, reactive } from 'vue';

import * as EmployeeDataService from '@/services/employeeDataService';
import { employeeData } from '@/utils/employeeData';

/**
 * tạo 1 empty object để lưu trữ các trường dữ liệu nhân viên
 * @returns đối tượng trống lưu trữ các trường của nhân viên
 * create by: TTANH - 25/08/2023
 * modified by: TTANH - 04/09/2023
 */
const getEmptyData = () => {
    const emptyObject = {}
    for (const key in employeeData[0]) {
        emptyObject[key] = ''
    }
    return emptyObject
};

/**
 * tạo store quản lý trạng thái nhân viên
 * create by: TTANH - 20/08/2023
 */
export const useEmployeeStore = defineStore('employeeState', () => {
    const initFields = getEmptyData()

    const current = ref(initFields)
    const list = ref([])

    /**
     * lấy dữ liệu danh sách nhân viên phân trang
     * @param {int} limit 
     * @param {int} offset 
     * create by: TTANH - 29/08/2023
     * modified by: TTANH - 04/09/2023
     */
    const getPaginationAsync = async (limit, offset) => {
        const response = await EmployeeDataService.getPagination(limit, offset);
        list.value = response.data.result.data
    }

    /**
     * lấy dữ liệu danh sách nhân viên lọc và phân trang
     * @param {string} keyword 
     * @param {int} limit 
     * @param {int} offset 
     * create by: TTANH - 29/08/2023
     * modified by: TTANH - 04/09/2023
     */
    const getFilterAndPaginationAsync = async (keyword, limit, offset) => {
        try {
            const response = await EmployeeDataService.getFilterAndPagination(keyword, limit, offset);
            list.value = response.data.result.data
        } catch (err) {
            console.log(err);
        }
    }

    const getByIdAsync = async (id) => {
        try {
            const response = await EmployeeDataService.getById(id);
            return response.data.result;
        } catch (err) {
            console.log(err);
        }
    }

    /**
     * set nhân viên hiện tại
     * @param {object} employee 
     * @returns
     * create by: TTANH - 20/08/2023
     */
    const setCurrent = (employee) => current.value = employee;

    /**
     * lấy dữ liệu nhân viên theo mã nhân viên
     * @param {string} code 
     * @returns
     * create by: TTANH - 20/08/2023
     */
    const getByCode = (code) => {
        const index = list.value.findIndex(employee => employee.EmployeeCode === code);
        return index
    }

    /**
     * thêm nhân viên
     * @param {object} employee 
     * create by: TTANH - 20/08/2023
     */
    const add = (employee) => {
        try {
            console.log('add:', employee);
            list.value.unshift(employee)
        } catch (err) {
            console.log(err);
        }
    };

    /**
     * sửa nhân viên
     * 1. tìm index của nhân viên có EmployeeID = tham số id đầu vào
     * 2. cập nhật nhân viên nếu tìm thấy
     * @param {string} id 
     * @param {object} data 
     * create by: TTANH - 20/08/2023
     */
    const update = (id, data) => {
        try {
            console.log('update:', id, data);
            if (current.value) {
                const index = list.value.findIndex(employee => employee.EmployeeID === id);

                if (index !== -1) {
                    list.value[index] = { ...list.value[index], ...data };
                }
            }
        } catch (err) {
            console.log(err);
        }
    };

    /**
     * tìm kiếm employee theo mã nhân viên hoặc tên nhân viên
     * 1. chuyển searchText sang định dạng lowercase
     * 2. tìm kiếm trong list, trả về những employee có chứa keyword
     * 3. chuyển EmployeeCode và FullName sang lowercase và thực hiện tìm kiếm
     * @param {string} searchText từ khóa tìm kiếm
     * @returns 
     * create by: TTANH - 20/08/2023
     */
    const search = (searchText) => {
        const keyword = searchText.toLowerCase()

        return list.value.filter((employee) => {
            return (
                employee.EmployeeCode.toString().includes(keyword) ||
                employee.FullName.toLowerCase().includes(keyword)
            );
        })
    }

    /**
     * xóa nhân viên
     * @param {string} employeeCode mã nhân viên bị xóa
     * create by: TTANH - 20/08/2023
     */
    const remove = (employeeCode) => {
        try {
            if (employeeCode) {
                // trả về mảng gồm những nhân viên có ID khác tham số đầu vào
                list.value = list.value.filter((el) =>
                    employeeCode !== (el.employeeCode)
                )
            }

        } catch (err) {
            console.log(err);

        }
    }

    /**
     * xóa nhiều nhân viên
     * @param {array} employeeIDs danh sách ID nhân viên bị xóa
     * create by: TTANH - 20/08/2023
     */
    const removeMultiple = (employeeIDs) => {
        try {
            if (employeeIDs.length) {
                list.value = list.value.filter((el) =>
                    !employeeIDs.includes(el.employeeID)
                )
            }
        } catch (err) {
            console.log(err);

        }
    }

    /**
     * quản lý thông tin chi tiết
     * create by: TTANH - 20/08/2023
     */

    const isShowDetails = ref(false);
    const isShowNewDetails = ref(false);

    const details = reactive({
        // tạo form nhân viên mới
        create: () => {
            isShowDetails.value = false;
            isShowNewDetails.value = true;
            current.value = initFields
        },

        // hiển thị thông tin nhân viên
        show: (employee) => {
            isShowDetails.value = true
            current.value = employee
        },

        // ẩn thông tin nhân viên
        hide: () => {
            isShowDetails.value = false;
            isShowNewDetails.value = false;
            current.value = initFields;
        }
    })

    return {
        initFields,
        current,
        list,
        getPaginationAsync,
        getFilterAndPaginationAsync,
        getByIdAsync,
        setCurrent,
        getByCode,
        add,
        update,
        search,
        remove,
        removeMultiple,
        isShowDetails,
        isShowNewDetails,
        details,
    };
});
