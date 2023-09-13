<template>
    <!-- eslint-disable -->
    <div class="flex">
        <h4 :data-tooltip="tooltip" style="margin-bottom: 2px;">{{ text }}</h4>
        <h4 v-if="isRequire" style="color: red">&nbsp;*</h4>
    </div>
    <input :class="{ 'error': hasError&&isRequire }" ref="input" :type="type" class="input w-100" v-model="inputValue"
        @blur="checkInput" />
</template>

<script>
export default {
    name: "MISAInputFull",
    props: {
        type: {
            type: String
        },
        isRequire: {
            type: Boolean,
            default: false,
        },
        text: {
            type: String,
        },
        modelValue: {
            type: String,
        },
        tooltip: {
            type: String,
        }
    },
    data() {
        return {
            inputValue: "",
            hasError: false,
        }
    },
    watch: {
        inputValue: function (newValue) {
            /**
             * Cập nhật giá trị inputValue từ modelValue
             * AUTHOR: DTTHANH (21/8/2023)
             */
            this.$emit("update:modelValue", newValue);
        },
        modelValue: function(newValue) {
            this.inputValue = newValue;
        }
    },
    created() {
        /**
         * Gán giá trị inputValue
         * AUTHORL: DTTHANH (21/08/2023)
         */
        this.inputValue = this.modelValue;
    },
    methods: {
        /**
         * Focus cho ô input
         * AUTHOR: DTTHANH (21/08/2023)
         */
        onFocus() {
            this.$refs.input.focus();
        },

        /**
         * Phương thức kiểm tra ô input có trống hay không
         * AUTHOR: DTTHANH (24/08/2023) 
         */
        checkInput() {
            if (!this.inputValue) {
                this.hasError = true;
            } else {
                this.hasError = false;
            }
        }
    },
}
</script>

<style scoped>
.error {
    border: 1px solid #e74c3c;
}
</style>