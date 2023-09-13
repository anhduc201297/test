<template>
    <div class="sidebar">
        <router-link to="/employee" class="sidebar__item">
            <div class="sidebar__icon">
                <div class="icon icon-dashboard"></div>
            </div>
            <div class="sidebar__label">Tổng quan</div>
        </router-link>
        <router-link to="/customer" class="sidebar__item">
            <div class="sidebar__icon">
                <div class="icon icon-cash"></div>
            </div>
            <div class="sidebar__label">Tiền mặt</div>
        </router-link>
        <a href="" class="sidebar__item">
            <div class="sidebar__icon">
                <div class="icon icon-bank"></div>
            </div>
            <div class="sidebar__label">Tiền gửi</div>
        </a>
        <a href="" class="sidebar__item">
            <div class="sidebar__icon">
                <div class="icon icon-buy"></div>
            </div>
            <div class="sidebar__label">Mua hàng</div>
        </a>
        <a href="" class="sidebar__item">
            <div class="sidebar__icon">
                <div class="icon icon-sale"></div>
            </div>
            <div class="sidebar__label">Bán hàng</div>
        </a>
        <a href="" class="sidebar__item">
            <div class="sidebar__icon">
                <div class="icon icon-invoice"></div>
            </div>
            <div class="sidebar__label">Quản lý hóa đơn</div>
        </a>
        <a href="" class="sidebar__item">
            <div class="sidebar__icon">
                <div class="icon icon-stock"></div>
            </div>
            <div class="sidebar__label">Kho</div>
        </a>
        <a href="" class="sidebar__item">
            <div class="sidebar__icon">
                <div class="icon icon-tools"></div>
            </div>
            <div class="sidebar__label">Công cụ dụng cụ</div>
        </a>
        <a href="" class="sidebar__item">
            <div class="sidebar__icon">
                <div class="icon icon-fixed-assets"></div>
            </div>
            <div class="sidebar__label">Tài sản cố định</div>
        </a>
        <a href="" class="sidebar__item">
            <div class="sidebar__icon">
                <div class="icon icon-tax"></div>
            </div>
            <div class="sidebar__label">Thuế</div>
        </a>
        <a href="" class="sidebar__item">
            <div class="sidebar__icon">
                <div class="icon icon-price"></div>
            </div>
            <div class="sidebar__label">Giá thành</div>
        </a>
        <a href="" class="sidebar__item">
            <div class="sidebar__icon">
                <div class="icon icon-general"></div>
            </div>
            <div class="sidebar__label">Tổng hợp</div>
        </a>
        <a href="" class="sidebar__item">
            <div class="sidebar__icon">
                <div class="icon icon-budget"></div>
            </div>
            <div class="sidebar__label">Ngân sách</div>
        </a>
        <a href="" class="sidebar__item">
            <div class="sidebar__icon">
                <div class="icon icon-report"></div>
            </div>
            <div class="sidebar__label">Báo cáo</div>
        </a>
    </div>
</template>

<script>
export default {
    name:"TheSideBar"   
}
</script>