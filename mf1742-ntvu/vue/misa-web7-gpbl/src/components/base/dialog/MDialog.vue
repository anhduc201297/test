<template>
    <div
        @click.prevent="handleClickOut"
        class="dialog-wrapper"
        @keydown="handleKeyDown"
    >
        <div ref="dialog" class="dialog-notify">
            <div class="dialog-header">
                <h1 class="dialog__title">{{ title }}</h1>
                <div class="pop-head-btns">
                    <m-btn-no-text
                        title="Đóng ( ESC )"
                        class="lg icon-24 icon-close"
                        @click="$emit('clickCancel')"
                    />
                </div>
            </div>
            <div class="dialog-body">
                <div class="noti-icon" :class="DialogType[type].icon"> </div>
                <div class="dialog-content">{{ content }} </div>
            </div>

            <div v-if="type !== $MEnum.DialogType.Error" class="dialog-footer">
                <div class="btns-left">
                    <m-button
                        tabindex="1"
                        ref="cancelBtn"
                        v-if="DialogType[type].leftBtn"
                        @click="$emit('clickCancel')"
                        class="text-bold focus-outline"
                        :type="'sub'"
                        >{{ this.$Resource.CancelText }}</m-button
                    >
                </div>
                <div class="btns-right">
                    <m-button
                        tabindex="1"
                        v-if="DialogType[type].rightBtn"
                        @click="$emit('clickSub')"
                        type="sub"
                        class="text-bold focus-outline"
                        >{{ this.$Resource.NoText }}</m-button
                    >
                    <m-button
                        tabindex="1"
                        ref="okBtn"
                        @click="$emit('clickOk')"
                        @keydown.tab.prevent="handleTabOnOk"
                        class="text-bold focus-outline"
                        >{{ this.$Resource.OkText }}</m-button
                    >
                </div>
            </div>
            <div v-else class="dialog-footer--error">
                <m-button
                    tabindex="1"
                    @keydown.tab.prevent="handleTabOnOk"
                    @click="$emit('clickCancel')"
                    ref="okBtn"
                    class="text-bold focus-outline"
                    >{{ this.$Resource.CloseText }}</m-button
                >
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'MDialog',
    props: {
        title: String,
        content: String,
        type: {
            type: String,
            validator: (value) => {
                return [
                    'Success',
                    'Info',
                    'Warning',
                    'Question',
                    'Error'
                ].includes(value);
            },
            default: 'Success'
        }
    },
    data() {
        const DialogType = {
            Success: { icon: 'icon-check-no-bg' },
            Info: { icon: 'icon-info' },
            Warning: { icon: 'icon-warning-48', leftBtn: true },
            Question: { icon: 'icon-ask-blue', leftBtn: true, rightBtn: true },
            Error: { icon: 'icon-fail' }
        };
        return {
            refIndex: 0,
            DialogType
        };
    },
    methods: {
        /**
         * Kiểm soát click ra khỏi dialog
         * @param {Event} e
         * @author NTVu - 11/09/2023
         */
        handleClickOut(e) {
            const target = e.target;

            if (target.contains(this.$refs.dialog)) {
                this.$refs.okBtn.focus();
            }
        },
        /**
         * Focus vào lại nút cancel khi đã tab đến nút OK
         * @author NTVu - 06/09/2023
         */
        handleTabOnOk() {
            this.$refs.cancelBtn?.focus();
        }
    },
    /**
     * Thực hiện focus vào các nút điều khiển dialog
     * @author NTVu - 30/08/2023
     */
    mounted() {
        this.$refs['okBtn'].focus();
    }
};
</script>

<style scoped>
@import url('./dialog.css');
</style>
