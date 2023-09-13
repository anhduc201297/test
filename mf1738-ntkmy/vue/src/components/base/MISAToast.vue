<template>
    <!-- Toast -->
    <div class="toast">
        <div class="toast__title">
            <div class="icon" :class="typeIcon"></div>
            <div class="toast__text" :class="typeText">{{title}}</div>
        </div>
        <div class="icon toast__close" @click="closeToast" ></div>
    </div>
</template>
  
<script>
export default {
    name: 'MISAToast',
    props: {
        type: String,
    },
    mounted() {
        setTimeout(() => {
        this.closeToast();
        }, 2000);
    },
    methods: {
        /**
        * Hàm đóng toast
         * CreateBy : NTKMY (30/08/2023)
         */
        closeToast() {
        this.$emit('close');
        },
    },
    computed: {
    typeIcon() {
      return this.$MISAEnum.ToastMode[this.type].typeIcon;
    },
    typeText() {
        return this.$MISAEnum.ToastMode[this.type].typeText;
    },
    title() {
        return this.$MISAEnum.ToastMode[this.type].title;
    },
  },
};
</script>

<style scoped>
@import url('../../css/base/toast.css');
@import url('../../css/base/icon.css');
</style>
  