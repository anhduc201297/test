<template>
  <m-modal
    :title="'Thông tin khách hàng'"
    :titleSubmitBtn="'Cất'"
    :titleSubmitAndNewBtn="'Cất và Thêm mới'"
    :method="'POST'"
    :data="customer"
    :URL="'https://cukcuk.manhnv.net/api/v1/customers'"
    :msgSuccess="this.$MISAResource.VN.CreateCustomerSuccess"
    @onSubmitAndNewForm="onClickStoreAndNew"
    @onCloseForm="onCloseForm"
    @onResetForm="onResetForm"
  >
    <template #modal__body>
      <div class="row">
        <div class="col-2 mr-24">
          <div class="row">
            <div class="col-2/5 mr-8">
              <m-input
                :label="'Mã'"
                :name="'customerCode'"
                v-model="customer.customerCode"
                :isRequired="true"
              ></m-input>
            </div>
            <div class="col-3/5">
              <m-input
                :label="'Họ và tên'"
                :name="'fullname'"
                v-model="customer.fullname"
                :isRequired="true"
              ></m-input>
            </div>
          </div>
          <div class="row">
            <div class="input-container">
              <div class="label-box">
                <label name="department" for="department">Đơn vị</label>
              </div>
              <m-combobox
                :url="'https://cukcuk.manhnv.net/api/v1/CustomerGroupss'"
                :propText="'CustomerGroupName'"
                :propValue="'CustomerGroupId'"
                :size="'small'"
              />
            </div>
          </div>
          <div class="row">
            <m-input
              :label="'Số dư'"
              :name="'debitAmount'"
              v-model="customer.debitAmount"
            />
          </div>
        </div>
        <div class="col-2">
          <div class="row">
            <div class="col-2/5 mr-8">
              <m-input
                :label="'Ngày sinh'"
                :name="'birth'"
                :type="'date'"
                :v-model="customer.dateOfBirth"
              />
            </div>
            <div class="col-3/5">
              <m-radio
                :name="'gender'"
                :label="'Giới tính'"
                :options="genderOptions"
                v-model="customer.gender"
              />
            </div>
          </div>
          <div class="row">
            <div class="col-3/5 mr-8">
              <m-input
                v-model="customer.identity"
                :label="'Số CCCD/CMTND'"
                :name="'identity'"
                :tooltips="'Số Căn Cước Công Dân/Chứng Minh Thư Nhân Dân'"
              />
            </div>
            <div class="col-2/5">
              <m-input
                v-model="customer.licenseDate"
                :label="'Ngày cấp'"
                :name="'lisence-date'"
                :type="'date'"
              />
            </div>
          </div>
          <div class="row">
            <m-input
              v-model="customer.licensePlace"
              :label="'Nơi cấp'"
              :name="'license-place'"
            />
          </div>
        </div>
      </div>
      <div class="row">
        <div class="col">
          <div class="row">
            <m-input
              :label="'Công ty'"
              :name="'company'"
              v-model="customer.companyName"
            />
          </div>
          <div class="row">
            <m-input
              :label="'Địa chỉ'"
              :name="'address'"
              v-model="customer.address"
            />
          </div>
          <div class="row">
            <div class="col-2 mr-8">
              <m-input
                v-model="customer.phoneNumber"
                :label="'ĐT di động'"
                :name="'mobile'"
                :tooltips="'Điện thoại di động'"
                :isRequired="true"
              />
            </div>
            <div class="col-2">
              <m-input
                v-model="customer.email"
                :label="'Email'"
                :type="'email'"
                :placeholder="'nghia2905.per@gmail.com'"
              ></m-input>
            </div>
          </div>
        </div>
      </div>
    </template>
    <template #modal__footer--end> </template>
  </m-modal>
</template>

<script>
export default {
  name: "CustomerDetail",
  props: [],
  data() {
    return {
      genderOptions: [
        {
          value: this.$MISAEnum.Gender.male,
          label: "Nam",
        },
        {
          value: this.$MISAEnum.Gender.female,
          label: "Nữ",
        },
        {
          value: this.$MISAEnum.Gender.other,
          label: "Khác",
        },
      ],
      customer: {
        customerCode: "",
        fullname: "",
        department: "",
        jobTitle: "",
        gender: "",
        dateOfBirth: "",
        identity: "",
        licenseDate: "",
        licensePlace: "",
        phoneNumber: "",
        email: "",
        debitAmount: "",
        companyName: "",
        address: "",
      },
    };
  },
  methods: {
    /**
     * On Reset Form input button
     * Created by: mf1733-gtnghia - 21/8/2023
     */
    onResetForm() {
      const inputBox = Array.from(document.querySelectorAll(".input-box"));
      // Reset in input field
      inputBox.forEach((e) => {
        e.firstElementChild.value = "";
      });
      // Reset in object
      Object.keys(this.customer).forEach((key) => {
        this.customer[key] = "";
      });
    },
    /**
     * On close form button - redirect to Customer List
     * Created by: mf1733-gtnghia - 21/8/2023
     */
    onCloseForm() {
      this.$router.push("/customer/list");
    },
    /**
     * On click Store and Insert button
     * Redirect to Customer List
     * Created by: mf1733-gtnghia - 21/8/2023
     */
    onClickStoreAndNew() {
      console.log("on click store and new");
      this.onResetForm();
    },
  },
};
</script>

<style scoped>
@import url(../../components/base/modal/modal.css);
</style>
