<template>
  <!-- <div class="main-container"  -->
  <!-- <TheSidebar></TheSidebar> -->
  <aside @click="hideEmpSelectFunc(empSelectFuncId)">
    <div class="emp-container">
      <!-- Tiêu đề bảng -->
      <div class="emp-head">
        <h1 class="emp-title">Nhân viên</h1>
        <button class="m-btn add-new-emp" @click="showEmpDetail">
          Thêm mới nhân viên
        </button>
      </div>

      <div class="emp-table-container">
        <!-- Search Employee -->
        <div class="search-emp-box">
          <div>
            <div class="batch-execute" v-if="isEmpBatchExe || isSelectAllEmp">
              <div class="selected-container">
                Đã chọn: <strong v-text="numEmpSelected"></strong>
              </div>
              <div class="unselected-container" @click="unselectedAllEmp">
                Bỏ chọn
              </div>

              <div
                class="selected-all-container"
                @click="selectAllEmp"
                v-if="!isSelectAllEmp"
              >
                Chọn tất cả các trang
              </div>
              <div class="action-container" @click="deleteAllEmpSelected">
                <div class="action-icon-box">
                  <div class="icons icon__trash"></div>
                </div>
                <div class="action-name">Xóa tất cả</div>
              </div>
              <div class="action-option-container" style="cursor: pointer">
                <div class="action-icon-box">
                  <img src="../../assets/img/threedot.png" alt="" />
                  <!-- <div class=""></div> -->
                </div>
                <!-- <div class="action-name"></div> -->
              </div>
            </div>
          </div>
          <div class="flex">
            <div class="search-box">
              <input
                type="text"
                id="search-emp-input"
                placeholder="Tìm kiếm theo mã, tên nhân viên"
                class="input-text"
                autofocus
              />
              <div class="icon-box" id="search-emp-btn">
                <div class="icons search-icon"></div>
              </div>
            </div>
            <div class="emptable--icon flex-center">
              <div class="icons icon__exportexcel"></div>
            </div>
            <div class="emptable--icon flex-center">
              <div class="icons icon__reload"></div>
            </div>
          </div>
        </div>
        <!-- End search employee -->

        <!--BEGIN: Bảng thông tin nhân viên -->
        <table
          class="emp-table"
          :class="{ emptable__disabled: isSelectAllEmp }"
        >
          <tr>
            <th>
              <!-- <input type="checkbox" class="emp-checkbox" /> -->
              <!-- <MInputCheckbox @click="selectAllEmp" :checked="isEmpBatchExe"></MInputCheckbox> -->
              <div class="minput-checkbox-container">
                <div class="checkbox-pseudobox">
                  <div class="icons" :class="checkAllEmpClass"></div>
                </div>
                <div class="checkbox-box">
                  <input
                    type="checkbox"
                    id="check"
                    ref="checkboxAllEmp"
                    @click="selectAllEmp"
                  />
                </div>
              </div>
            </th>
            <th style="min-width: 150px">Mã nhân viên</th>
            <th style="min-width: 200px">Tên nhân viên</th>
            <th>Ngày sinh</th>
            <th>Giới tính</th>
            <th>Số điện thoại</th>
            <th style="min-width: 150px">Chức danh</th>
            <th>Mã đơn vị</th>
            <th>Tên đơn vị</th>
            <!-- <th>Chi nhánh TK ngân hàng</th> -->
            <th>Chức năng</th>
          </tr>
          <tbody class="emp-tbody">
            <tr
              class="emp-tr"
              v-for="(emp, index) in empDataShow"
              :key="emp.empCode"
              @dblclick="showChangeEmpDetail(emp, index)"
            >
              <td>
                <!-- <input type="checkbox" class="emp-checkbox" /> -->
                <MInputCheckbox
                  :checked="isEmpSelected[index]"
                  @click="selectOneEmp(index)"
                ></MInputCheckbox>
              </td>
              <td>{{ emp.EmployeeCode }}</td>
              <td>{{ emp.FullName }}</td>
              <td>{{ this.$helper.formatDate(emp.DateOfBirth) }}</td>
              <td>
                {{ this.$baseEnum.Gender[this.$languageCode][emp.Gender] }}
              </td>
              <td>{{ emp.PhoneNumber }}</td>
              <td>{{ emp.PositionName }}</td>
              <td>{{ emp.DepartmentCode }}</td>
              <td>{{ emp.DepartmentName }}</td>
              <td>
                <div class="emp-func-container flex-center">
                  <div class="emp-change">Sửa</div>
                  <div
                    class="emp-select-func-box"
                    @click.stop="clickEmpSelectFunc(index)"
                  >
                    <div
                      class="emp-select-func flex-center"
                      :class="{
                        'border-0075c0': isShowEmpSelectFuncArray[index],
                      }"
                    >
                      <div class="icons icon-angle-down"></div>
                      <div
                        class="emp-drop-list"
                        v-show="isShowEmpSelectFuncArray[index]"
                      >
                        <div class="emp-drop-item">
                          <a href="">Nhân bản</a>
                        </div>
                        <div
                          class="emp-drop-item"
                          @click="deleteEmpById(emp.EmployeeId)"
                        >
                          <a href="">Xóa</a>
                        </div>
                        <div class="emp-drop-item">
                          <a href="">Ngừng sử dụng</a>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>

        <div class="emp-footer">
          <div class="emp-total">
            Tổng số:
            <strong class="emp-nums" style="font-weight: bold">
              {{ allEmpDatas.length }}
            </strong>
            bản ghi
          </div>

          <div class="pagination-container">
            <div class="num-perpage-container">
              <div class="num-perpage">
                Số bản ghi/trang:
                <strong>{{ numEmpsDataShow }}</strong>
              </div>
              <div class="num-perpage-icon">
                <div class="icons num-perpage-angledown"></div>
              </div>
            </div>

            <div class="index-emp-container">
              <strong>{{ firstEmpIndex }} - {{ lastEmpIndex }}</strong>
              bản ghi
            </div>

            <div class="change-page-container">
              <div class="prev-page">
                <div class="icons prev-page-angle-blur"></div>
              </div>
              <div class="next-page">
                <div class="icons next-page-angle-right"></div>
              </div>
            </div>
          </div>
        </div>
        <!-- END: Bảng thông tin nhân viên -->
      </div>
    </div>
    <EmployeeDetail
      v-if="isShowEmpDetail"
      :onCloseEmpDetail="hideEmpDetail"
      :type="empFormType"
      :empDetail="empDetail"
      :employeeBaseUrlApi="employeeBaseUrlApi"
    ></EmployeeDetail>
  </aside>
