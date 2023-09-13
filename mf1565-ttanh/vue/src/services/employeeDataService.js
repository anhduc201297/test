import * as httpRequest from '@/utils/httpRequest';

const endpoint = 'Employees'

/**
 * Lấy danh sách tất cả nhân viên
 * @returns danh sách nhân viên
 * create by: TTANH - 26/08/2023
 * modified by: TTANH - 05/09/2023
 */
export const getAll = async () => {
    const res = await httpRequest.get(endpoint, {
        params: { limit: -1, offset: -1 }
    });
    return res;
};

/**
 * Lấy danh sách nhân viên phân trang
 * @param {int} limit số bản ghi mỗi trang
 * @param {int} offset hàng bắt đầu truy xuất
 * @returns danh sách nhân viên
 * create by: TTANH - 26/08/2023
 * modified by: TTANH - 05/09/2023
 */
export const getPagination = async (limit = 20, offset = 0) => {
    const res = await httpRequest.get(endpoint, {
        params: { limit, offset }
    })
    return res;
}

/**
 * Lấy danh sách nhân viên phân trang và lọc theo mã nhân viên, tên nhân viên
 * @param {string} keyword từ khóa tìm kiếm
 * @param {int} limit số bản ghi mỗi trang
 * @param {int} offset hàng bắt đầu truy xuất
 * @returns danh sách nhân viên
 * create by: TTANH - 26/08/2023
 * modified by: TTANH - 05/09/2023
 */
export const getFilterAndPagination = async (keyword = '', limit = 20, offset = 0) => {
    try {
        const res = await httpRequest.get(endpoint, {
            params: { keyword, limit, offset }
        })
        console.log(res);
        return res;
    } catch (error) {
        console.log(error);
    }
}

/**
 * Lấy nhân viên theo ID
 * @param {string} id ID nhân viên
 * @returns danh sách nhân viên
 * create by: TTANH - 26/08/2023
 */
export const getById = async (id) => {
    try {
        const res = await httpRequest.get(`${endpoint}/${id}`);
        return res;
    } catch (error) {
        console.log(error);
    }
};

/**
 * Thêm mới nhân viên
 * @param {object} data dữ liệu nhân viên mới
 * @returns danh sách nhân viên
 * create by: TTANH - 26/08/2023
 */
export const createNewEmployee = async (data) => {
    try {
        const res = await httpRequest.post('${endpoint}/', data);
        return res;
    } catch (error) {
        console.log(error);
    }
};

/**
 * Cập nhật nhân viên
 * @param {*} id ID nhân viên
 * @param {*} data dữ liệu cập nhật
 * @returns danh sách nhân viên
 * create by: TTANH - 26/08/2023
 */
export const updateEmployee = async (id, data) => {
    try {
        const res = await httpRequest.put(`${endpoint}/${id}`, data);
        return res;
    } catch (error) {
        console.log(error);
    }
};

/**
 * Xóa nhân viên theo ID
 * @param {*} id ID nhân viên
 * @returns danh sách nhân viên
 * create by: TTANH - 26/08/2023
 */
export const removeByCode = async (id) => {
    try {
        const res = await httpRequest.delete(`${endpoint}/${id}`);
        return res;
    } catch (error) {
        console.log(error);
    }
};

/**
 * Xóa tất cả nhân viên
 * @returns danh sách nhân viên
 * create by: TTANH - 26/08/2023
 */
export const removeAll = async () => {
    try {
        const res = await httpRequest.delete(`${endpoint}`);
        return res;
    } catch (error) {
        console.log(error);
    }
};