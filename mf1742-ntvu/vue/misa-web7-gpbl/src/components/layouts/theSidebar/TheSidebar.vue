<template>
    <aside v-if="isOpenSidebar" class="sidebar">
        <header class="nav-header">
            <m-btn-no-text class="icon-grid lg icon-16" />
            <a class="nav-header-logo"></a>
        </header>

        <aside class="nav-control">
            <nav class="nav-link-list">
                <router-link
                    v-for="(item, index) of routes"
                    :key="index"
                    :to="item.to"
                    class="nav-link-item"
                    :class="item.class"
                >
                    {{ item.label }}
                </router-link>
            </nav>
        </aside>
    </aside>
    <aside v-else class="sidebar sidebar-snap">
        <header class="nav-header">
            <m-btn-no-text
                @click="$emit('onOpenSidebar')"
                title="Mở rộng"
                class="lg icon-menu-white icon-24"
            />
        </header>

        <aside class="nav-control">
            <nav class="nav-link-list">
                <router-link
                    v-for="(item, index) of routes"
                    :key="index"
                    :to="item.to"
                    class="nav-link-item lg focus-border"
                    :class="item.class"
                    :title="item.label"
                >
                </router-link>
            </nav>
        </aside>
    </aside>
</template>

<script>
export default {
    components: {},
    name: 'TheSidebar',
    props: {
        isOpenSidebar: Boolean
    },
    data() {
        return {
            routes: []
        };
    },

    /**
     * Thiết lập các routes của ứng dụng
     * @Author NTVu - MF1742 - 21/08/2023
     */
    created() {
        this.routes = [
            {
                label: 'Nhân viên',
                to: '/',
                class: 'icon-general icon-24 '
            },
            {
                label: 'Phòng ban',
                to: '/office',
                class: 'icon-cash icon-24 '
            },
            {
                label: 'Tiền gửi',
                to: '/keep-money',
                class: 'icon-money-keeper icon-24 '
            },
            {
                label: 'Mua hàng',
                to: '/buy',
                class: 'icon-buy icon-24 '
            },
            {
                label: 'Bán hàng',
                to: '/sell',
                class: 'icon-cart icon-24 '
            }
        ];
    }
};
</script>

<style scoped>
@import url('./sidebar.css');
</style>
