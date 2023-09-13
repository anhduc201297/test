<template>
  <div class="m-context-menu">
    <a v-for="item in items" :key="item"
    @click="this.$emit('contextMenuItemClick', item)">{{ item }}</a>
  </div>
</template>
<script>
export default {
  name: "MISAContextMenu",
  props: {
    items: {
      type: Array,
      required: true,
    },
  },
};
</script>
<style scoped>
@import url(./context-menu.css);
</style>
