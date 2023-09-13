<template>
    <!-- <div class="modal"> -->
        <div class="modal-content" :style="width!=='unset'?{width: width}:{}">
            <div class="modal-title-bar">
                <span title="Trợ giúp" class="help-btn"></span>
                <span @click="closeDialog" title="Đóng" class="close-btn"></span>
            </div>
            <div class="modal-header">
                <h3>{{ title }}</h3>
                <slot name="header-options"></slot>
            </div>
            <slot name="body"></slot>
            <div class="modal-footer">
                <slot name="footer"></slot>
            </div>
        </div>
    <!-- </div> -->
</template>

<script>
export default {
    name: "m-dialog",
    emits: ['onClose'],
    props: {
        title: String,
        width: {
            type: String,
            default: 'unset',
        }
    },
    methods: {
        /**
         * Đóng dialog
         * Author: dktuan (19/08/2023)
         */
        closeDialog() {
            this.$emit('onClose');
        }
    },
}
</script>

<style scoped>
@import url(./modal.css);
</style>