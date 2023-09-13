<script setup>
import { computed, onMounted, onUnmounted, ref } from 'vue';

import { IconExpandMore, IconSearch } from '@/assets/icons';
import {
    VButton,
    VCheckbox,
    VDialog,
    VDropdownlist,
    VIcon,
    VLoading,
    VTextField
} from '@/components';
import EmployeeForm from './EmployeeForm.vue';

import { useDialogStore } from '@/stores/dialog';
import { useEmployeeStore } from '@/stores/employee';
import { useTableStore } from '@/stores/table';
import { useToastMessageStore } from '@/stores/toastMessage';
import * as EmployeeDataService from '@/services/employeeDataService';

import * as dataHandler from '@/utils/dataHandler';
import { DialogName, ToastMessageType } from '@/utils/enums';
import { resources } from '@/utils/resources';
import { table } from '@/utils/table';

// global states
const Employee = useEmployeeStore();
const Table = useTableStore();
const Dialog = useDialogStore();
const ToastMessage = useToastMessageStore();

// resources theo ngôn ngữ
const VNDialogResources = resources.VN.Dialog;
const VNToastMessageResources = resources.VN.ToastMessage;

const isLoading = ref(false);
const errorResponse = ref(null);

/**
 * === XỬ LÝ LẤY DANH SÁCH NHÂN VIÊN TỪ API ===
 */

/**
 * load dữ liệu từ API:
 * 1. Trong khi loading, hiển thị spinner và lấy dữ liệu theo số bản ghi mỗi trang và hàng bắt đầu truy xuất.
 * 2. Nếu gặp exception, hiển thị Dialog thông báo lỗi.
 * create by: TTANH - 01/09/2023
 * modified by: TTANH - 05/09/2023
 */
const handleLoadData = async () => {
    try {
        isLoading.value = true;
        await Employee.getPaginationAsync(Table.recordPerPage, Table.offsetValue);
        isLoading.value = false;
    } catch (err) {
        isLoading.value = false;
        console.log(err);
        errorResponse.value = err;
        Dialog.show(DialogName.GetErrorResponse);
    }
};

const handleResetTable = async () => {
    Table.currentPage = Table.INIT_CURRENT_PAGE;
    Table.offsetValue = Table.INIT_OFFSET_VALUE;
};

// load dữ liệu khi được mount
onMounted(() => {
    handleLoadData();
    Table.getTotalRecord();
});

// lưu trữ dữ liệu nhân viên
const employeeData = computed(() => Employee.list);

/**
 * lấy giá trị recordPerPage từ selected option và load lại dữ liệu
 * @param {string} option
 * create by: TTANH - 04/09/2023
 * modified by: TTANH - 05/09/2023
 */
const handleChangeRecordPerPage = (option) => {
    const newValue = option.split(' ')[0];
    Table.recordPerPage = newValue;
    handleResetTable();
    handleLoadData();
};

/**
 * đi đến trang dữ liệu trước
 * create by: TTANH - 04/09/2023
 */
const handleToPreviousPage = () => {
    if (Table.currentPage > 1) {
        Table.toPreviousPage();
        handleLoadData();
    }
};

/**
 * đi đến trang dữ liệu kế tiếp
 * create by: TTANH - 04/09/2023
 */
const handleToNextPage = () => {
    if (Table.currentPage < Table.totalPages) {
        Table.toNextPage();
        handleLoadData();
    }
};

/**
 * đi đến trang dữ liệu bất kỳ
 * @param {int} page
 * create by: TTANH - 06/09/2023
 */
const handleToPage = (page) => {
    Table.toPage(page);
    handleLoadData();
};

/**
 * định dạng lại dữ liệu nhân viên lấy từ API.
 * @param {object} data dữ liệu nhân viên
 * @param {string} property trường dữ liệu cần định dạng lại
 * create by: TTANH - 20/08/2023
 * modified by: TTANH - 03/09/2023
 */
const formatDataCell = (data, property) => {
    switch (property) {
        case 'gender':
            return dataHandler.getGender(data['gender']);
        case 'dateOfBirth':
            return dataHandler.getDate(data['dateOfBirth']);
        default:
            return data[property];
    }
};

/**
 * === TÌM KIẾM NHÂN VIÊN THEO MÃ NHÂN VIÊN, TÊN NHÂN VIÊN ===
 */

