<template>
    <div class="toast-container">
        <div class="toast">
            <m-icon class="toast-icon" :icon="icon"></m-icon>
            <div class="toast-title">{{ title }}</div>
            <div class="toast-text">{{ textNotice }}</div>
            <div class="toast-button toast-icon--close" @click="onClose"></div>
        </div>
    </div>
</template>
  
<script>
export default {
    name: "MISAToast",
    props: {
        textNotice: {
            type: String,
        },
        icon: {
            type: String,
        },
        type: {
            type:String,
        },
        title: {
            type: String,
        }
    },

    methods: {
        /**
         * Phương thức đóng Toast với emit
         * AUTHOR: DTTHANH (20/08/2023)
         */
        onClose() {
            this.$emit("onClose");
        },
    },
};
</script>
  
<style scoped>
@import url(./toast.css);
</style>