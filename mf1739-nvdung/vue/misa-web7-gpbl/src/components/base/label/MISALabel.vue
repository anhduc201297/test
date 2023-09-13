<template>
  <label v-bind:title="tooltip">
    <slot></slot>
    <b v-show="lblRequired">&nbsp;*</b>
  </label>
</template>

<script>
export default {
  name: "MISALabel",
  props: {
    lblRequired: {
      type: Boolean,
      default: true,
      required: false,
    },
    tooltip: {
      type: String,
      default: "",
      required: false,
    },
  },
};
</script>
<style scoped>
@import url(./label.css);
</style>
