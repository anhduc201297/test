<template>
    <div class="label">
        <label v-if="label" :title="labelTitle" class="label-name" for="">{{ label }}</label>
        <div v-if="isRequired" class="required">*</div>
    </div>
    <input ref="input" :value="modelValue" @input="updateModelValue" required=isRequired class="text-input" :type="inputType" :name="name">

</template>

<script>
export default {
    emits: ['update:modelValue'],
    props: {
        label: String,
        labelTitle: String,
        isRequired: Boolean,
        inputType: {
            type: String,
            default: 'text',
        },
        placeHolder: String,
        name: String,
        data: null,
        modelValue: null,
    },
    methods: {
        /**
         * Two ways binding input
         * Author: dktuan (19/08/2023)
         */
        updateModelValue(event) {
            this.$emit('update:modelValue', event.target.value);
        },
        /**
         * focus input
         * Author: dktuan (24/08/2023)
         */
        focus() {
            this.$refs.input.focus();
        }
    },
}
</script>

<style scoped>
    @import url(./text.css);
    @import url(./radio.css);
    input {
        font-family: NotoRegular;
    }
</style>