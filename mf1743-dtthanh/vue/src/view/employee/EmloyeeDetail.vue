<template>
    <!-- eslint-disable -->
    <div id="idPopup" class="popup">
        <div class="add-form-container">
            <div class="popup-container ">
                <!-- Popup Header -->
                <header class="popup-header">
                    <div class="popup-title">
                        <h1 data-tooltip="Thông tin" class="popup-title__text">
                            Thông tin cá nhân
                        </h1>
                        <m-checkbox text="Là khách hàng" class="popup-title__checkbox"></m-checkbox>
                        <m-checkbox text="Là nhà cung cấp" class="popup-title__checkbox"></m-checkbox>
                    </div>
                    <div class="popup-close">
                        <div class="icon-24 mr-8">
                            <div data-tooltip="Giúp (F1)" class="icon-popup-help icon"></div>
                        </div>
                        <div @click="onClose" data-tooltip="Đóng (ESC)" id="closeBtn" style="margin-right: -4px"
                            class="icon-24">
                            <div class="icon-popup-close icon"></div>
                        </div>
                    </div>
                </header>
                <!-- Popup Header End -->

                <!-- Popup content -->
                <div class="popup-content">
                    <!-- Content rieng tung form -->
                    <div class="add-form-content">
                        <div class="flex pb-16">
                            <div class="add-form-tl">
                                <div class="add-form-row flex">
                                    <!-- Mã nhân viên -->
                                    <div class="w-40 pr-8">
                                        <m-input-full type="text" ref="inputCode" v-model="employee.EmployeeCode"
                                            :isRequire="true" text="Mã">
                                        </m-input-full>
                                    </div>
                                    <!-- Họ và tên -->
                                    <div class="w-60">
                                        <m-input-full type="text" :isRequire="true" text="Tên"
                                            v-model="employee.FullName"></m-input-full>
                                    </div>
                                </div>
                                <div class="add-form-row">
                                    <!-- Đơn vị -->
                                    <div class="w-100">
                                        <!-- <m-input-full type="text" :isRequire="true" text="Đơn vị"
                                            v-model="employee.DepartmentName"></m-input-full> -->
                                        <div class="center">
                                            <h4 style="margin-bottom: 2px;">Đơn vị</h4>
                                            <h4 style="color: red">&nbsp;*</h4>
                                        </div>
                                        <m-combobox v-model="employee.DepartmentName" :result="setDepartments">
                                        </m-combobox>
                                    </div>
                                </div>
                                <div class="add-form-row">
                                    <!-- Chức danh -->
                                    <div class="w-100">
                                        <div class="center">
                                            <h4 style="margin-bottom: 2px;">Chức danh</h4>
                                        </div>
                                        <m-combobox v-model="employee.PositionName" :result="setPositions">
                                        </m-combobox>
                                    </div>
                                </div>
                            </div>

                            <div class="add-form-tr">
                                <div class="add-form-row flex">
                                    <!-- Ngày sinh -->
                                    <div class="w-40 pr-8">
                                        <m-input-full type="date" text="Ngày sinh"
                                            v-model="employee.DateOfBirth"></m-input-full>
                                    </div>
                                    <!-- Giới tính -->
                                    <div class="w-60">
                                        <div class="flex">
                                            <h4>Giới tính</h4>
                                        </div>
                                        <div class="center p-8">
                                            <m-radio class="mr-24" name="gender" text="Nam"></m-radio>
                                            <m-radio class="mr-24" name="gender" text="Nữ"></m-radio>
                                            <m-radio name="gender" text="Khác"></m-radio>
                                        </div>
                                    </div>
                                </div>
                                <div class="add-form-row flex">
                                    <!-- Số chứng minh nhân dân -->
                                    <div class="w-60 pr-8">
                                        <m-input-full type="text" tooltip="Số chứng minh nhân dân" text="Số CMND"
                                            v-model="employee.IdentityNumber"></m-input-full>
                                    </div>
                                    <!-- Ngày cấp CMND -->
                                    <div class="w-40">
                                        <m-input-full type="date" text="Ngày cấp"></m-input-full>
                                    </div>
                                </div>
                                <div class="add-form-row">
                                    <!-- Nơi cấp CMND -->
                                    <div class="w-100">
                                        <m-input-full type="text" :isRequire="false" text="Nơi cấp"></m-input-full>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="pb-16">
                            <div class="add-form-row">
                                <!-- Địa chỉ -->
                                <div class="w-100">
                                    <m-input-full type="text" :isRequire="false" text="Địa chỉ"></m-input-full>
                                </div>
                            </div>
                            <div class="add-form-row flex">
                                <!-- Số điện thoại di động -->
                                <div class="w-25 pr-8">
                                    <m-input-full type="text" :isRequire="false" text="ĐT di động"
                                        dataTooltip="Số điện thoại di động" v-model="employee.PhoneNumber"></m-input-full>
                                </div>
                                <!-- Số điện thoại cố định -->
                                <div class="w-25 pr-8">
                                    <m-input-full type="text" :isRequire="false" text="ĐT cố định"
                                        dataTooltip="Số điện thoại cố định"></m-input-full>
                                </div>
                                <!-- Địa chỉ email -->
                                <div class="w-25 pr-8">
                                    <m-input-full type="text" :isRequire="false" v-model="employee.Email" text="Email"
                                        dataTooltip="Địa chỉ email"></m-input-full>

                                </div>
                            </div>
                            <div class="add-form-row flex">
                                <!-- Tài khoản ngân hàng -->
                                <div class="w-25 pr-8">
                                    <m-input-full type="text" :isRequire="false" text="Tài khoản ngân hàng"></m-input-full>
                                </div>
                                <!-- Tên ngân hàng -->
                                <div class="w-25 pr-8">
                                    <m-input-full type="text" :isRequire="false" text="Tên ngân hàng"></m-input-full>
                                </div>
                                <!-- Chi nhánh -->
                                <div class="w-25 pr-8">
                                    <m-input-full type="text" :isRequire="false" text="Chi nhánh"></m-input-full>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Popup content End -->
            </div>
            <div class="popup-footer-2 flex">
                <div class="popup-btn-left-group w-50">
                    <m-button class="btn-sub" text="Hủy"></m-button>
                </div>
                <div class="popup-btn-right-group flex w-50">
                    <m-button @click="" class="btn-sub mr-12" text="Cất"></m-button>
                    <m-button @click="btnSave()" text="Cất và thêm"></m-button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "EmployeeDetail",
    props: {
        employeeSelected: {

        },
        formMode: {

        }
    },
    data() {
        return {
            invalidEmployeeCode: false,
            invalidFullName: false,
            invalidDepartmentName: false,
            employee: {
                EmployeeCode: "",
                FullName: "",
                DepartmentName: "",
                DepartmentID: "",
                Email: "",
                Gender: "",
                PhoneNumber: "",
                PositionName: "",
                DateOfBirth: "",
                Address: "",
                IdentityNumber: ""
            },
            hasError: false,
            departments: [],
            positions: [],
        }
    },
    created() {
        this.employee = this.employeeSelected;
        /**
         * Thêm mới mã nhân viên
         * AUTHOR: DTTHANH (30/08/2023)
         */
        if (!this.employeeSelected || !this.employeeSelected.EmployeeCode) {
            try {
                this.$axios.get("https://cukcuk.manhnv.net/api/v1/Employees/NewEmployeeCode")
                    .then(res => {
                        // Cập nhật lại giá trị employee.EmployeeCode sau khi nhận được mã mới từ API
                        console.log(res);
                        this.employee.EmployeeCode = res.data;
                    });
            }
            catch (error) {
                this.$handle.handleAPIError(error);
            }
        }
        this.loadDeparment();
        this.loadPosition();
    },
    computed: {
        /**
         * Lọc lấy tên đơn vị 
         * AUTHOR: DTTHANH (30/08/2023)
         */
        setDepartments() {
            return this.departments.map((item) => item.name);
        },

        /**
         * Lọc lấy chức danh 
         * AUTHOR: DTTHANH (30/08/2023)
         */
        setPositions() {
            return this.positions.map((item) => item.name);
        },

    },
    methods: {
        /**
         * Phương thức đóng form dùng emit
         * AUTHOR: DTTHANH (19/08/2023) 
         */
        onClose() {
            this.$emit("onCloseForm");
        },

        /**
         * Lấy dữ liệu đơn vị từ API
         * AUTHOR: DTTHANH (30/08/2023)
         */
        loadDeparment() {
            try {
                let data = []
                this.$axios.get("https://cukcuk.manhnv.net/api/v1/Departments")
                    .then(res => {
                        data = res.data
                        data.forEach(element => {
                            this.departments.push({
                                id: element.DepartmentID,
                                name: element.DepartmentName
                            })
                        });
                    })

            } catch (error) {
                this.$handle.handleAPIError(error);
            }
        },

        /**
         * Lấy dữ liệu chức danh từ API
         * AUTHOR: DTTHANH (30/08/2023)
         */
        loadPosition() {
            try {
                let data = []
                this.$axios.get("https://cukcuk.manhnv.net/api/v1/Positions")
                    .then(res => {
                        data = res.data
                        data.forEach(element => {
                            this.positions.push({
                                id: element.PositionID,
                                name: element.PositionName
                            })
                        });
                    })

            } catch (error) {
                this.$handle.handleAPIError(error);
            }
        },


        /**
         * Lấy dữ liệu id đơn vị theo tên
         * AUTHOR: DTTHANH (30/08/2023)
         */
        getDepartmentId() {
            for (const department of this.departments) {
                if (department.name == this.employee.DepartmentName) {
                    this.employee.DepartmentId = department.id
                    break;
                }
            }
        },

        /**
         * Lấy dữ liệu id đơn vị theo tên
         * AUTHOR: DTTHANH (30/08/2023)
         */
        getPositionId() {
            for (const position of this.positions) {
                if (position.name == this.employee.PositionName) {
                    this.employee.PositionId = position.id
                    break;
                }
            }
        },

        /**
         * Hàm validate cho các trường bắt buộc
         * AUTHOR: DTTHANH (30/08/2023)
         */
        checkRequiredFields() {
            // Tạo mảng chứa lỗi
            const errors = [];
            // Kiểm tra các trường bắt buộc phải nhập
            if (!this.employee.EmployeeCode) {
                this.invalidEmployeeCode = true;
                errors.push(this.$resources[this.$LangCode].FormError.EmployeeCode);
            } else {
                this.invalidEmployeeCode = false;
            }

            if (!this.employee.FullName) {
                this.invalidFullName = true;
                errors.push(this.$resources[this.$LangCode].FormError.FullName);
            } else {
                this.invalidFullName = false;
            }

            if (!this.employee.DepartmentName) {
                this.invalidDepartmentName = true;
                errors.push(this.$resources[this.$LangCode].FormError.DepartmentName);
            } else {
                this.invalidDepartmentName = false;
            }

            return errors;
        },

        /**
         * Hàm thêm mới nhân viên
         * AUTHOR: DTTHANH (30/08/2023)
         */
        addEmployee() {
            const errors = this.checkRequiredFields();

            this.getDepartmentId();
            this.getPositionId();
            console.log(this.employee.DepartmentName);
            if (errors.length == 0) {
                this.$axios
                    .post("https://cukcuk.manhnv.net/api/v1/Employees", this.employee)
                    .then((res) => {
                        //Ẩn form khi hoàn thành
                        this.$emit("onClose");
                        console.log(res);
                        //Hiển thị toast thông báo thành công
                        this.$emitter.emit("showToast", this.$resources[this.$LangCode].Toast.Success, this.$resources[this.$LangCode].Toast.AddSuccess)
                        setTimeout(() => {
                            this.$emitter.emit("closeToast");
                        }, 3000);
                    })
                    .catch((res) => {
                        this.$handle.handleAPIError(res)
                    });
            } else {
                this.$handle.showDialog(this.$resources[this.$LangCode].Dialog.Invalid, errors, 3);
            }
        },

        editEmployee() {
            const employee = this.employee;
            const errors = this.checkRequiredFields();

            this.getDepartmentId();
            this.getPositionId();
            console.log(this.employee.DepartmentName);
            if (errors.length == 0) {
                this.$axios
                    .put(`https://cukcuk.manhnv.net/api/v1/Employees/${this.employeeSelected.EmployeeId}`, employee)
                    .then((res) => {
                        //Ẩn form khi hoàn thành
                        this.$emit("onClose");
                        console.log(res);
                        //Hiển thị toast thông báo thành công
                        this.$emitter.emit("showToast", this.$resources[this.$LangCode].Toast.Success, this.$resources[this.$LangCode].Toast.AddSuccess)
                        setTimeout(() => {
                            this.$emitter.emit("closeToast");
                        }, 3000);
                    })
                    .catch((res) => {
                        this.$handle.handleAPIError(res)
                    });
            } else {
                this.$handle.showDialog(this.$resources[this.$LangCode].Dialog.Invalid, errors, 3);
            }
        },
        /**
         * Lưu nhân viên vừa thêm
         * AUTHOR: DTTHANH (30/08/2023)
         */
        btnSave() {
            if (this.employeeSelected.EmployeeId) {
                this.editEmployee();
            } else {
                this.addEmployee();
            }
        }

    },
    mounted() {
        /**
         * Hàm focus ô nhập Mã
         * AUTHOR: DTTHANH (21/08/2023)
         */
        this.$nextTick(() => {
            this.$refs.inputCode.onFocus();
        });
    },
}
</script>

<style scoped></style>