// lưu trữ từ khóa tìm kiếm
const searchText = ref('');
const handleSearchInput = (event) => {
    searchText.value = event.target.value;
};

/**
 * thực hiện tìm kiếm nhân viên theo mã nhân viên hoặc tên nhân viên
 * create by: TTANH - 18/08/2023
 * modified by: TTANH - 04/09/2023
 */
const handleSearchEmployee = async () => {
    try {
        console.log('searchText:', searchText.value);
        isLoading.value = true;
        await Employee.getFilterAndPaginationAsync(
            searchText.value,
            Table.recordPerPage,
            Table.offsetValue
        );
        isLoading.value = false;
    } catch (err) {
        console.log(err);
    }
};

/**
 * === XỬ LÝ EXPAND DROPDOWN OPTIONS ===
 */

const isOpenDropdownOptions = ref(false);

/**
 * ngăn chặn nổi bọt sự kiện ra window khi click vào ExpandIcon và mở dropdown options
 * @param {event} event sự kiện click
 * @param {object} employee nhân viên có dropdown được mở
 * create by: TTANH - 12/08/2023
 * modified by: TTANH - 15/08/2023
 */
const handleClickExpandIcon = (event, employee) => {
    event.stopPropagation();
    isOpenDropdownOptions.value = true;
    Employee.setCurrent(employee);
};

// đóng dropdown options
const closeDropdownOptions = () => (isOpenDropdownOptions.value = false);

// khi click ra ngoài document, đóng dropdown options.
onMounted(() => document.addEventListener('click', closeDropdownOptions));

// khi unmount, loại bỏ sự kiện click khỏi đối tượng document.
onUnmounted(() => document.removeEventListener('click', closeDropdownOptions));

/**
 * === XÓA 1 NHÂN VIÊN ===
 */

const isDeleting = ref(false);

// hiển thị Dialog Xóa nhân viên
const handleShowDeleteEmployeeDialog = () => {
    isDeleting.value = true;
    Dialog.show(DialogName.DeleteEmployee);
};

/**
 * thực hiện xóa nhân viên theo ID
 * 1. nếu thành công, reset employee current và employee checklist và hiện message thông báo thành công
 * 2. nếu thất bại, hiện message thông báo lỗi
 * @param {object} employee nhân viên bị xóa
 * create by: TTANH - 15/08/2023
 * modified by: TTANH - 05/09/2023
 */
const handleDeleteEmployee = (employee) => {
    try {
        Employee.remove(employee.employeeCode);

        Employee.setCurrent({});
        employeeChecklist.value = [];

        ToastMessage.show(
            ToastMessageType.Success,
            VNToastMessageResources.DeleteEmployee.success(employee.employeeCode)
        );
    } catch (err) {
        ToastMessage.show(
            ToastMessageType.Error,
            VNToastMessageResources.DeleteEmployee.error(employee.employeeCode)
        );
        console.log(err);
    }
    isDeleting.value = false;
};

// hủy xóa nhân viên và đóng Dialog
const cancelDeleteEmployee = () => {
    isDeleting.value = false;
    Employee.setCurrent({});
    Dialog.hide();
};

/**
 * === XÓA NHIỀU NHÂN VIÊN ===
 */

// lưu trữ danh sách nhân viên đã chọn
const employeeChecklist = ref([]);

/**
 * xử lý chọn 1 nhân viên
 * 1. khi checkbox được check, thêm nhân viên đó vào checklist
 * 2. nếu không, tìm index của nhân viên đó và loại bỏ khỏi checklist
 * @param {event} event
 * @param {object} employee
 * create by: TTANH - 12/08/2023
 * modified by: TTANH - 15/08/2023
 */
const checkEmployee = (event, employee) => {
    if (event.target.checked) employeeChecklist.value.push(employee);
    else {
        const removeIndex = employeeChecklist.value.indexOf(employee);
        if (removeIndex !== -1) employeeChecklist.value.splice(removeIndex, 1);
    }
};

/**
 * xử lý check tất cả nhân viên
 * 1. khi checkbox được check, làm rỗng checklist và thêm tất cả nhân viên
 * 2. nếu không, làm rỗng checklist
 * @param {event} event
 * create by: TTANH - 12/08/2023
 * modified by: TTANH - 15/08/2023
 */
