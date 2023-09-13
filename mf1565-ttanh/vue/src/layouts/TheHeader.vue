<script setup>
import { VIcon } from '@/components';

const branchName = 'công ty cổ phần misa';
const userName = 'tăng thế anh';
</script>

<template>
    <header id="header">
        <div class="left-column">
            <div class="icon-menu">
                <VIcon class="menu-img" />
            </div>

            <div class="branch">
                <div class="name">{{ branchName }}</div>
                <div class="icon-arrow-down">
                    <VIcon class="arrow-down-img" />
                </div>
            </div>
        </div>

        <div class="right-column">
            <div class="icon-bell">
                <VIcon class="bell-img" />
            </div>

            <div class="current-user">
                <img
                    src="src/assets/images/avatar.jpg"
                    alt="avatar-image"
                    class="avatar-image"
                />
                <div class="name">{{ userName }}</div>
                <div class="icon-arrow-down">
                    <VIcon class="arrow-down-img" />
                </div>
            </div>
        </div>
    </header>
</template>

<style lang="scss" scoped>
@import '@/styles/mixins.scss';
/* === Styles for header === */
#header {
    height: var(--main-header-height);
    display: flex;
    padding: 12px 16px;
    justify-content: space-between;
    align-items: center;
}

#header .left-column {
    display: flex;
    gap: 16px;
    .icon-menu {
        @include size(24px);
        cursor: pointer;
        .menu-img {
            width: 16px;
            height: 14px;
            background-position: -847px -35px;
        }
    }
    .branch {
        display: flex;
        align-items: center;
        gap: 8px;
        cursor: pointer;
        .name {
            font-weight: 500;
            text-transform: uppercase;
        }

        .icon-arrow-down {
            @include size(14px);
            transform: rotate(90deg);
            filter: brightness(0);
            .arrow-down-img {
                width: 8px;
                height: 14px;
                background-position: -84px -361px;
            }
        }
    }
}
#header .right-column {
    display: flex;
    align-items: center;
    .icon-bell {
        margin-right: 24px;
        @include size(24px);
        cursor: pointer;
        .bell-img {
            width: 22px;
            height: 25px;
            background-position: -788px -30px;
        }
    }
    .current-user {
        display: flex;
        align-items: center;
        gap: 8px;
        cursor: pointer;
        .avatar-image {
            @include size(32px);
            border-radius: 999px;
        }
        .name {
            font-weight: 500;
            text-transform: capitalize;
        }
        .icon-arrow-down {
            @include size(14px);
            transform: rotate(90deg);
            filter: brightness(0);
            .arrow-down-img {
                width: 8px;
                height: 14px;
                background-position: -84px -361px;
            }
        }
    }
}
</style>
