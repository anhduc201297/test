<template>
  <div class="box-toast-message">
    <div class="toast-mess-content">
      <m-icon :boxIconType="boxIconType" :iconType="iconType"></m-icon>
      <div>
        <span v-if="boxIconType == 'box-icon-success'" class="success">
          {{ this.$MISAResource["VN"].toastMessage.success }}</span
        >
        <span v-else-if="boxIconType == 'box-icon-error'" class="error">{{
          this.$MISAResource["VN"].toastMessage.error
        }}</span>
        <span v-else-if="boxIconType == 'box-icon-warning'" class="warning">{{
          this.$MISAResource["VN"].toastMessage.warning
        }}</span>
        <span v-else-if="boxIconType == 'box-icon-info'" class="info">{{
          this.$MISAResource["VN"].toastMessage.information
        }}</span>
        {{ toastMsgContent }}
      </div>
    </div>
    <div class="toast-mess-action">
      <div class="action-undo" v-if="boxIconType == 'box-icon-success'">
        {{ this.$MISAResource["VN"].toastMessage.undo }}
      </div>
      <m-icon
        :boxIconType="'box-icon-x_M'"
        :iconType="'icon-x_M'"
        @click="onHideToastMessage"
      ></m-icon>
    </div>
  </div>
</template>

<script>
export default {
  name: "MISAToastMessage",
  props: ["boxIconType", "iconType", "closeFunction", "toastMsgContent"],
  data() {
    return {};
  },
  methods: {
    /**
     * author: ttkien
     * 24/08/2023
     * ẩn toast message
     */
    onHideToastMessage() {
      this.closeFunction();
    },
  },
};
</script>

<style scoped>
@import url(./MISAToastMessage.css);
</style>