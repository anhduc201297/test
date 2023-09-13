<script setup>
defineProps({
    icon: {
        type: Object,
        required: true
    },
    title: {
        type: String,
        required: true
    }
});
</script>

<template>
    <div class="menu-item">
        <component :is="icon" class="icon" />
        <div class="title">{{ title }}</div>
    </div>
</template>

<style lang="scss" scoped>
@import '@/styles/mixins.scss';

$--menu-item-padding-x: 12px;
$--menu-item-padding-y: 10px;
$--menu-item-border-radius: 6px;
$--menu-item-text-color: rgba(var(--c-white));
$--menu-item-bg-color: rgba(var(--c-gray-600), 0.9);

/* Styles for menu-item */
.menu-item {
    display: flex;
    gap: 12px;
    align-items: center;

    padding: $--menu-item-padding-y $--menu-item-padding-x;
    border-radius: $--menu-item-border-radius;
    cursor: pointer;

    &:hover {
        background-color: $--menu-item-bg-color;
    }

    .icon {
        @include size(24px);
    }

    .title {
        @include font(14, 500);
        color: $--menu-item-text-color;
    }
}
</style>
