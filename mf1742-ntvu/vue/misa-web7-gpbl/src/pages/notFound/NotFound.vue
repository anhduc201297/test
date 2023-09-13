<template>
    <div class="not-found-page">
        <h1 class="not-found__info">Không thể tìm thấy trang (404)</h1>
        <router-link
            class="back-to-home"
            :to="{ name: 'EmployeeList', param: '' }"
            >Quay lại trang chủ</router-link
        >
    </div>
</template>

<script>
export default {
    name: 'TheNotFound'
};
</script>

<style scoped>
@import url('./notFound.css');
</style>
