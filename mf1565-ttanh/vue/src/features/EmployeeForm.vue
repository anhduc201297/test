<script setup>
import { ref, useSlots } from 'vue';

import { IconClose, IconHelp } from '@/assets/icons';
import {
    VButton,
    VCheckbox,
    VDateField,
    VDialog,
    VRadioButton,
    VRadioField,
    VTextField
} from '@/components';

import { useDialogStore } from '@/stores/dialog';
import { useEmployeeStore } from '@/stores/employee';
import { useToastMessageStore } from '@/stores/toastMessage';

import { DialogName, Gender, ToastMessageType } from '@/utils/enums';
import { resources } from '@/utils/resources';
import * as dataHandler from '@/utils/dataHandler';

// global states
const Employee = useEmployeeStore();
const Dialog = useDialogStore();
const ToastMessage = useToastMessageStore();

// resources theo ngôn ngữ
const VNDialogResources = resources.VN.Dialog;
const ToastMessageResources = resources.VN.ToastMessage;

const slots = useSlots();

// lưu dữ liệu nhân viên hiện tại vào biến details
const details = ref(
    Employee.isShowNewDetails ? { ...Employee.initFields } : { ...Employee.current }
);

/**
 * === SỬA DỮ LIỆU NHÂN VIÊN ===
 */

/**
 * cập nhật input được sửa vào object details
 * @param {event} event
 * @param {string} property
 * create by: TTANH - 20/08/2023
 * modified by: TTANH - 21/08/2023
 */
// đối với text field
const handleInput = (event, property) => {
    details.value[property] = event.target.value;
};

// đổi với radio field
const handleCheckRadio = (event, property) => {
    details.value[property] = Number(event);
};

// đối với date picker
const handlePickDate = (event, property) => {
    details.value[property] = event;
};

const isUpdated = ref(false);

// lưu trữ danh sách lỗi input
const inputErrorList = ref(new Set());

/**
 * thêm thông báo lỗi vào inputErrorList
 * @param {string} errMsg
 * create by: TTANH - 22/08/2023
 */
const addError = (errMsg) => {
    inputErrorList.value.add(errMsg);
};

/**
 * loại bỏ thông báo lỗi khỏi inputErrorList
 * @param {string} errMsg
 * create by: TTANH - 22/08/2023
 */
const removeError = (errMsg) => {
    inputErrorList.value.delete(errMsg);
};

/**
 * ẩn form khi click vào close icon
 * 1. thực hiện so sánh dữ liệu trên form và dữ liệu gốc của nhân viên hiện tại
 * 2. nếu khác nhau, hiển thị Dialog xác nhận lưu thay đổi
 * 3. nếu không, ẩn form chi tiết
 * create by: TTANH - 22/08/2023
 * modified by: TTANH - 23/08/2023
 */
const handleHideForm = () => {
    console.log('handleHideForm');
    console.log('details:', details.value);
    console.log('Employee.current:', Employee.current);

    if (JSON.stringify(details.value) !== JSON.stringify(Employee.current)) {
        console.log(123);
        isUpdated.value = true;
        Dialog.show(DialogName.HideUpdatedEmployeeDetails);
    } else {
        Employee.details.hide();
    }
};

const isShowValidateDialog = ref(false);

// thực hiện validate form và hiển thị Dialog Validate
const validateForm = () => {
    isShowValidateDialog.value = true;
    Dialog.show(DialogName.Validate);
};

/**
 * cập nhật danh sách nhân viên
 * 1. nếu Employee.isShowNewDetails là true, thêm nhân viên
 * - validateForm
 * - nếu không có thông báo lỗi khi validate, thực hiện thêm nhân viên
 *   - nếu mã nhân viên chưa tồn tại, thêm nhân viên đó, thông báo thành công và đóng form
 *   - ngược lại, hiển thị dialog thông báo mã nhân viên trùng lặp
 * - nếu gặp exception, gửi message lỗi
 * 2. nếu Employee.isShowNewDetails là false, cập nhật nhân viên
 * - validateForm
 * - nếu không có thông báo lỗi khi validate, thực hiện cập nhật nhân viên
 *   - nếu mã nhân viên chưa tồn tại, cập nhật nhân viên đó, thông báo thành công và đóng form
 *   - ngược lại, hiển thị dialog thông báo mã nhân viên trùng lặp
 * - nếu gặp exception, gửi message lỗi
 * create by: TTANH - 23/08/2023
 * modified by: TTANH - 28/08/2023
 */
