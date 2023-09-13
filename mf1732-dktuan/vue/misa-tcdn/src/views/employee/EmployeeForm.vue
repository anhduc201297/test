<template>
    <m-modal :class="classes">
        <m-loading v-if="showLoading"></m-loading>
        <m-dialog @onClose="closeForm" :title="'Thông tin nhân viên'">
            <template #header-options>
                <input v-model="formData.isCustomer" class="checkbox" id="is-customer" type="checkbox" name="role"
                    value="0">
                <label for="is-customer">Là khách hàng</label>
                <input v-model="formData.isSupplier" class="checkbox" type="checkbox" name="role" value="1">
                <label for="is-supplier">Là nhà cung cấp</label>
            </template>
            <template #body>
                <div class="layout">
                    <div class="row">
                        <div class="col col-5">
                            <div class="row pd-6">
                                <div class="col col-4 ">
                                    <m-input ref="firstInput" v-model="formData.employeeCode" :label="'Mã'"
                                        :isRequired="true" :name="'employeeCode'"></m-input>
                                </div>
                                <div class="col col-6 pd-6">
                                    <m-input v-model="formData.fullName" :label="'Tên'" :isRequired="true"
                                        :name="'fullName'"></m-input>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col col-10" @click="()=>console.log(formData.departmentCode)">
                                    <m-combobox :label="'Đơn vị'" :isRequired="true" :items="items"
                                     v-model="formData.departmentCode"></m-combobox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col col-10">
                                    <m-input v-model="formData.title" :label="'Chức danh'" :name="'title'"></m-input>
                                </div>
                            </div>
                        </div>
                        <div class="col col-5">
                            <div class="row pd-6">
                                <div class="col col-4">
                                    <m-input v-model="formData.dateOfBirth" :label="'Ngày sinh'"
                                        :inputType="'date'"></m-input>
                                </div>
                                <div class="col col-6">
                                    <div class="label">
                                        <label class="label-name" for="">Giới tính</label>
                                    </div>
                                    <div class="input-wrapper">
                                        <input v-model="formData.gender" type="radio" class="radio-input" value="0"
                                            name="gender" id="">
                                        <label class="label-name">Nam</label>
                                        <input v-model="formData.gender" type="radio" class="radio-input" value="1"
                                            name="gender" id="">
                                        <label class="label-name">Nữ</label>
                                        <input v-model="formData.gender" type="radio" class="radio-input" value="2"
                                            name="gender" id="">
                                        <label class="label-name">Khác</label>
                                    </div>
                                </div>
                            </div>
                            <div class="row pd-6">
                                <div class="col col-6">
                                    <m-input v-model="formData.cmnd" :label="'Số CMND'"
                                        :labelTitle="'Số Chứng minh nhân dân'" :name="'cmnd'"></m-input>
                                </div>
                                <div class="col col-4">
                                    <m-input v-model="formData.releaseDate" :label="'Ngày cấp'" :inputType="'date'"
                                        :name="'releaseDate'"></m-input>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col col-10">
                                    <m-input v-model="formData.releasePlace" :label="'Nơi cấp'"
                                        :name="'releasePlace'"></m-input>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br>
                    <div class="row">
                        <div class="col col-10">
                            <div class="row pd-6">
                                <div class="col col-10">
                                    <m-input v-model="formData.address" :label="'Địa chỉ'" :name="'address'"></m-input>
                                </div>
                                <div class="col col-25">
                                    <m-input v-model="formData.mobilePhone" :label="'ĐT di động'" :name="'mobilePhone'"
                                        :labelTitle="'Điện thoại di động'"></m-input>
                                </div>
                                <div class="col col-25">
                                    <m-input v-model="formData.landlinePhone" :label="'ĐT cố định'" :name="'landlinePhone'"
                                        :labelTitle="'Điện thoại cố định'"></m-input>
                                </div>
                                <div class="col col-25">
                                    <m-input v-model="formData.email" :label="'Email'" :name="'email'"></m-input>
                                </div>
                            </div>
                            <div class="row pd-6">
                                <div class="col col-25">
                                    <m-input v-model="formData.bankAccount" :label="'Tài khoản ngân hàng'"
                                        :name="'bankAccount'"></m-input>
                                </div>
                                <div class="col col-25">
                                    <m-input v-model="formData.bankName" :label="'Tên ngân hàng'"
                                        :name="'bankName'"></m-input>
                                </div>
                                <div class="col col-25">
                                    <m-input v-model="formData.bankBranch" :label="'Chi nhánh'"
                                        :name="'bankBranch'"></m-input>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </template>
            <template #footer>
                <m-button :class="'outline'">Hủy</m-button>
                <m-button :class="'outline'" @click="store">Cất</m-button>
                <m-button @click="storeAndAdd">Cất và Thêm</m-button>
            </template>
        </m-dialog>
    </m-modal>
    <m-modal :class="classes" v-if="dialog.show">
        <m-dialog @onClose="closeDialog" :width="'540px'" :class="'no-footer-border'" :title="dialog.title">
            <template #body>
                <div class="noti-content">
                    <span class="icon warning-icon"></span>
                    {{ dialog.msg }}
                </div>
            </template>
            <template #footer>
                <!-- <m-button :class="'left-align outline'">{{ this.$message[this.$lang].form.buttons.CANCEL }}</m-button> -->
                <m-button :class="'left-align'" @click="closeDialog">{{ this.$message[this.$lang].form.buttons.AGREE
                }}</m-button>
            </template>
        </m-dialog>
    </m-modal>
