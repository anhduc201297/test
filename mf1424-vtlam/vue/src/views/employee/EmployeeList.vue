<template>
    <div class="content__header">
        <div class="content__header-left">
            <div class="content__header-title">Nhân viên</div>
        </div>
        <base-button btnText="Thêm mới nhân viên" @click="openEmployeeDetailForm"/>
    </div>
    <div class="content__body">
        <div class="toolbar">
            <div v-if="checkedEmployees.length>0" class="toolbar__left">
                <div class="checked-rows-total">Đã chọn <b>{{ checkedEmployees.length }}</b></div>
                <div class="clear-select" :style="{'color': 'red'}" @click="clearCheck">Bỏ chọn</div>
                <base-button 
                    btnText="Xóa" 
                    isSecondary="true" 
                    btnIcon="trash"
                    @click = "openDeleteDialog"
                />
            </div>
            <div v-else></div>
            <div class="toolbar__right">
                <base-input 
                    inputWidth = "input--search"
                    placeHolder="Tìm theo mã, tên nhân viên" 
                    icon="search"
                    v-model="searchFilter"
                    @keyup="filterEmployees"
                />
                <base-button btnIcon="rotate-right" isSecondary="true" @click="getEmployees"/>
                <base-button btnIcon="file-excel" isSecondary="true"/>
            </div>
        </div>
        <div class="table">
            <div class="table__content table__content--employee">
                <base-loading v-if="isShowLoading"/>
                <table>
                    <thead>
                        <th>
                            <base-checkbox @onChange="checkAll" :checked="isAllChecked"/>
                        </th>
                        <th>Mã nhân viên</th>
                        <th>Tên nhân viên</th>
                        <th>Giới tính</th>
                        <th class="date">Ngày sinh</th>
                        <th title="Số chứng minh nhân dân">Số CMND</th>
                        <th>Chức danh</th>
                        <th>Đơn vị</th>
                        <th>Số tài khoản</th>
                        <th>Tên ngân hàng</th>
                        <th>Chi nhánh</th>
                    </thead>
                    <tbody v-if="!isShowLoading">
                        <tr v-for="employee in employees" :key= "employee.EmployeeId" :class="{'row-checked': checkedEmployees.includes(employee.EmployeeId)}" @mouseover="showFunctionButtons($event,employee)" @dblclick="openEditEmployeeForm(employee)">
                            <td>
                                <base-checkbox :value="employee.EmployeeId" @onChange="checkOne(employee.EmployeeId)" :checked="checkedEmployees.includes(employee.EmployeeId)" @dblclick.stop/>
                            </td>
                            <td>{{ employee.EmployeeCode }}</td>
                            <td>{{ employee.FullName }}</td>
                            <td>{{ employee.GenderName }}</td>
                            <td class="date">{{ $helper.formatDate(employee.DateOfBirth) }}</td>
                            <td>{{ employee.IdentityNumber }}</td>
                            <td>{{ employee.PositionName }}</td>
                            <td>{{ employee.DepartmentName }}</td>
                            <td>{{ employee.BankAccountNumber }}</td>
                            <td>{{ employee.BankName }}</td>
                            <td>{{ employee.BankBranchName }}</td>
                        </tr>
                    </tbody>
                </table>
                <div class="table__functions" :style="{'top': (top-209)+'px'}">
                    <button class="table__button btn--secondary btn-modify">
                        <div class="icon icon-edit" @click="openEditEmployeeForm"></div>
                    </button>
                    <div class="table__more-functions">
                        <button class="table__button btn--secondary btn-more">
                            <div class="icon icon-more"></div>
                        </button>
                        <div class="table__function-list context-menu">
                            <div class="context-menu__item" @click="duplicateEmployee">
                                <div class="context-menu__icon">
                                    <div class="icon icon-copy"></div>
                                </div>
                                <div class="context-menu__label">Nhân bản</div>
                            </div>
                            <div class="context-menu__item" @click="openDeleteEmployeeDialog">
                                <div class="context-menu__icon">
                                    <div class="icon icon-delete"></div>
                                </div>
                                <div class="context-menu__label">Xóa</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="paging">
            <div class="paging__left">Tổng : <b>{{ totalRecord }}</b></div>
            <div class="paging__right">
                <div>Số bản ghi/trang:</div>
                <div><b>{{ pageSize*(pageNumber-1)+1 }}</b> - <b>{{ pageSize*(pageNumber-1) + employees.length }}</b> bản ghi</div>
                <base-combobox
                    :dataList="pageSizeList" 
                    :modelValue="pageSize"
                    className="combobox__menu--up"
                    inputWidth="dropdown--record"
                    isReadOnly="true"
                    @selectAction="selectPageSize"
                />
                <button class="paging__arrow" :disabled="pageNumber==1" @click="selectPageNumber(pageNumber-1)">
                    <font-awesome-icon  icon="chevron-left" class="icon-16"/>
                </button>
                <button class="paging__arrow" :disabled="pageNumber==totalPage" @click="selectPageNumber(pageNumber+1)">
                    <font-awesome-icon  icon="chevron-right" class="icon-16"/>
                </button>
            </div>
        </div>
    </div>
    <EmployeeDetail v-if="isShowForm" @onClose = "closeEmployeeDetailForm" :employeeData="employeeData" :formMode="formMode"/>
    <base-dialog v-if="isShowDialog" @onClose = "closeDeleteDialog">
        <template #header>
            <div class="dialog__title">{{ dialogTitle }}</div>
        </template>
        <template #body>
            <div class="dialog__content">
                <font-awesome-icon  icon="triangle-exclamation" class="dialog__icon--alert"/>
                <div>{{ dialogContent }}</div>
            </div>
            
        </template>
        <template #footer>
            <div></div>
            <div class="dialog__footer-right">
                <base-button btnText="Hủy" isSecondary="true" @click="closeDeleteDialog"/>
                <base-button btnText="Xóa" @click ="deleteEmployee"/>
            </div>
        </template>
    </base-dialog>
    