const isDuplicatedEmployeeCode = ref(false);
const handleStoreForm = () => {
    if (Employee.isShowNewDetails) {
        try {
            validateForm();
            if (!inputErrorList.value.size) {
                if (Employee.getByCode(details.value.EmployeeCode) === -1) {
                    Employee.add(details.value);

                    ToastMessage.show(
                        ToastMessageType.Success,
                        ToastMessageResources.AddEmployee.success(details.value.EmployeeCode)
                    );

                    Employee.details.hide();
                } else {
                    isDuplicatedEmployeeCode.value = true;
                    Dialog.show(DialogName.DuplicatedEmployeeCode);
                }
            }
        } catch (err) {
            Employee.details.hide();
            ToastMessage.show(ToastMessageType.Error, ToastMessageResources.AddEmployee.error);
            console.log(err);
        }
    } else {
        try {
            validateForm();
            if (!inputErrorList.value.size) {
                if (Employee.getEmployeeByCode(details.value.EmployeeCode) === -1) {
                    Employee.updateEmployee(Employee.current.EmployeeID, details.value);

                    ToastMessage.show(
                        ToastMessageType.Success,
                        ToastMessageResources.UpdateEmployee.success(
                            Employee.current.EmployeeCode
                        )
                    );
                    isUpdated.value = false;
                    Employee.details.hide();
                } else if (details.value.EmployeeCode !== Employee.current.EmployeeCode) {
                    isDuplicatedEmployeeCode.value = true;
                    Dialog.show(DialogName.DuplicatedEmployeeCode);
                } else {
                    Employee.details.hide();
                }
            }
        } catch (err) {
            isUpdated.value = false;
            Employee.details.hide();

            ToastMessage.show(
                ToastMessageType.Error,
                ToastMessageResources.UpdateEmployee.error(Employee.current.EmployeeCode)
            );
            console.log(err);
        }
    }
};

// ẩn Dialog Validate khi click vào nút Đóng của Dialog Validate
const hideValidateDialog = () => {
    isShowValidateDialog.value = false;
    Dialog.hide();
};

// ẩn Dialog Duplicated EmployeeCode khi click vào nút Đóng của Duplicated EmployeeCode
const hideDuplicatedEmployeeCodeDialog = () => {
    isDuplicatedEmployeeCode.value = false;
    Dialog.hide();
};

// hủy cập nhật dữ liệu nhân viên và đóng form khi click vào nút hủy
const cancelUpdateForm = () => {
    details.value = {};
    isUpdated.value = false;
    Employee.details.hide();
};

/**
 * cất và tạo mới form
 * create by: TTANH - 28/08/2023
 * modified by: TTANH - 29/08/2023
 */
const handleStoreAndCreateForm = () => {
    try {
        handleStoreForm();
        setTimeout(() => {
            Employee.createDetails();
        }, 1);
    } catch (err) {
        console.log(err);
    }
};
</script>

