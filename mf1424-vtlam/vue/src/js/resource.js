const resource = {
    VN: {
        EmptyEmployeeCode: "Mã không được để trống",
        InvalidEmployeeCode: "Mã nhân viên không đúng định dạng",
        EmptyEmployeeName: "Họ và tên không được để trống",
        EmptyDepartmentName: "Đơn vị không được để trống",
        InvalidDateOfBirth: "Ngày sinh không được lớn hơn hiện tại",
        InvalidIdentityDate: "Ngày cấp không được lớn hơn hiện tại",
        InvalidEmail: "Email không đúng định dạng",
        DeleteEmployeeTitle:"Xóa nhân viên",
        DeleteEmployeeMessage: (employeeCode) => `Nhân viên <${employeeCode}> sẽ bị xóa.`,
        DeleteBatchEmployeeTitle: (number) => `Xóa ${number} nhân viên`,
        DeleteBatchEmployeeMessage:"Những nhân viên được chọn sẽ bị xóa.",
        ChangeDataTitle:"Dữ liệu thay đổi",
        ChangeDataMessage:"Lưu lại những thay đổi?",
        AddEmployeeSuccess: "Thêm nhân viên thành công.",
        EditEmployeeSuccess: "Sửa nhân viên thành công.",
        DeleteEmployeeSuccess: "Xóa nhân viên thành công.",
        Success:"Thành công!",
        Error: "Lỗi!",
        Alert: "Cảnh báo!",
        Info: "Thông tin!"

    },

    EN: {
        EmptyEmployeeCode: "Employee code cannot be empty",
        InvalidEmployeeCode: "Invalid employee code",
        EmptyEmployeeName: "Name cannot be empty",
        EmptyDepartmentName: "Department cannot be empty",
        InvalidDateOfBirth: "Date of birth cannot be after now",
        InvalidIdentityDate: "indentity date cannot be after now",
        InvalidEmail: "Invalid email",
        DeleteEmployeeTitle:"Delete employee",
        DeleteEmployeeMessage: (employeeCode) => `Employee <${employeeCode}> will be deleted.`,
        DeleteBatchEmployeeTitle: (number) => `Delete ${number} employees`,
        DeleteBatchEmployeeMessage:"Selected employess will be deleted.",
        ChangeDataTitle:"Data changed",
        ChangeDataMessage:"Save changes?",
        AddEmployeeSuccess: "Add employee successfully.",
        EditEmployeeSuccess: "Edit employee successfully.",
        DeleteEmployeeSuccess: "Delete employee successfully.",
        Success:"Successful!",
        Error: "Error!",
        Alert: "Alert!",
        Info: "Information!"
    }
}

export default resource