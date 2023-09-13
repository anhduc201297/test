<template>
    <div>
        <div class="container-header">
            <h1 class="container-title">Nhân viên</h1>
            <m-button @click="openEmployeeDetails(null)"
                >Thêm mới nhân viên</m-button
            >
        </div>

        <div class="container-content">
            <div class="container-actions">
                <div class="container-left">
                    <m-button disabled type="link" v-show="isShowActionMultiBtn"
                        >Đã chọn:
                        <span class="text-bold">{{
                            employeeCheckedAmount
                        }}</span></m-button
                    >
                    <m-button
                        @click="setCheckAllPages(false)"
                        v-show="isShowActionMultiBtn"
                        type="link"
                    >
                        <span class="text-red">Bỏ chọn</span>
                    </m-button>
                    <m-button
                        v-show="
                            isShowActionMultiBtn &&
                            employeeCheckedAmount < totalRecords
                        "
                        type="link"
                        @click="setCheckAllPages(true)"
                    >
                        <span class="color-blue-500 text-bold"
                            >Chọn tất cả các trang</span
                        >
                    </m-button>
                    <m-button-icon
                        v-show="isShowActionMultiBtn"
                        @click="openDialogRemoveMulti"
                        class="icon-bin icon-20"
                        type="sub"
                        ><span class="text-red mt-4"
                            >Xóa mục đã chọn</span
                        ></m-button-icon
                    >
                </div>
                <!-- search -->
                <div class="container-search">
                    <m-input-icon
                        :model-value="searchValue"
                        @update:modelValue="updateSearchValue"
                        placeholder="Tìm theo mã, tên nhân viên"
                        :icon="'icon-search icon-20'"
                        title="Tìm kiếm (ENTER)"
                    />

                    <m-btn-no-text
                        @click="refreshData"
                        title="Làm mới dữ liệu"
                        class="icon-refresh icon-24 lg"
                    />
                    <m-btn-no-text
                        @click="clickExportToExcel"
                        title="Xuất ra Excel"
                        class="icon-excel icon-24 lg"
                    />
                </div>
            </div>

            <!-- table -->
            <div
                class="container-table-wrapper"
                :style="{
                    overflow: isTableOverflow
                }"
            >
                <m-table
                    ref="employeeTable"
                    :headers="tableHeaderCols"
                    :data="tableData"
                    :unique-field="'employeeId'"
                    :page-size-options="pageSizeOptions"
                    :current-page="currentPage"
                    :total-records="totalRecords"
                    :records-per-page="recordsPerPage"
                    @setRecordsPerPage="setRecordsPerPage"
                    @changePage="setPage"
                    @changeAmountItemChecked="setAmountCheckedEmployees"
                    @dblClickRow="openEmployeeDetails"
                    @clickAction="openEmployeeDetails"
                />

                <m-mask :isShow="isShowMask"></m-mask>
                <m-loading v-show="isLoading" />
            </div>
        </div>
        <employee-details
            v-if="isOpenEmployeeDetails"
            @onCloseFormDetails="closeEmployeeDetails"
            :dataInput="tableData[employeeEdited]"
        />
        <m-dialog
            v-if="isShowDialogRemoveMulti"
            :content="this.$Resource.ConfirmRemoveMulti"
            :title="this.$Resource.ConfirmRemoveMultiText"
            @clickOk="removeEmployees"
            @clickCancel="closeDialogRemoveMulti"
            type="Warning"
        />

        <!-- Dialog remove confirm employee  -->
        <m-dialog
            v-if="itemIsRemove != null"
            :content="
                this.$Resource.confirmRemoveEmployee(itemIsRemove.employeeCode)
            "
            :title="this.$Resource.ConfirmRemoveOneText"
            @clickOk="removeEmployee"
            @clickCancel="closeDialogRemoveOne"
            type="Warning"
        />
    </div>
</template>

<script>
import EmployeeDetails from '../employeeDetails/EmployeeDetails.vue';
import MInputIcon from '@/components/base/input/MInputIcon.vue';
import MTable from '@/components/base/table/MTable.vue';
import MButtonIcon from '@/components/base/button/MButtonIcon.vue';
import MMask from '@/components/base/mask/MMask.vue';
import {
    getEmployeesApi,
    removeAllEmployeesApi,
    removeEmployeesApi,
    exportToExcel,
    removeEmployeeByIdApi
} from '@/js/requests/employees.request';

