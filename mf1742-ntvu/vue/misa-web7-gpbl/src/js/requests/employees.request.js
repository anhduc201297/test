import { api } from '../utils/api';

/**
 * Lấy mã employee mới từ máy chủ
 * @returns {Promise} - Promise trả về kết quả của việc lấy mã employee mới
 * @author NTVu - 06/09/2023
 */
export const getNewCodeEmployeeApi = () => {
    return api.get('/Employees/GetNewCode');
};

/**
 * Lấy danh sách employee dựa trên các tham số tìm kiếm
 * @param {object} options - Các tùy chọn tìm kiếm
 * @param {string} options.search - Từ khoá tìm kiếm
 * @param {number} options.page - Trang hiện tại
 * @param {number} options.pageSize - Số lượng mục trên mỗi trang
 * @returns {Promise} - Promise trả về danh sách employee
 * @author NTVu - 06/09/2023
 */
export const getEmployeesApi = ({ search, page, pageSize }) => {
    return api.get('/Employees/Filter', {
        params: {
            search,
            page,
            pageSize
        }
    });
};

/**
 * Tạo mới một bản ghi employee trên máy chủ
 * @param {object} employeeData - Thông tin employee cần tạo
 * @param {string} employeeData.employeeId
 * @param {string} employeeData.employeeCode
 * @param {string} employeeData.fullName
 * @param {Number?} employeeData.gender
 * @param {string?} employeeData.email
 * @param {string?} employeeData.phoneHome
 * @param {string?} employeeData.phoneMobile
 * @param {string} employeeData.departmentId
 * @param {string} employeeData.departmentName
 * @param {string} employeeData.positionName
 * @param {Date?} employeeData.birthday
 * @param {string?} employeeData.address
 * @param {string?} employeeData.identityNo
 * @param {Date?} employeeData.identityDate
 * @param {string?} employeeData.identityPlace
 * @param {string?} employeeData.bankName
 * @param {string?} employeeData.bankPlace
 * @param {string?} employeeData.bankAccountNo
 * @param {string} employeeData.createdBy
 * @param {Date} employeeData.createdDate
 * @param {Date?} employeeData.modifiedDate
 * @param {string?} employeeData.modifiedBy
 * @returns {Promise} - Promise trả về kết quả của việc tạo employee
 * @author NTVu - 06/09/2023
 */
export const createEmployeeApi = (employeeData) => {
    return api.post('/Employees', employeeData);
};

/**
 * Cập nhật thông tin của một employee dựa trên ID
 * @param {object} employeeData - Thông tin cập nhật của employee
 * @param {string} employeeId - ID của employee cần cập nhật
 * @returns {Promise} - Promise trả về kết quả của việc cập nhật employee
 * @author NTVu - 06/09/2023
 */
export const updateEmployeeApi = (employeeData, employeeId) => {
    return api.put(`/Employees/${employeeId}`, employeeData);
};
/**
 * Xóa một employee dựa trên employeeId
 * @param {string} employeeId - Mã employee cần xóa
 * @returns {Promise} - Promise trả về kết quả của việc xóa employee
 * @author NTVu - 06/09/2023
 */
export const removeEmployeeByIdApi = (employeeId) => {
    return api.delete(`/Employees/${employeeId}`);
};
/**
 * Xóa một employee dựa trên mã employee
 * @param {string} employeeCode - Mã employee cần xóa
 * @returns {Promise} - Promise trả về kết quả của việc xóa employee
 * @author NTVu - 06/09/2023
 */
export const removeEmployeeByCodeApi = (employeeCode) => {
    return api.delete(`/Employees/Code/${employeeCode}`);
};

/**
 * Xóa nhiều employee dựa trên danh sách ID
 * @param {string[]} employeeIds - Danh sách ID employee cần xóa
 * @returns {Promise} - Promise trả về kết quả của việc xóa nhiều employee
 * @author NTVu - 06/09/2023
 */
export const removeEmployeesApi = (employeeIds) => {
    return api.delete('/Employees/', {
        data: employeeIds
    });
};

/**
 * Xóa tất cả employee
 * @returns {Promise} - Promise trả về kết quả của việc xóa tất cả employee
 * @author NTVu - 06/09/2023
 */
export const removeAllEmployeesApi = () => {
    return api.delete('/Employees/All');
};

/**
 * Xuất danh sách employee ra file Excel từ máy chủ
 * @returns {Promise} - Promise trả về file Excel dưới dạng dữ liệu nhị phân (blob)
 * @author NTVu - 06/09/2023
 */
export const exportToExcel = () => {
    return api.get('/Employees/Export', {
        responseType: 'blob'
    });
};
