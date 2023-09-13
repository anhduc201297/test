<template>
    <div id="app">
        <the-sidebar
            :isOpenSidebar="isOpenSidebar"
            @onOpenSidebar="onOpenSidebar"
        />
        <div
            id="main"
            :style="{
                'max-width': isOpenSidebar
                    ? 'calc(100vw - 200px)'
                    : 'calc(100vw - 73px)'
            }"
        >
            <the-header
                :isOpenSidebar="isOpenSidebar"
                @onCloseSidebar="onCloseSidebar"
            />
            <the-container>
                <router-view />
            </the-container>
        </div>
        <!-- Toasts -->
        <div class="toast_container">
            <m-toast ref="toast1" />
            <m-toast ref="toast2" />
            <m-toast ref="toast3" />
        </div>
        <!-- Dialog Error -->
        <m-dialog
            v-if="isShowDialogError"
            :title="dialogErrorTitle"
            :content="dialogErrorContent"
            type="Error"
            @clickCancel="onCloseDialogError"
        />
    </div>
</template>

<script>
import TheSidebar from './components/layouts/theSidebar/TheSidebar.vue';
import TheContainer from './components/layouts/theContainer/TheContainer.vue';
import TheHeader from './components/layouts/theHeader/TheHeader.vue';
import MToast from './components/base/toast/MToast.vue';
import MDialog from './components/base/dialog/MDialog.vue';

export default {
    name: 'App',
    components: {
        TheSidebar,
        TheContainer,
        TheHeader,
        MToast,
        MDialog
    },
    data() {
        return {
            isLoading: false,
            isOpenSidebar: true,
            isShowDialogError: false,
            dialogErrorTitle: '',
            dialogErrorContent: '',

            onCloseDialog: null
        };
    },
    methods: {
        /**
         * Rút gọn sidebar
         * @Author NTVu - MF1742 - 19/08/2023
         */
        onCloseSidebar() {
            this.isOpenSidebar = false;
        },
        /**
         * Mở rộng sidebar
         * @Author NTVu - MF1742 - 19/08/2023
         */
        onOpenSidebar() {
            this.isOpenSidebar = true;
        },
        /**
         * Mở toast message
         * @Author NTVu - MF1742 - 23/08/2023
         * @param {{title: string, type: Enum, data: object}}
         */
        onOpenToast({ title, type, data }) {
            for (let i = 3; i >= 1; i--) {
                if (!this.$refs[`toast${i}`].isShow) {
                    this.$refs[`toast${i}`].onOpen({ title, type, data });
                    break;
                }
            }
        },
        /**
         * Mở Dialog Error
         * @Author NTVu - MF1742 - 23/08/2023
         * @param {{title: string, content: string}}
         * @param {Function} onClose - optional callback function
         */
        onOpenDialogError({ title, content }, onClose) {
            this.dialogErrorTitle = title;
            this.dialogErrorContent = content;
            if (onClose) this.onCloseDialog = onClose;
            this.isShowDialogError = true;
        },
        /**
         * Đóng dialog error
         * @Author NTVu - MF1742 - 23/08/2023
         */
        onCloseDialogError() {
            this.isShowDialogError = false;
            if (this.onCloseDialog) {
                this.onCloseDialog();
                this.onCloseDialog = null;
            }
        }
    },
    /**
     * Cung cấp hàm rút gọn sidebar cho các element con
     * @Author NTVu - MF1742 - 19/08/2023
     */
    provide() {
        return {
            onOpenToast: this.onOpenToast,
            onOpenDialogError: this.onOpenDialogError
        };
    }
};
</script>

<style lang="css">
@import url('../src/css/main.css');
</style>
