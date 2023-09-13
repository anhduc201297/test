<template>
    <!-- Input Text -->
    <div class="subcontent__form-input">
        <div class="subcontent__label">
          <label class="tooltip subcontent__name"  v-if="showLabel">
            {{label}}
            <span class="subcontent__required" v-if="required">*</span>
            <span v-if="isInputEmpty" class="error-message" :style="errorStyle">{{errorMessage}}</span>
            <MISATooltip v-if="showTooltip" :tooltip="tooltip"></MISATooltip>
          </label>
        </div>
        <input 
            :class="{ 'error': isInputEmpty }"    
            type="text"
            :id="id"
            :style="style"
            :name=" name"
            class="input__text"
            v-model="inputValue"
            v-bind="$attrs"
            :ref="ref"
            @input="handleInput()"
        />
    </div>
</template>
  
<script>
import MISATooltip from '../MISATooltip.vue';

export default {
    name: 'MISAInputText',
    props: ["label", "id", "style", "name", "showLabel", "required","tooltip", "showTooltip", "errorStyle","value",],
    data() {
        return {
            inputValue: null ,
            isInputEmpty: false
        };
    },
    components: { MISATooltip },
    methods: {
        handleInput() {
            this.isInputEmpty = this.required && this.inputValue.trim() === "";
            this.$emitter.emit('inputChanged', this.inputValue); 
        },
    },

     /**
    * Hàm hiển thị message nếu input bắt buộc nhập bị trống
    * CreateBy : NTKMY (30/08/2023)
    */
    computed:{
        errorMessage() {
            return this.label + " không để trống"
        }
    },

    /**
     * Thực hiện 2-way binding
     * CreateBy : NTKMY (30/08/2023)
     */
    created() {
        this.inputValue = this.value;
    },
    /**
     * Cập nhập giá trị cho input
     * CreateBy : NTKMY (30/08/2023)
     */
    watch: {
        inputValue: function (newValue) {
            this.$emit('update:value', newValue);
        },
        value: function () {
            this.inputValue = this.value;
        },
    },
};
</script>

<style>
@import url('../../../css/base/input.css');
@import url('../../../css/base/tooltip.css');
</style>
  