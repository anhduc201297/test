<template>
    <base-dialog width="900" @onClose="checkDataChange" >
        <template #header>
            <div class="dialog__title">Thông tin nhân viên</div>
            <base-checkbox ref="First" checkBoxLabel="Là khách hàng" tabIndex="1"/>
            <base-checkbox ref="Last" checkBoxLabel="Là nhà cung cấp" tabIndex="21"/>
        </template>
        <template #body>
            <div class="dialog__body--top">
                    <div class="dialog__body--left w-50">
                        <div class="row-flex">
                            <base-input
                                v-model = "employee.EmployeeCode"
                                ref="EmployeeCode" 
                                inputLabel="Mã" 
                                inputWidth="w-40"
                                inputType="required"
                                :errorMess="$resource[$LangCode].EmptyEmployeeCode" 
                                tabIndex="2"
                                @keydown="focusLast"
                            />
                            <base-input
                                v-model = "employee.FullName" 
                                inputLabel="Tên"
                                inputWidth="flex-1"
                                inputType="required" 
                                :errorMess="$resource[$LangCode].EmptyEmployeeName"
                                tabIndex="3"
                            />
                        </div>
                        <base-combobox
                            v-model="employee.DepartmentName"
                            @selectAction="selectDepartment"
                            inputLabel="Đơn vị"
                            :errorMess="$resource[$LangCode].EmptyDepartmentName" 
                            :dataList="departmentList.map(dep => dep.DepartmentName)" 
                            className="combobox__menu--down"
                            inputType="required" 
                            tabIndex="6"
                        />
                        <base-input
                            v-model="employee.PositionName"
                            inputLabel="Chức danh"
                            tabIndex="9"
                        />
                    </div>
                    <div class="dialog__body--right w-50">
                        <div class="row-flex">
                            <base-input
                                v-model="employee.DateOfBirth" 
                                inputLabel="Ngày sinh"
                                inputWidth="w-40"
                                inputType="date" 
                                :errorMess="$resource[$LangCode].InvalideDateOfBirth" 
                                tabIndex="3"
                            />
                            <div class="input flex-1">
                                <label class="input__label">Giới tính</label>
                                <div class="checkbox-list--gender">
                                    <base-checkbox checkBoxLabel="Nam" type="radio"  tabIndex="5" :value="$enums.Gender.Male" name="Gender" :checked="employee.Gender==$enums.Gender.Male" @change="(e)=>(employee.Gender = parseInt(e.target.value))"/>
                                    <base-checkbox checkBoxLabel="Nữ" type="radio" tabIndex="5" :value="$enums.Gender.Female" name="Gender" :checked="employee.Gender==$enums.Gender.Female" @change="(e)=>(employee.Gender = parseInt(e.target.value))"/>
                                    <base-checkbox checkBoxLabel="Khác" type="radio" tabIndex="5" :value="$enums.Gender.Other" name="Gender" :checked="employee.Gender==$enums.Gender.Other" @change="(e)=>(employee.Gender = parseInt(e.target.value))"/>
                                </div>
                            </div>
                        </div>
                        <div class="row-flex">
                            <base-input
                                v-model="employee.IdentityNumber"
                                inputLabel="Số CMND" 
                                inputWidth="w-60" 
                                labelTooltip="Số chứng minh nhân dân"
                                tabIndex="7" 
                            />
                            <base-input
                                v-model="employee.IdentityDate"
                                inputLabel="Ngày cấp"
                                inputWidth="flex-1"
                                inputType="date" 
                                :errorMess="$resource[$LangCode].InvalideIdentityDate"  
                                tabIndex="8"
                            />
                        </div>
                        <base-input
                            v-model="employee.IdentityPlace"
                            inputLabel="Nơi cấp" 
                            tabIndex="10"
                        />
                    </div>
                </div>
                <div class="dialog__body--bottom">
                    <base-input
                        v-model="employee.Address"
                        inputLabel="Địa chỉ" 
                        tabIndex="11" 
                    />
                    <div class="row-flex">
                        <base-input
                            v-model="employee.PhoneNumber"
                            inputLabel="ĐT di động" 
                            labelTooltip="Điện thoại di động" 
                            inputWidth="w-25" 
                            tabIndex="12" 
                        />
                        <base-input 
                            inputLabel="ĐT cố định" 
                            labelTooltip="Điện thoại cố định" 
                            tabIndex="13" 
                        />
                        <base-input
                            v-model="employee.Email"
                            inputLabel="Email" 
                            inputWidth="w-25" 
                            inputType="email"
                            :errorMess="$resource[$LangCode].InvalidEmail" 
                            placeHolder="example@gmail.com" 
                            tabIndex="14"
                        />
                        <div class="w-25"></div>
                    </div>
                    <div class="row-flex">
                        <base-input 
                            inputLabel="Tài khoản ngân hàng" 
                            inputWidth="w-25" 
                            tabIndex="15" 
                        />
                        <base-input 
                            inputLabel="Tên ngân hàng" 
                            inputWidth="w-25" 
                            tabIndex="16" 
                        />
                        <base-input
                            inputLabel = "Chi nhánh TK ngân hàng"
                            labelTooltip="Chi nhánh tài khoản ngân hàng"
                            inputWidth="w-25" 
                            tabIndex="17" 
                        />
                        <div class="w-25"></div>
                    </div>
                </div>
        </template>
        <template #footer>
            <base-button btnText="Hủy" isSecondary="true" tabIndex="20" @click="closeEmployeeDetailForm" @keydown="focusFirst"/>
            <div class="dialog__footer-right">
                <base-button btnText="Cất" isSecondary="true" tabIndex="19" @click="onSave"/>
                <base-button btnText="Cất và thêm" tabIndex="18" @click="onSaveAdd"/>
            </div>
        </template>
    </base-dialog>
    <base-dialog v-if="isShowDialog" @onClose="closeDialog">
        <template #header>
            <div class="dialog__title">Dữ liệu thay đổi</div>
        </template>
        <template #body>
            Lưu lại những thay đổi?
        </template>
        <template #footer>
            <div></div>
            <div class="dialog__footer-right">
                <base-button btnText="Hủy" isSecondary="true" @click="closeDialog"/>
                <base-button btnText="Không" isSecondary="true" @click="closeAll"/>
                <base-button btnText="Xóa" @click="onSave"/>
            </div>
        </template>
    </base-dialog>
