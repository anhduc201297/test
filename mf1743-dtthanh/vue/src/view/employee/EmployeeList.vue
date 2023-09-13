<template>
    <!-- eslint-disable -->
    <EmloyeeDetail v-if="showForm" @onCloseForm="onCloseForm" :employeeSelected="employeeSelected">
    </EmloyeeDetail>
    <div class="content-header">
        <h1 class="content-title">Nhân viên</h1>
        <m-button @click="btnAddNewEmployee" :text="this.$resources[this.$LangCode].Button.AddEmployee"></m-button>
    </div>
    <div class="content-menu">
        <div class="content-menu-left center">
            <div class="menu-selected mr-14">
                Đã chọn
                <span>2</span>
            </div>
            <div class="menu-unselected">Bỏ chọn</div>
            <div class="menu-delete center">
                <m-icon icon="trash"></m-icon>
                Xóa tất cả
            </div>
        </div>
        <div class="content-menu-right center">
            <input type="text" class="input input-sm" placeholder="Tìm theo mã, tên nhân viên" />
            <m-icon class="content-menu__icon" icon="excel"></m-icon>
            <m-icon class="content-menu__icon" icon="reload"></m-icon>
        </div>
    </div>
    <div class="content-main">
        <div class="content-main-table">
            <table>
                <thead>
                    <tr>
                        <th class="text-center" style="width: 30px">
                            <m-checkbox text=""></m-checkbox>
                        </th>
                        <th class="text-left" style="width: 120px">
                            MÃ NHÂN VIÊN
                        </th>
                        <th class="text-left">TÊN NHÂN VIÊN</th>
                        <th class="text-left" style="width: 70px">
                            GIỚI TÍNH
                        </th>
                        <th class="text-center" style="width: 90px">
                            NGÀY SINH
                        </th>
                        <th class="text-left" data-tooltip="Số chứng minh nhân dân">
                            SỐ CMND
                        </th>
                        <th class="text-left">CHỨC DANH</th>
                        <th class="text-left">TÊN ĐƠN VỊ</th>
                        <th class="text-left">SỐ TÀI KHOẢN</th>
                        <th class="text-left">TÊN NGÂN HÀNG</th>
                        <th class="text-left">
                            CHI NHÁNH NGÂN HÀNG
                        </th>
                        <th class="text-center">CHỨC NĂNG</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(employee, index) in employees" :key="index">
                        <td class="text-center">
                            <m-checkbox text=""></m-checkbox>
                        </td>
                        <td class="text-left">{{ employee.EmployeeCode }}</td>
                        <td class="text-left">{{ employee.FullName }}</td>
                        <td class="text-left">{{ $helper.formatGender(employee.Gender) }}</td>
                        <td class="text-center">{{ $helper.formatDate(employee.DateOfBirth) }}</td>
                        <td class="text-left">{{ employee.IdentityNumber }}</td>
                        <td class="text-left">{{ employee.PositionName }}</td>
                        <td class="text-left">
                            {{ employee.DepartmentName }}
                        </td>
                        <td class="text-right"></td>
                        <td class="text-left"></td>
                        <td class="text-left"></td>
                        <td class="text-center">
                            <div class="table-btn">
                                <button class="table-text" @click="btnEditEmployee(employee)">
                                    Sửa
                                </button>
                                <button class="table-icon-down icon-24 icon">
                                    <div class="table-dropdown">
                                        <ul>
                                            <li>Nhân bản</li>
                                            <li>Xóa</li>
                                            <li>Ngừng sử dụng</li>
                                        </ul>
                                    </div>
                                </button>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!-- Table Paging -->
        <div class="table-paging">
            <div class="table-paging-left">
                Tổng: <span class="paging-total">1035</span>
            </div>
            <div class="table-paging-right center">
                <div class="paging-record center">
                    <span class="mr-8">Số bản ghi/trang:</span>
                    <div class="record-show center mr-8">
                        <div class="num--record">10</div>
                        <div class="icon-record-down icon-24"></div>
                    </div>
                    <div class="record-list-up">
                        <div class="num-record">
                            <span>10</span>
                        </div>
                        <div class="num-record">
                            <span>20</span>
                        </div>
                        <div class="num-record">
                            <span>30</span>
                        </div>
                        <div class="num-record">
                            <span>50</span>
                        </div>
                        <div class="num-record">
                            <span>100</span>
                        </div>
                    </div>
                </div>
                <div class="paging-num mr-16">
                    <span>1 - 9</span> bản ghi
                </div>
                <div class="paging-btn-record center">
                    <div class="btn-pre icon-24"></div>
                    <div class="btn-next icon-24 mr-0"></div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import EmloyeeDetail from '@/view/employee/EmloyeeDetail.vue'
import employeeApi from '@/api/employee.js'
export default {
    name: "EmployeeList",
    components: {
        EmloyeeDetail
    },
    created() {
        this.loadData();
    },
    methods: {
        /**
         * Mở form thêm mới nhân viên
         * AUTHOR: DTTHANH (19/8/2023)
         */
        btnAddNewEmployee() {
            this.showForm = true
            this.employeeSelected = {};
        },

        /**
         * Đóng form thêm nhân viên
         * AUTHOR: DTTHANH (19/8/2023)
         */
        onCloseForm() {
            this.showForm = false;
            this.loadData();
        },

        /**
         * Lấy dữ liệu từ API
         * AUTHOR: DTTHANH (24/08/2023)
         */
        async loadData() {
            // try {
            //     this.$axios.get("https://cukcuk.manhnv.net/api/v1/Employees")
            //     .then(res => {
            //         this.employees = res.data;
            //     })
            // } catch (error) {
            //     this.$handle.handleAPIError(error);
            // }
            const response = await employeeApi.getEmployee();
            this.employees = response.data;
        },

        /**
         * Mở form sửa thông tin nhân viên
         * AUTHOR: DTTHANH (02/09/2023)
         * @param {*} employee: nhân viên 
         */
        btnEditEmployee(employee) {
            this.employeeSelected = employee;
            this.showForm = true
        },

        /**
         * Xóa 1 nhân viên khỏi danh sách
         * AUTHOR: DTTHANH (04/09/2023)
         */
        // async btnDeleteEmployee(employeeID) {
        //     try {
        //         this.$axios.delete()
        //     } catch (error) {
                
        //     }
        // }

    },
    data() {
        return {
            showForm: false,
            employees: [],
            employeeSelected: {},
        }
    },


}
</script>

<style></style>