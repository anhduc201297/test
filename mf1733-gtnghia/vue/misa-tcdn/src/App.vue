<template>
  <div class="root">
    <TheHeader @toggleSidebar="toggleSidebar"></TheHeader>
    <TheSidebar :isShowSidebar="isShowSidebar"></TheSidebar>
    <TheMainContent :isShowSidebar="isShowSidebar"></TheMainContent>
    <m-toast-msg :toastInput="toasts" />
    <m-dialog v-if="isShowDialog" :dialog="dialogMsg" />
  </div>
</template>

<script>
import TheHeader from "./components/layouts/header/TheHeader.vue";
import TheMainContent from "./components/layouts/maincontent/TheMainContent.vue";
import TheSidebar from "./components/layouts/sidebar/TheSidebar.vue";
export default {
  name: "App",
  components: {
    TheSidebar,
    TheHeader,
    TheMainContent,
  },
  props: [],
  data() {
    return {
      toasts: null,
      isShowSidebar: true,
      isShowDialog: false,
      dialogMsg: null,
    };
  },
  created() {
    this.$emitter.on("addToast", this.addToast);
    this.$emitter.on("showDialog", this.showDialog);
    this.$emitter.on("closeDialog", this.closeDialog);
  },
  beforeUnmount() {
    this.$emitter.off("addToast", this.addToast);
    this.$emitter.off("showDialog", this.showDialog);
    this.$emitter.off("closeDialog", this.closeDialog);
  },
  methods: {
    /**
     * Add new Toast
     * Created by: mf1733-gtnghia - 30/8/2023
     */
    addToast(status, msg) {
      this.toasts = {
        status: status, // Number
        message: msg, // String
      };
    },
    /**
     * show Dialog
     * Created by: mf1733-gtnghia - 31/8/2023
     */
    showDialog(status, title, message, type, onAccept) {
      this.dialogMsg = {
        status,
        title,
        message,
        type,
        onAccept,
        acceptTitle: "Xác nhận",
        cancelTitle: "Hủy",
      };
      console.log(this.dialogMsg);
      this.isShowDialog = true;
    },
    /**
     * close Dialog
     * Created by: mf1733-gtnghia - 9/1/2023
     */
    closeDialog() {
      this.isShowDialog = false;
    },
    /**
     * On/off Sidebar
     * Created by: mf1733-gtnghia - 21/8/2023
     */
    toggleSidebar() {
      this.isShowSidebar = !this.isShowSidebar;
    },
  },
  computed: {},
};
</script>

<style>
@import url(./css/main.css);
</style>
