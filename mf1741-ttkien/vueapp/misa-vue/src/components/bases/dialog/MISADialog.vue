<template>
  <!-- Thông báo -->
  <div class="notif-view">
    <div class="notif-window">
      <div class="box-notif-title">
        <div class="notif-title">
          {{ this.$MISAResource["VN"].dialog.dataExist }}
        </div>
        <m-icon
          :boxIconType="'box-icon-x_M'"
          :iconType="'icon-x_M'"
          @click="closeDialog"
        ></m-icon>
      </div>
      <div class="notif-describe">
        <m-icon
          :boxIconType="'box-icon-warning'"
          :iconType="'icon-warning'"
        ></m-icon>
        <div class="notif-contVNt">
          {{ this.$MISAResource["VN"].dialog.employeeCodeExist }}
        </div>
      </div>
      <div class="notif-btn">
        <m-button
          class="minor-button"
          :title="this.$MISAResource['VN'].button.cancel"
        ></m-button>
        <m-button
          class="btn-agree main-button"
          :title="this.$MISAResource['VN'].button.agree"
        ></m-button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MISADialog",
  props: ["closeFunction"],
  methods: {
    closeDialog() {
      this.closeFunction();
    },
  },
};
</script>

<style>
@import url(./MISADialog.css);
</style>