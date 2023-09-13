<template>
    <div class="modal">
        <slot></slot>
    </div>
</template>

<script>
export default {
   name: "m-modal"
}
</script>

<style scoped>
    @import url(./modal.css)
</style>