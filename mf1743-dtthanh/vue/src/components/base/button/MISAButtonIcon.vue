<template>
    <button class="btn"><m-icon></m-icon></button>
</template>

<script>
export default {
    name: "MISAButtonIcon",
    props: {
        text: {
            type: String,
            default: ''
        }
    }
}
</script>

<style scoped>
@import url(./button.css);
</style>