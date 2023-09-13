<template>
  <nav class="menu-container">
    <div class="logo-container">
      <a class="menu-container-options mi mi-16 mi-app"></a>
      <a class="logo"
        ><img
          src="../../../assets/img/Logo_Module_TiengViet_White.66947422.svg"
          alt="Logo phần mềm"
      /></a>
    </div>

    <div class="menu-bar">
      <div class="menu-item-list scroller">
        <div class="parent-item-menu">
          <a class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-dashboard"></div>
            </div>
            <div class="menu-item__title">
              {{ this.$resource["vi-VN"].sideBarMenu.sideBarDashboard }}
            </div></a
          >
        </div>
        <div class="parent-item-menu">
          <a class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-cash"></div>
            </div>
            <div class="menu-item__title">
              {{ this.$resource["vi-VN"].sideBarMenu.sideBarCash }}
            </div></a
          >
        </div>
        <div class="parent-item-menu">
          <a class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-bank"></div>
            </div>
            <div class="menu-item__title">
              {{ this.$resource["vi-VN"].sideBarMenu.sideBarBank }}
            </div></a
          >
        </div>
        <div class="parent-item-menu">
          <a class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-purchase"></div>
            </div>
            <div class="menu-item__title">
              {{ this.$resource["vi-VN"].sideBarMenu.sideBarPurchase }}
            </div></a
          >
        </div>
        <div class="parent-item-menu">
          <a class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-sale"></div>
            </div>
            <div class="menu-item__title">
              {{ this.$resource["vi-VN"].sideBarMenu.sideBarSale }}
            </div></a
          >
        </div>
        <div class="parent-item-menu">
          <a class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-invoice"></div>
            </div>
            <div class="menu-item__title">
              {{ this.$resource["vi-VN"].sideBarMenu.sideBarInvoice }}
            </div></a
          >
        </div>
        <div class="parent-item-menu">
          <a class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-stock"></div>
            </div>
            <div class="menu-item__title">
              {{ this.$resource["vi-VN"].sideBarMenu.sideBarStock }}
            </div></a
          >
        </div>
        <div class="parent-item-menu">
          <a class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-tools"></div>
            </div>
            <div class="menu-item__title">
              {{ this.$resource["vi-VN"].sideBarMenu.sideBarTools }}
            </div></a
          >
        </div>
        <div class="parent-item-menu">
          <a class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-fixed-asets"></div>
            </div>
            <div class="menu-item__title">
              {{ this.$resource["vi-VN"].sideBarMenu.sideBarFixedAssets }}
            </div></a
          >
        </div>
        <div class="parent-item-menu">
          <a class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-tax"></div>
            </div>
            <div class="menu-item__title">
              {{ this.$resource["vi-VN"].sideBarMenu.sideBarTax }}
            </div></a
          >
        </div>
        <div class="parent-item-menu">
          <a class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-price"></div>
            </div>
            <div class="menu-item__title">
              {{ this.$resource["vi-VN"].sideBarMenu.sideBarPrice }}
            </div></a
          >
        </div>
        <div class="parent-item-menu">
          <a class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-general"></div>
            </div>
            <div class="menu-item__title">
              {{ this.$resource["vi-VN"].sideBarMenu.sideBarGeneral }}
            </div></a
          >
        </div>
        <div class="parent-item-menu">
          <a class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-budget"></div>
            </div>
            <div class="menu-item__title">
              {{ this.$resource["vi-VN"].sideBarMenu.sideBarBudget }}
            </div></a
          >
        </div>
        <div class="parent-item-menu">
          <a class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-report"></div>
            </div>
            <div class="menu-item__title">
              {{ this.$resource["vi-VN"].sideBarMenu.sideBarReport }}
            </div></a
          >
        </div>
        <div class="parent-item-menu">
          <a class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-finance"></div>
            </div>
            <div class="menu-item__title">
              {{ this.$resource["vi-VN"].sideBarMenu.sideBarFinance }}
            </div></a
          >
        </div>

        <!-- RouterLink -->
        <div class="parent-item-menu">
          <router-link to="/EmployeeList" class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-finance"></div>
            </div>
            <div class="menu-item__title">
              Router EmployeeList
              <!-- {{ this.$resource["vi-VN"].sideBarMenu.sideBarFinance }} -->
            </div></router-link
          >
        </div>
        <!-- RouterLink -->
        <div class="parent-item-menu">
          <router-link to="/CustomerList" class="menu-item"
            ><div class="menu-item__icon">
              <div class="mi mi-24 mi-sidebar-finance"></div>
            </div>
            <div class="menu-item__title">
              Router CustomerList
              <!-- {{ this.$resource["vi-VN"].sideBarMenu.sideBarFinance }} -->
            </div></router-link
          >
        </div>
      </div>
    </div>
  </nav>
</template>
<script>
export default {
  name: "TheSidebar",
};
</script>
<style scoped>
@import url(./the-sidebar.css);
</style>