export default {
    name: 'EmployeeView',
    inject: ['onOpenToast', 'onOpenDialogError'],
    components: {
        MInputIcon,
        MTable,
        EmployeeDetails,
        MMask,
        MButtonIcon
    },
    data() {
        return {
            searchDebounce: null,
            isLoading: false,
            isShowMask: false,
            tableData: [],
            totalRecords: 0,
            recordsPerPage: null,
            currentPage: 1,
            searchValue: '',

            employeeCheckedAmount: 0,
            isOpenEmployeeDetails: false,
            isShowDialogRemoveMulti: false,

            messageRemoveOne: '',
            employeeIdRemoved: null,
            employeeEdited: null,
            itemIsRemove: null
        };
    },
    methods: {
        /**
         * Mở Dialog xác nhận xóa 1 employee có id
         * @Author NTVu - MF1742 - 22/08/2023
         * @param {Number} data
         */
        openDialogRemoveOne(data) {
            this.itemIsRemove = data;
        },
        /**
         * Đóng Dialog xác nhận xóa 1 employee
         * @Author NTVu - MF1742 - 22/08/2023
         */
        closeDialogRemoveOne() {
            this.itemIsRemove = null;
        },
        /**
         * Lấy data từ server
         * @author NTVu - 29/08/2023
         */
        async fetchEmployeesData() {
            try {
                this.isLoading = true;
                const { data } = await getEmployeesApi(this.$route.query);

                this.tableData = data.data;
                this.totalRecords = data.totalRecords;
            } catch (error) {
                this.onOpenDialogError({
                    content: error.message,
                    title: this.$Resource.Error
                });
            } finally {
                this.isLoading = false;
            }
        },
        /**
         * Mở Form nhân viên qua index
         * @Author NTVu - MF1742 - 20/08/2023
         * @param {Number?} index
         */
        openEmployeeDetails(index) {
            if (index != null) {
                this.employeeEdited = index;
            }
            this.isOpenEmployeeDetails = true;
        },
        /**
         * Đóng Form nhân viên
         * @Author NTVu - MF1742 - 20/08/2023
         */
        closeEmployeeDetails() {
            this.isOpenEmployeeDetails = false;
            this.employeeEdited = null;
            // this.fetchEmployeesData();
        },
        /**
         * Mở Dialog xác nhận xóa nhiều
         * @Author NTVu - MF1742 - 22/08/2023
         */
        openDialogRemoveMulti() {
            this.isShowDialogRemoveMulti = true;
        },
        /**
         * Đóng Dialog xác nhận xóa nhiều
         * @Author NTVu - MF1742 - 22/08/2023
         */
        closeDialogRemoveMulti() {
            this.isShowDialogRemoveMulti = false;
        },

        /**
         * Thay đổi trang
         * @author NTVu - MF1742 - 20/08/2023
         * @param {Number} newPage
         */
        setPage(newPage) {
            this.currentPage = newPage;
            this.$router.push({
                path: '',
                query: {
                    ...this.$route.query,
                    page: this.currentPage
                }
            });
            this.setCheckAllPages(false);
        },

        /**
         * Làm mới lại dữ liệu
         * @Author NTVu - MF1742 - 20/08/2023
         */
        refreshData() {
            this.fetchEmployeesData();
        },
        /**
         * Xóa các nhân viên đã được chọn
         * @Author NTVu - MF1742 - 22/08/2023
         */
        async removeEmployees() {
            this.closeDialogRemoveMulti();

            try {
                this.isLoading = true;
                if (this.employeeCheckedAmount < this.totalRecords) {
                    const employeeIds =
                        this.$refs.employeeTable.getAllItemChecked();

                    const { status } = await removeEmployeesApi(employeeIds);

                    if (status === 204)
                        this.onOpenToast({
                            title: this.$Resource.RemoveEmployeesSuccess,
                            type: this.$MEnum.ToastType.Success
                        });
                    this.setCheckAllPages(false);
                } else {
                    const res = await removeAllEmployeesApi();
                    if (res.status === 204)
                        this.onOpenToast({
                            title: this.$Resource.RemoveAllEmployeesSuccess,
                            type: this.$MEnum.ToastType.Success
                        });
                }

                this.fetchEmployeesData();
            } catch (error) {
                this.onOpenDialogError({
                    content: error.message,
                    title: this.$Resource.Error
                });
            } finally {
                this.isLoading = false;
            }
        },
        /**
         * Xóa 1 nhân viên đã được chọn
         * @Author NTVu - MF1742 - 22/08/2023
         */
        async removeEmployee() {
            try {
                this.isLoading = true;

                const { status } = await removeEmployeeByIdApi(
                    this.itemIsRemove.employeeId
                );

                if (status === 204)
                    this.onOpenToast({
                        title: this.$Resource.removeEmployeeSuccess(
                            this.itemIsRemove.employeeCode
                        ),
                        type: this.$MEnum.ToastType.Success
                    });
                else {
                    this.onOpenDialogError({
                        content: this.$Resource.ErrorConflict,
                        title: this.$Resource.Error
                    });
                }
                this.fetchEmployeesData();
                this.closeDialogRemoveOne();
            } catch (error) {
                this.onOpenDialogError({
                    content: error.message,
                    title: this.$Resource.Error
                });
            } finally {
                this.isLoading = false;
            }
        },
        /**
         * Xử lí export to excel
         * @author NTVu - 05/09/2023
         */
        async clickExportToExcel() {
            try {
                this.isLoading = true;

                const response = await exportToExcel();

                // Tạo một đường dẫn URL từ dữ liệu blob và tạo một liên kết tạm thời
                const url = window.URL.createObjectURL(
                    new Blob([response.data])
                );

                const link = document.createElement('a');
                link.href = url;
                link.setAttribute('download', 'ThongTinNhanVien.xlsx');
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
            } catch (error) {
                this.onOpenDialogError({
                    title: this.$Resource.Error,
                    content: error.message
                });
            } finally {
                this.isLoading = false;
            }
        },
        /**
         * Thay đổi số bản ghi trên 1 trang
         * @Author NTVu - MF1742 - 20/08/2023
         * Modified: NTVu - 22/08/2023: Thêm chuyển về trang 1 sau khi thay đổi page size
         * @param {{value : Number }}

         */
        setRecordsPerPage({ value }) {
            this.currentPage = 1;
            this.$router.push({
                path: '',
                query: {
                    ...this.$route.query,
                    page: 1,
                    pageSize: value
                }
            });
        },

        /**
         * Set trạng thái check all pages
         * @author NTVu - 08/09/2023
         * @param {boolean} isCheck
         */
        setCheckAllPages(isCheck) {
            this.isShowMask = isCheck;
            this.$refs.employeeTable.setAllItemCheck(isCheck);
            this.employeeCheckedAmount = isCheck ? this.totalRecords : 0;
        },

        /**
         * Thay đổi số lượng nhân viên được check của bảng nhân viên
         * @param {Number} value
         */
        setAmountCheckedEmployees(value) {
            this.employeeCheckedAmount = value;
        },
        /**
         * Update search value
         * @author NTVu - 09/09/2023
         */
        updateSearchValue(newValue) {
            this.searchValue = newValue;
            if (!this.searchDebounce) {
                this.searchDebounce = this.$Helper.debounce(() => {
                    this.currentPage = 1;
                    this.$router.push({
                        path: '',
                        query: {
                            ...this.$route.query,
                            page: 1,
                            search: this.searchValue
                        }
                    });
                }, 500);
            }
            this.searchDebounce();
        }
    },
    /**
     * Thiết lập các cột dữ liệu, các pageSizeOptions
     * @Author NTVu - MF1742 - 21/08/2023
     */
    created() {
        // Cấu hình các cột dữ liệu
        this.tableHeaderCols = [
            {
                width: 50,
                fieldName: 'checkbox',
                format: 'center',
                class: 'sticky l-0',
                label: ''
            },
            {
                width: 180,
                fieldName: 'employeeCode',
                label: 'Mã nhân viên',
                format: 'left'
            },
            {
                width: 250,
                fieldName: 'fullName',
                label: 'Tên nhân viên',
                format: 'left'
            },
            {
                width: 120,
                fieldName: 'gender',
                label: 'Giới tính',
                format: 'left',
                isConstant: true
            },
            {
                width: 120,
                fieldName: 'phoneMobile',
                label: 'Điện thoại',
                format: 'left'
            },
            {
                width: 150,
                fieldName: 'birthday',
                label: 'Ngày sinh',
                format: 'center',
                formatType: 'date|dd/MM/yyyy',
                title: 'Ngày / Tháng / Năm'
            },
            {
                title: 'Số chứng minh nhân dân',
                format: 'left',
                width: 180,
                fieldName: 'identityNo',
                label: ' số cmnd'
            },
            {
                format: 'left',
                width: 200,
                fieldName: 'email',
                label: 'Email'
            },
            {
                format: 'left',
                width: 220,
                fieldName: 'positionName',
                label: 'chức danh'
            },
            {
                format: 'left',
                width: 300,
                fieldName: 'departmentName',
                label: 'tên đơn vị'
            },
            {
                format: 'left',
                width: 400,
                fieldName: 'address',
                label: 'địa chỉ'
            },
            {
                format: 'left',
                width: 220,
                fieldName: 'bankName',
                label: 'Tên ngân hàng'
            },
            {
                format: 'left',
                width: 220,
                fieldName: 'bankAccountNo',
                label: 'số tk ngân hàng'
            },
            {
                format: 'left',
                width: 220,
                fieldName: 'bankAccountPlace',
                label: 'Chi nhánh ngân hàng'
            },
            {
                label: 'Chức năng',
                class: 'sticky r-0',
                width: 120,
                format: 'center',
                fieldName: 'action',
                text: 'Sửa',

                actions: [
                    {
                        label: 'Xóa',
                        value: 0,
                        action: this.openDialogRemoveOne
                    }
                ]
            }
        ];

        // Cấu hình các pageSize options
        this.pageSizeOptions = [
            {
                value: 10,
                label: '10 bản tin trên 1 trang'
            },
            {
                value: 20,
                label: '20 bản tin trên 1 trang'
            },
            {
                value: 40,
                label: '40 bản tin trên 1 trang'
            },
            {
                value: 100,
                label: '100 bản tin trên 1 trang'
            }
        ];

        if (this.$route.query.pageSize) {
            const indexPageSizeOnQuery = this.pageSizeOptions.find(
                (option) =>
                    option.value.toString() ===
                    this.$route.query.pageSize.toString()
            );
            this.recordsPerPage = indexPageSizeOnQuery
                ? {
                      ...indexPageSizeOnQuery
                  }
                : { ...this.pageSizeOptions[0] };
        } else {
            this.recordsPerPage = { ...this.pageSizeOptions[0] };
            this.$route.query.pageSize = this.recordsPerPage.value;
        }
        if (this.$route.query.page) {
            this.currentPage = parseInt(this.$route.query.page);
        }
        this.$route.query.search = '';

        this.fetchEmployeesData();
    },

    computed: {
        /**
         * Dùng trên tableWrapper, nếu đang loading hoặc chọn tất cả thì khoá bảng
         * @author NTVu - 28/08/2023
         */
        isTableOverflow() {
            return this.isLoading || this.isShowMask ? 'hidden' : 'auto';
        },
        /**
         * Nếu check nhiều hơn 2 nhân viên thì show nút action nhiều
         * @author NTVu - 07/09/2023
         */
        isShowActionMultiBtn() {
            return this.employeeCheckedAmount >= 2;
        }
    },
    watch: {
        /**
         * Xem sự thay đổi query để fetch lại api
         * @author NTVu - 29/08/2023
         * @param {Route} to - Điều hướng đến
         * @param {Route} from - Điều hướng từ
         */
        '$route.query'(to, from) {
            if (to !== from) {
                this.fetchEmployeesData();
            }
        }
    }
};
</script>

<style scoped>
@import url('./employeeList.css');
</style>
