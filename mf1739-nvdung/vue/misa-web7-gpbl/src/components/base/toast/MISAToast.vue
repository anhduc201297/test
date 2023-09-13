<template>
  <div class="toast-block">
    <div class="toast" :class="'toast-' + typeToast">
      <div :class="'mi mi-24 mi-' + typeToast"></div>
      <div class="toast-text">
        <!-- Title -->
        <span class="toast-title">{{ title }}</span>
        <!-- Description -->
        <span class="toast-message">{{ message }}</span>
      </div>
      <!-- Nút hoàn tác không bắt buộc -->
      <a v-if="showButton" class="toast-button">Hoàn tác</a>
      <div @click="closeToast" class="toast-close mi mi-24 mi-close"></div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MISAToast",
  data() {
    return {};
  },
  props: {
    /* Các giá trị: success, error, warning, info */
    typeToast: {
      type: String,
      required: true,
    },
    showButton: {
      type: Boolean,
      default: true,
      required: false,
    },
    title: {
      type: String,
      required: true,
    },
    message: {
      type: String,
      required: true,
    },
  },
  methods: {
    /**
     * Phát sự kiện đóng Toast Message đến component cha
     * Author: NVDUNG (20/08/2023)
     */
    closeToast() {
      this.$emit("closeToast");
    },
  },
  mounted() {
    /**
     * Phát sự kiện đóng Toast Message đến component cha sau 5 giây
     * Author: NVDUNG (20/08/2023)
     */
    setTimeout(this.closeToast, 5000);
  },
};
</script>
<style scoped>
@import url(./toast.css);
</style>
