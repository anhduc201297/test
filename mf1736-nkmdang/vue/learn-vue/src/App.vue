<template>
  <div class="flex">
    <TheSidebar></TheSidebar>
    <div class="right-container">
      <TheHeader></TheHeader>
      <RouterView></RouterView>
    </div>
    <MIsLoading v-if="isLoading"></MIsLoading>
    <MNotificationDialog
      v-if="isShowNotificationDialog"
      :dialogContent="dialogContent"
      :onClose="hideNotificationDialog"
    ></MNotificationDialog>
    <!-- <MToastMessage
      :toastMesContent="toastMessageContent"
      v-if="isShowToastMessage"
    ></MToastMessage> -->
    <MToastMessageGroup
      :toastMessageContents="this.toastMessageGroup.toastMessageContents"
    ></MToastMessageGroup>
  </div>
</template>

<script>
export default {
  name: "App",

  methods: {
    /**
     * Author: Minh Đăng 4/9/2023
     * Feat: Hiển thị loading
     */
    showLoading() {
      this.isLoading = true;
    },
    /**
     * Author: Minh Đăng 4/9/2023
     * Feat: Ẩn loading
     */
    hideLoading() {
      this.isLoading = false;
    },
    /**
     * Author: Minh Đăng 4/9/2023
     * Feat: Hiển thị dialog thông báo
     */
    showNotificationDialog() {
      this.isShowNotificationDialog = true;
    },
    /**
     * Author: Minh Đăng 4/9/2023
     * Feat: Ẩn dialog thông báo
     */
    hideNotificationDialog() {
      this.isShowNotificationDialog = false;
    },
    /**
     * Author: Minh Đăng 4/9/2023
     * Feat: Gán nội dung thông báo cho dialog
     */
    setDialogContent(dialogContent) {
      this.dialogContent = dialogContent;
    },
    /**
     * Author: Minh Đăng 5/9/2023
     * Feat: Hiển thị toast message
     */
    showToastMessage() {
      this.isShowToastMessage = true;
    },
    /**
     * Author: Minh Đăng 5/9/2023
     * Feat: Ẩn toast message
     */
    hideToastMessage() {
      this.isShowToastMessage = false;
    },
    /**
     * Author: Minh Đăng 5/9/2023
     * Feat: Gán nội dung toast message
     */
    setToastMessageContent(toastMessageContent) {
      this.toastMessageContent = toastMessageContent;
    },

    /**
     * Author: Minh Đăng 8/9/2023
     * Feat: Tạo mới 1 toastMessage
     * @param {Object{    
     *    Status: "",
          Desc: "",
          TextClass: "",
          OptionAction: "",
          Icon: "",}} toastMesContent 
     */
    createNewToastMessage(toastMesContent) {
      this.toastMessageGroup.Index = this.toastMessageGroup.Index + 1;
      console.log(this.toastMessageGroup.Index);
      toastMesContent.Index = this.toastMessageGroup.Index;
      this.toastMessageGroup.toastMessageContents.push(toastMesContent);
      // setTimeout(() => {
      //   this.deleteFirstToastMessage(this.toastMessageGroup.Index);
      // }, 5000);
    },

    /**
     * Author: Minh Đăng 8/9/2023
     * Feat: Xóa bỏ toast message đầu tiên
     */
    deleteFirstToastMessage(Index) {
      console.log(Index);
      this.toastMessageGroup.toastMessageContents =
        this.toastMessageGroup.toastMessageContents.filter(
          (toast) => toast.Index !== Index
        );
    },
  },
  data() {
    return {
      isLoading: false,
      isShowToastMessage: false,
      isShowNotificationDialog: false,
      dialogContent: {
        Title: "",
        Icon: "",
        Message: "",
      },
      // toastMessageContent: {},
      toastMessageGroup: {
        Index: 0,
        toastMessageContents: [],
      },
    };
  },
  created() {
    this.$emitter.on("onShowLoading", this.showLoading);
    this.$emitter.on("onHideLoading", this.hideLoading);
    this.$emitter.on("onShowNotificationDialog", this.showNotificationDialog);
    this.$emitter.on("onHideDialog", this.hideDialog);
    this.$emitter.on("setDialogContent", this.setDialogContent);
    this.$emitter.on("onShowToastMessage", this.showToastMessage);
    this.$emitter.on("onHideToastMessage", this.hideToastMessage);
    this.$emitter.on("setToastMessageContent", this.setToastMessageContent);
    this.$emitter.on("createNewToastMessage", this.createNewToastMessage);
  },
  beforeUnmount() {
    this.$emitter.off("onShowLoading");
    this.$emitter.off("onHideLoading");
    this.$emitter.off("onShowNotificationDialog");
    this.$emitter.off("onHideNotificationDialog");
    this.$emitter.off("setDialogContent");
    this.$emitter.off("onShowToastMessage");
    this.$emitter.off("onHideToastMessage");
    this.$emitter.off("setToastMessageContent");
    this.$emitter.off("createNewToastMessage");
  },
};
</script>

<style>
@import url(./css/main.css);
/* #app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
} */
</style>
