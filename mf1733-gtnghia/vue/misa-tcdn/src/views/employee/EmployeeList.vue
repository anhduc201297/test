<template>
  <div class="main-container">
    <div class="title-container">
      <div class="title-container__title">
        <p class="heading1">Nhân viên</p>
      </div>
      <div class="title-container__button">
        <m-button
          :route="'employee.list.form'"
          :title="'Thêm mới nhân viên'"
          :type="'route'"
        >
        </m-button>
      </div>
    </div>
    <m-loading v-if="isLoading" />
    <m-table
      v-else
      :placeholderSeachbox="'Tìm theo mã, tên nhân viên'"
      :tableData="employees"
      :tableTitle="tableTitle"
      :tableField="tableField"
      :tableKey="'EmployeeId'"
      :API="API"
      @reloadData="fetchEmployeeData"
    >
    </m-table>
  </div>
  <router-view @reloadData="reloadData"></router-view>
</template>

<script>
export default {
  name: "EmployeeList",
  data() {
    return {
      isLoading: false,
      employees: null,
      API: {
        delete: "https://cukcuk.manhnv.net/api/v1/employees/",
      },
      tableTitle: [
        { title: "Mã nhân viên" },
        { title: "Tên nhân viên" },
        { title: "Phòng/Ban" },
        { title: "Vị trí" },
        { title: "Lương", align: "right" },
        { title: "Giới tính" },
        { title: "Ngày sinh", align: "center" },
        { title: "Số điện thoại" },
        { title: "Email" },
        { title: "Chức năng" },
      ],
      tableField: [
        { label: "EmployeeCode" },
        { label: "FullName" },
        { label: "DepartmentName" },
        { label: "PositionName" },
        { label: "Salary", align: "right" },
        { label: "Gender" },
        { label: "DateOfBirth", align: "center" },
        { label: "PhoneNumber" },
        { label: "Email" },
      ],
    };
  },
  created() {
    this.employees = this.fetchEmployeeData();
  },
  methods: {
    /**
     * fetch Customer Data render to Customer List
     * Created by: mf1733-gtnghia - 30/8/2023
     */
    async fetchEmployeeData() {
      this.isLoading = true;
      this.employees = await this.$sendRequest(
        "https://cukcuk.manhnv.net/api/v1/employees"
      );
      this.checkedBoxs = Array(this.employees.length).fill(false);
      this.isLoading = false;
    },
    /**
     * reload employee data to render
     * Created by: mf1733-gtnghia - 31/8/2023
     */
    reloadData() {
      this.fetchEmployeeData();
    },
    /**
     * toggle Loading icon
     * Created by: mf1733-gtnghia - 30/8/2023
     */
    toggleLoading() {
      this.$store.dispatch("updateLoading");
    },
    /**
     * filter number of checked employees
     * Created by: mf1733-gtnghia - 2/9/2023
     */
    getCheckedEmployees() {
      const checkedEmployees = this.employees.filter((employee, index) => {
        return this.checkedBoxs[index];
      });
      return checkedEmployees.length;
    },
  },
};
</script>

<style scoped>
@import url(@/components/layouts/maincontent/content.css);
</style>
