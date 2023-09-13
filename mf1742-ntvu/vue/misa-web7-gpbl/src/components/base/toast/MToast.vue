<template>
    <div
        v-if="isShow"
        class="toast-wrapper"
        :style="{ width: title.length > 30 ? '650px' : '450px' }"
    >
        <div class="toast__icon">
            <m-btn-no-text
                class="lg icon-32"
                :class="this.$Resource.ToastType[type].Icon"
            />
        </div>

        <div class="toast__content">
            <div>
                <span
                    :style="{
                        color: this.$Resource.ToastType[type].Color
                    }"
                    >{{ this.$Resource.ToastType[type].Label }}!
                </span>
                <span class="toast__title"> {{ title }} </span>
            </div>
            <span
                v-if="this.$Resource.ToastType[type].LabelAction"
                class="toast__content__action"
                @click="onOpenDetails"
            >
                {{ this.$Resource.ToastType[type].LabelAction }}</span
            >
        </div>
        <div @click="isShow = false" class="toast__close">
            <m-btn-no-text class="icon-close icon-16 lg" />
        </div>
        <div
            :style="{ 'animation-duration': `${timeout}ms` }"
            class="timeout line-timeout"
        ></div>
    </div>
</template>

<script>
export default {
    name: 'MToast',
    inject: ['onOpenDialogError'],
    data() {
        return {
            isShow: false,
            timeout: 5000,
            countDown: null,
            title: '',
            type: 0,
            onOpenDetails: null
        };
    },
    methods: {
        /**
         * Mở toast với message, kiểu được truyền vào
         * @Author NTVu - 20/08/2023
         * @param {{title: string, type: string, data: object}}
         * type in ['Success', 'Info', 'Warning', 'Danger']
         */
        onOpen({ title, type, data, ...args }) {
            if (data) {
                this.onOpenDetails = () =>
                    this.onOpenDialogError({
                        content: JSON.stringify(data),
                        title: this.$Resource.Error
                    });
            }
            this.title = title;
            this.type = type;
            this.timeout = args.timeout || this.timeout;
            this.isShow = true;
            this.countDown = setTimeout(this.onClose, this.timeout);
        },
        /**
         * Đóng toast
         * @Author NTVu - 20/08/2023
         */
        onClose() {
            this.isShow = false;
            clearTimeout(this.countDown);
        }
    }
};
</script>

<style scoped>
@import url('./toast.css');
</style>
