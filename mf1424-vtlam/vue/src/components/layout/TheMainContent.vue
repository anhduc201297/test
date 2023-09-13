<template>
    <div class="main-content">
        <router-view></router-view>
    </div>
    <base-toast v-if = "isShowToast" @onClose = "closeToast" :type="toastType" :message="toastMessage"/>
</template>

<script>
export default {
    name:"TheMainContent",
    created(){
        this.$emitter.on("showToast",this.showToast)
    },
    beforeUnmount() {
        this.$emitter.off("showToast",this.showToast)
    },

    methods:{

        /**
         * Mở thông báo
         * @param {*} toastType 
         * @param {*} toastMessage 
         * CreatedBy: VTLam (31/08/2023)
         */
        showToast(toastType,toastMessage){
            this.toastType=toastType;
            this.toastMessage=toastMessage;
            this.isShowToast=true;
        },
        
        /**
         * Đóng thông báo
         * CreatedBy: VTLam (31/08/2023)
         */
        closeToast(){
            this.isShowToast=false;
        }
    },

    data(){
        return {
            toastMessage:"",
            toastType: this.$enums.Toast.Info,
            isShowToast:false,
        }
    }
    
}
</script>