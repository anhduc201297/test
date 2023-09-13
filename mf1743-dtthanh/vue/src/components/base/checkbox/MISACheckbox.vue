<template>
    <label for="">
        <input type="checkbox" />
        <span>{{text}}</span>
    </label>
</template>

<script>
export default {
    name:"MISACheckbox",
    props: ["text"]
}
</script>

<style scoped>
@import url(./checkbox.css);
</style>