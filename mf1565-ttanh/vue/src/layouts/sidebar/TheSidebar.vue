<script setup>
import { IconOption } from '@/assets/icons';
import { menuItemList } from '@/utils/menuItemList';
import SidebarItem from './SidebarItem.vue';

// logo image path
const logoSrc = 'src/assets/images/logo.svg';
</script>

<template>
    <div id="sidebar">
        <div class="logo">
            <IconOption class="option-icon" />
            <img :src="logoSrc" alt="logo-image" class="logo-image" />
        </div>
        <div class="menu">
            <SidebarItem
                v-for="(menuItem, index) in menuItemList"
                :key="index"
                :icon="menuItem.icon"
                :title="menuItem.title"
            />
        </div>
    </div>
</template>

<style lang="scss" scoped>
@import '@/styles/mixins.scss';
#sidebar {
    display: flex;
    flex-direction: column;
    align-items: center;
    background-color: rgba(var(--c-gray-900), 0.9);
}

.logo {
    display: flex;
    gap: 20px;
    align-items: center;
    height: var(--main-header-height);
    cursor: pointer;
    .option-icon {
        @include size(24px);
    }

    .logo-image {
        height: 32px;
    }
}

.menu {
    display: flex;
    padding: 12px;
    flex-direction: column;
    gap: 4px;

    width: var(--sidebar-width);
    max-height: calc(100vh - var(--main-header-height));
    height: 100%;
    overflow-y: overlay;
}
</style>
@/helpers/menuItemList
