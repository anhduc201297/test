const MResource = {
    VN: {
        OkText: 'Đồng ý',
        NoText: 'Không',
        CancelText: 'Hủy',
        CloseText: 'Đóng',
        EditText: 'Sửa',
        ConfirmSaveText: 'Lưu bản ghi',
        ConfirmRemoveOneText: 'Xóa bản ghi',
        ConfirmRemoveMultiText: 'Xóa nhiều bản ghi',
        Error: 'Lỗi',
        Unauthorized: 'Thông tin người dùng không hợp lệ.',
        Forbidden: 'Tài khoản không có quyền truy cập vào mục này.',
        NotFound: 'Không tìm thấy bất kì truy cập nào hợp lệ.',
        MethodNotAllowed:
            'Phương thức này đã bị chặn và không còn được sử dụng nữa.',
        ErrorConflict:
            'Yêu cầu không thể hoàn thành vì mâu thuẫn với trạng thái hiện tại.',
        UnsupportedMediaType: 'Tệp không được máy chủ hỗ trợ.',
        ErrorText: 'Có lỗi xảy ra, vui lòng liên hệ MISA để được hỗ trợ.',
        DataNotValid: 'Dữ liệu không hợp lệ.',
        NotEndWithNumber: 'Mã nhân viên phải kết thúc bằng các chữ số.',
        EmailNotValid: 'Email không hợp lệ.',
        RequiredInput: 'Thông tin không được phép để trống.',
        EmailNotFormatted: 'Định dạng email không hợp lệ.',
        ValueNotInOptions: 'Dữ liệu không có trong danh mục.',
        ConfirmRemoveMulti: 'Bạn có muốn xóa những danh mục này không?',
        RemoveEmployeesError: 'Không thể xóa toàn bộ nhân viên hiện tại.',
        RemoveEmployeeError: 'Không thể xóa nhân viên này.',
        RemoveEmployeesSuccess: 'Xóa thành công.',
        RemoveAllEmployeesSuccess: 'Xóa toàn bộ nhân viên thành công.',

        AddEmployeeSuccess: 'Thêm bản ghi thành công.',
        AddEmployeeFail: 'Thêm bản ghi thất bại, vui lòng thử lại sau.',
        DuplicateEmployeeCode:
            'Mã nhân viên đã tồn tại, hãy thử mã nhân viên khác!',
        ConfirmUpdateRecords: 'Dữ liệu đã bị thay đổi. Bạn có muốn cất không?',
        UpdateEmployeeFailure: 'Cập nhật thông tin nhân viên thất bại.',
        UpdateEmployeeSuccess: 'Cập nhật thông tin nhân viên thành công.',
        ErrorDateOfBirth: 'Ngày sinh phải sớm hơn ngày hiện tại.',
        ErrorDateOfIdentity: 'Ngày cấp CCCD/CMND phải sớm hơn ngày hiện tại.',

        Gender: {
            1: 'Nữ',
            2: 'Nam',
            3: 'Khác'
        },
        ToastType: {
            Success: {
                Label: 'Thành công',
                Color: 'green',
                Icon: 'icon-check-no-bg',
                LabelAction: 'Hoàn tác'
            },
            Warning: {
                Label: 'Cảnh báo',
                Color: '#F49342',
                Icon: 'icon-info-orange',
                LabelAction: 'Chi tiết'
            },
            Danger: {
                Label: 'Lỗi',
                Color: 'red',
                Icon: 'icon-fail',
                LabelAction: 'Xem chi tiết'
            },
            Info: {
                Label: 'Thông tin',
                Color: 'blue',
                Icon: 'icon-info',
                LabelAction: false
            }
        },

        errorFieldEmpty: (fieldName) =>
            `${fieldName || 'Thông tin'} không được phép để trống.`,
        /**
         * Trả về message: 'Mã nhân viên bị trùng: <code> '
         * @Author NTVu - 30/08/2023
         * @param {string} code
         * @returns {string}
         */
        errorEmployeeCode: (code) => `Mã nhân viên bị trùng: <${code}>.`,
        /**
         * Trả về message: 'Cần tối thiểu n kí tự'
         * @Author NTVu - 24/08/2023
         * @param {number} n
         * @returns {string}
         */
        leastDigitError: (n) => `Tối thiểu ${n} kí tự.`,
        /**
         * Trả về message: 'Cần tối đa n kí tự'
         * @Author NTVu - 24/08/2023
         * @param {number} n
         * @returns {string}
         */
        maxDigitError: (n) => `Tối đa ${n} kí tự.`,
        /**
         * Message xóa thành công một nhân viên
         * @Author NTVu - 24/08/2023
         * @param {string} id
         */
        removeEmployeeSuccess: (id) => `Xóa nhân viên <${id}> thành công.`,
        /**
         * Trả về message xác nhận xóa một nhân viên
         * @Author NTVu - 24/08/2023
         * @param {string} id
         * @returns {string}
         */
        confirmRemoveEmployee: (id) =>
            `Bạn có thực sự muốn xóa nhân viên <${id}> không?`
    },
    EN: {
        OkText: 'OK',
        NoText: 'No',
        CancelText: 'Cancel',
        CloseText: 'Close',
        EditText: 'Sửa',
        ConfirmRemoveMultiText: 'Remove records',
        ConfirmRemoveOneText: 'Remove a record',
        ConfirmSaveText: 'Save record',
        Error: 'Error',
        Unauthorized: 'UnAuthorization.',
        Forbidden: 'This account does not have permission to access.',
        NotFound: 'Not found anything about your access.',
        MethodNotAllowed: 'This method has been disabled and cannot be used.',
        ErrorConflict:
            'The request could not be completed due to a conflict with the current state of the resource.',
        UnsupportedMediaType: 'The media-type is not supported by the server.',
        ErrorText: 'Have an error, contact MISA to be supported.',
        DataNotValid: 'Data is not valid.',
        NotEndWithNumber: "Employee's code must end with numbers",
        EmailNotValid: 'Email is not valid.',
        RequiredInput: 'This input is required.',
        EmailNotFormatted: 'Invalid email format.',
        ValueNotInOptions: 'Data is not in options.',
        ConfirmRemoveMulti: 'Do you want to remove this items?',
        RemoveEmployeesError: "Can't remove all employees now.",
        RemoveEmployeeError: "Can't remove this employee.",
        RemoveEmployeesSuccess: 'Remove successfully.',
        RemoveAllEmployeesSuccess: 'Remove all employees successfully.',
        AddEmployeeSuccess: 'Add record successfully.',
        AddEmployeeFail: 'Add record failed, try again later.',
        DuplicateEmployeeCode: 'Duplicate employee code, let try other code!',
        ConfirmUpdateRecords: 'Data changed. Do you want to save?',
        UpdateEmployeeFailure: 'Update data fail.',
        UpdateEmployeeSuccess: 'Update employee successfully.',
        ErrorDateOfBirth: 'Date of birth have to be earlier than now.',
        ErrorDateOfIdentity: 'Date of identity is not valid.',
        Gender: {
            1: 'Nữ',
            2: 'Nam',
            3: 'Khác'
        },
        ToastType: {
            Success: {
                Label: 'Success',
                Color: 'green',
                Icon: 'icon-check-no-bg',
                LabelAction: 'Undo'
            },
            Warning: {
                Label: 'Warning',
                Color: '#F49342',
                Icon: 'icon-info-orange',
                LabelAction: 'View details'
            },
            Danger: {
                Label: 'Failure',
                Color: 'red',
                Icon: 'icon-fail',
                LabelAction: 'View details'
            },
            Info: {
                Label: 'Inform',
                Color: 'blue',
                Icon: 'icon-info',
                LabelAction: false
            }
        },
        /**
         * Trả về tin nhắn: {FieldName} không được phép để trống.
         * @author NTVu 03/09/2023
         * @param {string} fieldName
         * @returns
         */
        errorFieldEmpty: (fieldName) =>
            `${fieldName || 'This field'} can not be empty.`,
        /**
         * Trả về message: 'Mã nhân viên bị trùng: <code> '
         * @Author NTVu - 30/08/2023
         * @param {string} code
         * @returns {string}
         */
        errorEmployeeCode: (code) => `Duplicate employee code: <${code}> `,
        /**
         * Trả về message cần tối thiểu n kí tự
         * @Author NTVu - 24/08/2023
         * @param {number} n
         * @returns {string}
         */
        leastDigitError: (n) => `At least ${n} digits`,
        /**
         * Trả về message cần tối đa n kí tự
         * @Author NTVu - 24/08/2023
         * @param {number} n
         * @returns {string}
         */
        maxDigitError: (n) => `Maximum ${n} digits`,
        /**
         * Message xóa thành công một nhân viên
         * @Author NTVu - 24/08/2023
         * @param {string} id
         */
        removeEmployeeSuccess: (id) => `Remove employee <${id}> successfully.`,
        /**
         * Trả về message xác nhận xóa một nhân viên
         * @Author NTVu - 24/08/2023
         * @param {string} id
         * @returns {string}
         */
        confirmRemoveEmployee: (id) =>
            `Are you sure you want to delete employee <${id}>?`
    }
};

export default MResource;