const toggleCheckAllEmployee = (event) => {
    if (event.target.checked) {
        employeeChecklist.value = [];
        employeeChecklist.value.push(...employeeData.value);
    } else employeeChecklist.value = [];
};

// nếu có từ 2 nhân viên trở lên được chọn, hiện Dialog Xóa nhiều nhân viên
const isShowBulkDeleteAction = computed(() => employeeChecklist.value.length > 1);
const handleShowBulkDeleteDialog = () => {
    isDeleting.value = true;
    Dialog.show(DialogName.DeleteMultipleEmployees);
};

/**
 * xử lý xóa nhiều nhân viên
 * 1. lấy tất cả ID của nhân viên trong checklist
 * 2. xóa tất cả nhân viên có ID trong checklist
 * 3. nếu thành công, reset employee checklist và hiện message thông báo thành công
 * 4. nếu thất bại, hiện message thông báo lỗi
 * create by: TTANH - 12/08/2023
 * modified by: TTANH - 15/08/2023
 */
const handleBulkDeleteAction = async () => {
    try {
        const employeeIDChecklist = employeeChecklist.value.map((el) => el.employeeID);
        console.log('employeeIDChecklist:', employeeIDChecklist);

        Employee.removeMultiple(employeeIDChecklist);

        employeeChecklist.value = [];

        ToastMessage.show(
            ToastMessageType.Success,
            VNToastMessageResources.DeleteMultipleEmployees.success
        );
    } catch (err) {
        ToastMessage.show(
            ToastMessageType.Error,
            VNToastMessageResources.DeleteMultipleEmployees.error
        );
        console.log(err);
    }
    Dialog.hide();
    isDeleting.value = false;
};

// hủy xóa nhiều nhân viên và đóng Dialog
const cancelBulkDeleteAction = () => {
    employeeChecklist.value = [];
    isDeleting.value = false;
    Dialog.hide();
};

/**
 * === SỬA DỮ LIỆU NHÂN VIÊN ===
 */

const isUpdating = ref(false);

/**
 * hiển thị thông tin chi tiết nhân viên
 * @param {object} employee nhân viên đang được chọn
 * create by: TTANH - 18/08/2023
 */
const handleShowEmployeeDetails = async (employee) => {
    try {
        isUpdating.value = true;
        const selectedEmployee = await Employee.getByIdAsync(employee.employeeID);
        Employee.details.show(selectedEmployee);
    } catch (err) {
        console.log(err);
        errorResponse.value = err;
        Dialog.show(DialogName.GetErrorResponse);
    }
};

/**
 * thực hiện xuất dữ liệu
 * create by: TTANH - 07/09/2023
 */
const handleExportData = async () => {
    const response = await EmployeeDataService.getAll();
    if (response.data.result)
        dataHandler.exportDataFromJSON(response.data.result, 'employee-data');
};
</script>

