export const Gender = Object.freeze({
    Male: 0,
    Female: 1,
    Other: 2,
});

export const DialogName = Object.freeze({
    DeleteEmployee: Symbol('delete-employee'),
    DeleteMultipleEmployees: Symbol('delete-multiple-employees'),
    HideUpdatedEmployeeDetails: Symbol('hide-updated-employee-details'),
    Validate: Symbol('validate'),
    DuplicatedEmployeeCode: Symbol('duplicated-employee-code'),
    GetErrorResponse: Symbol('cannot-get-employees-data')
})

export const ToastMessageType = Object.freeze({
    Success: Symbol('success'),
    Error: Symbol('error'),
    Warning: Symbol('warning'),
    Info: Symbol('info')
})
