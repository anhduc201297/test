<template>
  <div class="toast__group">
    <MToastMessage
      v-for="toastMessageContent in toastMessageContents"
      :key="toastMessageContent.Title"
      :toastMesContent="toastMessageContent"
    ></MToastMessage>
    <!-- <m-toast-message
      :toastMesContent="
        this.$resource[this.$languageCode].ToastMessageContent.EmployeeForm
          .AddSuccess
      "
    ></m-toast-message> -->
  </div>
</template>

<script>
import MToastMessage from "./MToastMessage.vue";
export default {
  components: { MToastMessage },
  props: ["toastMessageContents"],
  watch: {
    toastMessageContents() {
      console.log(this.toastMessageContents);
    },
  },
};
</script>
<style>
.toast__group {
  position: fixed;
  right: 20px;
  bottom: 10px;
  width: 450px;
  min-height: 56px;
  display: flex;
  flex-direction: column;

  z-index: 999;
  /* background-color: #ccc; */
}
</style>
