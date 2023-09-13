<template>
  <!-- Ngoài ra, có thể thêm class: .m-btn-secondary -->
  <button class="m-btn">
    <div :class="classIcon" class="mi mi-16" v-if="hasIcon"></div
    ><slot></slot>
  </button>
</template>
<style scoped>
@import url(./button.css);
</style>
<script>
export default {
  name: "MISAButton",
  props: {
    hasIcon: {
      type: Boolean,
      default: false,
      required: false,
    },
    classIcon: {
      type: String,
      default: "",
      required: false,
    },
  },
};
</script>
