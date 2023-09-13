<template>
    <div class="m-input-wrap">
        <label
            v-if="label"
            @click="autoFocus"
            class="m-label"
            :title="tooltip ? tooltip : ''"
        >
            {{ label }}
            <span v-if="required" class="text-red text-bold">*</span>
        </label>
        <input
            class="m-input"
            ref="inputField"
            v-model="localValue"
            :type="inputType"
            :class="[`m-input-${type}`]"
            :style="{ color: this.localValue && '#1f1f1f' }"
            :tabindex="tabindex"
            @input="handleInput"
            @focus="select"
            v-bind="$attrs"
        />
        <div
            :style="{ visibility: errorMessage ? 'visible' : 'hidden' }"
            class="m-input-danger--info"
            >{{ errorMessage || '...' }}</div
        >
    </div>
</template>

<script>
import validateRules from '../../../js/utils/validateForm';
export default {
    name: 'MInputText',
    props: {
        tabindex: String,
        label: String,
        required: Boolean,
        tooltip: String,
        inputType: {
            type: String,
            validator: (value) => {
                return ['date', 'email', 'text'].includes(value);
            },
            default: 'text'
        },
        modelValue: String,

        rules: Array // các luật validate
    },
    data() {
        return {
            debounceValidate: null,
            localValue: null,
            type: null,
            errorMessage: ''
        };
    },
    /**
     * Thực hiện 2-way binding và gán id nếu có
     * @Author NTVu - MF1742
     * Modified: NTVu - 19/08/2023
     */
    created() {
        this.localValue = this.modelValue;
    },
    methods: {
        /**
         * Bôi đen giá trị trong input
         * @Author NTVu - MF1742
         * Modified: NTVu - 07/09/2023
         */
        select() {
            this.$refs.inputField.select();
        },
        /**
         * Focus vào input hiện tại
         * @Author NTVu - MF1742
         * Modified: NTVu - 19/08/2023
         */
        focus() {
            this.$refs.inputField.focus();
        },
        /**
         * Focus và select vào input
         * @Author NTVu - MF1742
         * Modified: NTVu - 19/08/2023
         */
        autoFocus() {
            this.focus();
            this.select();
        },
        /**
         * Hiển thị lỗi trên input
         * @Author NTVu - MF1742 (20/08/2023)
         * @param {String} message
         * @requires message
         */
        onShowError(message) {
            this.errorMessage = message;
            this.type = 'danger';
        },
        /**
         * Hiển thị input đã đúng tất cả các rules
         * @Author NTVu - MF1742 (20/08/2023)
         */
        onValid() {
            this.type = null;
            this.errorMessage = null;
        },
        /**
         * Validate dữ liệu input hiện tại
         * @Author NTVu - MF1742 (20/08/2023)
         * @param {Boolean} toFocus - Nếu toFocus = true thì khi dữ liệu không hợp lệ, input sẽ tự động được focus
         * @returns {Number} 0 - Dữ liệu ko hợp lệ, 1 - Dữ liệu hợp lệ
         */
        validate(toFocus = false) {
            // Nếu input không required, nếu ko có value => ko validate
            if (!this.localValue && !this.required) {
                this.onValid();
                return 1;
            }

            // Nếu input required, validate required
            if (this.required) {
                if (!validateRules.required(this.localValue)) {
                    this.onShowError(
                        this.$Resource.errorFieldEmpty(this.label)
                    );
                    if (toFocus) this.autoFocus();
                    return 0;
                }
            }

            // Validate các luật còn lại.
            if (this.rules && this.rules.length > 0) {
                for (const rule of this.rules) {
                    const infoRule = rule.split('|');

                    if (
                        !validateRules[infoRule[0]](
                            this.localValue,
                            infoRule[1]
                        )
                    ) {
                        if (toFocus) this.autoFocus();
                        if (infoRule[2]) {
                            this.onShowError(infoRule[2]);
                            return 0;
                        } else {
                            this.onShowError(this.$Resource.DataNotValid);
                            return 0;
                        }
                    }
                }
            }

            this.onValid();
            return 1;
        },
        /**
         * Validate input khi người dùng không nhập gì trong 500ms
         * @param {Event} e
         * @Author NTVu - MF1742 (20/08/2023)
         * Modified: NTVu - 21/08/2023: Áp dụng hàm debounce
         */
        handleInput(e) {
            if (!this.debounceValidate) {
                this.debounceValidate = this.$Helper.debounce(
                    this.validate,
                    500
                );
            }
            this.debounceValidate();
            this.$emit('update:modelValue', e.target.value);
        }
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
