<template>
    <div
        @click.prevent="handleClickOut"
        class="pop-up-container"
        @keydown="listenKeyDown"
        tabindex="0"
    >
        <div ref="dialog" class="pop-up-form">
            <div class="pop-up-header">
                <h1 class="m-text-head-1">{{
                    this.formMode === this.$MEnum.FormMode.Create
                        ? 'Tạo mới nhân viên'
                        : 'Thông tin nhân viên'
                }}</h1>

                <div class="pop-head-btns">
                    <m-btn-no-text
                        class="lg icon-24 icon-ask"
                        title="Trợ giúp (F1)"
                    />

                    <m-btn-no-text
                        title="Đóng (ESC)"
                        class="lg icon-24 icon-close"
                        @click="onClickCloseBtn"
                    />
                </div>
            </div>
            <div class="pop-body">
                <div class="form-row gutter-32">
                    <div class="w-half">
                        <div class="form-row gutter-8">
                            <div class="form-row-item w-40p">
                                <m-input-text
                                    tabindex="1"
                                    ref="EmployeeCode"
                                    :label="'Mã'"
                                    :rules="[
                                        `max|20|${this.$Resource.maxDigitError(
                                            20
                                        )}`,
                                        `format|endWithNum|${this.$Resource.NotEndWithNumber}`
                                    ]"
                                    v-model="formData.employeeCode"
                                    required
                                />
                            </div>
                            <div class="form-row-item w-60p">
                                <m-input-text
                                    ref="FullName"
                                    tabindex="1"
                                    :label="'Tên'"
                                    :rules="[
                                        `max|100|${this.$Resource.maxDigitError(
                                            100
                                        )}`
                                    ]"
                                    required
                                    v-model="formData.fullName"
                                />
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-row-item w-full">
                                <m-combo-box
                                    :label="'Đơn vị'"
                                    ref="Department"
                                    :rules="[
                                        `max|255|${this.$Resource.maxDigitError(
                                            255
                                        )}`
                                    ]"
                                    v-model="departmentForm"
                                    :comboboxItems="departmentConfig"
                                    :focusColor="'#50b83c'"
                                    tabindex="1"
                                    required
                                ></m-combo-box>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-row-item w-full">
                                <m-input-text
                                    tabindex="1"
                                    :label="'Chức danh'"
                                    :rules="[
                                        `max|255|${this.$Resource.maxDigitError(
                                            255
                                        )}`
                                    ]"
                                    v-model="formData.positionName"
                                />
                            </div>
                        </div>
                    </div>
                    <div class="w-half">
                        <div class="form-row gutter-8">
                            <div class="form-row-item w-40p">
                                <m-input-text
                                    tabindex="1"
                                    ref="Birthday"
                                    :rules="[
                                        `earlier|${new Date()}|${
                                            $Resource.ErrorDateOfBirth
                                        }`
                                    ]"
                                    label="Ngày sinh"
                                    title="Tháng / Ngày / Năm"
                                    :inputType="'date'"
                                    v-model="formData.birthday"
                                />
                            </div>
                            <div class="form-row-item w-60p">
                                <label class="m-label">Giới tính</label>
                                <div class="radio-group gutter-8">
                                    <m-radio
                                        :tabindex="1"
                                        v-for="(value, index) of this.$MEnum
                                            .Gender"
                                        :key="index"
                                        v-model="formData.gender"
                                        :value="value"
                                        :label="this.$Resource.Gender[value]"
                                        :name="'gender'"
                                    />
                                </div>
                            </div>
                        </div>
                        <div class="form-row gutter-8">
                            <div class="form-row-item w-60p">
                                <m-input-text
                                    tabindex="1"
                                    :rules="[
                                        `max|25|${this.$Resource.maxDigitError(
                                            25
                                        )}`
                                    ]"
                                    ref="IdentityNumber"
                                    title="Số chứng minh nhân dân"
                                    :label="'Số CMND'"
                                    :tooltip="'Số chứng minh nhân dân'"
                                    v-model="formData.identityNo"
                                />
                            </div>
                            <div class="form-row-item w-40p">
                                <m-input-text
                                    tabindex="1"
                                    :rules="[
                                        `earlier|${new Date()}|${
                                            this.$Resource.ErrorDateOfIdentity
                                        }`
                                    ]"
                                    ref="IdentityDate"
                                    label="Ngày cấp"
                                    title="Tháng / Ngày / Năm"
                                    :inputType="'date'"
                                    v-model="formData.identityDate"
                                />
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-row-item w-full">
                                <m-input-text
                                    tabindex="1"
                                    ref="IdentityPlace"
                                    :label="'Nơi cấp'"
                                    :rules="[
                                        `max|255|${this.$Resource.maxDigitError(
                                            255
                                        )}`
                                    ]"
                                    v-model="formData.identityPlace"
                                />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-row pt-16">
                    <div class="form-row-item w-full">
                        <m-input-text
                            tabindex="1"
                            label="Địa chỉ"
                            ref="Address"
                            :rules="[
                                `max|255|${this.$Resource.maxDigitError(255)}`
                            ]"
                            v-model="formData.address"
                        />
                    </div>
                </div>
                <div class="form-row gutter-8">
                    <div class="form-row-item w-25p">
                        <m-input-text
                            tabindex="1"
                            label="ĐT di động"
                            ref="PhoneMobile"
                            :rules="[
                                `max|50|${this.$Resource.maxDigitError(50)}`
                            ]"
                            :tooltip="'Điện thoại di động'"
                            v-model="formData.phoneMobile"
                        />
                    </div>
                    <div class="form-row-item w-25p">
                        <m-input-text
                            tabindex="1"
                            label="ĐT cố định"
                            ref="PhoneHome"
                            :tooltip="'Điện thoại cố định'"
                            :rules="[
                                `max|50|${this.$Resource.maxDigitError(50)}`
                            ]"
                            v-model="formData.phoneHome"
                        />
                    </div>
                    <div class="form-row-item w-25p">
                        <m-input-text
                            ref="Email"
                            tabindex="1"
                            label="Email"
                            :inputType="'email'"
                            :rules="[
                                `format|email|${this.$Resource.EmailNotFormatted}`,
                                `max|100|${this.$Resource.maxDigitError(100)}`
                            ]"
                            placeholder="abc@xyz.com"
                            v-model="formData.email"
                        />
                    </div>
                </div>
                <div class="form-row gutter-8">
                    <div class="form-row-item w-25p">
                        <m-input-text
                            tabindex="1"
                            ref="BankAccountNo"
                            :label="'Tài khoản ngân hàng'"
                            :rules="[
                                `max|255|${this.$Resource.maxDigitError(255)}`
                            ]"
                            v-model="formData.bankAccountNo"
                        />
                    </div>
                    <div class="form-row-item w-25p">
                        <m-input-text
                            tabindex="1"
                            ref="BankName"
                            :label="'Tên ngân hàng'"
                            :rules="[
                                `max|255|${this.$Resource.maxDigitError(255)}`
                            ]"
                            v-model="formData.bankName"
                        />
                    </div>
                    <div class="form-row-item w-25p">
                        <m-input-text
                            tabindex="1"
                            ref="BankAccountPlace"
                            :label="'Chi nhánh ngân hàng'"
                            :rules="[
                                `max|255|${this.$Resource.maxDigitError(255)}`
                            ]"
                            v-model="formData.bankAccountPlace"
                        />
                    </div>
                </div>
            </div>
            <div class="pop-footer">
                <div class="btns-left">
                    <m-button
                        tabindex="5"
                        @click="onCloseFormDetails"
                        title="Hủy"
                        :type="'sub'"
                        class="text-bold focus-outline"
                        @keydown.tab.prevent="focusFirstInput"
                        >Hủy</m-button
                    >
                </div>
                <div class="btns-right">
                    <m-button
                        tabindex="3"
                        @click="onClickSubBtn"
                        title="Cất (CTRL + S)"
                        :type="'sub'"
                        class="text-bold focus-outline"
                        :isLoading="isLoading"
                        >Cất</m-button
                    >
                    <m-button
                        tabindex="4"
                        @click="onClickOkBtn"
                        title="Cất và thêm (CTRL + SHIFT + S)"
                        :type="'primary'"
                        class="text-bold focus-outline"
                        :isLoading="isLoading"
                        >Cất và thêm</m-button
                    >
                </div>
            </div>
        </div>
        <m-dialog
            v-if="isShowDialog"
            :content="this.$Resource.ConfirmUpdateRecords"
            :title="this.$Resource.ConfirmSaveText"
            type="Question"
            @clickOk="onSubmit"
            @clickSub="onCloseFormDetails"
            @clickCancel="onCloseDialogConfirm"
        />

        <m-mask v-if="isLoading"><m-loading /></m-mask>
    </div>