</template>

<script>
import * as employeeServices from '@/services/employee-services'
import * as departmentServices from '@/services/department-services'
export default {
    emits: ['closeForm'],
    props: ['formMode', 'classes'],
    methods: {
        /**
         * lấy danh sách đơn vị
         * Author: dktuan (20/08/2023)
         */
        async getDepartments() {
            const res = await departmentServices.getDepartments();
            return res;
        },
        /**
         * gửi form, hiện dialog (lỗi) hoặc toast (thành công)
         * Author: dktuan (20/08/2023)
         */
        async submit() {
            this.showLoading = true
            const res = await employeeServices.addEmployees(this.formData, this.dialog)
            this.showLoading = false
            if (res.error) {
                this.dialog = { ...this.dialog, ...res }
                return false;
            }
            else {
                const toast = {
                    title: this.$message[this.$lang].toast.titles.SUCCESS,
                    type: this.$enum.ToastType.SUCCESS,
                    message: this.$message[this.$lang].toast.messages.ADD_SUCCESS,
                    action: this.$message[this.$lang].toast.actions.UNDO
                }
                this.$emitter.emit('addToast', toast)
                return true;
            }
        },
        /**
         * cất
         * Author: dktuan (30/08/2023)
         */
        async store() {
            await this.submit();
            this.closeForm();
        },
        /**
         * cất và thêm
         * Author: dktuan (30/08/2023)
         */
        async storeAndAdd() {
            console.log(this.formData)
            // const res = await this.submit();
            // if (res) this.formData = { ...this.defaultFormData, employeeCode: Number(this.formData.employeeCode) + 1 }
        },
        /**
         * emit cho component cha ẩn form
         * Author: dktuan (19/08/2023)
         */
        closeForm() {
            this.$emit('closeForm')
            this.formData = {}
            // this.formData={...this.defaultFormData}
        },
        /**
         * Đóng dialog
         * Author: dktuan (24/08/2023)
         */
        closeDialog() {
            this.dialog.show = false;
        }
    },

    data() {
        return {
            items: [],
            selectedItem: null,
            showLoading: false,
            dialog: {
                show: false,
                title: "",
                msg: "",
            },
            defaultFormData: {
                isCustomer: "",
                isSupplier: "",
                fullName: "",
                employeeCode: "",
                dateOfBirth: "",
                gender: "",
                departmentCode: "",
                cmnd: "",
                releaseDate: "",
                releasePlace: "",
                title: "",
                address: "",
                mobilePhone: "",
                landlinePhone: "",
                bankAccount: "",
                bankName: "",
                bankBranch: "",
                email: ""
            },
            formData: {
            }
        }

    },
    async created() {
        this.items = await this.getDepartments();
    },
    mounted() {
        /**
         * focus first input
         * Author: dktuan (24/08/2023)
         */
        this.$refs.firstInput.focus()
    }
}
</script>

<style scoped>
@import url(./employee-list.css);
.layout {
    margin-bottom: 32px;
}
</style>