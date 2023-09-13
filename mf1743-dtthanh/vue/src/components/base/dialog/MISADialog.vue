<template>
    <div class="dialog">
        <div class="dialog-box">
            <div class="dialog-header center">
                <h1>{{ title }}</h1>
                <m-icon @click="onClose" icon="close"></m-icon>
            </div>
            <div class="dialog-content center">
                <div :class="[
                    'icon',
                    { 'icon--error': type == $enum.DialogType.Error },
                    { 'icon--warning': type == $enum.DialogType.Warning },
                    { 'icon--question': type == $enum.DialogType.Question },
                ]">
                </div>
                <div class="dialog-text">
                    <li style="list-style: none" v-for="(msg, index) in message" :key="index">
                        - {{ msg }}
                    </li>
                </div>
            </div>
            <div class="dialog-footer-1">
                <m-button @click="onClose" :text="this.$resources[this.$LangCode].Button.Close" class="btn"></m-button>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "MISADialog",
    props: {
        title: {
            type: String
        },
        message: {
            type: String
        },
        type: {
            type: String
        }
    },
    methods: {
        /**
         * Phương thức đóng dialog
         * AUTHOR: DTTHANH (20/08/2023)
         */
        onClose() {
            this.$emit("onClose")
        }
    }
}
</script>

<style scoped>
@import url(./dialog.css);
</style>