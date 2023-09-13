export const table =
{
    columnInfos: [
        {
            name: 'employee-check',
            width: 'extra-small',
        },
        {
            name: 'employee-code',
            header: 'mã nhân viên',
            property: 'employeeCode',
            width: 'medium',
        },
        {
            name: 'full-name',
            header: 'tên nhân viên',
            property: 'fullName',
            width: 'extra-large',
        },
        {
            name: 'gender',
            header: 'giới tính',
            property: 'gender',
            width: 'small',
        },
        {
            name: 'date-of-birth',
            header: 'ngày sinh',
            property: 'dateOfBirth',
            width: 'medium',
        },
        {
            name: 'identity-number',
            header: 'số cmnd',
            property: 'identityNumber',
            width: 'medium',
            title: 'Số chứng minh nhân dân',
        },
        {
            name: 'position-name',
            header: 'chức danh',
            property: 'positionName',
            width: 'large',
        },
        {
            name: 'department-name',
            header: 'tên đơn vị',
            property: 'departmentName',
            width: 'extra-large',
        },
        {
            name: 'bank-account',
            header: 'số tài khoản',
            property: 'bankAccount',
            width: 'medium',
        },
        {
            name: 'bank-name',
            header: 'tên ngân hàng',
            property: 'bankName',
            width: 'extra-large',
        },
        {
            name: 'bank-branch',
            header: 'chi nhánh tk ngân hàng',
            property: 'bankBranch',
            width: 'extra-large',
        },
    ],
    // các cột được cố định khi scroll
    fixedColumns: ['employee-check', 'employee-code', 'full-name'],
    // số bản ghi mỗi trang
    recordPerPageOptions: ['10 bản ghi trên 1 trang',
        '20 bản ghi trên 1 trang',
        '30 bản ghi trên 1 trang',
        '50 bản ghi trên 1 trang',
        '100 bản ghi trên 1 trang']
    ,
    /**
     * định dạng danh sách trang hiển thị theo trang hiện tại và tổng số trang
     * @param {int} currentPage 
     * @param {int} totalPages 
     * @returns danh sách trang đã được định dạng
     * create by: TTANH - 06/09/2023
     * modified by: TTANH - 06/09/2023
     */
    generatePageNumbers: (currentPage, totalPages) => {
        // số trang hiển thị bên trái, ở giữa, bên phải
        const visibleLeftPages = 3
        const visibleMiddlePages = 3
        const visibleRightPages = 3

        // trang đầu, trang cuối
        const startPage = 1;
        const endPage = totalPages

        // mảng lưu trữ các trang bên trái, ở giữa, bên phải
        const leftPages = [startPage];
        const middlePages = [];
        const rightPages = [endPage];

        const isHandlingLeftPages = currentPage < visibleLeftPages
        const isHandlingMiddlePages = currentPage >= visibleLeftPages && currentPage <= endPage - visibleRightPages
        const isHandlingRightPages = currentPage > endPage - visibleRightPages && currentPage <= endPage

        // nếu số trang nhỏ hơn visibleLeftPages, thêm vào leftPages và trả về
        if (totalPages <= visibleLeftPages) {
            for (let i = startPage + 1; i <= totalPages; i++) {
                leftPages.push(i)
            }
            return leftPages
        }

        if (isHandlingLeftPages) {
            for (let i = startPage + 1; i <= visibleLeftPages; i++) {
                leftPages.push(i)
            }
        } else if (isHandlingMiddlePages) {
            for (let i = currentPage; i <= currentPage + visibleMiddlePages - 1; i++) {
                middlePages.push(i)
            }
        } else if (isHandlingRightPages) {
            for (let i = endPage - 1; i > endPage - visibleRightPages; i--) {
                rightPages.unshift(i)
            }
        }

        return [leftPages, middlePages.length ? ['...', ...middlePages, '...'] : ['...'], rightPages].flat()
    }
}