<template>
    <div class="form">
        <div class="form-container">
            <!-- form header -->
            <div class="form-header">
                <!-- form-header left-group -->
                <div class="left-group">
                    <div class="heading-1 heading">Thông tin nhân viên</div>
                    <VCheckbox id="customer-checkbox" label="Là khách hàng" />
                    <VCheckbox id="provider-checkbox" label="Là nhà cung cấp" />
                </div>

                <!-- form-header right-group -->
                <div class="right-group">
                    <IconHelp class="help-icon" />
                    <slot v-if="slots.closeIcon"></slot>
                    <IconClose v-else class="close-icon" @click="handleHideForm" />
                </div>
                <!-- hide updated employee details dialog -->
                <VDialog
                    v-if="
                        isUpdated && Dialog.current === DialogName.HideUpdatedEmployeeDetails
                    "
                >
                    <template #title>
                        {{ VNDialogResources.HideUpdatedEmployeeDetails.title }}
                    </template>
                    <template #text>
                        {{ VNDialogResources.HideUpdatedEmployeeDetails.text }}
                    </template>
                    <template #primaryAction>
                        <VButton type="primary" @click="handleStoreForm">
                            {{ VNDialogResources.HideUpdatedEmployeeDetails.primaryAction }}
                        </VButton>
                    </template>
                    <template #secondaryAction>
                        <VButton type="outline" @click="cancelUpdateForm">
                            {{ VNDialogResources.HideUpdatedEmployeeDetails.secondaryAction }}
                        </VButton>
                    </template>
                </VDialog>
            </div>

            <!-- form content -->
            <div class="form-content">
                <div class="group-1">
                    <div class="left-group">
                        <div style="display: flex; gap: 8px">
                            <VTextField
                                size="small"
                                width="medium"
                                id="employee-code"
                                required
                                :value="details.employeeCode"
                                @input="handleInput($event, 'EmployeeCode')"
                                :errMsgs="{ isEmpty: `Mã không được để trống` }"
                                @add-error-message="addError"
                                @remove-error-message="removeError"
                                firstFocus
                            >
                                <template #label>Mã</template>
                            </VTextField>

                            <VTextField
                                size="small"
                                width="large"
                                id="full-name"
                                title="title"
                                required
                                :value="details.fullName"
                                @input="handleInput($event, 'FullName')"
                                :errMsgs="{ isEmpty: 'Tên không được để trống' }"
                                @add-error-message="addError"
                                @remove-error-message="removeError"
                            >
                                <template #label>Tên</template>
                            </VTextField>
                        </div>

                        <VTextField
                            size="small"
                            width="full"
                            id="department-name"
                            required
                            :value="details.departmentName"
                            @input="handleInput($event, 'DepartmentName')"
                            :errMsgs="{
                                isEmpty: `Đơn vị không được để trống`,
                                isInvalid: `Dữ liệu Đơn vị không có trong danh mục`
                            }"
                            @add-error-message="addError"
                            @remove-error-message="removeError"
                        >
                            <template #label>Đơn vị</template>
                        </VTextField>

                        <VTextField
                            size="small"
                            width="full"
                            id="position-name"
                            :value="details.positionName"
                            @input="handleInput($event, 'PositionName')"
                        >
                            <template #label>Chức danh</template>
                        </VTextField>
                    </div>

                    <!-- Form group 2 -->
                    <div class="right-group">
                        <div style="display: flex; justify-content: space-between">
                            <VDateField
                                size="small"
                                id="date-of-birth"
                                :value="dataHandler.getDate(details.dateOfBirth)"
                                @update:input="handlePickDate($event, 'DateOfBirth')"
                                :errMsgs="{
                                    isInvalid: `Ngày sinh không hợp lệ`
                                }"
                            >
                                <template #label>Ngày sinh</template>
                            </VDateField>

                            <VRadioField>
                                <template #legend>Giới tính</template>
                                <template #input-group>
                                    <VRadioButton
                                        id="male"
                                        name="gender"
                                        :value="Gender.Male"
                                        :checked="details.gender === Gender.Male"
                                        @check-radio="handleCheckRadio($event, 'Gender')"
                                    >
                                        <template #label>Nam</template>
                                    </VRadioButton>

                                    <VRadioButton
                                        id="female"
                                        name="gender"
                                        :value="Gender.Female"
                                        :checked="details.gender === Gender.Female"
                                        @check-radio="handleCheckRadio($event, 'Gender')"
                                    >
                                        >
                                        <template #label>Nữ</template>
                                    </VRadioButton>

                                    <VRadioButton
                                        id="other"
                                        name="gender"
                                        :value="Gender.Other"
                                        :checked="details.gender === Gender.Other"
                                        @check-radio="handleCheckRadio($event, 'Gender')"
                                    >
                                        <template #label>Khác</template>
                                    </VRadioButton>
                                </template>
                            </VRadioField>
                        </div>

                        <div style="display: flex; gap: 8px">
                            <VTextField
                                size="small"
                                width="large"
                                id="identity-number"
                                title="Số chứng minh nhân dân"
                                :value="details.identityNumber"
                                @input="handleInput($event, 'IdentityNumber')"
                            >
                                <template #label>Số CMND</template>
                            </VTextField>

                            <VDateField
                                size="small"
                                id="date-of-issue"
                                :value="dataHandler.getDate(details.identityIssuedDate)"
                                @update:input="handlePickDate($event, 'DateOfIssue')"
                                :errMsgs="{
                                    isInvalid: `Ngày cấp không hợp lệ`
                                }"
                            >
                                <template #label>Ngày cấp</template>
                            </VDateField>
                        </div>

                        <VTextField
                            size="small"
                            width="full"
                            id="identity-department"
                            :value="details.identityIssuedPlace"
                            @input="handleInput($event, 'PlaceOfIssue')"
                        >
                            <template #label>Nơi cấp</template>
                        </VTextField>
                    </div>
                </div>

                <VTextField
                    size="small"
                    width="full"
                    id="address"
                    style="margin-top: 24px"
                    :value="details.address"
                    @input="handleInput($event, 'Address')"
                >
                    <template #label>Địa chỉ</template>
                </VTextField>

                <div class="group-2">
                    <div class="first-line">
                        <VTextField
                            size="small"
                            width="large"
                            id="mobile-phone"
                            :value="details.mobilePhone"
                            @input="handleInput($event, 'MobilePhone')"
                        >
                            <template #label>ĐT di động</template>
                        </VTextField>

                        <VTextField
                            size="small"
                            width="large"
                            id="landline-phone"
                            :value="details.landlinePhone"
                            @input="handleInput($event, 'LandlinePhone')"
                        >
                            <template #label>ĐT cố định</template>
                        </VTextField>

                        <VTextField
                            size="small"
                            width="large"
                            id="email"
                            :value="details.email"
                            @input="handleInput($event, 'Email')"
                        >
                            <template #label>Email</template>
                        </VTextField>
                    </div>

                    <div class="second-line">
                        <VTextField
                            size="small"
                            width="large"
                            id="bank-account"
                            :value="details.bankAccount"
                            @input="handleInput($event, 'BankAccount')"
                        >
                            <template #label>Tài khoản ngân hàng</template>
                        </VTextField>

                        <VTextField
                            size="small"
                            width="large"
                            id="bank-name"
                            :value="details.bankName"
                            @input="handleInput($event, 'BankName')"
                        >
                            <template #label>Tên ngân hàng</template>
                        </VTextField>

                        <VTextField
                            size="small"
                            width="large"
                            id="bank-branch"
                            :value="details.bankBranch"
                            @input="handleInput($event, 'BankBranch')"
                        >
                            <template #label>Chi nhánh</template>
                        </VTextField>
                    </div>
                </div>
            </div>

            <div class="form-footer">
                <div class="left-group">
                    <VButton type="outline" class="cancel-btn" @click="Employee.details.hide">
                        Hủy
                    </VButton>
                </div>

                <div class="right-group">
                    <VButton type="outline" class="add-btn" @click="handleStoreForm">
                        Cất
                    </VButton>
                    <VButton
                        type="primary"
                        class="add-more-btn"
                        @click="handleStoreAndCreateForm"
                    >
                        Cất và Thêm
                    </VButton>
                </div>
            </div>

            <!-- validate dialog -->
            <VDialog
                v-if="
                    isShowValidateDialog &&
                    Dialog.current === DialogName.Validate &&
                    inputErrorList.size
                "
            >
                <template #title> {{ VNDialogResources.Validate.title }} </template>
                <template #text> {{ Array.from(inputErrorList)[0] }} </template>
                <template #primaryAction>
                    <VButton type="primary" @click="hideValidateDialog">
                        {{ VNDialogResources.Validate.primaryAction }}
                    </VButton>
                </template>
            </VDialog>

            <!-- duplicated employee code -->
            <VDialog
                v-if="
                    isDuplicatedEmployeeCode &&
                    Dialog.current === DialogName.DuplicatedEmployeeCode
                "
            >
                <template #title>
                    {{ VNDialogResources.DuplicatedEmployeeCode.title }}</template
                >
                <template #text>
                    {{ VNDialogResources.DuplicatedEmployeeCode.text(details.EmployeeCode) }}
                </template>
                <template #primaryAction>
                    <VButton type="primary" @click="hideDuplicatedEmployeeCodeDialog">
                        {{ VNDialogResources.DuplicatedEmployeeCode.primaryAction }}
                    </VButton>
                </template>
            </VDialog>
        </div>
    </div>
