<template>
    <div id="popupContainer" class="popup-container">
        <div class="popup-content">
          <div class="user-form">
            <div class="header__user">
              <div class="header__user-title">
                <div class="header__user-name">{{ 
                  formMode === this.$MISAEnum.FormMode.Add ? 'Thêm mới nhân viên': 'Thông tin nhân viên'
                }}</div>
                <div class="header__checkbox">
                  <div class="header__checkbox--guest">
                    <MISAInputCheckbox class="checkbox-input"></MISAInputCheckbox>
                    <label for="customCheckbox" class="header__checkbox-label"
                      >Là khách hàng</label
                    >
                  </div>
                  <div class="header__checkbox--supplier">
                    <MISAInputCheckbox class="checkbox-input"></MISAInputCheckbox>
                    <label for="customCheckbox" class="header__checkbox-label"
                      >Là nhà cung cấp</label
                    >
                  </div>
                </div>
              </div>
              <div class="header__user-icon">
                <div class="icon tooltip header__icon--question">
                  <MISATooltip tooltip="Giúp (F1)"></MISATooltip>
                </div>
                <div
                  class="icon tooltip header__icon--quit"
                  id="icon--quit"
                  @click="openSaveDialog"
                >
                  <MISATooltip tooltip="Đóng (ESC)"></MISATooltip>
                </div>
                <MISADialog 
                  v-if="showSaveDialog" 
                  @close="closeSaveDialog"
                  :title="$MISAEnum.DialogMode.Save.title"
                  :content="$MISAEnum.DialogMode.Save.content"
                  :typeIcon="$MISAEnum.DialogMode.Save.typeIcon"
                ></MISADialog>
                <MISADialog 
                  v-if="showWarningDialog" 
                  @close="closeWarningDialog"
                  :title="$MISAEnum.DialogMode.Warning.title"
                  :content="warningDialogContent"
                  :typeIcon="$MISAEnum.DialogMode.Warning.typeIcon"
                  :showButton="false"
                  :onConfirm="onConfirmWarning"
                ></MISADialog>
              </div>
            </div>
            <div class="content">
              <div class="subcontent">
                <MISAInputText 
                  :label="$MISAResource[$LangCode].Label.Code"
                  :style="'width: 160px; margin-right: 8px'"
                  :name="'code'"
                  id="employeeCode"
                  :required="true"
                  :showLabel="true"
                  :errorStyle="'margin-right: 8px'"
                  v-model="employee.EmployeeCode"
                >
                </MISAInputText>
                <MISAInputText 
                  :label='$MISAResource[$LangCode].Label.FullName'
                  :style="'width: 220px; margin-right: 24px'"   
                  name="fullname"
                  :required="true"
                  :showLabel="true"
                  :errorStyle="'margin-right: 24px'"
                  v-model="employee.FullName"
                  >
                </MISAInputText>
                <div class="subcontent__form-input">
                  <div class="subcontent__label">
                    <label class="subcontent__name">{{$MISAResource[$LangCode].Label.DateOFBirth}}</label>
                  </div>
                  <div style="position: relative; display: inline-block">
                    <MISAInputDateTime style="width: 160px; margin-right: 8px;"  v-model="employee.EmployeeDateOfBirth"></MISAInputDateTime>
                  </div>
                </div>
                <div class="subcontent__form-input">
                  <div class="subcontent__label">
                    <label class="subcontent__name" style="margin-left: 14px"
                      >{{$MISAResource[$LangCode].Label.Gender}}</label
                    >
                  </div>
                  <form class="gender">
                      <MISAInputRadio 
                        :value="$MISAEnum.Gender.Male" 
                        :label="$MISAResource[$LangCode].Gender[$MISAEnum.Gender.Male]">
                      </MISAInputRadio>
                      <MISAInputRadio 
                        :value="$MISAEnum.Gender.Female" 
                        :label="$MISAResource[$LangCode].Gender[$MISAEnum.Gender.Female]">
                      </MISAInputRadio>
                      <MISAInputRadio 
                        :value="$MISAEnum.Gender.Other" 
                        :label="$MISAResource[$LangCode].Gender[$MISAEnum.Gender.Other]"></MISAInputRadio>
                  </form>
                </div>
              </div>
              <div class="subcontent">
                <MISAInputText 
                  :label='$MISAResource[$LangCode].Label.Unit'
                  :style="'width: 400px; margin-right: 24px'"   
                  name="unit"
                  :required="true"
                  :showLabel="true"
                  :errorStyle="'margin-right: 24px'"
                  v-model="employee.DepartmentName"
                  >
                </MISAInputText>
                <MISAInputText 
                  :label='$MISAResource[$LangCode].Label.IdNumber'
                  :style="'width: 225px; margin-right: 8px'"   
                  name="idnumber"
                  :showLabel="true"
                  :showTooltip="true"
                  tooltip="Số chứng minh nhân dân"
                  v-model="employee.IdNumber"
                  >
                </MISAInputText>
                <div class="subcontent__form-input">
                  <div class="subcontent__label">
                    <label class="subcontent__name">{{$MISAResource[$LangCode].Label.Date}}</label>
                  </div>
                  <div style="position: relative; display: inline-block">
                    <MISAInputDateTime style="width: 162px; margin-right: 8px;" ></MISAInputDateTime>
                  </div>
                </div>
              </div>
              <div class="subcontent" style="margin-top: 24px">
                <MISAInputText 
                  :label='$MISAResource[$LangCode].Label.Position'
                  :style="'width: 400px; margin-right: 24px'"   
                  name="position"
                  :showLabel="true"
                  v-model="employee.PositionName"
                >
                </MISAInputText>
                <MISAInputText 
                  :label='$MISAResource[$LangCode].Label.ProvideAdrress'
                  :style="'width: 405px; margin-right: 24px'"   
                  name="provideadrress"
                  :showLabel="true"
                  v-model="employee.ProvideAdrress"
                >
                </MISAInputText>
              </div>
              <div class="subcontent">
                <MISAInputText 
                  :label='$MISAResource[$LangCode].Label.Address'
                  :style="'width: 840px; margin-right: 24px'"   
                  name="address"
                  :showLabel="true"
                  v-model="employee.Address"
                >
                </MISAInputText>
              </div>

              <div class="subcontent">
                <MISAInputText 
                  :label='$MISAResource[$LangCode].Label.PhoneNumber'
                  :style="'width: 200px; margin-right: 8px'"   
                  name="phonenumber"
                  :showTooltip="true"
                  tooltip="Điện thoại di động"
                  :showLabel="true"
                  v-model="employee.PhoneNumber"
                >
                </MISAInputText>
                <MISAInputText 
                  :label='$MISAResource[$LangCode].Label.LandlinePhone'
                  :style="'width: 200px; margin-right: 8px'"   
                  name="landlinephone"
                  :showTooltip="true"
                  tooltip="Điện thoại cố định"
                  :showLabel="true"
                  v-model="employee.LandlinePhone"
                >
                </MISAInputText>
                <MISAInputText 
                  :label='$MISAResource[$LangCode].Label.Email'
                  :style="'width: 200px; margin-right: 8px'"   
                  name="email"
                  :showLabel="true"
                  v-model="employee.Email"
                >
                </MISAInputText>
              </div>
              <div class="subcontent">
                <MISAInputText 
                  :label='$MISAResource[$LangCode].Label.AccountNumber'
                  :style="'width: 200px; margin-right: 8px'"   
                  name="accountnumber"
                  :showLabel="true"
                  v-model="employee.AccountNumber"
                >
                </MISAInputText>
                <MISAInputText 
                  :label='$MISAResource[$LangCode].Label.BankName'
                  :style="'width: 200px; margin-right: 8px'"   
                  name="bankname"
                  :showLabel="true"
                  v-model="employee.BankName"
                >
                </MISAInputText>
                <MISAInputText 
                  :label='$MISAResource[$LangCode].Label.BankBranch'
                  :style="'width: 200px; margin-right: 8px'"   
                  name="bankbranch"
                  :showLabel="true"
                  v-model="employee.BankBranch"
                ></MISAInputText>
              </div>
            </div>
            <hr style="
              margin: 32px 24px 32px 24px;
              background-color: #babec5; 
              height: 1px; 
              border: 0;
          }"/>
            <div class="btn">
              <div class="btn--left">
                <MISAButton 
                  class="btn-cancel" 
                  :class="$MISAEnum.ButtonType.Normal" 
                  :text="$MISAResource[$LangCode].CancelText" 
                  @click="closePopup"
                ></MISAButton>
              </div>
              <div class="btn--right" >
                <MISAButton 
                  class="btn--hidden" 
                  :class="$MISAEnum.ButtonType.Normal" 
                  :text="$MISAResource[$LangCode].SaveText"
                  :showTooltip="true"
                  tooltip="Cất (Ctrl + S)"
                  @click="onSave()">
                </MISAButton>
                <MISAToast v-if="showToast" :type="toastType" @close="closeToast"></MISAToast>
                <div
                  id="warning-nameContainer"
                  class="popup-container"
                  style="display: none"
                >
                </div>
                <MISAButton 
                  class="btn--add" 
                  :text="$MISAResource[$LangCode].SaveAndAddTExt"
                  :showTooltip="true"
                  tooltip="Cất và thêm (Ctrl + Shift + S)">
                  ></MISAButton>
              </div>
            </div>
          </div>
        </div>
      </div>