</template>

<script>
import EmployeeDetail from './EmployeeDetail.vue';
export default {
    name:"EmployeeList",
    components: {EmployeeDetail},
    created(){
        this.getEmployees();
        this.$emitter.on('reloadData',this.getEmployees);
        this.$emitter.on('openForm',this.openEmployeeDetailForm);
    },
    beforeUnmount(){
        this.$emitter.off('reloadData',this.getEmployees);
        this.$emitter.off('openForm',this.openEmployeeDetailForm);
    },
    methods:{

        /**
         * Lấy dữ liệu nhân viên
         * Author:Vũ Tùng Lâm (30/08/2023)
         */
        getEmployees(){
            this.isShowLoading=true;
            this.$axios.get(
            `https://cukcuk.manhnv.net/api/v1/Employees/Filter`, {params:{pageSize: this.pageSize, pageNumber:this.pageNumber, employeeCode:this.employeeFilter,fullName:this.searchFilter}}
            ).then(res => {
                this.employees = res.data.Data;
                this.totalRecord = res.data.TotalRecord;
                this.totalPage = res.data.TotalPage;
                this.isShowLoading=false;
                this.clearCheck();
            }).catch(error =>{
                this.$emitter.emit('showToast',this.$enums.Toast.Error,this.$helper.handleException(error))
            });
        },

        /**
         * Lấy mã nhân viên mới
         * Author:Vũ Tùng Lâm (30/08/2023)
         */
        async getNewEmployeeCode(){
            await this.$axios.get(
            `https://cukcuk.manhnv.net/api/v1/Employees/NewEmployeeCode`
            ).then(res => {
                this.employeeData.EmployeeCode = res.data
            }).catch(error =>{
                this.$emitter.emit('showToast',this.$enums.Toast.Error,this.$helper.handleException(error))
            });
        },

        /**
         * Mở form thêm nhân viên
         * Author: VTLam (20/08/2023)
         */
        async openEmployeeDetailForm(){
            this.employeeData={};
            await this.getNewEmployeeCode();
            this.employeeData.Gender = this.$enums.Gender.Male;
            this.formMode = this.$enums.FormMode.Add;
            this.isShowForm = true;
            
        },

        /**
         * Đóng form nhân viên
         * Author: VTLam (20/08/2023)
         */
        closeEmployeeDetailForm(){
            this.isShowForm = false;
        },

        /**
         * Mở form sửa nhân viên
         * @param {Object} employee 
         * Author: VTLam (20/08/2023)
         */
         openEditEmployeeForm(employee){
            this.employeeData = employee;
            this.formMode = this.$enums.FormMode.Edit;
            this.isShowForm = true;
        },

        /**
         * Mở form nhân viên
         * Author: VTLam (20/08/2023)
         */
         async duplicateEmployee(){
            this.employeeData = this.selectedEmployee;
            await this.getNewEmployeeCode();
            this.formMode = this.$enums.FormMode.Edit;
            this.isShowForm = true;
        },

        /**
        * Xóa nhân viên
        * Author: VTLam (20/08/2023)
        */
        deleteEmployee(){
            this.$axios.delete(`https://cukcuk.manhnv.net/api/v1/Employees/${this.selectedEmployee.EmployeeId}`)
                .then(()=>{
                    this.isShowDialog=false;
                    this.$emitter.emit('showToast',this.$enums.Toast.Success,this.$resource[this.$LangCode].DeleteEmployeeSuccess);
                    this.getEmployees();
                })
                .catch(error=>{
                    this.$emitter.emit('showToast',this.$enums.Toast.Error,this.$helper.handleException(error))
                })
            
        },

        /**
        * mở toast message thành công
        * @param {String} message 
        * Author: VTLam (20/08/2023)
        */
        showSuccessToast(message){
            this.isShowToast= true;
            this.toastType = this.$enums.Toast.Success;
            this.toastMessage = message;
            this.getEmployees();
        },

        /**
        * đóng toast message
        * Author: VTLam (20/08/2023)
        */
        closeToast(){
            this.isShowToast= false;
        },

        /**
        * mở dialog xóa nhiều
        * Author: VTLam (24/08/2023)
        */
        openDeleteBatchDialog(){
            this.isShowDialog= true;
            this.dialogTitle = this.$resource[this.$LangCode].DeleteBatchEmployeeTitle(this.checkedEmployees);
            this.dialogContent = this.$resource[this.$LangCode].DeleteBatchEmployeeMessage;

        },

        /**
        * mở dialog xóa 1 nhân viên
        * Author: VTLam (24/08/2023)
        */
        openDeleteEmployeeDialog(){
            this.isShowDialog= true;
            this.dialogTitle = this.$resource[this.$LangCode].DeleteEmployeeTitle;
            this.dialogContent = this.$resource[this.$LangCode].DeleteEmployeeMessage(this.selectedEmployee.EmployeeCode);

        },

        /**
        * đóng dialog xóa nhiều
        * Author: VTLam (24/08/2023)
        */
        closeDeleteDialog(){
            this.isShowDialog= false;
        },

        /**
        * Hiển thị thanh chức năng
        * @param {*} e
        * Author: VTLam (20/08/2023)
        */
        showFunctionButtons(e,employee){
            this.top = e.target.getBoundingClientRect().top;
            this.selectedEmployee=employee;
        },

        /**
        * Chọn số bản ghi 1 trang
        * @param {int} pageSize
        * Author: VTLam (30/08/2023)
        */
        selectPageSize(pageSize){
            this.pageSize = pageSize;
            this.getEmployees();
        },

        /**
        * Chọn số trang
        * @param {int} pageNumber
        * Author: VTLam (30/08/2023)
        */
        selectPageNumber(pageNumber) {
            this.pageNumber = pageNumber;
            this.getEmployees();
        },

        /**
         * chọn tất cả
         * Author: VTLam (30/08/2023)
        */
        checkAll(){
            this.isAllChecked = !this.isAllChecked;
            if (this.isAllChecked) {
                console.log("hello");
                this.checkedEmployees = this.employees.map(data => data.EmployeeId)
            } else {
                this.checkedEmployees = []
            }
        },

        /**
         * chọn một
         * Author: VTLam (30/08/2023)
        */
        checkOne(id){
            if (this.checkedEmployees.includes(id)) {
                this.checkedEmployees = this.checkedEmployees.filter(item => item != id);
            } else {
                this.checkedEmployees.push(id);
            }
            this.isAllChecked = this.checkedEmployees.length == this.employees.length;
        },

        /**
         * Bỏ chọn
         * Author: VTLam (30/08/2023)
        */
        clearCheck(){
            this.isAllChecked=false;
            this.checkedEmployees = [];
        },

        /**
         * Lọc nhân viên
         * Author: VTLam (30/08/2023)
        */
        filterEmployees(){
            const me = this;
            me.pageNumber=1;
            if (me.timer) {
                clearTimeout(me.timer);
                me.timer = null;
            }
            me.timer = setTimeout(() => {
                me.getEmployees();
            }, 500);
            // setTimeout(function(){
            //     me.getEmployees();
            // }, 500);
        }
    },
    data() {
        return {
            isShowLoading:false,
            isShowForm:false,
            isShowDialog:false,
            top:400,
            employees:[],
            employeeData:{},
            formMode: this.$enums.FormMode.Add,
            dialogContent:"",
            dialogTitle:"", 
            pageSizeList:['10','20','30','50','100'],
            pageSize:'10',
            pageNumber:1,
            employeeFilter:"NV",
            totalRecord:0,
            totalPage:1,
            checkedEmployees:[],
            isAllChecked:false,
            selectedEmployee:{},
            searchFilter:""
        }
    },
}
</script>