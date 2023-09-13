<template>
    <!-- eslint-disable -->
    <div class="content">
        <router-view></router-view>
    </div>
    <m-toast 
        v-if="showToast" 
        @onClose="onCloseToast" 
        icon="success" 
        :title="toastTitle"
        :textNotice="toastNotice"
    ></m-toast>
    <m-dialog 
        v-if="showDialog" 
        @onClose="onCloseDialog" 
        :title="dialogTitle"
        :type="dialogType" 
        :message="errorMsg"
    ></m-dialog>
</template>

<script>
export default {
    name: "TheMainContent",
    data() {
        return {
            showToast: false,
            showDialog: false,
            errorMsg: "",
            dialogType: "",
            dialogTitle: "",
            toastTitle: "",
            toastNotice: "",
        }
    },
    created() {
        this.$emitter.on("showDialog", this.onShowDialogFunc);
        this.$emitter.on("closeDialog", this.onCloseDialog);
        this.$emitter.on("showToast", this.onOpenToastFunc);
        this.$emitter.on("closeToast", this.onCloseDialog);
    },
    unmounted() {
        this.$emitter.off("showDialog", this.onShowDialogFunc);
        this.$emitter.off("closeDialog", this.onCloseDialog);
        this.$emitter.off("showToast", this.onOpenToastFunc);
        this.$emitter.off("closeToast", this.onCloseDialog);
    },
    methods: {
        /**
         * Đóng toast
         * AUTHOR: DTTHANH (21/8/2023)
         */
        onCloseToast() {
            this.showToast = false
        },

        /**
         * Hiển thị và thiết lập Toast
         * AUTHOR: DTTHANH (21/8/2023)
         */
        onOpenToastFunc(title, notice) {
            this.toastNotice = notice
            this.toastTitle = title
            this.showToast = true
        },

        /**
         * Hiển thị và thiết lập Dialog
         * AUTHOR: DTTHANH (21/8/2023)
         */
        onShowDialogFunc(title, errorMsg, type) {
            this.dialogTitle = title;
            this.errorMsg = errorMsg;
            this.dialogType = type;
            this.showDialog = true;
        },
        
        /**
         * Đóng Dialog
         * AUTHOR: DTTHANH (21/8/2023)
         */
        onCloseDialog() {
            this.showDialog = false;
        }
    },
}
</script>

<style scoped></style>