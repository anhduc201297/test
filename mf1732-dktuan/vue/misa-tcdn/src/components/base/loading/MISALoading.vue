<template>
    <div class="loading-container">
        <img :src="require('@/assets/loading.svg')" />
    </div>
</template>

<script>
export default {
    name: "m-loading"
}
</script>

<style scoped>
    @import url(./loading.css)
</style>