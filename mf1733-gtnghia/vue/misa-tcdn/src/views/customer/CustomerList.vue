<template>
  <div class="main-container">
    <div class="title-container">
      <div class="title-container__title">
        <p class="heading1">Khách hàng</p>
      </div>
      <div class="title-container__button">
        <m-button
          :route="'customer.list.form'"
          :title="'Thêm mới khách hàng'"
          :type="'route'"
        >
        </m-button>
      </div>
    </div>
    <m-loading v-if="isLoading" />
    <m-table v-else
      :placeholderSeachbox="'Tìm theo mã, tên khách hàng'"
      :tableField="tableField"
      :tableTitle="tableTitle"
      :tableData="customerData"
      :tableKey="'CustomerId'"
      :API="API"
      @reloadData="fetchCustomerData"
    >
    </m-table>
  </div>
  <router-view></router-view>
</template>

<script>
export default {
  name: "CustomerList",
  data() {
    return {
      customerData: null,
      isLoading: true,
      ld: true,
      API: {
        delete: "https://cukcuk.manhnv.net/api/v1/customers/",
      },
      tableTitle: [
        { title: "Mã khách hàng" },
        { title: "Tên khách hàng" },
        { title: "Giới tính" },
        { title: "Ngày sinh", align: "center" },
        { title: "Số điện thoại" },
        { title: "Email" },
        { title: "Công ty" },
        { title: "Số dư", align: "right" },
        { title: "Chức năng" },
      ],
      tableField: [
        { label: "CustomerCode" },
        { label: "FullName" },
        { label: "Gender" },
        { label: "DateOfBirth", align: "center" },
        { label: "PhoneNumber" },
        { label: "Email" },
        { label: "CompanyName" },
        { label: "DebitAmount", align: "right" },
      ],
    };
  },
  created() {
    this.fetchCustomerData();
  },
  computed: {},
  methods: {
    /**
     * Redirect to "/customer/form" and re-render CustomerList
     * Created by: mf1733-gtnghia - 30/8/2023
     */
    onShowCustomerForm() {
      this.$router.push("/customer/form");
      this.fetchCustomerData();
    },
    /**
     * fetch Customer Data render to Customer List
     * Created by: mf1733-gtnghia - 30/8/2023
     */
    async fetchCustomerData() {
      this.isLoading = true;
      this.customerData = await this.$sendRequest(
        "https://cukcuk.manhnv.net/api/v1/customers"
      );
      this.isLoading = false;
    },
    /**
     * toggle Loading icon
     * Created by: mf1733-gtnghia - 30/8/2023
     */
    toggleLoading() {
      this.$store.dispatch("updateLoading");
    },
  },
  watch: {},
};
</script>

<style scoped>
@import url(@/components/layouts/maincontent/content.css);
</style>
