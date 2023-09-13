<script setup>
import { IconClose } from '@/assets/icons';
import { VIcon } from '@/components';
import { useDialogStore } from '@/stores/dialog';

const Dialog = useDialogStore();
</script>

<template>
    <div class="dialog">
        <div class="dialog-container">
            <div class="dialog-wrapper">
                <div class="dialog-header">
                    <div class="heading-1">
                        <slot name="title"></slot>
                    </div>

                    <IconClose class="close-icon" @click="Dialog.hide" />
                </div>
                <div class="dialog-content">
                    <div class="icon-warning">
                        <VIcon class="warning-img" />
                    </div>
                    <div class="dialog-text">
                        <slot name="text"></slot>
                    </div>
                </div>
            </div>
            <div class="dialog-footer">
                <slot name="secondaryAction"></slot>
                <slot name="primaryAction"></slot>
            </div>
        </div>
    </div>
</template>

<style lang="scss" scoped>
@import '@/styles/mixins.scss';
.dialog {
    position: fixed;
    top: 0;
    bottom: 0;
    right: 0;
    left: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 100;
    background-color: var(--modal-background);
}

.dialog-container {
    max-width: 480px;
    width: 100%;
    display: flex;
    flex-direction: column;
    gap: 20px;
    padding: 24px;
    border-radius: 4px;
    background-color: rgb(var(--c-white));
    .dialog-wrapper {
        display: flex;
        flex-direction: column;
        justify-content: center;
        gap: 24px;
    }
}

.dialog-content {
    display: flex;
    align-items: center;
    gap: 20px;
    .icon-warning {
        @include size(40px);
        .warning-img {
            width: 36px;
            height: 37px;
            background-position: -598px -463px;
        }
    }
    .dialog-text {
        @include font(14);
    }
}

.dialog-header {
    display: flex;
    justify-content: space-between;
    align-items: center;

    .close-icon {
        @include size(24px);
        transform: scale(20/24);
        cursor: pointer;
    }
}

.dialog-footer {
    display: flex;
    justify-content: flex-end;
    gap: 8px;
}
</style>