</template>
<script>
import EmployeeListHandler from "./employeelisthandler";
import EmployeeDetail from "./EmployeeDetail.vue";
import EmployeeApi from "./employeeapi";
// import axios from "axios";

export default {
  name: "EmployeeList",
  methods: {
    hideEmpDetail: EmployeeListHandler.hideEmpDetail,
    showEmpDetail: EmployeeListHandler.showEmpDetail,
    showChangeEmpDetail: EmployeeListHandler.showChangeEmpDetail,
    clickEmpSelectFunc: EmployeeListHandler.clickEmpSelectFunc,
    hideEmpSelectFunc: EmployeeListHandler.hideEmpSelectFunc,
    clickEmpPerPageList: EmployeeListHandler.clickEmpPerPageList,
    deleteEmpById: EmployeeListHandler.deleteEmpById,
    selectAllEmp: EmployeeListHandler.selectAllEmp,
    selectOneEmp: EmployeeListHandler.selectOneEmp,
    changeNumEmpsDataShowed: EmployeeListHandler.changeNumEmpsDataShowed,
    unselectedAllEmp: EmployeeListHandler.unselectedAllEmp,
    deleteAllEmpSelected: EmployeeListHandler.deleteAllEmpSelected,
    closeDialog: EmployeeListHandler.closeDialog,
    getAllEmployees: EmployeeApi.getAllEmployees,
  },
  data() {
    return {
      checkAllEmpClass: false,
      isShowNotificationDialog: false,
      isShowAcceptDialog: false,
      isLoading: false,
      isShowEmpDetail: false,
      isShowEmpPerPageList: false,
      isShowDialog: false,
      allEmpDatas: {},
      empDataShow: {},
      numEmpsDataShow: 10,
      departmentInformation: "",
      // Mảng lưu trữ trạng thái ẩn hiện của các dropdown list trong mục chức năng trong bảng
      isShowEmpSelectFuncArray: {},
      empSelectFuncId: -1,
      empDetail: {},
      empFormType: "",
      isEmpBatchExe: false, // Đánh dấu thực hiện hàng loạt
      isEmpSelected: {}, // Đánh dấu nhân viên nào được chọn
      isSelectAllEmp: false,
      numEmpSelected: 0,
      isSelectAllEmpCheckboxChecked: false,
      indexEmpToChange: -1, // index của nhân viên được sửa đổi để binding dữ liệu
      dialogContent: {
        Title: "",
        Message: "",
        Icon: "",
      },

      firstEmpIndex: 1,
      lastEmpIndex: 10,
      formStatus: 0,
      employeeBaseUrlApi: "https://cukcuk.manhnv.net/api/v1/Employees",
      departmenBaseUrlApi: "https://cukcuk.manhnv.net/api/v1/Departments",
    };
  },

  components: {
    EmployeeDetail,
  },

  /**
   * Author: Minh Đăng 25/08/2023
   * Feat: Điều chỉnh thẻ input checkbox hợp lí khi click, disabled bảng khi chọn tất cả nhân viên
   */
  updated() {
    if (this.numEmpSelected >= 2) {
      this.$refs.checkboxAllEmp.checked = true;
    } else if (this.numEmpSelected <= 1) {
      this.$refs.checkboxAllEmp.checked = false;
    }
  },
  /**
   * Author: Minh Đăng 27/8/2023
   * Feat: Lấy dữ liệu nhân viên từ api
   */
  async created() {
    await this.getAllEmployees();
    this.$emitter.on("onSaveOneEmployeeSuccess", this.getAllEmployees);
  },
  beforeUnmount() {
    this.$emitter.off("onSaveOneEmployeeSuccess");
  },
};
</script>
<style>
.icon__trash {
  background-position: -464px -313px;
  width: 15px;
  height: 15px;
}
</style>
