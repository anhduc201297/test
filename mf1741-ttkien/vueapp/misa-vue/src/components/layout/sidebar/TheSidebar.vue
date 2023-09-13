<template>
  <div class="sidebar">
    <!-- logo và tiêu đề -->
    <div class="sidebar-logo">
      <router-link to="/">
        <m-icon :iconType="'icon-menu-option'"></m-icon>
      </router-link>
      <a href="" class="logo">
        <img src="../../../assets/img/Logo_Module_TiengViet_White.66947422.svg" alt="Logo Kế toán"
          class="logo-img-icon" />
      </a>
    </div>

    <!-- menu -->
    <div class="sidebar-menu scroll-menu">
      <div class="parent-menu-item">
        <router-link to="/home" class="menu-item">
          <m-icon :boxIconType="'menu-item_icon'" :iconType="'icon-dashboard'"></m-icon>
          <div class="menu-item_title">Tổng quan</div>
        </router-link>
      </div>
      <div class="parent-menu-item">
        <router-link to="/employeelist" class="menu-item">
          <m-icon :boxIconType="'menu-item_icon'" :iconType="'icon-cash'"></m-icon>
          <div class="menu-item_title">Tiền mặt</div>
        </router-link>
      </div>
      <div class="parent-menu-item">
        <router-link to="/customerList" class="menu-item">
          <m-icon :boxIconType="'menu-item_icon'" :iconType="'icon-bank'"></m-icon>
          <div class="menu-item_title">Tiền gửi</div>
        </router-link>
      </div>
      <div class="parent-menu-item">
        <router-link to="/" class="menu-item">
          <m-icon :boxIconType="'menu-item_icon'" :iconType="'icon-pu'"></m-icon>
          <div class="menu-item_title">Mua hàng</div>
        </router-link>
      </div>
      <div class="parent-menu-item">
        <router-link to="/" class="menu-item">
          <m-icon :boxIconType="'menu-item_icon'" :iconType="'icon-sale'"></m-icon>
          <div class="menu-item_title">Bán hàng</div>
        </router-link>
      </div>
      <div class="parent-menu-item">
        <router-link to="/" class="menu-item">
          <m-icon :boxIconType="'menu-item_icon'" :iconType="'icon-invoice'"></m-icon>
          <div class="menu-item_title">Quản lý hóa đơn</div>
        </router-link>
      </div>
      <div class="parent-menu-item">
        <router-link to="/" class="menu-item">
          <m-icon :boxIconType="'menu-item_icon'" :iconType="'icon-stock'"></m-icon>
          <div class="menu-item_title">Kho</div>
        </router-link>
      </div>
      <div class="parent-menu-item">
        <router-link to="/" class="menu-item">
          <m-icon :boxIconType="'menu-item_icon'" :iconType="'icon-tools'"></m-icon>
          <div class="menu-item_title">Công cụ dụng cụ</div>
        </router-link>
      </div>
      <div class="parent-menu-item">
        <router-link to="/" class="menu-item">
          <m-icon :boxIconType="'menu-item_icon'" :iconType="'icon-assets'"></m-icon>
          <div class="menu-item_title">Tài sản cố định</div>
        </router-link>
      </div>
      <div class="parent-menu-item">
        <router-link to="/" class="menu-item">
          <m-icon :boxIconType="'menu-item_icon'" :iconType="'icon-salary'"></m-icon>
          <div class="menu-item_title">Thuế</div>
        </router-link>
      </div>
      <div class="parent-menu-item">
        <router-link to="/" class="menu-item">
          <m-icon :boxIconType="'menu-item_icon'" :iconType="'icon-price'"></m-icon>
          <div class="menu-item_title">Giá thành</div>
        </router-link>
      </div>
      <div class="parent-menu-item">
        <router-link to="/" class="menu-item">
          <m-icon :boxIconType="'menu-item_icon'" :iconType="'icon-general'"></m-icon>
          <div class="menu-item_title">Tổng hợp</div>
        </router-link>
      </div>
      <div class="parent-menu-item">
        <router-link to="/" class="menu-item">
          <m-icon :boxIconType="'menu-item_icon'" :iconType="'icon-budget'"></m-icon>
          <div class="menu-item_title">Ngân sách</div>
        </router-link>
      </div>
      <div class="parent-menu-item">
        <router-link to="/" class="menu-item">
          <m-icon :boxIconType="'menu-item_icon'" :iconType="'icon-report'"></m-icon>
          <div class="menu-item_title">Báo cáo</div>
        </router-link>
      </div>
      <div class="parent-menu-item">
        <router-link to="/" class="menu-item">
          <m-icon :boxIconType="'menu-item_icon'" :iconType="'icon-finance'"></m-icon>
          <div class="menu-item_title">Phân tích tài chính</div>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheSidebar",
};
</script>

<style scoped>
@import url(./TheSideBar.css);
</style>