<template>
  <div class="main-content">
    <!-- <router-view> </router-view> -->

    <!-- Danh sách nhân viên -->
    <EmployeeList @toastSuccess="toastSuccess"></EmployeeList>

    <!-- div toast message -->
    <div class="toast-container">
      <TransitionGroup
        ><m-toast
          v-for="toast in arrayToast"
          :key="toast.keyToast"
          :typeToast="toast.toastType"
          :showButton="toast.showButton"
          :title="toast.title"
          :message="toast.message"
          @closeToast="removeToast(toast.keyToast)"
          >{{ toast.message }}</m-toast
        ></TransitionGroup
      >
    </div>

    <!-- dialog-->
    <m-dialog v-if="showMISADialog" :inputDialog="inputDialog"></m-dialog>
    <!-- Loading -->
    <m-loading v-if="showLoading" :isLoadingControl="false"></m-loading>
  </div>
</template>
<script>
import EmployeeList from "../../../views/employee/employeeList/EmployeeList.vue";
export default {
  name: "TheMainContent",
  components: {
    EmployeeList,
  },
  data() {
    return {
      showLoading: false,

      arrayToast:
        [] /* obj {keyToast: 0, toastType: "success", showButton: true, message: ""} */,
      keyToastMessage: 0 /* Đánh dấu key đã tạo một toast */,

      showMISADialog: false,
      inputDialog: {} /* obj {title: "", content: "", iconClass: "", 
        hasButtonSecondary: true/false, buttonSecondaryText: "",
        buttonContinueText: ""} */,
    };
  },
  created() {
    /* ============= Event listener loading ============= */
    /**
     * Tạo lắng nghe sự kiện ẩn hiện loading
     * Author: NVDUNG (1/9/2023)
     */
    this.$emitter.on("showLoading", (isShow) => {
      this.showLoading = isShow;
    });

    /* ============= Event listener dialog ============= */
    /**
     * Tạo lắng nghe sự kiện hiện dialog
     * Author: NVDUNG (1/9/2023)
     */
    this.$emitter.on("showDialog", (inputObject) => {
      // Tạo đối tượng truyền dữ liệu cho dialog
      this.inputDialog = inputObject;
      // Hiện dialog
      this.showMISADialog = true;
    });

    /**
     * Tạo lắng nghe sự kiện hủy dialog
     * Author: NVDUNG (1/9/2023)
     */
    this.$emitter.on("closeDialog", () => {
      this.showMISADialog = false;
    });

    /* ============= Event listener toast ============= */
    /**
     * Tạo lắng nghe sự kiện hiện toast
     * Author: NVDUNG (1/9/2023)
     */
    this.$emitter.on("showToast", (toastObject) => {
      console.log("dữ liệu toast: ", toastObject);
      // Tạo đối tượng truyền dữ liệu cho toast
      this.addToast(toastObject);
    });
  },
  beforeUnmount() {
    // Hủy lắng nghe sự kiện showLoading
    this.$emitter.off("showLoading");
    // Hủy lắng nghe sự kiện showMISADialog
    this.$emitter.off("showDialog");
    // Hủy lắng nghe sự kiện closeMISADialog
    this.$emitter.off("closeDialog");
    // Hủy lắng nghe sự kiện showToast
    this.$emitter.off("showToast");
  },

  provide() {
    return {
      toastSuccess200: this.toastSuccess200,
      toastWarning: this.toastWarning,
      showDialogError: this.dialogError,
    };
  },

  methods: {
    /* ============= EmployeeList ============= */

    /**
     * Hiện thông báo toast thành công khi xóa nhân viên
     */
    toastSuccess(toastData) {
      // Tạo toast
      this.addToast(toastData);
    },

    /* ============= Toast message ============= */

    /**
     * Thêm toast message
     * Author: NVDUNG (20/08/2023)
     */
    addToast(toastData) {
      try {
        // Tạo đối tượng toastArrayItem
        const toastArrayItem = {
          keyToast: this.keyToastMessage,
          toastType: toastData.toastType,
          showButton: toastData.showButton,
          title: toastData.title,
          message: toastData.message,
        };
        // Thêm vào arrayToast
        this.arrayToast.push(toastArrayItem);
        // Tăng keyToastMessage
        this.keyToastMessage++;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Xóa toast message
     * Author: NVDUNG (20/08/2023)
     */
    removeToast(key) {
      try {
        // Lấy vị trí của toast cần xóa
        const pos = this.arrayToast.map((e) => e.keyToast).indexOf(key);
        // Xóa toast khỏi array
        this.arrayToast.splice(pos, 1);
      } catch (error) {
        console.log(error);
      }
    },

    /* ============= Methods xử lý api chung ============= */
    /**
     * Toast thành công 200 Ok
     * Author: NVDUNG (20/08/2023)
     */
    toastSuccess200(toastMessage) {
      // Tạo đối tượng toast
      const toastData = {
        toastType: this.$resource["vi-VN"].toast.toastTypeSuccess,
        title: this.$resource["vi-VN"].toast.titleSuccess,
        message: toastMessage,
        showButton: false,
      };
      // Tạo emitter
      this.$emitter.emit("showToast", toastData);
    },

    /**
     * Toast cảnh báo
     * Author: NVDUNG (20/08/2023)
     */
     toastWarning(toastMessage) {
      // Tạo đối tượng toast
      const toastData = {
        toastType: this.$resource["vi-VN"].toast.toastTypeWarning,
        title: this.$resource["vi-VN"].toast.titleWarning,
        message: toastMessage,
        showButton: false,
      };
      // Tạo emitter
      this.$emitter.emit("showToast", toastData);
    },

    /**
     * Dialog thông báo khi có lỗi xảy ra
     * Author: NVDUNG (20/08/2023)
     */
    dialogError(errorMessage) {
      // Tạo đối tượng dialog
      const dialogData = {
        title: "Lỗi",
        iconClass: "mi-warning",
        content: errorMessage,
        hasButtonSecondary: false,
        buttonContinueText: this.$resource["vi-VN"].button.textButtonOk,
      };
      // Tạo emitter
      this.$emitter.emit("showDialog", dialogData);
    },
  },
};
</script>
<style scoped>
@import url(./the-main-content.css);

/* css cho toast transitionGroup */
.v-enter-from {
  opacity: 0;
  scale: 0;
  /* rotate: 360deg; */
  translate: 0 100px;
}
.v-enter-to {
  opacity: 1;
  scale: 1;
  /* rotate: 0deg; */
  translate: 0 0;
}

.v-enter-active,
.v-leave-active,
.v-move {
  transition: all 1s;
}

.v-leave-from {
  opacity: 1;
  translate: 0 0;
  /* rotate: 0deg; */
}
.v-leave-to {
  opacity: 0;
  translate: 1000px 0;
  /* rotate: 360deg; */
}

.v-leave-active {
  position: absolute;
}
</style>
