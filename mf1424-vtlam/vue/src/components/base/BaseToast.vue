<template>
    <div class="toast">
        <div class="toast__content">
            <div class="toast__type">
                <font-awesome-icon class="toast__icon" icon="circle-check" :style="{'color': toastColor}"/>
                <div :style="{'color': toastColor}" class="toast__title">{{ toastTitle }}</div>
            </div>
            <div class="toast__text">{{message}}</div>
        </div>
        <font-awesome-icon class="toast__icon--close" icon="xmark" @click="onClose"/>
    </div>
</template>

<script>
export default {
    name:"BaseToast",
    props:["message","type"],
    created() {
        const me = this;
        me.toastType();
        setTimeout(function(){
            me.onClose();
        }, 5500)
    },
    methods: {

        /**
         * Đóng toast message
         * Author: VTLam (24/08/2023)
         */
        onClose(){
            this.$emit("onClose");
        },

        /**
         * Style cho toast message
         * Author: VTLam (24/08/2023)
         */
        toastType(){
            if(this.type==this.$enums.Toast.Success){
                this.toastTitle = this.$resource[this.$LangCode].Success;
                this.toastColor = "green";
            }else if(this.type==this.$enums.Toast.Error){
                this.toastTitle = this.$resource[this.$LangCode].Error;
                this.toastColor = "red";
            }else if(this.type==this.$enums.Toast.Alert){
                this.toastTitle = this.$resource[this.$LangCode].Alert;
                this.toastColor = "orange";
            }
        }
    },
    data() {
        return {
            toastTitle:this.$resource[this.$LangCode].Info,
            toastColor:"blue"
        }
    },
}
</script>