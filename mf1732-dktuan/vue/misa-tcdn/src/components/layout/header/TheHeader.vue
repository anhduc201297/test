<template>
    <div class="header">
        <div class="logo-wrapper">
            <span @click="handleExtendSidebar" class="shortened-sidebar-btn icon"></span>
            <img class="logo" :src="require('@/assets/img/Logo_Module_TiengViet_White.66947422.svg')" alt="">
        </div>
        <span class="submenu-item icon"></span>
        <h3 class="company-name">CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC</h3>
        <span class="more-btn icon"></span>
        <span class="noti-btn icon"></span>
        <img :src="require('@/assets/img/avatar.png')" alt="avatar" class="avatar">
        <span class="username">Dương Khánh Tuân</span>
        <span class="more-btn icon small"></span>
        <!-- <select v-model="lang" class="header__lang_options">
            <option value="VN">Tiếng Việt</option>
            <option value="EN">English</option>
        </select> -->
    </div>
</template>

<script>
import app from '@/main'
export default {
    emits: ['onExtend'],
    data() {
        return {
            lang: this.$lang,
        }
    },
    methods: {
        /**
         * mở rộng hoặc thu nhỏ thanh sidebar
         * Author: dktuan (19/08/2023)
         */
        handleExtendSidebar() {
            this.$emit('onExtend')
        }
    },

    watch: {
        /**
         * thay đổi ngôn ngữ theo select input
         * Author: dktuan (24/08/2023)
         */
        lang(newVal) {
            console.log(newVal)
            app.config.globalProperties.$lang  = newVal
            console.log(this.$lang)
        }
    }
}
</script>

<style scoped>
    @import url(./header.css);
</style>