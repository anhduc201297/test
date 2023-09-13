<template>
    <section class="header-section">
        <h3>Nhân viên</h3>
        <m-button @click="addEmpl">Thêm nhân viên mới</m-button>
    </section>
    <m-table @loadNewPage="getEmployees" ref="table" :headers="tableHeader" :totalRecords="97" :data="tableDatas" v-model="checkState" @refreshRecords="refresh" :showLoading="showLoading"
        :searchPlaceHolder="'Tìm theo mã, tên nhân viên'">
        <!-- <template #theaders>
            <th>Mã nhân viên</th>
            <th>Tên nhân viên</th>
            <th>Giới tính</th>
            <th class="tb-center">Ngày sinh</th>
            <th title="Chứng minh nhân dân">Số CMND</th>
            <th>Chức danh</th>
            <th>Tên đơn vị</th>
            <th>Số tài khoản</th>
            <th>Tên ngân hàng</th>
            <th>Chi nhánh TK ngân hàng</th>
            <th class="edit-btn">Chức năng</th>
        </template> -->
    </m-table>
    <EmployeeForm :formMode="formMode" @closeForm="closeForm" v-if="showDialog" />
</template>

<script>
import EmployeeForm from './EmployeeForm.vue';
import * as employeeServices from '@/services/employee-services'

export default {
    name: "EmployeeList",
    components: { EmployeeForm },
    methods: {
        /**
         * Fetch dữ liệu danh sách nhân viên
         * Author: dktuan (30/08/2023)
         */
        async getEmployees(size, page) {
            this.showLoading = true
            const res = await employeeServices.getEmployees(size, page);
            this.showLoading = false
            if (!res.error) this.tableDatas = res
            else {
                const toast = {
                    title: this.$message[this.$lang].toast.titles.WARNING,
                    type: this.$enum.ToastType.WARNING,
                    message: this.$message[this.$lang].toast.messages.ERROR_TABLE,
                }
                this.$emitter.emit('addToast', toast)
            }
        },
        /**
         * refresh các bản ghi và hiện toast message thông báo
         * Author: dktuan (19/08/2023)
         */
        refresh() {
            const toast = {
                title: this.$message[this.$lang].toast.titles.SUCCESS,
                type: this.$enum.ToastType.SUCCESS,
                message: this.$message[this.$lang].toast.messages.REFRESH_SUCCESS,
                action: this.$message[this.$lang].toast.actions.UNDO
            }
            this.$emitter.emit('addToast', toast)
        },
        /**
         * Mở form thêm nhân viên
         * Author: dktuan (19/08/2023)
         */
        addEmpl() {
            this.showDialog = true;
            this.formMode = this.$enum.FormType.ADD
        },
        /**
         * Mở form sửa thông tin nhân viên
         * Author: dktuan (24/08/2023)
         */
        editEmpl() {
            this.showDialog = true;
            this.formMode = this.$enum.FormType.EDIT
        },
        /**
         * Đóng form
         * Author: dktuan (19/08/2023)
         */
        closeForm() {
            this.showDialog = false;
            this.formData = {};
        },
    },
    data() {
        return {
            tableHeader: {
                EmployeeCode: {
                    title: "Mã nhân viên"
                },
                FullName: {
                    title: "Tên nhân viên"
                },
                Gender: {
                    title: "Giới tính"
                },
                DateOfBirth: {
                    title: "Ngày sinh",
                    class: "tb-center"
                },
                IdentityNumber: {
                    title: "Số CMND",
                    tooltip: "Số chứng minh nhân dân"
                },
                PositionName: {
                    title: "Chức danh",
                },
                DepartmentName: {
                    title: "Tên đơn vị",
                },
                BankAccountNumber: {
                    title: "Số tài khoản"
                },
                BankName: {
                    title: "Tên ngân hàng"
                },
                BankBranchName: {
                    title: "Chi nhánh TK ngân hàng",
                    tooltip: "Chi nhánh tài khoản ngân hàng"
                }
            },
            formMode: null,
            showLoading: false,
            tableDatas: [],
            showDialog: false,
            checkState: {
                isCheckAll: false,
                checkCount: 0,
            }
        };
    },
    async mounted() {
        await this.getEmployees(this.$refs.table.recordPerPage, this.$refs.table.currentPage)
    }
}
</script>

<style scoped></style>