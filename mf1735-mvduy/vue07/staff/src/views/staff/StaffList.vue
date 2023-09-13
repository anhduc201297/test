<template>
  <div class="header-content">
    <span class="header-title">Nhân viên</span>
    <m-button @click="onShowForm"> Thêm mới nhân viên </m-button>
  </div>
  <div class="main-content">
    <div class="toolbar">
      <div v-show="rowSelected < 2"></div>
      <div class="left-toolbar" v-show="rowSelected > 1">
        <div class="row-selected">
          Đã chọn <span style="font-weight: 700">{{ rowSelected }}</span>
        </div>
        <div class="row-leaved">Bỏ chọn</div>
        <button class="btn-normal delete-row">
          <div class="icon icon-bin"></div>
          Xóa
        </button>
      </div>
      <div class="right-toolbar">
        <input
          class="d-search icon-search"
          type="text"
          name=""
          id=""
          placeholder="Tìm theo mã, tên nhân viên"
        />
        <div class="icon icon-refresh" title="Lấy lại dữ liệu"></div>
        <div class="icon icon-excel" title="Xuất ra excel"></div>
        <div class="icon icon-setting" title="Tùy chỉnh giao diện"></div>
      </div>
    </div>

    <div class="table-content">
      <table class="d-table">
        <thead>
          <tr>
            <th class="text-center" style="width: 20px">
              <input
                class="d-checkbox icon"
                :class="{ 'icon-checked': checkedAll }"
                type="checkbox"
                v-model="checkedAll"
                @change="checkedAllCheckbox"
              />
            </th>
            <th class="text-left" style="width: 140px">MÃ NHÂN VIÊN</th>
            <th class="text-left" style="min-width: 150px">TÊN NHÂN VIÊN</th>
            <th class="text-left" style="width: 100px">GIỚI TÍNH</th>
            <th class="text-center">NĂM SINH</th>
            <th class="text-left" title="Số chứng minh nhân dân">SỐ CMND</th>
            <th class="text-left">CHỨC DANH</th>
            <th class="text-left" style="min-width: ">TÊN ĐƠN VỊ</th>
            <th class="text-left">SỐ TÀI KHOẢN</th>
            <!-- <th class="text-left">TÊN NGÂN HÀNG</th>
              <th class="text-left">CHI NHÁNH TK NGÂN HÀNG</th> -->
            <th class="text-center sticky-column" style="width: 120px">
              CHỨC NĂNG
            </th>
          </tr>
        </thead>
        <tbody>
          <tr
            :class="{ 'row-hover': checked[index] }"
            v-for="(staff, index) in staffs"
            :key="staff.CustomerID"
          >
            <td class="text-center" style="width: 20px">
              <input
                class="d-checkbox icon"
                :class="{ 'icon-checked': checked[index] }"
                type="checkbox"
                v-model="checked[index]"
                @change="
                  checked[index] ? this.rowSelected++ : this.rowSelected--
                "
              />
            </td>
            <td class="text-left" style="width: 70px">
              {{ staff.EmployeeCode }}
            </td>
            <td class="text-left">{{ staff.FullName }}</td>
            <td class="text-left" style="width: 40px">
              {{ $commonJS.formatGender(staff.Gender) }}
            </td>
            <td class="text-center">
              {{ $commonJS.formatDate(staff.DateOfBirth) || "" }}
            </td>
            <td class="text-left">123456789</td>
            <td class="text-left">Fresher</td>
            <td class="text-left">{{ staff.DepartmentName || "" }}</td>
            <td class="text-left">{{ staff.DebitAmount || "" }}</td>
            <!-- <td class="text-left"></td>
              <td class="text-left"></td> -->

            <td class="text-center sticky-column" style="width: 50px">
              Sửa
              <div class="down-select">
                <div class="down-select-button" @click="onShowSelect">
                  <div class="icon icon-triangle-down"></div>
                </div>
                <div class="select-content">
                  <a>Nhân bản</a>
                  <a @click="onShowDialog(staff.EmployeeId)">Xóa</a>
                  <a>Ngừng sử dụng</a>
                </div>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <div class="d-paging">
      <div class="paging-left">
        Tổng số:
        <span style="font-family: MISA bold">1035</span>
        bản ghi
      </div>
      <div class="paging-right">
        <div class="paging-select">
          <div class="icon icon-arrow-select"></div>
          <select class="d-select">
            <option value="10">10 bản ghi trên 1 trang</option>
            <option value="20">20 bản ghi trên 1 trang</option>
            <option value="30">30 bản ghi trên 1 trang</option>
            <option value="50">50 bản ghi trên 1 trang</option>
            <option value="100">100 bản ghi trên 1 trang</option>
          </select>
        </div>
        <div class="paging-number">
          <button class="btn-paging-prev">Trước</button>
          <div class="page-number-group">
            <button class="page-number" id="first">1</button>
            <button class="page-number">2</button>
            <button class="page-number">3</button>
            <span style="margin-left: 5px">...</span>
            <button class="page-number">52</button>
          </div>
          <button class="btn-paging-next">Sau</button>
        </div>
      </div>
    </div>
  </div>
  <m-dialog
    v-if="showDialog"
    :content="this.$resourceJS['VN'].WantToDelete"
    title="Xóa người dùng"
    :type="this.$enumJS.Toast.Question"
    :confirmAction="deleteStaff"
    @onClose="onCloseDialog"
  ></m-dialog>

  <StaffForm
    :showForm="isShowForm"
    :onCloseForm="onCloseForm"
    @onShowToast="onShowToast"
  ></StaffForm>
  <m-toast
    v-if="showToast"
    :type="this.$enumJS.Toast.Success"
    :title="this.$resourceJS['VN'].Success"
    content=""
  ></m-toast>
