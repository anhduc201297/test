<template>
    <div v-show="isShow" class="mask"><slot /></div>
</template>
<script>
export default {
    name: 'MMask',
    props: {
        isShow: {
            type: Boolean,
            default: false
        }
    }
};
</script>

<style lang="css" scoped>
@import url(./mask.css);
</style>
