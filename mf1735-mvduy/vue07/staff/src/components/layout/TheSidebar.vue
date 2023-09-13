<template>
    <div class="sidebar">
        <div class="logo-container">
            <a href="#" class="icon icon-grid"></a>
            <img class="logo" src="../../assets/img/Logo_Module_TiengViet_White.66947422.svg" alt="">
        </div>
        <div class="menu">

            <router-link to="/" class="menu-item">
                <div class="icon icon-dashboard"></div>
                <a href="#">Tổng quan</a>
            </router-link>
            <router-link to="/staff" class="menu-item">
                <div class="icon icon-cash"></div>
                <a href="#">Tiền mặt</a>
            </router-link>
            <router-link to="/deposit" class="menu-item">
                <div class="icon icon-bank"></div>
                <a href="#">Tiền gửi</a>
            </router-link>
            <router-link to="/sale" class="menu-item">
                <div class="icon icon-bag"></div>
                <a href="#">Mua hàng</a>
            </router-link>
            <div class="menu-item">
                <div class="icon icon-sale"></div>
                <a href="#">Bán hàng</a>
            </div>
            <div class="menu-item">
                <div class="icon icon-invoice"></div>
                <a href="#">Quản lý hóa đơn</a>
            </div>
            <div class="menu-item">
                <div class="icon icon-stock"></div>
                <a href="#">Kho</a>
            </div>
            <div class="menu-item">
                <div class="icon icon-tool"></div>
                <a href="#">Công cụ dụng cụ</a>
            </div>
            <div class="menu-item">
                <div class="icon icon-asset"></div>
                <a href="#">Tài sản cố định</a>
            </div>
            <div class="menu-item">
                <div class="icon icon-tax"></div>
                <a href="#">Thuế</a>
            </div>
            <div class="menu-item">
                <div class="icon icon-price"></div>
                <a href="#">Giá thành</a>
            </div>
            <div class="menu-item">
                <div class="icon icon-general"></div>
                <a href="#">Tổng hợp</a>
            </div>
            <div class="menu-item">
                <div class="icon icon-budget"></div>
                <a href="#">Ngân sách</a>
            </div>
            <div class="menu-item">
                <div class="icon icon-report"></div>
                <a href="#">Báo cáo</a>
            </div>
            <div class="menu-item">
                <div class="icon icon-finance"></div>
                <a href="#">Phân tích tài chính</a>
            </div>

        </div>
    </div>
</template>

<script>
export default {
    name: 'TheSidebar',
}
</script>

<style scoped>
@import url(../../css/layout/sidebar.css);
</style>