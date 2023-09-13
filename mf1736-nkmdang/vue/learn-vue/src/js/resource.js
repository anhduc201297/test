// const resource = {
//   VN: {
//     EmployeeCodeNotEmpty: "Mã nhân viên không được phép để trống!",
//     EmployeeNameNotEmpty: "Tên nhân viên không được phép để trống!",
//     EmployeeUnitNotEmpty: "Đơn vị không được phép để trống!",
//     DeleteEmployeeTitle: "Xóa những nhân viên đã chọn?",
//     DeleteEmployeeDesc:
//       "Thao tác xóa thông tin nhân viên sẽ không thể khôi phục.",
//     DeleteEmployeeCancel: "Hủy",
//     DeleteEmployeeAccept: "Đồng ý",
//     ToastMesContent: {
//       Success: {
//         Status: "Thành công!",
//         Desc: "Yêu cầu đã được thực hiện",
//         TextClass: "text-50b83c",
//         OptionAction: "Hoàn tác",
//         Icon: "icon__success",
//       },
//       Error: {
//         Status: "Lỗi!",
//         Desc: "Có lỗi xảy ra",
//         TextClass: "text-DE3618",
//         OptionAction: "Xem hướng dẫn",
//         Icon: "icon__error",
//       },
//     },
//     DialogContent: {
//       CustomerForm: {
//         ErrorInput: {
//           MissingRequiredField: {
//             Title: "Thiếu trường bắt buộc",
//             Icon: "icon__bigwarning",
//             Message: "Bạn chưa nhập đầy đủ các trường bắt buộc có dấu sao đỏ!",
//           },
//           InvalidField: {
//             Title: "Thông tin đã nhập không hợp lệ",
//             Icon: "icon__bigwarning",
//             Message: "",
//           },
//         },
//         DeleteError: {
//           Title: "Lỗi",
//           Icon: "icon__bigwarning",
//           Message: "Có lỗi khi thực hiện xóa nhân viên.",
//         },
//       },
//       EmployeeForm: {
//         ErrorInput: {
//           MissingRequiredField: {
//             Title: "Thiếu trường bắt buộc",
//             Icon: "icon__bigwarning",
//             Message: "Bạn chưa nhập đầy đủ các trường bắt buộc có dấu sao đỏ!",
//           },
//           InvalidField: {
//             Title: "Thông tin đã nhập không hợp lệ",
//             Icon: "icon__bigwarning",
//             Message: "",
//           },
//         },
//         DeleteError: {
//           Title: "Lỗi",
//           Icon: "icon__bigwarning",
//           Message: "Có lỗi khi thực hiện xóa nhân viên.",
//         },
//       },
//     },
//     Cancel: "Hủy",
//     Accept: "Đồng ý",
//   },
//   EN: {
//     EmployeeCodeNotEmpty: "Employee's code is not empty!",
//     EmployeeNameNotEmpty: "Employee's name is not empty!",
//     EmployeeUnitNotEmpty: "Employee's unit is not empty!",
//     DeleteEmployeeTitle: "Delete all selected employees?",
//     DeleteEmployeeDesc: "This behavior is not reverse.",
//     DeleteEmployeeCancel: "Cancel",
//     DeleteEmployeeAccept: "Accept",
//     ToastMesContent: {
//       Success: {
//         Status: "Success!",
//         Desc: "Request has been done",
//         TextClass: "text-50b83c",
//         OptionAction: "Undo",
//         Icon: "icon__success",
//       },
//       Error: {
//         Status: "Error!",
//         Desc: "Error has been occur",
//         TextClass: "text-DE3618",
//         OptionAction: "View guide",
//         Icon: "icon__error",
//       },
//     },
//     DialogContent: {
//       CustomerForm: {
//         ErrorInput: {
//           MissingRequiredField: {
//             Title: "Missing required fields!",
//             Icon: "icon__bigwarning",
//             Message:
//               "You have not filled in all required fields marked with a red asterisk!",
//           },
//           InvalidField: {
//             Title: "Invalid information entered",
//             Icon: "icon__bigwarning",
//             Message: "",
//           },
//         },
//         NotFound: {
//           Title: "Lỗi",
//           Icon: "icon__bigwarning",
//           Message: "Vui lòng liên hệ MISA để biết thêm chi tiết.",
//         },
//       },
//       EmployeeForm: {
//         ErrorInput: {
//           MissingRequiredField: {
//             Title: "Missing required fields!",
//             Icon: "icon__bigwarning",
//             Message:
//               "You have not filled in all required fields marked with a red asterisk!",
//           },
//           InvalidField: {
//             Title: "Invalid information entered",
//             Icon: "icon__bigwarning",
//             Message: "",
//           },
//         },
//         NotFound: {
//           Title: "Error",
//           Icon: "icon__bigwarning",
//           Message: "Please contact MISA to get more information.",
//         },
//       },
//     },
//     Cancel: "Cancel",
//     Accept: "Accept",
//   },
// };

const resource = {
  VN: {
    Accept: "Đồng ý",
    Cancel: "Hủy",
    DialogContent: {
      MissingInput: {
        Title: "Thiếu trường bắt buộc",
        Icon: "icon__bigwarning",
        Message: "Bạn chưa nhập đầy đủ các trường bắt buộc.",
      },
      Error: {
        Title: "Lỗi",
        Icon: "",
        Message: "Có lỗi xảy ra! Vui lòng liên hệ chúng tôi để được hỗ trợ.",
      },
      EmployeeForm: {},
    },
    ToastMessageContent: {
      EmployeeForm: {
        AddSuccess: {
          Status: "Thành công!",
          Desc: "Thêm mới nhân viên thành công.",
          TextClass: "text-50b83c",
          OptionAction: "Hoàn tác",
          Icon: "icon__success",
        },
        CannotGetNewEmployeeCode: {
          Status: "Lỗi!",
          Desc: "Không thế lấy mã nhân viên mới.",
          TextClass: "text-F49342",
          OptionAction: "",
          Icon: "icon__warning",
        },
      },
    },
  },
  EN: {
    Accept: "Accept",
    Cancel: "Cancel",
    DialogContent: {
      Error: {
        Title: "Error",
        Icon: "",
        Message: "Some error occured! Please contact us to get help.",
      },
      MissingInput: {
        Title: "Missing required field",
        Icon: "icon__bigwarning",
        Message: "You hasnot been enterd all required field.",
      },
      EmployeeForm: {},
    },
    ToastMessageContent: {
      EmployeeForm: {
        AddSuccess: {
          Status: "Success!",
          Desc: "Add new employee successfully.",
          TextClass: "text-50b83c",
          OptionAction: "Undo",
          Icon: "icon__warning",
        },
        CannotGetNewEmployeeCode: {
          Status: "Error!",
          Desc: "Cannot get new employee code.",
          TextClass: "text-F49342",
          OptionAction: "",
          Icon: "icon__success",
        },
      },
    },
  },
};

export default resource;
