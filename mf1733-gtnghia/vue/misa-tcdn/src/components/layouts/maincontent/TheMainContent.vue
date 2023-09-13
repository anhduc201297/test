<template>
  <div class="main" :class="{expand: !isShowSidebar}">
    <router-view></router-view>
    <!-- <m-modal
      v-if="isShowModalComponent"
      @onCloseModal="onCloseModalComponent"
      :title="'Misa Modal Template'"
    >
      <template v-slot:modal__header--end>
        <m-button :title="'Chức năng'" />
      </template>
      <template v-slot:modal__body>
        <div class="row">
          <div class="col">
            <div class="row">
              <m-input :label="'Địa chỉ'" :name="'address'" />
            </div>
            <div class="row">
              <div class="col-3 mr-8">
                <m-input
                  :label="'ĐT di động'"
                  :name="'mobile'"
                  :tooltips="'Điện thoại di động'"
                  :isRequired="true"
                />
              </div>
              <div class="col-3 mr-8">
                <m-input
                  :label="'ĐT cố định'"
                  :name="'landline'"
                  :tooltips="'Điện thoại cố định'"
                  :isRequired="true"
                />
              </div>
              <div class="col-3">
                <m-input
                  :isRequired="true"
                  :label="'Email'"
                  :type="'email'"
                  :placeholder="'nghia2905.per@gmail.com'"
                ></m-input>
              </div>
            </div>
          </div>
        </div>
      </template>
      <template v-slot:modal__footer--start>
        <m-button :type="'extra'" :title="'Hủy'" />
      </template>
      <template v-slot:modal__footer--end>
        <m-button :title="'Xác nhận'" />
      </template>
    </m-modal>
    <m-popup
      v-if="isShowPopup"
      @onClose="onClosePopup"
      :title="'Dữ liệu đã bị thay đổi'"
      :message="'Bạn có muốn lưu dữ liệu không?'"
    >
      <template v-slot:popup__footer>
        <m-button :type="'extra'" :title="'Đóng'" class="mr-8" />
        <m-button
          :type="'extra'"
          :title="'Không Lưu'"
          class="btn-dangerous mr-8"
        />
        <m-button :title="'Lưu'" class="mr-8" />
      </template>
    </m-popup> -->
  </div>

</template>

<script>
export default {
  name: "TheMainContent",
  props: {
    isShowSidebar: Boolean,
  },
  data() {
    return {
      dialog: null,
      isShowContextMenu: false,
      isContextMenuVisible: {},
      contextItems: [
        {
          className: "duplicate",
          label: "Nhân bản",
          icon: "",
        },
        {
          className: "delete",
          label: "Xóa",
          icon: "",
        },
        {
          className: "disable",
          label: "Nhưng sử dụng",
          icon: "",
        },
      ],
    };
  },
  created() {
    
  },
  methods: {
    /* 
      Hàm hiển thị Modal CustomerDetailForm
      Người viết: mf1733-gtnghia Ngày 20/8/2023
    */
    onShowCustomerDetailForm() {
      this.isShowCustomerDetailForm = true;
    },
    /* 
      Hàm tắt Modal CustomerDetailForm
      Người viết: mf1733-gtnghia Ngày 20/8/2023
    */
    onCloseCustomerDetailForm() {
      this.isShowCustomerDetailForm = false;
    },
    /* 
      Hàm thêm Dialog bỏ chọn nhân viên
      Người viết: mf1733-gtnghia Ngày 21/8/2023 
    */
    addSuccessUncheckedDialog() {
      this.dialog = {
        dialogMode: this.$MISAEnum.DialogStatus.success,
      };
    },
  },
  watch: {},
};
</script>

<style scoped>
@import url(./content.css);
</style>
