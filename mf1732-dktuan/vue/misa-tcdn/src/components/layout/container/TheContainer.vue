<template>
    <div class="container">
        <m-toast v-model:toasts="toasts"></m-toast>
        <router-view></router-view>
    </div>
</template>

<script>
export default {
    name: 'TheContainer',
    data() {
        return {
            toasts: []
        }
    },
    methods: {
        /**
         * Thêm toast mới vào mảng
         * Author: dktuan (30/08/2023)
         */
        addToast(toast) {
            this.toasts = [...this.toasts, toast]
        }
    },
    created() {
        this.$emitter.on('addToast', this.addToast)
    },

    beforeUnmount() {
        this.$emitter.off('addToast', this.addToast)
    },
}
</script>

<style scoped>
@import url(./container.css);
</style>