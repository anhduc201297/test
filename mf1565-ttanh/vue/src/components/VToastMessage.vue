<script setup>
import { computed } from 'vue';

import { useToastMessageStore } from '@/stores/toastMessage';
import { ToastMessageType } from '@/utils/enums';
import VIcon from './VIcon.vue';

const props = defineProps({
    type: {
        type: Symbol,
        required: true,
        default: ToastMessageType.Warning
    }
});

const ToastMessage = useToastMessageStore();

const typeMappings = {
    [ToastMessageType.Success]: {
        iconImgClass: 'success-img',
        title: 'Thành công!',
        titleClass: ['title', 'title-success']
    },
    [ToastMessageType.Error]: {
        iconImgClass: 'error-img',
        title: 'Lỗi!',
        titleClass: ['title', 'title-error']
    },
    [ToastMessageType.Warning]: {
        iconImgClass: 'warning-img',
        title: 'Cảnh báo!',
        titleClass: ['title', 'title-warning']
    },
    [ToastMessageType.Info]: {
        iconImgClass: 'info-img',
        title: 'Thông tin!',
        titleClass: ['title', 'title-info']
    }
};

const typeMapping = typeMappings[props.type] || typeMappings[ToastMessageType.Warning];
const iconImgClass = computed(() => typeMapping.iconImgClass);
const title = computed(() => typeMapping.title);
const titleClass = computed(() => typeMapping.titleClass);
</script>

<template>
    <div class="toast-message">
        <div class="content">
            <div class="message-icon">
                <VIcon :class="iconImgClass" />
            </div>
            <div :class="titleClass">{{ title }}&nbsp;</div>
            <div class="dialog-text">
                {{ ToastMessage.current.text }}
            </div>
        </div>
        <div class="icon-close" @click="ToastMessage.hide">
            <VIcon class="close-img" />
        </div>
    </div>
</template>

<style lang="scss" scoped>
@import '@/styles/mixins.scss';

.toast-message {
    @include centerAbsolute('left');
    top: 40px;

    max-width: 500px;
    width: 100%;
    height: 56px;

    display: flex;
    align-items: center;
    justify-content: space-between;

    padding: 0 16px;
    border-radius: 4px;

    background-color: rgb(var(--c-white));
    filter: drop-shadow(0 4px 40px rgba(0, 0, 0, 0.078));
    z-index: 200;
    .content {
        display: flex;
        align-items: center;
    }
}

.title {
    font-weight: 500;
    &.title-success {
        color: rgb(var(--c-primary));
    }
    &.title-error {
        color: #de3618;
    }
    &.title-warning {
        color: #f49342;
    }
    &.title-info {
        color: #009eeb;
    }
}

.message-icon {
    @include size(24px);
    margin-right: 8px;
    & > * {
        transform: translate(-50%, -50%) scale(calc(24 / 36));
    }
    .success-img {
        width: 36px;
        height: 36px;
        background-position: -990px -462px;
    }
    .error-img {
        width: 36px;
        height: 36px;
        background-position: -752px -462px;
    }
    .warning-img {
        width: 36px;
        height: 37px;
        background-position: -598px -463px;
    }
    .info-img {
        width: 36px;
        height: 36px;
        background-position: -832px -462px;
    }
}

.icon-close {
    @include size(16px);
    cursor: pointer;
    .close-img {
        @include size(18px);
        transform: translate(-50%, -50%) scale(calc(12 / 18));
        background-position: -147px -147px;
    }
}
</style>