<template>
    <div id="data-table">
        <div class="data-table-header">
            <div class="bulk-actions" v-if="isShowBulkDeleteAction">
                <span class="checked-count">
                    Đã chọn <b>{{ employeeChecklist.length }}</b>
                </span>
                <span class="uncheck-all-button" @click="employeeChecklist = []">Bỏ chọn</span>
                <VButton
                    type="outline"
                    size="medium"
                    colorScheme="red"
                    class="bulk-delete-button"
                    @click="handleShowBulkDeleteDialog"
                >
                    <template #icon>
                        <div class="icon-delete">
                            <VIcon class="delete-img" />
                        </div>
                    </template>
                    Xóa hàng loạt
                </VButton>

                <!-- bulk delete dialog -->
                <VDialog
                    v-if="isDeleting && Dialog.current === DialogName.DeleteMultipleEmployees"
                >
                    <template #title>
                        {{ VNDialogResources.DeleteMultipleEmployees.title }}
                    </template>
                    <template #text>
                        {{ VNDialogResources.DeleteMultipleEmployees.text }}
                    </template>
                    <template #primaryAction>
                        <VButton
                            type="primary"
                            colorScheme="red"
                            @click="handleBulkDeleteAction"
                        >
                            {{ VNDialogResources.DeleteMultipleEmployees.primaryAction }}
                        </VButton>
                    </template>
                    <template #secondaryAction>
                        <VButton type="outline" @click="cancelBulkDeleteAction">
                            {{ VNDialogResources.DeleteMultipleEmployees.secondaryAction }}
                        </VButton>
                    </template>
                </VDialog>
            </div>
            <div class="tools">
                <VTextField
                    size="medium"
                    width="extra-large"
                    placeholder="Tìm theo mã, tên nhân viên"
                    id="search-employee"
                    class="search-employee-textfield"
                    :input="searchText"
                    @input="handleSearchInput($event)"
                    :tooltip="false"
                >
                    <template #icon>
                        <IconSearch class="search-icon" @click="handleSearchEmployee" />
                    </template>
                </VTextField>

                <VButton type="icon" class="export-button" @click="handleExportData">
                    <div class="icon-export">
                        <VIcon class="export-img" />
                    </div>
                </VButton>

                <VButton type="icon" class="reload-button" @click="handleLoadData">
                    <div class="icon-reload">
                        <VIcon class="reload-img" />
                    </div>
                </VButton>
            </div>
        </div>

        <!-- dialog show error response -->
        <VDialog v-if="Dialog.current === DialogName.GetErrorResponse">
            <template #title>
                {{ VNDialogResources.GetErrorResponse.title }}
            </template>
            <template #text>
                {{
                    errorResponse.response
                        ? errorResponse.response.data.userMsg
                        : VNDialogResources.GetErrorResponse.text()
                }}
            </template>
            <template #primaryAction>
                <VButton type="primary" @click="Dialog.hide">
                    {{ VNDialogResources.GetErrorResponse.primaryAction }}
                </VButton>
            </template>
        </VDialog>

        <VLoading v-if="isLoading" />
        <div v-else class="table-group">
            <table>
                <thead>
                    <tr>
                        <th
                            v-for="column in table.columnInfos"
                            :key="column.name"
                            :title="column.title"
                            :class="[
                                `th-${column.name}`,
                                { fixed: table.fixedColumns.includes(column.name) }
                            ]"
                        >
                            <div v-if="column.header" :class="`cell-${column.width}`">
                                {{ column.header }}
                            </div>
                            <VCheckbox
                                v-else
                                id="multiple-employees-checkbox"
                                :checked="
                                    employeeData.length !== 0 &&
                                    employeeChecklist.length === employeeData.length
                                "
                                @click="toggleCheckAllEmployee($event)"
                            />
                        </th>
                        <th class="th-options"><div class="cell-small">chức năng</div></th>
                    </tr>
                </thead>

                <tbody>
                    <tr
                        v-for="employee in employeeData"
                        :key="employee.EmployeeCode"
                        :class="[
                            { 'selected-row': employeeChecklist.includes(employee) },
                            {
                                'higher-elevation':
                                    isOpenDropdownOptions && Employee.current === employee
                            }
                        ]"
                        @dblclick="handleShowEmployeeDetails(employee)"
                    >
                        <td
                            v-for="column in table.columnInfos"
                            :key="column.name"
                            :class="[
                                `td-${column.name}`,
                                { fixed: table.fixedColumns.includes(column.name) }
                            ]"
                        >
                            <div
                                v-if="column.property"
                                :class="['cell-data', `cell-${column.width}`]"
                            >
                                {{ formatDataCell(employee, column.property) }}
                            </div>
                            <VCheckbox
                                v-else
                                id="employee-checkbox"
                                :checked="employeeChecklist.includes(employee)"
                                @change="checkEmployee($event, employee)"
                            />
                        </td>

                        <td class="td-options">
                            <span
                                class="update-button"
                                @click="Employee.details.show(employee)"
                            >
                                Sửa
                            </span>
                            <div class="dropdown">
                                <IconExpandMore
                                    class="expand-more-icon"
                                    @click="handleClickExpandIcon($event, employee)"
                                />
                                <div
                                    v-if="
                                        isOpenDropdownOptions && Employee.current === employee
                                    "
                                    class="dropdown-menu"
                                >
                                    <div class="dropdown-menu-item">Nhân bản</div>
                                    <div
                                        class="dropdown-menu-item"
                                        @click="handleShowDeleteEmployeeDialog"
                                    >
                                        Xóa
                                    </div>
                                    <div class="dropdown-menu-item">Ngừng sử dụng</div>
                                </div>
                            </div>
                        </td>
                        <!-- delete employee dialog -->
                        <Teleport to="#main">
                            <VDialog v-if="isDeleting && Employee.current === employee">
                                <template #title>
                                    {{ VNDialogResources.DeleteEmployee.title }}
                                </template>
                                <template #text>
                                    {{
                                        VNDialogResources.DeleteEmployee.text(
                                            employee.employeeCode
                                        )
                                    }}
                                </template>
                                <template #primaryAction>
                                    <VButton
                                        type="primary"
                                        color-scheme="red"
                                        @click="handleDeleteEmployee(employee)"
                                    >
                                        {{ VNDialogResources.DeleteEmployee.primaryAction }}
                                    </VButton>
                                </template>
                                <template #secondaryAction>
                                    <VButton type="outline" @click="cancelDeleteEmployee">
                                        {{ VNDialogResources.DeleteEmployee.secondaryAction }}
                                    </VButton>
                                </template>
                            </VDialog>
                        </Teleport>
                    </tr>
                </tbody>
                <EmployeeForm v-if="Employee.isShowDetails" />
            </table>

            <div class="data-table-footer">
                <span>
                    Tổng số: <b>{{ employeeData.length }}</b> bản ghi
                </span>
                <div class="paging-group">
                    <VDropdownlist
                        size="small"
                        width="large"
                        id="record-per-page"
                        :options="table.recordPerPageOptions"
                        :value="`${Table.recordPerPage} bản ghi trên 1 trang`"
                        @select-option="handleChangeRecordPerPage"
                    />
                    <div class="page-switcher">
                        <div class="before-page" @click="handleToPreviousPage">Trước</div>
                        <div class="page-list">
                            <div
                                v-for="(page, index) in table.generatePageNumbers(
                                    Table.currentPage,
                                    Table.totalPages
                                )"
                                :key="index"
                                :class="[
                                    'page',
                                    { 'current-page': page === Table.currentPage }
                                ]"
                                @click="handleToPage(page)"
                            >
                                {{ page }}
                            </div>
                        </div>
                        <div class="after-page" @click="handleToNextPage">Sau</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style lang="scss" scoped>
