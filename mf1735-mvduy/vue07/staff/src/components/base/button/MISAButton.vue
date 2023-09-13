<template>
    <button class="btn-primary">
        <slot></slot>
    </button>
</template>

<script>
export default {
    name: 'MISAButton',

}
</script>

<style scoped>
@import url(./button.css);
</style>