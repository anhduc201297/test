<template>
    <div class="input" :class="inputWidth" >
        <label v-if="inputLabel" class="input__label" :title="labelTooltip">
            {{inputLabel}}
            <span v-if="inputType=='required'" class="require-mark">*</span>
        </label>
        <div class="input__field-and-icon">
            <input
            ref="input" 
            :value="modelValue" 
            :class="['input__field',{'input--error':isError}]"
            :tabindex="tabIndex"
            :placeholder="placeHolder"
            :readonly="isReadOnly"
            :title="errorMess"
            :type="inputType"
            @blur="validateInputValue"
            @input="onInput"
            >
            <font-awesome-icon v-if="icon" :icon="icon" :class="['input__icon',iconSize ? iconSize : 'icon-20']" @click="onClick"/>
        </div>
        <span v-if="isError" class="input__error-mess">{{errorMess}}</span>
    </div>
</template>

<script>
export default {
    name:"BaseInput",
    props:["inputLabel","errorMess","inputWidth","modelValue","inputType","tabIndex","placeHolder","icon","iconSize","labelTooltip","isReadOnly"],
    emits:['update:modelValue'],
    methods: {

        /**
         * click vào icon của input
         * @param {*} e
         * Author: Vũ Tùng Lâm (7/9/2023)
         */
        onClick(){
            this.$emit('onClick');
        },

        /**
         * trả giá trị input
         * @param {*} e
         * Author: Vũ Tùng Lâm (20/08/2023)
         */
        onInput(e){
            const me = this;
            me.$emit('update:modelValue',e.target.value);
            me.validateInputValue(e);
        },

        /**
         * validate input
         * @param {*} e
         * Author: Vũ Tùng Lâm (20/08/2023)
         */
        validateInputValue(e){
            const me=this;
            let value = e.target.value;
            if(me.inputType=="required"){
                me.isError = value ? false : true 
            }
            if(me.inputType=="email"){
                let emailFormat = /^([a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+)@([a-zA-Z0-9-]+)((\.[a-zA-Z0-9-]{2,3})+)$/;
                me.isError = value ? (value.match(emailFormat) ? false : true) : false;
            }
            // if(me.inputType == "date"){
            //     let date = value ? (new Date(value)).toISOString().split('T')[0] : null
            //     let currentDate = (new Date()).toISOString().split('T')[0];
            //     console.log(date,currentDate,date < currentDate);
            //     me.isError = value ? (date < currentDate ? false : true) : false;
            // }
            
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

    },
    data() {
        return {
            isError:false
        }
    },
}
</script>