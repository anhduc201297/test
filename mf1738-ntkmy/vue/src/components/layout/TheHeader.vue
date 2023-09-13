<template>
    <!-- Header -->
    <div class="header">
        <div class="header--left">
            <div class="icon header__icon"></div>
            <div class="header__name">Phầm mềm kế toán Misa Amis</div>
            <div class="icon header__title--dropdown"></div>
        </div>
        <div class="header--right">
            <div class="icon tooltip header__logo--notify">
                <span class="tooltiptext">Thông báo</span>
            </div>
            <div class="header__account">
                <div class="header__avatar"></div>
                <div class="header__username">Nguyễn Quang Minh</div>
                <div class="icon header__title--dropdown"></div>
            </div>
        </div>
    </div>
</template>
  
<script>
export default {
    name: 'TheHeader'
}
</script>

<style scoped>
@import url('../../css/base/icon.css');
@import url('../../css/base/tooltip.css');
@import url('../../css/layout/header.css');
</style>
  