</template>

<script>
import {
    createEmployeeApi,
    getNewCodeEmployeeApi,
    updateEmployeeApi
} from '@/js/requests/employees.request';
import MMask from '@/components/base/mask/MMask.vue';

export default {
    name: 'EmployeeDetails',
    inject: ['onOpenToast', 'onOpenDialogError'],
    components: {
        MMask
    },
    props: {
        /**
         * thông tin nhân viên
         */
        dataInput: Object
    },

    data() {
        return {
            isLoading: false,

            formData: null,
            formMode: null,
            isShowDialog: false,

            departmentConfig: null,
            departmentForm: { value: null, label: null },
            isContinueCreate: false
        };
    },

    methods: {
        /**
         * Kiểm soát click ra khỏi dialog
         * @param {Event} e
         * @author NTVu - 11/09/2023
         */
        handleClickOut(e) {
            const target = e.target;

            if (target.contains(this.$refs.dialog)) {
                this.$refs.EmployeeCode.focus();
            }
        },
        /**
         * Lấy Employee Code mới từ server
         * @author NTVu - 29/08/2023
         */
        async getNewCode() {
            try {
                this.isLoading = true;
                const res = await getNewCodeEmployeeApi();
                this.formData.employeeCode = res.data.toString();
            } catch (error) {
                this.formData.employeeCode = '';

                this.onOpenToast({
                    title: error.message,
                    type: this.$MEnum.ToastType.Warning
                });
            } finally {
                this.isLoading = false;
            }
        },
        /**
         * Thêm mới nhân viên (tạo request lên server)
         * @author NTVu - (27/08/2023)
         */
        async createEmployee() {
            try {
                this.isLoading = true;
                this.formData.createdBy = 'Vuz';
                this.formData.createdDate = Date.now;
                this.formData.departmentName = this.departmentForm.label;
                const res = await createEmployeeApi(this.formData);

                if (res.status === 201) {
                    this.onOpenToast({
                        title: this.$Resource.AddEmployeeSuccess,
                        type: this.$MEnum.ToastType.Info
                    });
                    this.checkContinueCreate();
                }
            } catch (error) {
                this.handleError(error);
            } finally {
                this.isLoading = false;
            }
        },
        /**
         * Thêm mới nhân viên (tạo request lên server)
         * @author NTVu - (27/08/2023)
         */
        async updateEmployee() {
            try {
                this.isLoading = true;
                this.formData.departmentName = this.departmentForm.label;

                const res = await updateEmployeeApi(
                    this.formData,
                    this.formData.employeeId
                );

                if (res.data > 0) {
                    this.onOpenToast({
                        title: this.$Resource.UpdateEmployeeSuccess,
                        type: this.$MEnum.ToastType.Info
                    });

                    this.checkContinueCreate();
                } else {
                    this.onOpenDialogError({
                        title: this.$Resource.Error,
                        content: this.$Resource.ErrorText
                    });
                }
            } catch (error) {
                this.handleError(error);
            } finally {
                this.isLoading = false;
            }
        },
        /**
         * Focus vào input đầu tiên khi mở form
         * @Author NTVu - MF1742 - 19/08/2023
         */
        focusFirstInput() {
            this.$refs.EmployeeCode.autoFocus();
        },
        /**
         * Hàm set value cho trường 'Đơn vị' của employee
         * @Author NTVu - MF1742 - 19/08/2023
         * @param {String} newValue
         */
        setDepartment(newName) {
            this.formData.departmentName = newName;
        },
        /**
         * Lắng nghe các phím tắt của Form, nếu dialog confirm đang bật thì không lắng nghe phím tắt
         * @Author NTVu - MF1742 (20/08/2023)
         * @param {KeyboardEvent} e
         */
        listenKeyDown(e) {
            if (this.isShowDialog) {
                return;
            }
            if (e.key === 'Escape') {
                this.onClickCloseBtn();
            } else if (e.ctrlKey && e.shiftKey && e.key === 'S') {
                e.preventDefault();

                this.isContinueCreate = true;
                this.onSubmit();
            } else if (e.ctrlKey && e.key === 's') {
                e.preventDefault();

                this.isContinueCreate = false;
                this.onSubmit();
            }
        },
        /**
         * Validate dữ liệu của tất cả input và trả về field đầu tiên bị lỗi
         * @Author NTVu - 20/08/2023
         * @Modified: NTVu - Thay đổi kiểu trả về để hiển thị lên dialog
         */
        async validateData() {
            let firstError = null;

            for (const ref of Object.values(this.$refs)) {
                if (firstError) {
                    ref.validate && (await ref.validate());
                } else if (ref.validate && !(await ref.validate(true))) {
                    firstError = ref;
                }
            }

            return firstError;
        },
        /**
         * hàm đóng dialog hiện tại
         * @Author NTVu - MF1742 - 19/08/2023
         */
        onCloseFormDetails() {
            this.$emit('onCloseFormDetails');
        },
        /**
         * Mở dialog xác nhận lại quyết định của người dùng
         * @Author NTVu - MF1742 (19/08/2023)
         * Modified: NTVu - 20/08/2023
         */
        onShowDialogConfirm() {
            this.isShowDialog = true;
        },
        /**
         * Hàm đóng dialog xác nhận lại quyết định của người dùng
         * @Author NTVu - MF1742 - 19/08/2023
         */
        onCloseDialogConfirm() {
            this.isShowDialog = false;
            this.focusFirstInput();
        },
        /**
         * Sự kiện khi bấm nút Cất và thêm, nếu data validate ok thì submit
         * @Author NTVu - MF1742 - 25/08/2023
         */
        async onClickOkBtn() {
            const error = await this.validateData();
            if (!error) {
                this.onShowDialogConfirm();
                this.isContinueCreate = true;
            }
        },
        /**
         * Sự kiện khi bấm nút Cất, nếu data validate ok thì hiện dialog confirm
         * @Author NTVu - MF1742 - 25/08/2023
         */
        async onClickSubBtn() {
            const error = await this.validateData();
            if (!error) {
                this.onShowDialogConfirm();
                this.isContinueCreate = false;
            }
        },
        /**
         * Sự kiện khi bấm nút Close (X), hiện dialog xác nhận ng dùng muốn thay đổi hoặc không
         * @Author NTVu - MF1742 - 25/08/2023
         */
        onClickCloseBtn() {
            this.isContinueCreate = false;
            this.onShowDialogConfirm();
        },

        /**
         * Submit form hiện tại (DEMO)
         * @Author NTVu - MF1742 - 19/08/2023
         */
        async onSubmit() {
            const error = await this.validateData();
            if (error) {
                this.onCloseDialogConfirm();
                if (error) {
                    this.onOpenDialogError(
                        {
                            title: this.$Resource.Error,
                            content: error.errorMessage
                        },
                        error.autoFocus
                    );
                }
                return;
            }
            this.onCloseDialogConfirm();
            if (this.formMode === this.$MEnum.FormMode.Create) {
                await this.createEmployee();
            } else if (this.formMode === this.$MEnum.FormMode.Edit) {
                await this.updateEmployee();
            }
        },
        /**
         * TIếp tục thêm mới hoặc không
         * @author NTVu - (27/08/2023)
         */
        checkContinueCreate() {
            if (this.isContinueCreate === true) {
                const oldDepartment = this.formData.departmentName;
                this.formData = {
                    gender: this.$MEnum.Gender.Male,
                    departmentName: oldDepartment
                };
                this.getNewCode();
                this.focusFirstInput();
                this.formMode = this.$MEnum.FormMode.Create;
                return;
            }
            this.onCloseFormDetails();
        },
        /**
         * Xử lí lỗi khi fetch API
         * @author NTVu - 30/08/2023
         * @param {object} error
         * @param {object} error.data
         * @param {string} error.message
         */
        handleError({ data, message }) {
            const keys = Object.keys(data);
            if (keys.length > 0) {
                // Lặp qua từng trường lỗi => hiện lỗi được gửi từ server
                keys.forEach((k) => {
                    if (this.$refs[k]) this.$refs[k].onShowError(data[k]);
                });

                // Mở dialog cảnh báo lỗi đầu tiên, focus vào input lỗi đó nếu user đóng dialog
                if (data[keys[0]]) {
                    this.onOpenDialogError(
                        {
                            title: this.$Resource.Error,
                            content: data[keys[0]]
                        },
                        this.$refs[keys[0]]?.autoFocus()
                    );
                }
                return;
            }
            this.onOpenDialogError(
                {
                    content: message,
                    title: this.$Resource.Error
                },
                this.focusFirstInput
            );
        }
    },
    /**
     * Copy dataInput vào formData để thay đổi, set các dữ liệu mặc định nếu là form tạo mới
     * Convert các input Date để hiển thị lên Date Input
     * @Author NTVu - MF1742
     * Modified: NTVu - 19/08/2023
     */
    created() {
        if (this.dataInput) {
            // Đặt trạng thái form là Update
            this.formMode = this.$MEnum.FormMode.Edit;

            // Cài giá trị cho form
            this.formData = this.dataInput;

            this.departmentForm.label = this.formData.departmentName;

            this.formData.birthday = this.dataInput.birthday
                ? this.$Helper.formatDate(this.dataInput.birthday, 'yyyy-MM-dd')
                : null;
            this.formData.identityDate = this.dataInput.identityDate
                ? this.$Helper.formatDate(
                      this.dataInput.identityDate,
                      'yyyy-MM-dd'
                  )
                : null;
        } else {
            // Đặt trạng thái form là Create
            this.formMode = this.$MEnum.FormMode.Create;

            // Cài lại giá trị cho form
            this.formData = {};

            this.formData.gender = this.$MEnum.Gender.Male;
            this.formData.fullName = 'Test Name';
        }
        this.departmentConfig = {
            header: {
                render: () => `<div class="w-30p color-text">Mã đơn vị</div>
                             <div class="w-70p color-text">Tên đơn vị</div>`
            },
            body: {
                render: (_, label) => {
                    return `<span class="w-30p color-text">${this.$Helper.abbreviateString(
                        label
                    )}</span>
                            <span class="w-70p color-text">${label}</span>`;
                },
                url: `${process.env.VUE_APP_API_END_POINT}/api/v1/Departments`
            },
            footer: {
                label: 'Cập nhật cơ cấu tổ chức'
            }
        };
    },
    /**
     * Khi mở form, nếu là mode create thì lấy code mới từ server
     * @author NTVu - 29/08/2023
     */
    beforeMount() {
        if (this.formMode === this.$MEnum.FormMode.Create) this.getNewCode();
    },
    /**
     * Khi form được bật: focus vào input đầu tiên
     * @Author NTVu - MF1742
     * Modified: NTVu - 19/08/2023
     */
    mounted() {
        this.focusFirstInput();
    }
};
</script>

<style scoped>
@import url('./employeeDetails.css');
</style>
