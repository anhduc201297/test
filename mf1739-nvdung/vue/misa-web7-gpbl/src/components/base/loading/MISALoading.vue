<template>
  <div class="container-loading">
    <div class="m-loading-background"></div>
    <div class="m-loading--content">
      <div class="loading" :class="{ 'loading-control': isLoadingControl }">
        <div class="background"></div>
        <div class="corner"></div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MISALoading",
  props: {
    isLoadingControl: {
      type: Boolean,
      default: false,
      required: false,
    },
  },
};
</script>
<style scoped>
@import url(./loading.css);
</style>