</template>

<script>
export default {
    name:"EmployeeDetail",
    props:['employeeData','formMode'],

    created(){
        /**
         * Lấy dữ liệu phòng ban
         * Author:VTLam(4/9/2023)
         */
        this.$axios.get(
        `https://cukcuk.manhnv.net/api/v1/Departments`
        ).then(res=>{
            this.departmentList = res.data;
        }).catch(error=>{
            this.$emitter.emit('showToast',this.$enums.Toast.Error,this.$helper.handleException(error))
        })
    },

    mounted(){
        /**
         * focus vào input đầu tiên
         * Author:VTLam(4/9/2023)
         */
        this.$refs.EmployeeCode.focusInput();
    },

    methods:{

        /**
         * focus từ nút cuối cùng quay về input đầu tiên
         * @param {*} e
         * Author: Vũ Tùng Lâm (4/9/2023)
         */
         focusFirst(e){
            if(!e.shiftKey && e.which==9){
                this.$refs.First.focusInput();
            }
            
        },

        /**
         * focus từ input đầu tiên về nút cuối cùng
         * @param {*} e
         * Author: Vũ Tùng Lâm (4/9/2023)
         */
         focusLast(e){
            if((e.shiftKey && e.which==9)){
                this.$refs.Last.focusInput();
            }
            
        },

        /**
         * combobox chọn phòng ban
         * @param {String} dep
         * Author: Vũ Tùng Lâm (30/8/2023)
         */
        selectDepartment(departmentName){
            for(const dep of this.departmentList){
                if(dep.DepartmentName==departmentName){
                    this.employee.DepartmentId=dep.DepartmentId;
                    this.employee.DepartmentCode=dep.DepartmentCode;
                }
            }
        },

        /**
         * Đóng form
         * Author: Vũ Tùng Lâm(4/9/2023)
         */
        closeEmployeeDetailForm(){
            this.$emit("onClose");
        },

        /**
         * Lưu dữ liệu nhân viên và thêm
         * Author: Vũ Tùng Lâm(4/9/2023)
         */
        onSaveAdd(){
            this.isSaveAdd=true;
            this.onSave();
        },

        /**
         * Lưu dữ liệu nhân viên
         * Author: Vũ Tùng Lâm(31/08/2023)
         */
        onSave(){
            if(this.isShowDialog){
                this.closeDialog();
            }
            if(this.formMode==this.$enums.FormMode.Add){
                this.$axios.post(`https://cukcuk.manhnv.net/api/v1/Employees`,this.employee)
                .then(()=>{
                    this.$emitter.emit('showToast',this.$enums.Toast.Success,this.$resource[this.$LangCode].AddEmployeeSuccess);
                    this.afterSave();
                })
                .catch(error=>{
                    this.$emitter.emit('showToast',this.$enums.Toast.Error,this.$helper.handleException(error))
                })
            }
            else if(this.formMode==this.$enums.FormMode.Edit){
                this.$axios.put(`https://cukcuk.manhnv.net/api/v1/Employees/${this.employee.EmployeeId}`,this.employee)
                .then(()=>{
                    this.$emitter.emit('showToast',this.$enums.Toast.Success,this.$resource[this.$LangCode].EditEmployeeSuccess);
                    this.afterSave();
                })
                .catch(error=>{
                    this.$emitter.emit('showToast',this.$enums.Toast.Error,this.$helper.handleException(error))
                })
            }
        },

        /**
         * sau khi dữ liệu nhân viên
         * Author: Vũ Tùng Lâm(4/9/2023)
         */
        afterSave(){
            this.$emitter.emit('reloadData');
            this.closeEmployeeDetailForm();
            if(this.isSaveAdd){
                this.$emitter.emit('openForm'); 
            }
        },


        /**
         * Kiểm tra dữ liệu thay đổi
         * Author: VTLam (4/9/2023)
         */
        checkDataChange(){
            console.log(this.employee);
            console.log(this.employeeData);
            if(JSON.stringify(this.employee) != JSON.stringify(this.employeeData)){
                this.isShowDialog=true;
            }
            else{
                this.closeEmployeeDetailForm();
            }
        },

        /**
         * Đóng thông báo
         * Author: VTLam (4/9/2023)
         */
        closeDialog(){
            this.isShowDialog=false;
        },

        /**
         * Đóng tất cả
         * Author: VTLam (4/9/2023)
         */
        closeAll(){
            this.closeDialog();
            this.closeEmployeeDetailForm();
        }
    },

    data(){
        return {
            employee: JSON.parse(JSON.stringify(this.employeeData)),
            departmentList:[],
            isShowDialog:false,
            isSaveAdd: false
        }
    }
}
</script>