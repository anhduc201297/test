<template>
  <div class="sidebar" :class="{expand: isShowSidebar}">
    <div class="menu-bar scrollbar">
      <div class="menu-bar__group-items">
        <div
          v-for="(menuItem, index) in menuItems"
          :key="index"
          class="menu-bar__item"
        >
          <router-link class="menu-bar__item--box" :to="menuItem.link">
            <div class="menu-item__icon icon-container">
              <i class="icon" :class="menuItem.iconClass"></i>
            </div>
            <div class="menu-item__title">{{ menuItem.title }}</div>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "TheSidebar",
  props: {
    isShowSidebar: Boolean,
  },
  data() {
    return {
      menuItems: [
        {
          title: "Tổng quan",
          iconClass: "icon-overview",
          link: "/",
        },
        {
          title: "Báo cáo",
          iconClass: "icon-report",
          link: "/report",
        },
        {
          title: "Khách hàng",
          iconClass: "icon-customer",
          link: "/customer",
        },
        {
          title: "Nhân viên",
          iconClass: "icon-employee",
          link: "/employee",
        },
        // {
        //   title: "Tiền mặt",
        //   iconClass: "icon-cash",
        //   link: "/cash",
        // },
        // {
        //   title: "Tiền gửi",
        //   iconClass: "icon-deposit",
        //   link: "/deposit",
        // },
        // {
        //   title: "Mua hàng",
        //   iconClass: "icon-purchase",
        //   link: "/purchase",
        // },
        // {
        //   title: "Bán hàng",
        //   iconClass: "icon-sell",
        //   link: "/sell",
        // },
        // {
        //   title: "Quản lý hóa đơn",
        //   iconClass: "icon-bill",
        //   link: "/bill",
        // },
        // {
        //   title: "Kho",
        //   iconClass: "icon-warehouse",
        //   link: "/store",
        // },
        // {
        //   title: "Công cụ dụng cụ",
        //   iconClass: "icon-tools",
        //   link: "/tools",
        // },
        // {
        //   title: "Tài sản cố định",
        //   iconClass: "icon-assets",
        //   link: "/assets",
        // },
        // {
        //   title: "Tiền lương",
        //   iconClass: "icon-salary",
        //   link: "/salary",
        // },
        // {
        //   title: "Thuế",
        //   iconClass: "icon-tax",
        //   link: "/tax",
        // },
        // {
        //   title: "Giá thành",
        //   iconClass: "icon-price",
        //   link: "/price",
        // },
        // {
        //   title: "Kết nối vay vốn",
        //   iconClass: "icon-cash",
        //   link: "/loan",
        // },
        // {
        //   title: "Tổng hợp",
        //   iconClass: "icon-synthetic",
        //   link: "/general",
        // },
        // {
        //   title: "Ngân sách",
        //   iconClass: "icon-budget",
        //   link: "/budget",
        // },
        // {
        //   title: "Phân tích tài chính",
        //   iconClass: "icon-analysis",
        //   link: "/analysis",
        // },
      ],
    };
  },
};
</script>

<style scoped>
@import url(./sidebar.css);
</style>
