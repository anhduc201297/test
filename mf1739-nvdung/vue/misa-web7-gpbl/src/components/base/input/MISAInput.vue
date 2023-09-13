<template>
    <input type="text"  >
</template>
<script>
export default {
    name: "MISAInput",
    data: {
    },
    props: {
        initValue: {
            type: String,
            default: "",
            required: false,
        }
    },

}
</script>
<style scoped>
@import url(./input.css);
</style>
