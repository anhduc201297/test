<template>
  <div class="contextmenu">
    <div class="contextmenu-wrapper">
      <a v-for="(item,index) in menuItems" @click="onItemClick(index)" :key="item">{{
        item
      }}</a>
    </div>
  </div>
</template>

<script>
export default {
  name: "MisaContextMenu",
  props: {
    menuItems: {
      type: Array,
      required: true,
    },
  },
  methods: {
     onItemClick(index) {
      this.$emit("onItemClick", index);
    },
    // Đóng contextmenu
    closeContextMenu() {
      this.$emit("onClose");
    },
  },
  data() {
    return {};
  },
};
</script>
<style scoped>
.contextmenu {
  z-index: 100000000000000000000;
}
.contextmenu-wrapper a{
  color: #000;
}
.contextmenu-wrapper a:hover{
  color:#fff
}
</style>
