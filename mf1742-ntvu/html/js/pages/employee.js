Common.onLoading();
$(document).ready(function () {
    new EmployeePage();
});
class EmployeePage {
    employeeActions = {
        /**
         * Hiện dialog nhân đôi
         * Author: NTVu - MF1742
         * Modified: NTVu - 15/08/2023
         *
         * @param {string} name
         */
        duplicate: (name) => {
            console.log(name);
            this.dialog.show(
                `Nhân đôi "${name}" ?`,
                Dialog.DialogTypes.warning.value,
                () => {
                    console.log('warning');
                }
            );
        },
        /**
         * Hiện dialog nhân đôi
         * Author: NTVu - MF1742
         * Modified: NTVu - 15/08/2023
         *
         * @param {string} name
         */
        remove: (name) => {
            this.dialog.show(
                `Xóa "${name}" ?`,
                Dialog.DialogTypes.warning.value,
                () => {
                    console.log('warning');
                }
            );
        },
        /**
         * Hiện dialog nhân đôi
         * Author: NTVu - MF1742
         * Modified: NTVu - 15/08/2023
         *
         * @param {string} name
         */
        stopUse: (name) => {
            this.dialog.show(
                `Bảo lưu "${name}" ?`,
                Dialog.DialogTypes.warning.value,
                () => {
                    console.log('warning');
                }
            );
        },
    };

    table;
    dialog;
    popupForm;
    /**
     * Load data và sự kiện cho các elements
     * Author: NTVu - MF1742
     * Modified: NTVu - 15/08/2023
     */
    constructor() {
        this.dialog = new Dialog($('#dialog'));
        this.popupForm = new PopupForm($('#popup'));
        // new ComboBox();

        const load = async () => {
            await this.loadData();
            this.loadEvents();
        };
        load();
    }
    /**
     * Load data cho trang employee
     * Author: NTVu - MF1742
     * Modified: NTVu - 16/08/2023
     */
    async loadData() {
        try {
            Common.onLoading();

            let tableData = [];
            const getRandomDate = () => {
                const start = new Date(1990, 0, 1);
                const end = new Date(2005, 11, 31);
                const randomTime =
                    start.getTime() +
                    Math.random() * (end.getTime() - start.getTime());
                return new Date(randomTime).toLocaleDateString();
            };

            const roles = [
                'Nhân viên mới',
                'Thực tập sinh',
                'Nhân viên chính thức',
                'Trưởng nhóm',
                'Quản lí',
            ];
            const fields = [
                'Khối sản xuất',
                'Khối quản lý',
                'Khối giải pháp bán lẻ',
                'Khối hành chính - sự nghiệp',
                'Khối công nghệ thông tin',
                '',
            ];
            const bankNames = [
                'Ngân hàng Vietcombank',
                'Ngân hàng BIDV',
                'Ngân hàng Techcombank',
                'Ngân hàng Á Châu ACB',
                '',
            ];

            for (let i = 0; i < 200; i++) {
                const staffId = `ID${i + 1}`;
                const staffName = `Người dùng ${i + 1}`;
                const staffGender = Math.floor(Math.random() * 3);
                const staffBirthday = getRandomDate();
                const staffIdentity = Math.floor(
                    Math.random() * 10000000000
                ).toString();
                const staffRole =
                    roles[Math.floor(Math.random() * roles.length)];
                const staffField =
                    fields[Math.floor(Math.random() * fields.length)];
                const staffBankNo = Math.floor(
                    Math.random() * 10000000
                ).toString();
                const staffBankName =
                    bankNames[Math.floor(Math.random() * bankNames.length)];
                const staffBankLocation = `Địa chỉ ngân hàng ${i + 1}`;
                const isCustomer = Math.round(Math.random());
                const isContributor = Math.round(Math.random());

                const entry = {
                    id: staffId,
                    name: staffName,
                    gender: staffGender,
                    birthday: staffBirthday,
                    identityNo: staffIdentity,
                    role: staffRole,
                    office: staffField,
                    bankNo: staffBankNo,
                    bankName: staffBankName,
                    bankLocation: staffBankLocation,
                    isCustomer,
                    isContributor,
                };

                tableData.push(entry);
            }

            this.table = new Table(
                $('#employee .m-table')[0],
                tableData,
                this.employeeActions,
                this.popupForm
            );
        } catch (error) {
            console.log(error);
        } finally {
            Common.endLoading();
        }
    }
    /**
     * Load sự kiện mặc định cho trang employee
     * Author: NTVu - MF1742
     * Modified: NTVu - 16/08/2023
     */
    loadEvents() {
        const _this = this;

        $('.icon-bell')
            .parent()
            .click(function () {
                _this.dialog.show(
                    'kiểm thử',
                    Dialog.DialogTypes.warning.value,
                    () => {
                        console.log('DIALOG OK');
                    }
                );
            });
    }
}
