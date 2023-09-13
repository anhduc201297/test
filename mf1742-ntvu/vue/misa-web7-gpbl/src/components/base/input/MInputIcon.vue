<template>
    <div class="m-input-wrap m-input-with-icon">
        <input
            v-model="localValue"
            v-bind="$attrs"
            type="text"
            class="m-input"
            @input="handleInput"
        />
        <m-btn-no-text class="lg icon-20" :class="icon" />
    </div>
</template>
<script>
export default {
    name: 'MInputIcon',
    props: {
        icon: String,
        modelValue: String
    },
    data() {
        return {
            localValue: null
        };
    },
    methods: {
        /**
         * Lắng nghe thay đổi của local value => update the model value
         * @Author NTVu - MF1742
         * Modified: NTVu - 19/08/2023
         * @param {Event} e
         */
        handleInput(e) {
            this.$emit('update:modelValue', e.target.value);
        }
    },
    /**
     * Thực hiện 2-way binding
     * @Author NTVu - MF1742
     * Modified: NTVu - 19/08/2023
     */
    created() {
        this.localValue = this.modelValue || '';
    },
    watch: {
        /**
         * Lắng nghe thay đổi của model value => update the local value
         * @Author NTVu - MF1742
         * Modified: NTVu - 19/08/2023
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
