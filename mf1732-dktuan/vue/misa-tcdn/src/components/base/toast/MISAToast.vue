<template>
    <div class="toast-container">
        <div v-for="toast in toasts" :key="toast.code" class="toast-item" :class="{
            'success': toast.type === this.$enum.ToastType.SUCCESS,
            'warning': toast.type === this.$enum.ToastType.WARNING
        }">
            <span class="toast-icon icon"></span>
            <h5 class="toast-title success">{{ toast.title }}</h5>
            <p class="toast-body-text">{{ toast.message }}</p>
            <span class="toast-action">{{ toast.action }}</span>
            <span class="toast-close-icon icon"></span>
        </div>
    </div>
</template>

<script>
export default {
    name: "m-toast",
    props: ["toasts"],
    methods: {
        /**
         * Thêm một toast mới, đồng thời cập nhật lại props toasts ở component cha
         * Author: dktuan (19/08/2023)
         */
        addToast(type, title, message, action) {
            const toast = {
                code: Math.random(),
                title,
                type,
                message,
                action
            }
            for (let toastItem of this.myToasts) {
                if (toastItem.type === this.$enum.ToastType.SUCCESS) {
                    toast.title = this.$message[this.$lang].toast.titles.WARNING
                    toast.type = this.$enum.ToastType.WARNING
                    toast.message = this.$message[this.$lang].toast.messages.TOO_FAST_ACTION
                    toast.action = null
                    break
                }
            }
            this.myToasts.push(toast)
            this.$emit('update:toasts', this.myToasts)
            setTimeout(() => {
                this.myToasts.shift();
            }, 7000)
        },
    },
    data() {
        return {
            myToasts: this.toasts,
        }
    },
    watch: {
        /**
         * Two ways binding danh sách toast với component cha
         * Author: dktuan (19/08/2023)
         */
        toasts(newVal, oldVal) {
            if(oldVal.length < newVal.length)
                this.addToast(newVal.at(-1).type,newVal.at(-1).title,newVal.at(-1).message, newVal.at(-1).action)
        }
    }
}
</script>

<style scoped>
@import url(./toast.css);
</style>