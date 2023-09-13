<template>
    <button id="addBtn" class="btn">{{ text }}</button>
</template>

<script>
export default {
    name: "MISAButton",
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