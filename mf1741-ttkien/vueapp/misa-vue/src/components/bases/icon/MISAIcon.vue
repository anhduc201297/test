<template>
  <div class="box-icon" :class="boxIconType" :id="id">
    <div class="icon-img" :class="iconType" :id="id" :title="tooltip"></div>
  </div>
</template>

<script>
export default {
  name: "MISAIcon",
  props: ["boxIconType", "iconType", "tooltip", "id"],
};
</script>

<style scoped>
@import url(./MISAIcon.css);
</style>