@import '@/styles/mixins.scss';

#data-table {
    max-height: var(--data-table-height);
    height: 100%;
    @include font(13);
}

.data-table-header {
    display: flex;
    justify-content: space-between;
    align-items: center;

    min-height: var(--data-table-header-height);
    padding: 16px 0;
    @include font(14);
    &:not(:has(.bulk-delete-button)) {
        justify-content: flex-end;
    }
}

.data-table-header .bulk-actions {
    display: flex;
    align-items: center;
    .checked-count {
        margin-right: 16px;
    }
    .uncheck-all-button {
        margin-right: 24px;
        color: rgb(var(--c-red-500));
        cursor: pointer;
    }
    .bulk-delete-button {
        .icon-delete {
            @include size(18px);
            transform: scale(calc(18 / 15));
            margin-right: 4px;
            margin-left: -4px;
            .delete-img {
                @include size(15px);
                background-position: -464px -313px;
                filter: brightness(0) invert(1);
            }
        }
    }
}

.data-table-header .tools {
    display: flex;
    align-items: center;
    justify-content: flex-end;
    gap: 12px;
    input.search-employee-textfield {
        @include font(13);
        height: 60px;
        font-style: italic !important;

        &::placeholder {
            @include font(13);
            font-style: italic !important;
        }
        & + i > .search-icon {
            @include size(20px);
            cursor: pointer;
        }
    }

    .export-button {
        .icon-export {
            @include size(20px);
            transform: scale(calc(20 / 24));
            .export-img {
                width: 23px;
                height: 20px;
                background-position: -705px -202px;
                filter: sepia(1) hue-rotate(100deg) brightness(0.5) saturate(10) grayscale(0.4);
            }
        }
        &:active {
            .export-img {
                filter: sepia(1) hue-rotate(101deg) brightness(0.6) saturate(10) grayscale(0.5);
            }
        }
    }

    .reload-button {
        .icon-reload {
            @include size(20px);
            transform: scale(calc(20 / 24));
            .reload-img {
                width: 22px;
                height: 23px;
                background-position: -424px -201px;
                filter: sepia(1) hue-rotate(100deg) brightness(0.5) saturate(10) grayscale(0.4);
            }
        }
        &:active {
            .reload-img {
                filter: sepia(1) hue-rotate(101deg) brightness(0.6) saturate(10) grayscale(0.5);
            }
        }
    }
}

