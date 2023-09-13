<template>
    <div class="radio-item">
        <input
            type="radio"
            hidden
            :name="name"
            :id="id"
            :value="value"
            :checked="modelValue.toString() === value.toString()"
            v-model="localValue"
            v-bind="$attrs"
        />

        <m-button-icon
            :tabindex="tabindex"
            @click="setLocalValue"
            class="icon-radio-unchecked icon-24 m-btn--link focus-outline"
            >{{ label }}</m-button-icon
        >
    </div>
</template>
<script>
import MButtonIcon from '../button/MButtonIcon.vue';

export default {
    name: 'MRadio',
    components: {
        MButtonIcon
    },
    props: {
        label: String,
        modelValue: [String, Number, Boolean],
        name: String,
        tabindex: [String, Number],
        id: String,
        value: [String, Number]
    },
    data() {
        return {
            localValue: null
        };
    },
    /**
     * Gán giá trị local Value là model value
     * @Author NTVu - MF1742 - 19/08/2023
     */
    created() {
        this.localValue = this.modelValue || '';
    },
    methods: {
        /**
         * Gán giá trị local Value là value của radio
         * @Author NTVu - MF1742 - 19/08/2023
         */
        setLocalValue() {
            this.localValue = this.value;
            this.$emit('update:modelValue', this.value);
        }
    },
    watch: {
        /**
         * Lắng nghe thay đổi của model value => set value cho local value
         * @Author NTVu - MF1742 - 19/08/2023
         */
        modelValue() {
            this.localValue = this.modelValue;
        }
    }
};
</script>
<style scoped>
@import url('./input.css');
</style>
