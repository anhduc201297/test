<template>
        <!-- Title -->
        <div class="content__title">
            <div class="content__name">Nhân viên</div>
            <MISAButton 
              class="btn__add-user" 
              :text="$MISAResource[$LangCode].AddEmployee" 
              @click="openAddPopup()"
              ></MISAButton>
            <EmployeeInfor  
              v-if="showPopup" 
              @close="closePopup"
              :formMode="formMode"
              @success="showSuccessToast"
              @error="showErrorToast"
              @warning="showWarningToast"
              @infor="showInforToast"
            ></EmployeeInfor>
          </div>
          <!-- Container -->
          <div class="content__table">
            <div class="content__search">
              <div class="">
                <button class="btn-normal btn__execution">Thực hiện hàng loạt</button>
              </div>
              <div class="content__search--input">
                <div class="content__input">
                  <input
                    class="search__input"
                    placeholder="Tìm theo mã, tên nhân viên"/>
                </div>
                <div class="content__sreach-icon"></div>
                <div class="icon content__repeat tooltip" @click="onLoading">
                  <MISATooltip tooltip="Load dữ liệu"></MISATooltip>
                </div>
                <div class="icon content__export tooltip" @click="onExport">
                  <MISATooltip tooltip="Xuất ra Excel"></MISATooltip>
                </div>
              </div>
            </div>
            <MISALoading v-if="isLoading"></MISALoading>
            <!-- Table -->
            <MISAToast v-if="showToast" :type="toastType" @close="closeToast"></MISAToast>
            <div class="content__main-table">
              <div class="content__table-content">
                <table>
                  <tr>
                    <th class="table__checkbox">
                      <MISAInputCheckbox 
                        class="table__checkbox-input" 
                        :value="selectAll"   
                        @click="onChecked"   
                      ></MISAInputCheckbox>
                    </th>
                    <th class="table__id">Mã nhân viên</th>
                    <th class="table__name">Tên nhân viên</th>
                    <th class="table__gender">Giới tính</th>
                    <th class="table__dob">Ngày sinh</th>
                    <th class="table_idnumber">Số CMND</th>
                    <th class="table__position">Chức danh</th>
                    <th class="table__unitname">Tên đơn vị</th>
                    <th class="table__account-number">Số tài khoản</th>
                    <th class="table__bank-name">Tên ngân hàng</th>
                    <th class="table__bank-branch">Chi nhánh ngân hàng</th>
                    <th class="table__function">Chức năng</th>
                  </tr>
                  <tr v-for="employee in employees" :key="employee.EmployeeId">
                    <td><MISAInputCheckbox 
                          class="checkbox-input"        
                          :value="selectAll"
                        ></MISAInputCheckbox>
                    </td>
                    <td>{{employee.EmployeeCode}}</td>
                    <td>{{employee.FullName}}</td>
                    <td>{{employee.Gender}}</td>
                    <td>{{$helper.formatDate(employee.DateOfBirth)}}</td>
                    <td>{{employee.IdentityNumber}}</td>
                    <td>{{employee.PositionName}}</td>
                    <td>{{employee.DepartmentName}}</td>
                    <td>{{employee.EmployeeAccountNumber}}</td>
                    <td>{{employee.EmployeeBankName}}</td>
                    <td>{{employee.EmployeeBankBranch}}</td>
                    <td class="table__action">
                      <div class="table__action-button">
                        <span class="table__action-name" @click="openEditPopup()">Sửa</span>
                        <div class="table__icon-check">
                          <div class="table__icon"  @click="showEmployeeDropdown(employee)"></div>
                            <div class="table--dropdown" v-if="employee.isDropdownVisible">
                              <a href="#">Nhân bản</a>
                              <a href="#" @click="openDeleteDialog(employee)">Xóa</a>
                                <MISADialog 
                                  v-if="showDeleteDialog"
                                  @close="closeDeleteDialog()"
                                  :title="$MISAEnum.DialogMode.Delete.title"
                                  :content="$MISAEnum.DialogMode.Delete.content"
                                  :typeIcon="$MISAEnum.DialogMode.Delete.typeIcon"
                                  :onConfirm="deleteEmployee"
                                ></MISADialog>
                                <MISADialog 
                                  v-if="showDeleteMultipleDialog"
                                  @close="closeDeleteDialog()"
                                  :title="$MISAEnum.DialogMode.DeleteAll.title"
                                  :content="$MISAEnum.DialogMode.DeleteAll.content"
                                  :typeIcon="$MISAEnum.DialogMode.DeleteAll.typeIcon"
                                  :onConfirm="deleteMultipleEmployee"
                              ></MISADialog>
                              <a href="#">Ngừng sử dụng</a>
                          </div>
                        </div>
                      </div>
                    </td>
                  </tr>
                </table>
              </div>
              <hr style="margin: 12px 0px 20px 0px;  
              background-color: #E0E0E0; 
              height: 1px; 
              border: 0">
              <!-- Paging -->
              <div class="table__detail">
                <div class="table__detail--sum">Tổng số: <b>1035</b> bản ghi</div>
                <MISAPaging></MISAPaging>
              </div>
            </div>
          </div>
