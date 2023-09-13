<template>
    <div class="sidebar">
        <ul class="siderbar-list">
            <m-sidebar-item v-for="(item, index) in sidebarItem" :key="index" :icon="item.icon" :title="item.title"
                :path="item.path">
            </m-sidebar-item>
        </ul>
    </div>
</template>

<script>
export default {
    name: "TheSidebar",
    data() {
        return {
            sidebarItem: [
                {
                    path: "/",
                    icon: "dashboard",
                    title: "Tổng quan"
                },
                {
                    path: "/employee",
                    icon: "employee",
                    title: "Nhân viên"
                },

                {
                    icon: "cash",
                    title: "Tiền mặt"
                },
                {
                    icon: "bank",
                    title: "Tiền gửi"
                },
                {
                    icon: "pu",
                    title: "Mua hàng"
                },
                {
                    icon: "sale",
                    title: "Bán hàng"
                },
                {
                    icon: "invoice",
                    title: "Quản lý hóa đơn"
                },
                {
                    icon: "stock",
                    title: "Kho"
                },
                {
                    icon: "tool",
                    title: "Công cụ dụng cụ"
                },
                {
                    icon: "price",
                    title: "Giá thành"
                },
                {
                    icon: "report",
                    title: "Báo cáo"
                },
            ]
        }
    },
}
</script>

<style scoped></style>