<template>
    <div class="sidebar">    
            <div class="sidebar-content">
                <router-link to="/">
                    <button class="sidebar-btn active">
                        <span title="Tổng quan" class="sidebar-icon" style="background-position: -33px -1587px;"></span>
                        <span class="sidebar-menu-item">
                            Tổng quan
                        </span>
                    </button>
                </router-link>
                <router-link to="cash">
                    <button class="sidebar-btn">
                        <span title="Tiền mặt" class="sidebar-icon" style="background-position: -77px -1587px;"></span>
                        <span class="sidebar-menu-item">
                            Tiền mặt
                        </span>
                    </button>
                </router-link>
                <router-link to="/q">
                    <button class="sidebar-btn">
                        <span title="Tiền gửi" class="sidebar-icon" style="background-position: -121px -1587px;"></span>
                        <span class="sidebar-menu-item">
                            Tiền gửi
                        </span>
                    </button>
                </router-link>
                <router-link to="/q">
                    <button class="sidebar-btn">
                        <span title="Mua hàng" class="sidebar-icon" style="background-position: -165px -1587px;"></span>
                        <span class="sidebar-menu-item">
                            Mua hàng
                        </span>
                    </button>
                </router-link>
                <router-link to="/q">
                    <button class="sidebar-btn">
                        <span title="Bán hàng" class="sidebar-icon" style="background-position: -207px -1587px;"></span>
                        <span class="sidebar-menu-item">
                            Bán hàng
                        </span>
                    </button>
                </router-link>
                <router-link to="/q">
                    <button class="sidebar-btn">
                        <span title="Quản lý hóa đơn" class="sidebar-icon" style="background-position: -254px -1587px;"></span>
                        <span class="sidebar-menu-item">
                            Quản lý hóa đơn
                        </span>
                    </button>
                </router-link>
                <router-link to="/q">
                    <button class="sidebar-btn">
                        <span title="Kho" class="sidebar-icon" style="background-position: -294px -1587px;"></span>
                        <span class="sidebar-menu-item">
                            Kho
                        </span>
                    </button>
                </router-link>
                <router-link to="/q">
                    <button class="sidebar-btn">
                        <span title="Công cụ dụng cụ" class="sidebar-icon" style="background-position: -339px -1587px;"></span>
                        <span class="sidebar-menu-item">
                            Công cụ dụng cụ
                        </span>
                    </button>
                </router-link>
                <router-link to="/q">
                    <button class="sidebar-btn">
                        <span title="Tài sản cố định" class="sidebar-icon" style="background-position: -382px -1587px;"></span>
                        <span class="sidebar-menu-item">
                            Tài sản cố định
                        </span>
                    </button>
                </router-link>
                <router-link to="/q">
                    <button class="sidebar-btn">
                        <span title="Thuế" class="sidebar-icon" style="background-position: -427px -1587px;"></span>
                        <span class="sidebar-menu-item">
                            Thuế
                        </span>
                    </button>
                </router-link>
                <router-link to="/q">
                    <button class="sidebar-btn">
                        <span title="Giá thành" class="sidebar-icon" style="background-position: -471px -1587px;"></span>
                        <span class="sidebar-menu-item">
                            Giá thành
                        </span>
                    </button>
                </router-link>
                <router-link to="/q">
                    <button class="sidebar-btn">
                        <span title="Tổng hợp" class="sidebar-icon" style="background-position: -514px -1587px;"></span>
                        <span class="sidebar-menu-item">
                            Tổng hợp
                        </span>
                    </button>
                </router-link>
                <router-link to="/q">
                    <button class="sidebar-btn">
                        <span title="Ngân sách" class="sidebar-icon" style="background-position: -427px -1661px;"></span>
                        <span class="sidebar-menu-item">
                            Ngân sách
                        </span>
                    </button>
                </router-link>
                <router-link to="/q">
                    <button class="sidebar-btn">
                        <span title="Báo cáo" class="sidebar-icon" style="background-position: -552px -1587px;"></span>
                        <span class="sidebar-menu-item">
                            Báo cáo
                        </span>
                    </button>
                </router-link>
                <router-link to="/q">
                    <button class="sidebar-btn">
                        <span title="Phân tích tài chính" class="sidebar-icon" style="background-position: -252px -1663px;"></span>
                        <span class="sidebar-menu-item">
                            Phân tích tài chính
                        </span>
                    </button>
                </router-link>
            </div>
        </div>
</template>

<script>
export default {
    data() {
        return {
            
        }
    },
}
</script>

<style scoped>
@import url(./sidebar.css);
</style>