export const resources = {
    VN: {
        Dialog: {
            DeleteEmployee: {
                title: ' Xóa nhân viên',
                text: (employeeCode) => `Bạn có chắc chắn muốn xóa nhân viên <${employeeCode}> không?`,
                primaryAction: 'Xóa nhân viên',
                secondaryAction: 'Hủy'
            },
            DeleteMultipleEmployees: {
                title: 'Xóa danh sách nhân viên đã chọn?',
                text: 'Danh sách nhân viên sau khi xóa sẽ không thể khôi phục lại được.',
                primaryAction: 'Xóa danh sách đã chọn',
                secondaryAction: 'Hủy'
            },
            HideUpdatedEmployeeDetails: {
                title: 'Lưu thay đổi',
                text: 'Dữ liệu đã bị thay đổi. Bạn có muốn cất không?',
                primaryAction: 'Cất dữ liệu',
                secondaryAction: 'Hủy'
            },
            Validate: {
                title: 'Lỗi nhập liệu',
                primaryAction: 'Đóng',
            },
            DuplicatedEmployeeCode: {
                title: 'Dữ liệu trùng lặp',
                text: (employeeCode) => `Mã nhân viên <${employeeCode}> đã tồn tại trong hệ thống, vui lòng kiểm tra lại.`,
                primaryAction: 'Đóng',
            },
            GetErrorResponse: {
                title: 'Lỗi phản hồi',
                text: (errorText) => errorText ?? 'Có lỗi xảy ra. Vui lòng liên hệ trung tâm tư vấn!',
                primaryAction: 'Đóng',
            }
        },
        ToastMessage: {
            AddEmployee: {
                success: (employeeCode) => `Nhân viên <${employeeCode}> đã được thêm vào danh sách.`,
                error: 'Không thể thêm nhân viên vào danh sách.'
            },
            UpdateEmployee: {
                success: (employeeCode) => `Dữ liệu nhân viên <${employeeCode}> đã được cập nhật.`,
                error: (employeeCode) => `Không thể cập nhật dữ liệu nhân viên <${employeeCode}>.`
            },
            DeleteEmployee: {
                success: (employeeCode) => `Nhân viên <${employeeCode}> đã bị xóa.`,
                error: (employeeCode) => `Không thể xóa nhân viên <${employeeCode}>.`
            },
            DeleteMultipleEmployees: {
                success: 'Danh sách nhân viên đã chọn đã bị xóa.',
                error: 'Không thể xóa danh sách nhân viên đã chọn.'
            },
        }
    },
    EN: {
        Dialog: {
            DeleteEmployee: {
                title: 'Delete Employee',
                text: (employeeCode) => `Are you sure you want to delete employee <${employeeCode}>?`,
                primaryAction: 'Delete Employee',
                secondaryAction: 'Cancel'
            },
            DeleteMultipleEmployees: {
                title: 'Delete Selected Employees?',
                text: 'The selected employees will be permanently deleted.',
                primaryAction: 'Delete Selected',
                secondaryAction: 'Cancel'
            },
            HideUpdatedEmployeeDetails: {
                title: 'Save Changes',
                text: 'Data has been changed. Do you want to save?',
                primaryAction: 'Save Data',
                secondaryAction: 'Cancel'
            },
            Validate: {
                title: 'Input Error',
                primaryAction: 'Close',
            },
            DuplicatedEmployeeCode: {
                title: 'Duplicate Data',
                text: (employeeCode) => `Employee code <${employeeCode}> already exists in the system. Please check again.`,
                primaryAction: 'Close',
            },
        },
        ToastMessage: {
            AddEmployee: {
                success: (employeeCode) => `Employee <${employeeCode}> has been added to the list.`,
                error: 'Could not add employee to the list.'
            },
            UpdateEmployee: {
                success: (employeeCode) => `Employee data <${employeeCode}> has been updated.`,
                error: (employeeCode) => `Could not update employee data <${employeeCode}>.`,
            },
            DeleteEmployee: {
                success: (employeeCode) => `Employee <${employeeCode}> has been deleted.`,
                error: (employeeCode) => `Could not delete employee <${employeeCode}>.`,
            },
            DeleteMultipleEmployees: {
                success: 'Selected employees have been deleted.',
                error: 'Could not delete selected employees.',
            },
        }
    },
}