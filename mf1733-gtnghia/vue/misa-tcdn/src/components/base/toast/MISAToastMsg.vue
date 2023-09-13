<template>
  <div class="toast-container">
    <div v-for="(toast, index) in toasts" :key="index" class="toast">
      <div class="toast__icon">
        <i class="icon mr-8" :class="`icon-${toast.status}`"></i>
      </div>
      <div class="toast__body body2 mr-24">
        <span class="toast__title" :class="toast.status"
          >{{ toast.title }} !
        </span>
        <span class="toast__msg">{{ toast.message }}</span>
      </div>
      <div class="toast__end">
        <span class="toast__action body1">{{ toast.undo }}</span>
        <m-button
          :type="'icon'"
          class="btn-toast-close"
          :icon="'icon-xmark'"
          @click="onCloseToast(index)"
        />
      </div>
    </div>
  </div>
</template>

<script>
import app from "@/main.js";
export default {
  name: "m-toast-msg",
  props: {
    toastInput: {
      status: "",
      message: "",
      title: "",
      undo: "",
    },
    duration: {
      type: Number,
      default: 5000,
    },
  },
  data() {
    return {
      toasts: [],
    };
  },
  methods: {
    /**
     * Add new Toast
     * Created by: mf1733-gtnghia 24/8/2023
     */
    addToast(newToast) {
      const formatedToast = this.formatToast(newToast);
      this.toasts.push(formatedToast);
      setTimeout(() => {
        const index = this.toasts.indexOf(formatedToast);
        if (index !== -1) {
          this.toasts.splice(index, 1);
        }
      }, this.duration);
    },
    /**
     * Remove Toast on Click Xmark
     * Created by: mf1733-gtnghia 24/8/2023
     */
    onCloseToast(index) {
      this.toasts.splice(index, 1);
    },
    /**
     * Format toast content by user's language
     * Created by: mf1733-gtnghia 24/8/2023
     */
    formatToast(toast) {
      if (toast.status == this.$MISAEnum.ToastStatus.success) {
        const lang = app.config.globalProperties.$LangCode;
        toast.title = this.$MISAResource[lang].success;
        toast.message = this.toastInput.message;
        toast.status = this.$MISAHelper.getPropertyFromValue(
          toast.status,
          this.$MISAEnum.ToastStatus
        );
        toast.undo = this.$MISAResource[lang].undoMsg;
      } else if (toast.status == this.$MISAEnum.ToastStatus.warning) {
        const lang = app.config.globalProperties.$LangCode;
        toast.title = this.$MISAResource[lang].warning;
        toast.message = this.toastInput.message;
        toast.status = this.$MISAHelper.getPropertyFromValue(
          toast.status,
          this.$MISAEnum.ToastStatus
        );
        toast.undo = this.$MISAResource[lang].undoMsg;
      } else if (toast.status == this.$MISAEnum.ToastStatus.error) {
        const lang = app.config.globalProperties.$LangCode;
        toast.title = this.$MISAResource[lang].error;
        toast.message = this.toastInput.message;
        toast.status = this.$MISAHelper.getPropertyFromValue(
          toast.status,
          this.$MISAEnum.ToastStatus
        );
        toast.undo = this.$MISAResource[lang].undoMsg;
      }
      return toast;
    },
  },
  watch: {
    /**
     * Watching new toast is added
     * Created by: mf1733-gtnghia 24/8/2023
     */
    toastInput: function (newToast) {
      this.addToast(newToast);
    },
  },
  computed: {},
};
</script>

<style scoped>
@import url(./toast.css);
</style>
