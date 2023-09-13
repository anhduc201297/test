<template>
    <div class="checkbox">
        <input ref="input" class="checkbox__field" :type="type ? type : 'checkbox'" :value="value" :name="name" 
        :checked="checked" :tabindex="tabIndex" @change="onChange">
        <label v-if="checkBoxLabel" class="checkbox__label">{{ checkBoxLabel }}</label>
    </div>
</template>

<script>
export default {
    name:"BaseCheckBox",
    props:['checkBoxLabel','tabIndex',"type","name","value","checked"],
    methods:{

        /**
         * Khi click vào checkbox
         * Author :VTLam(4/9/2023)
         */
        onChange(){
            this.$emit("onChange")
        },

        /**
         * focus vào input field
         * Author :VTLam(4/9/2023)
         */
         focusInput(){
            this.$nextTick(()=>{
                this.$refs.input.focus();
            });
        }
    }
}
</script>