</template>

<script>
import StaffForm from "@/views/staff/StaffForm.vue";

export default {
  name: "TheContent",
  components: { StaffForm },
  created() {
    this.getData();
  },
  methods: {
    async getData() {
      this.$emitter.emit("onShowLoading");
      /*
       * Lấy dữ liệu
       * Author: mf1735-duy (24/08/2023)
       */
      await this.$axios
        .get("https://cukcuk.manhnv.net/api/v1/Employees")
        .then((res) => {
          this.staffs = res.data;
        })
        .catch((err) => {
          console.log(err);
        });
      this.$emitter.emit("onShowLoading");
    },

    /*
     * Click mở Dialog
     * Author: mf1735-duy (20/08/2023)
     */
    onShowDialog(id) {
      this.showDialog = true;
      this.staffId = id;
    },
    /*
     * Click đóng Dialog
     * Author: mf1735-duy (20/08/2023)
     */
    onCloseDialog() {
      this.showDialog = false;
    },

    /*
     * Click mở form
     * Author: mf1735-duy (20/08/2023)
     */
    onShowForm() {
      this.$emitter.emit("onTest");
      this.isShowForm = true;
    },
    /*
     * Click đóng form, bật toast trong 3s
     * Author: mf1735-duy (20/08/2023)
     */
    onCloseForm() {
      this.isShowForm = false;
    },

    /*
     * Click bật toast trong 3s
     * Author: mf1735-duy (24/08/2023)
     */
    onShowToast() {
      this.showToast = true;
      setTimeout(() => {
        this.showToast = false;
      }, 3000);
    },
    /*
     * Gọi đến sự kiện onShowDialog trong TheContent.vue
     * Author: mf1735-duy (20/08/2023)
     */

    checkedAllCheckbox() {
      for (let i = 0; i < this.staffs.length; i++) {
        this.checked[i] = this.checkedAll;
        this.checked[i] ? this.rowSelected++ : this.rowSelected--;
      }
    },
    /*
     * Gọi đến sự kiện onShowDialog trong TheContent.vue
     * Author: mf1735-duy (20/08/2023)
     */
    onShowSelect() {
      this.showSelect = !this.showSelect;
    },
    /*
     * Xóa dữ liệu
     * Author: mf1735-duy (31/08/2023)
     */
    deleteStaff() {
      // console.log(id);
      try {
        this.$axios
          .delete(`https://cukcuk.manhnv.net/api/v1/Employees/${this.staffId}`)
          .then((res) => {
            console.log(res);
            // console.log(1);
            this.onShowToast();
            this.getData();
            this.onCloseDialog();
          });
      } catch (error) {
        console.log(error);
      }
    },
  },
  data() {
    return {
      showDialog: false,
      isShowForm: false,
      showToast: false,
      checkedAll: false,
      checked: [],
      staffs: [],
      rowSelected: 0,
      showSelect: false,
      staffId: "",
    };
  },
};
</script>

<style scoped>
@import url(../../css/layout/content.css);
@import url(../../components/base/table/table.css);
</style>