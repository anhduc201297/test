<template>
    <button v-bind="$attrs" ref="btn" class="m-btn" :class="[`m-btn--${type}`]">
        <slot />
        <m-mask :isShow="isLoading"><m-loading /></m-mask>
    </button>
</template>
<script>
export default {
    name: 'MButton',

    props: {
        type: {
            validator(value) {
                return ['primary', 'sub', 'link'].includes(value);
            },
            default: 'primary'
        },
        isLoading: {
            type: Boolean,
            default: false
        }
    },

    methods: {
        /**
         * Focus vào button hiện tại
         * @author NTVu - 30/08/2023
         */
        focus() {
            this.$refs.btn.focus();
        }
    }
};
</script>
<style scoped>
@import url('./button.css');
</style>
