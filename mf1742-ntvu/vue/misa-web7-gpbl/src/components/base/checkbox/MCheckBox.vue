<template>
    <div
        @click="onToggleCheckBox"
        class="m-checkbox"
        :class="{ 'm-checkbox-label': label }"
    >
        <input v-model="localValue" type="checkbox" hidden />
        <span
            class="checkbox-display color-grey-400"
            :class="{ checked: localValue }"
        >
        </span>
        <span>{{ label ? label : '' }}</span>
    </div>
</template>

<script>
export default {
    name: 'MCheckBox',

    props: {
        label: String,
        modelValue: [Boolean, Number] // giá trị model (2-way-binding)
    },
    data() {
        return {
            localValue: null // giá trị model tại local (2-way-binding)
        };
    },

    methods: {
        /**
         * Thay đổi value local
         * @author NTVu - 20/08/2023
         */
        onToggleCheckBox() {
            this.localValue = !this.localValue;
            this.$emit('update:modelValue', this.localValue);
        }
    },
    watch: {
        /**
         * lắng nghe thay đổi của modelValue để cập nhật lên localValue
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
@import url('./checkbox.css');
</style>
