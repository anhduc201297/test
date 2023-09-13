<template>
    <!-- Side Bar -->
    <div class="sidebar">
        <!-- Logo -->
        <div class="sidebar__logo">
            <div class="icon sidebar__icon"></div>
            <img class="sidebar__image" src="../../assets/img/Logo_Module_TiengViet_White.66947422.svg" alt="Logo Kế Toán"/>
        </div>
        <!-- Container -->
        <div class="sidebar__content">
            <router-link to="/employee" class="sidebar__feature">
                <div class="icon sidebar__feature-logo" style="background-position: -30px -1628px"></div>
                <div class="sidebar__feature-name">Tổng quan</div>
            </router-link>
            <router-link to="/" class="sidebar__feature">
                <div class="icon sidebar__feature-logo" style="background-position: -73px -1628px"></div>
                <div class="sidebar__feature-name">Tiền mặt</div>
            </router-link>
            <router-link to="/" class="sidebar__feature">
                <div class="icon sidebar__feature-logo" style="background-position: -117px -1585px"></div>
                <div class="sidebar__feature-name">Tiền gửi</div>
            </router-link>
            <router-link to="/" class="sidebar__feature">
                <div class="icon sidebar__feature-logo" style="background-position: -161px -1628px"></div>
                <div class="sidebar__feature-name">Mua hàng</div>
            </router-link>
            <router-link to="/" class="sidebar__feature">
                <div class="icon sidebar__feature-logo" style="background-position: -204px -1585px"></div>
                <div class="sidebar__feature-name">Bán hàng</div>
            </router-link>
            <router-link to="/" class="sidebar__feature">
                <div class="icon sidebar__feature-logo" style="background-position: -249px -1628px"></div>
                <div class="sidebar__feature-name">Quản lý đơn</div>
            </router-link>
        </div>
    </div>
</template>
  
<script>
export default {
    name: 'TheSidebar',
}
</script>
  
<style scoped>
@import url('../../css/layout/sidebar.css');
</style>
  

