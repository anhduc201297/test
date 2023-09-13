<template>
    <button class="m-btn m-btn-icon" :class="[`m-btn--${type}`]">
        <slot />
        <m-mask :isShow="isLoading"><m-loading /></m-mask>
    </button>
</template>
<script>
export default {
    name: 'MButtonIcon',
    props: {
        type: {
            validator(value) {
                return ['primary', 'sub', 'link'].includes(value);
            },
            default: 'link'
        },
        isLoading: {
            type: Boolean,
            default: false
        }
    }
};
</script>
<style scoped>
@import url('./button.css');
</style>
