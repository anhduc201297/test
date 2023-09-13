<template>
  <div class="context-menu show">
    <div
      v-for="(item, index) in contextItems"
      :key="index"
      :class="`content-menu-${item.className}`"
      @click="handleContextMenuItem(item.className)"
      class="context-menu__row"
    >
      <button class="btn-context__item">
        <p class="row-label body2">{{ item.label }}</p>
      </button>
    </div>
  </div>
</template>

<script>
export default {
  name: "m-contextmenu",
  props: {
    contextItems: {
      type: Array,
      validator() {
        return [""];
      },
    },
  },
  data() {
    return {};
  },
  methods: {
    /* 
      Hàm xử lý context menu
      Người viết: mf1733-gtnghia
      Ngày 21/8/2023
    */
    handleContextMenuItem(action) {
      if (action === "duplicate") {
        console.log("duplicate");
      }
      else console.log("others");
    },
  },
};
</script>

<style scoped>
@import url(./contextmenu.css);
</style>
