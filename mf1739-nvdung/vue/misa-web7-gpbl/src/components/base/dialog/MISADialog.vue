<template>
  <div class="container-dialog">
    <div class="dialog-background"></div>
    <div class="dialog-container">
      <div class="dialog-header">
        <div class="dialog-title">
          <slot name="title">{{ this.inputDialog.title }}</slot>
        </div>
        <!-- Nút close -->
        <div class="mi mi-24 mi-close" @click="closeDialogClick()"></div>
      </div>
      <div class="dialog-content flex-row">
        <div class="dialog-icon">
          <div
            class="mi mi-24"
            v-bind:class="iconClass"
            :class="inputDialog.iconClass"
          ></div>
        </div>
        <div class="dialog-description flex-align-center">
          <slot name="dialog-description">{{ this.inputDialog.content }}</slot>
        </div>
      </div>
      <div class="dialog-footer flex-row flex-right" style="padding: 0">
        <m-button
          class="m-btn-secondary"
          v-show="hasButtonSecondary && this.inputDialog.hasButtonSecondary"
          @click="btnCancelClick"
          ><slot name="btn-second-text">{{
            this.inputDialog.buttonSecondaryText
          }}</slot></m-button
        >
        <m-button @click="btnContinueClick"
          ><slot name="btn-continue-text">{{
            this.inputDialog.buttonContinueText
          }}</slot></m-button
        >
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MISADialog",
  emits: ["closeDialog", "confirmContinue"],
  props: {
    iconClass: {
      type: String,
      default: "mi-warning",
      required: false,
    },
    hasButtonSecondary: {
      type: Boolean,
      default: true,
      required: false,
    },
    inputDialog: {
      /* obj {title: "", content: "", iconClass: "", hasButtonSecondary: true/false, buttonSecondaryText: "", buttonContinueText: ""} */
      type: Object,
      required: false,
    },
  },
  methods: {
    /**
     * Phát sự kiện đóng dialog
     * Author: NVDUNG (20/08/2023)
     */
    closeDialogClick() {
      // Phát sự kiện đóng dialog
      this.$emitter.emit("closeDialog");
    },

    /**
     * Phát sự kiện hủy dialog
     * Author: NVDUNG (20/08/2023)
     */
    btnCancelClick() {
      this.$emitter.emit("closeDialog");
    },

    /**
     * Phát sự kiện xác nhận tiếp tục hành động'
     * Author: NVDUNG (20/08/2023)
     */
    btnContinueClick() {
      // Phát sự kiện xác nhận tiếp tục hành động
      this.$emit("confirmContinue");
      // Đóng dialog
      this.closeDialogClick();
    },
  },
};
</script>
<style scoped>
@import url(./dialog.css);
</style>
