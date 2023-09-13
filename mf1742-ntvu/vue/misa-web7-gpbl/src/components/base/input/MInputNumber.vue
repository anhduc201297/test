<template>
    <div class="m-input-wrap m-input-number-wrap">
        <label
            v-if="label"
            :for="localId"
            class="m-label"
            :title="tooltip ? tooltip : ''"
        >
            {{ label }}
            <span v-if="required" class="text-red text-bold">*</span>
        </label>
        <input
            v-model="localValue"
            :id="localId"
            @input="handleInput"
            type="text"
            class="m-input m-input-number"
            :class="[`m-input-${type}`]"
            ref="inputField"
            :tabindex="tabindex"
        />
        <div class="m-input-danger--info">{{ errorMessage }}</div>
    </div>
</template>
<script>
export default {
    name: 'MInputNumber',
    props: {
        tabindex: [String, Number],
        label: String,
        required: Boolean,
        tooltip: String,
        modelValue: String,
        id: String,
        rules: Array
    },
    data() {
        return {
            localId: Math.ceil(Math.random() * 10000) + 'ID', // tạo id local ngẫu nhiên
            localValue: null,
            type: null,
            errorMessage: ''
        };
    },
    created() {
        this.localValue = this.modelValue;
        if (this.id) {
            this.localId = this.id;
        }
    },
    methods: {
        /**
         * Xử lí dữ liệu khi được nhập, sau đó emit lên để cập nhật model value
         * @Author NTVu - MF1742 (20/08/2023)
         */
        handleInput() {
            // Loại bỏ tất cả các ký tự không phải số hoặc dấu chấm thập phân
            this.localValue = this.localValue.replace(/[^0-9,]/g, '');

            // Đảm bảo chỉ có một dấu chấm thập phân được phép
            const decimalCount = (this.localValue.match(/,/g) || []).length;
            if (decimalCount > 1) {
                this.localValue = this.localValue.slice(0, -1);
            }

            // Đảm bảo ký tự đầu tiên là số hoặc dấu trừ (âm)
            if (!/^[-0-9]/.test(this.localValue)) {
                this.localValue = '';
            }
            const formatted = this.localValue.replace(
                /\B(?=(\d{3})+(?!\d))/g,
                '.'
            );

            this.$emit('update:modelValue', formatted);
        }
    },
    watch: {
        /**
         * Lắng nghe model value thay đổi, loại bỏ hết dấu '.' trong modelValue
         * @Author NTVu - MF1742 (20/08/2023)
         */
        modelValue() {
            this.modelValue.replaceAll('.', '');
            this.localValue = this.modelValue;
        }
    }
};
</script>
<style scoped>
@import url('./input.css');
</style>
