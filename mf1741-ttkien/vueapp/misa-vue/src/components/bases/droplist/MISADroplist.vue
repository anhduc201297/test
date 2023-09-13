<template>
  <div class="droplist" :class="droplistType" v-show="showDropList === id">
    <div class="droplist-item" v-for="(item, index) in items" :key="index">
      {{ item }}
    </div>
  </div>
</template>

<script>
export default {
  name: "MISADroplist",
  props: ["droplistType", "items", "id", "showDropList"],
};
</script>

<style scoped>
@import url(./MISADroplist.css);
</style>