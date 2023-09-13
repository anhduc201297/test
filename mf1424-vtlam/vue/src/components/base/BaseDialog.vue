<template>
    <div class="dialog">
        <div class="dialog__container" :style="{'width': width + 'px'}">
            <div class="dialog__header">
                <div class="dialog__header--left">
                    <slot name="header"></slot>
                </div>
                <div class="dialog__header--right">
                    <div class="dialog__icon">
                        <div class="icon icon-close" title="Thoát" @click="onClose"></div>
                    </div>
                </div>
            </div>
            <div class="dialog__body">
                <slot name="body"></slot>
            </div>
            <div class="dialog__footer">
                <slot name="footer"></slot>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name:"BaseDialog",
    props:['width'],
    methods:{

        /**
         * Đóng form
         * Author: VTLam (24/08/2023)
         */
        onClose(){
            this.$emit("onClose");
        }
    }
}
</script>