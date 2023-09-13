<template>
  <m-icon
    v-if="spinnerType == 'normal'"
    :boxIconType="'box-icon-spinner'"
    :iconType="'icon-spinner_normal'"
  ></m-icon>
  <m-icon
    v-else-if="spinnerType == 'small'"
    :boxIconType="'box-icon-spinner'"
    :iconType="'icon-spinner_small'"
  ></m-icon>
</template>

<script>
export default {
  name: "MISALoading",
  props: ["spinnerType"],
};
</script>

<style scoped>
@import url(./MISALoading.css);
</style>