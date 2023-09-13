const MISAEnum = {
    FormMode: {
        Add: 1,
        Edit: 2,    
    },
    Gender: {
        Male: 1,
        Female: 0,
        Other: 2,
    },
    ButtonType: {
        Success: "btn-success",
        Normal: "btn-normal"
    },
    ToastMode: {
        Success: { 
            typeIcon: "icon--success",
            typeText: "text--suscess",
            title: "Thành công!",
        },
        Warning: { 
            typeIcon: "icon--warning",
            typeText: "text--warning",
            title: "Cảnh báo!" 
        },
        Info: { 
            typeIcon: "icon--info", 
            typeText: "text--info",
            title: "Thông tin!" 
        },
        Error: { 
            typeIcon: "icon-error",
            typeText: "text--error",
            title: "Đã xảy ra lỗi!" 
        },
    },
    DialogMode: {
        Delete: {
            typeIcon: "dialog__icon--warning",
            title:"Xóa nhân viên",
            content: "Xác nhận xóa Nhân viên",

        },
        DeleteAll: {
            typeIcon: "dialog__icon--warning",
            title:"Xóa nhân viên",
            content:"Bạn có xác nhận xóa những nhân viên này không"
        },
        Warning: {
            typeIcon: "dialog__icon--error",
            title:"Lỗi",
            content:" không được để trống!"
        },
        Save: {
            typeIcon: "dialog__icon--question",
            title:"Lưu nhân viên",
            content:"Dữ liệu đã bị thay đổi. Bạn có muốn cất không ?"
        }
    }
}

export default MISAEnum;