.table-group {
    max-height: var(--data-table-group-table-height);
    height: 100%;
    border: 1px solid rgb(var(--c-gray-300));
    background-color: rgb(var(--c-white));
    overflow: auto;
    &::-webkit-scrollbar {
        @include size(8px);
    }
    &::-webkit-scrollbar-thumb {
        border-radius: 4px;
        background-color: rgb(var(--c-gray-400));
    }
    &::-webkit-scrollbar-track {
        background-color: rgb(var(--c-gray-100));
    }
}

table {
    @include font('small');
    border-spacing: 0px;
    thead {
        position: sticky;
        top: 0;
        z-index: 10;
        background-color: rgb(var(--c-gray-200));
    }
    tbody {
        z-index: 0;
    }
}

/* Table Header Row */
/* Width of cell */
.cell-extra-large {
    width: 220px;
}
.cell-large {
    width: 180px;
}
.cell-medium {
    width: 140px;
}
.cell-small {
    width: 100px;
}

.cell-extra-small {
    width: 20px;
}

tr {
    &.selected-row > td {
        background-color: rgb(var(--c-light-green-100));
    }
    &.higher-elevation {
        z-index: 20;
    }
}

th {
    padding: 0 10px;
    text-transform: uppercase;

    height: var(--table-row-height);
    background-color: rgb(var(--c-gray-200));

    border: 1px solid rgb(var(--c-gray-300));
    border-width: 0 1px 1px 0;
}

.th-bank-branch {
    border-width: 0 0 1px 0;
}
.th-options {
    position: sticky;
    right: 0;
    border-width: 0 0 1px 1px;
}

/* Table Data Row */
td {
    padding: 0 10px;

    height: var(--table-row-height);
    background-color: rgb(var(--c-white));

    border: 1px solid rgb(var(--c-gray-300));
    border-width: 0 1px 1px 0;
    &.td-date-of-birth {
        text-align: center;
    }
    &.td-bank-branch {
        border-width: 0 0 1px 0;
    }
    &.td-options {
        position: sticky;
        right: 0;
        z-index: 10;

        padding-left: 12px;
        display: flex;
        justify-content: center;
        align-items: center;
        gap: 4px;

        font-weight: 500;
        border-width: 0 0 1px 1px;
        .update-button {
            color: rgb(var(--c-blue-600));
            cursor: pointer;
        }
    }

    .cell-data {
        text-overflow: ellipsis;
        overflow: hidden;
        white-space: nowrap;
    }
}

.fixed {
    position: sticky;
    z-index: 50;
}

.td-employee-check,
.th-employee-check {
    left: 0;
}

.td-employee-code,
.th-employee-code {
    left: 39px;
}

.td-full-name,
.th-full-name {
    left: 199.5px;
}

.dropdown {
    .expand-more-icon {
        @include size(16px);
        cursor: pointer;
    }
    .expand-more-icon-border {
        border: 1px solid rgb(var(--c-blue-600));
    }
    .dropdown-menu {
        position: absolute;
        right: -8px;
        width: max-content;

        background-color: rgb(var(--c-white));
        font-weight: 400;
        border: 1px solid rgb(var(--c-gray-300));
        .dropdown-menu-item {
            padding: 6px 10px;
            cursor: pointer;
            &:hover {
                color: rgb(var(--c-primary));
                background-color: rgb(var(--c-gray-200));
            }
        }
    }
}

/* Styles for data-table-footer */
.data-table-footer {
    position: sticky;
    bottom: 0;
    left: 0;

    display: flex;
    justify-content: space-between;
    align-items: center;

    height: 56px;
    padding: 12px;
    background-color: rgb(var(--c-white));
    border-top: 1px solid rgb(var(--c-gray-300));
}
.paging-group {
    display: flex;
    align-items: center;
    gap: 16px;
    .page-switcher {
        display: flex;
        .before-page,
        .after-page {
            color: rgb(var(--c-gray-600));
            cursor: pointer;
        }
        .page-list {
            display: flex;
            align-items: center;
            margin: 0 12px;
            .page {
                padding: 0 6px;
                cursor: pointer;
            }
            .current-page {
                font-weight: 700;
                border: 1px solid rgb(var(--c-gray-300));
            }
        }
    }
}
</style>