</template>
  
<script>
import MISAInputCheckbox from '@/components/base/MISAInput/MISAInputCheckbox.vue';
import { API_BASE_URL } from '../../js/baseurl';
import MISAToast from '@/components/base/MISAToast.vue';

export default {
    name: 'EmployeeInfor',
    props: ["formMode"],
    components: { MISAInputCheckbox, MISAToast },
    data() {
        return {
            showWarningDialog: false,
            showSaveDialog: false,
            showToast: false, 
            toastType: null,
            employee: {
              EmployeeCode:"",
            },
            dialogIconStyle: "background-position:  -826px -456px;",
        };
    },
    methods: {
        /**
        * Hàm đóng dialog cảnh báo
        * CreateBy : NTKMY (20/08/2023)
        */
      onConfirmWarning() {
        this.showWarningDialog = false;
      },
        /**
        * Hàm mở dialog lưu
        * CreateBy : NTKMY (20/08/2023)
        */
        openSaveDialog() {
            this.showSaveDialog = true;
        },
        /**
        * Hàm đóng dialog lưu
        * CreateBy : NTKMY (20/08/2023)
        */
        closeSaveDialog() {
            this.showSaveDialog = false;
        },

        /**
        * Hàm đóng dialog cảnh báo
        * CreateBy : NTKMY (20/08/2023)
        */
        closeWarningDialog() {
            this.showWarningDialog = false;
        },
        /**
        * Hàm close dialog
        * CreateBy : NTKMY (20/08/2023)
        */
        closePopup() {
            this.$emit('close');
        },

        /**
        * Hàm xác thực các trường thông tin của nhân viên
        * CreateBy : NTKMY (03/09/2023)
        */
        validateEmployeeFields() {
          const { EmployeeCode, FullName, DepartmentName } = this.employee;
          let warningDialogContent = "";

          if (!EmployeeCode) {
            warningDialogContent += 'Mã';
          }
          if (!FullName && EmployeeCode) {
            warningDialogContent += warningDialogContent ? ', Tên' : 'Tên';
          }
          if (!DepartmentName && EmployeeCode && FullName) {
            warningDialogContent += warningDialogContent ? ', Đơn vị' : 'Đơn vị';
          }

          if (warningDialogContent) {
            warningDialogContent += this.$MISAEnum.DialogMode.Warning.content;
          }

          return warningDialogContent;
        },
        /**
        * Hàm lưu thông tin nhân viên
        * CreateBy : NTKMY (30/08/2023)
        */
        onSave() {
          const warningDialogContent = this.validateEmployeeFields();

          if (warningDialogContent !== "") {
            this.showWarningDialog = true;
            this.warningDialogContent = warningDialogContent;
          } else {
            this.showFormMode();
          }
        },

        showFormMode() {
          if(this.formMode == this.$MISAEnum.FormMode.Add) {
              this.createEmployee();
            }else if(this.formMode == this.$MISAEnum.FormMode.Edit) {
              this.updateEmployee();
          }
        },
        /**
        * Gọi đến api thêm nhân viên
        * CreateBy : NTKMY (30/08/2023)
        */
        async createEmployee() {
          try {
            await this.$axios.post(`${API_BASE_URL}Employees`, this.employee);
            this.closePopup();
            this.$emit("success");
          } catch (error) {
            this.closePopup();
            this.$emit("error");
          }
        },
        /**
        * Hàm đóng Toast
        * CreateBy : NTKMY (30/08/2023)
        */
        closeToast() {
          this.showToast = false;
        },
      },
    /**
    * Hàm focus vào input đầu tiên
    * CreateBy : NTKMY (01/09/2023)
    */
    mounted() {
      document.getElementById("employeeCode").focus();
    },

}
</script>

<style scoped>
@import url('../../css/view/employee-infor.css');
.btn-cancel, .btn--hidden {
  border: 1px solid #babec5;
}
</style>
  