</template>
  
<script>
import axios from 'axios';
import EmployeeInfor from './EmployeeInfor.vue';
import MISAButton from '@/components/base/MISAButton.vue';
import { handleError } from '../../js/errorhandle';
import { API_BASE_URL } from '../../js/baseurl';
import MISAToast from '@/components/base/MISAToast.vue';
import { ref } from "vue";

export default {
    name: 'EmployeeList', 
    components: {
      MISAButton,
      EmployeeInfor,
      MISAToast
    },
    data() {
      return {
        showDeleteMultipleDialog:false,
        selectAll: false,
        isLoading:false,
        showPopup: false,
        isDropdownVisible: false,
        formMode: null,
        showDeleteDialog: false,
        employee: null,
        employees: [],
        
      };
  },
    methods: {
        /**
        * Hàm mở dialog xóa nhân viên
        * CreateBy : NTKMY (24/08/2023)
        */
        openDeleteDialog(employee) {
          this.showDeleteDialog = true;
          this.employeeToDelete = employee;
        },
        /**
        * Hàm đóng dialog xóa nhân viên
        * CreateBy : NTKMY (24/08/2023)
        */
        closeDeleteDialog() {
          this.showDeleteDialog  = false;
        },
        /**
        * Hàm open dropdown
        * CreateBy : NTKMY (20/08/2023)
        */
        showEmployeeDropdown(employee) {
          employee.isDropdownVisible = !employee.isDropdownVisible;
        },

        /**
        * Hàm mở Form nhân viên theo chức năng: Thêm mới 
        * CreateBy : NTKMY (30/08/2023)
        */
        openAddPopup() {
          this.openPopup(this.$MISAEnum.FormMode.Add);
        },

        /**
        * Hàm mở Form nhân viên theo chức năng: Sửa
        * CreateBy : NTKMY (30/08/2023)
        */
        openEditPopup() {
          this.openPopup(this.$MISAEnum.FormMode.Edit);
        },

        /**
        * Hàm mở Form nhân viên 
        * CreateBy : NTKMY (30/08/2023)
        */
        openPopup(mode) {
          this.showPopup = true;
          this.formMode = mode;
        },

        /**
        * Hàm đóng popup
        * CreateBy : NTKMY (20/08/2023)
        */
        closePopup() {
          this.showPopup = false;
        },

        /**
        * Lấy dữ liệu, call api lấy toàn bộ dữ liệu nhân viên
        * CreateBy : NTKMY (30/08/2023)
        */
        async fetchEmployees() {
          try {
            const response = await axios.get(`${API_BASE_URL}Employees`);
            this.employees = response.data;
          } catch (error) {
            console.error('Error fetching employees:', error);
          }
        },
        /**
        * Lấy dữ liệu, call api xóa 1 nhân viên
        * CreateBy : NTKMY (30/08/2023)
        */
        deleteEmployee() {
          if (this.employeeToDelete) {
            this.closeDeleteDialog();
            this.deleteAnEmployee(this.employeeToDelete);
          }
        },


        /**
        * Lấy dữ liệu, call api xóa nhiều nhân viên
        * CreateBy : NTKMY (02/09/2023)
        */
        deleteMultipleEmployee() {
        },

        /**
        * Gọi đến api xóa nhân viên
        * CreateBy : NTKMY (30/08/2023)
        */
        async deleteAnEmployee(employee) {
          try {
            const response = await axios.delete(`${API_BASE_URL}Employees/${employee.EmployeeId}`);
            console.log('Employee deleted:', response.data);
            this.showSuccessToast();
            this.fetchEmployees();
          } catch (error) {
            handleError(error);
          }
        },

        /**
        * Hiển thị loading, set thời gian loading sau đó sẽ load lại dữ liệu
        * CreateBy : NTKMY (01/09/2023)
        */
        async onLoading() {
          this.isLoading = true;
          setTimeout(async () => {
            await this.fetchEmployees();
            this.isLoading = false;
          }, 1500);
        }, 

        /**
        * Export dữ liệu
        * CreateBy : NTKMY (01/09/2023)
        */
        onExport() {},

        onChecked() {
          this.selectAll=!this.selectAll;
        },

    },

    /**
    * Hiển thị Toast theo trạng thái Success, Warning, Error, Infor
    * CreateBy : NTKMY (01/09/2023)
    */
    setup() {
        const showToast = ref(false);
        const toastType = ref(null);

        const showSuccessToast = () => {
          showToast.value = true;
          toastType.value = "Success";
        };
    
        const showWarningToast = () => {
          showToast.value = true;
          toastType.value = "Warning";
        };

        const showInforToast = () => {
          showToast.value = true;
          toastType.value = "Infor";
        };
    
        const showErrorToast = () => {
          showToast.value = true;
          toastType.value = "Error";
        };
    
        const closeToast = () => {
          showToast.value = false;
        };

        return {
          showToast,
          toastType,
          showSuccessToast,
          showWarningToast,
          showInforToast,
          showErrorToast,
          closeToast,
        };
      },

    created() {
      this.fetchEmployees();
    },
}
</script>

<style scoped>
@import url('../../css/view/employee-list.css');
</style>
  