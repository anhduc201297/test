<template>
    <button class="btn">
        <slot></slot>
    </button>
</template>

<script>
export default {
    name: "m-button"
}
</script>

<style scoped>
    @import url(./button.css);
</style>