</template>

<style lang="scss" scoped>
@import '@/styles/mixins.scss';
.form {
    position: fixed;
    top: 0;
    left: 0;
    z-index: 50;

    display: flex;
    justify-content: center;
    align-items: center;

    width: 100%;
    height: 100%;
    background-color: rgb(var(--c-black), 0.4);
}

.form-container {
    display: flex;
    flex-direction: column;
    border-radius: 4px;
    background-color: rgb(var(--c-white));
}

/* Styles for form-header */
.form-header {
    @include font(14);
    margin-top: 24px;
    padding: 0 24px;

    display: flex;
    justify-content: space-between;
    align-items: center;
    .left-group {
        display: flex;
        gap: 24px;
        .heading {
            margin-right: 6px;
        }
    }
    .right-group {
        display: flex;
        gap: 8px;
        .help-icon,
        .close-icon {
            @include size(24px);
            cursor: pointer;
        }
    }
}

/* Styles for form-content */
.form-content {
    display: flex;
    flex-direction: column;
    gap: 16px;
    padding: 24px;
    .group-1 {
        display: flex;
        justify-content: space-between;
        gap: 20px;
        .left-group,
        .right-group {
            display: flex;
            flex-direction: column;
            gap: 16px;
        }
    }
    .group-2 {
        display: flex;
        flex-direction: column;
        gap: 16px;
        .first-line,
        .second-line {
            display: flex;
            gap: 8px;
        }
    }
}

/* Styles for form-footer */
.form-footer {
    display: flex;
    justify-content: space-between;
    padding: 12px 24px;
    border-radius: 0 0 4px 4px;

    background-color: rgb(var(--c-gray-100));
    border-top: 1px solid rgb(var(--c-gray-300));
    .right-group {
        display: flex;
        gap: 8px;
    }
}
</style>
