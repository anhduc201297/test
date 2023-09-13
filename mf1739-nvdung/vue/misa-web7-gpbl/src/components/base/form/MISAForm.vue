<template>
  <div class="m-form---content">
    <header class="m-form--header">
      <div class="m-form--title">
        <div class="title-popup">{{ formTitle }}</div>
        <!-- Đặt thêm checkbox -->
        <slot name="header"></slot>
      </div>
      <div class="m-form-close">
        <div class="mi mi-24 mi-help" title="Trợ giúp"></div>
        <div class="mi mi-24 mi-close" title="Đóng" @click="closeForm"></div>
      </div>
    </header>
    <main class="m-form-content">
      <div class="form-content scroller">
        <slot name="main"></slot>
      </div>
    </main>
    <footer>
      <slot name="footer"></slot>
    </footer>
  </div>
</template>

<script>
export default {
  name: "MISAForm",
  props: {
    formTitle: {
      type: String,
      required: true,
    },
  },
  methods: {
    /**
     * Đóng form
     * Author: NVDUNG (20/08/2023)
     */
    closeForm() {
      this.$emit("closeForm");
    },
  },
};
</script>

<style scoped>
@import url(./form.css);
</style>
