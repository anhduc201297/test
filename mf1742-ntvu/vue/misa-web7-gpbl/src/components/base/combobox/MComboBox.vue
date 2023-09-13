<template>
    <label @click="autoFocus" v-if="label" class="m-label">
        {{ label }}
        <span v-if="required" class="text-red">*</span>
    </label>
    <div
        class="combo-box-wrapper"
        :style="{ color: focusColor }"
        @keydown="onInputKeyDown"
        tabindex="0"
        :class="[{ isClose: !isOpen }, { isDropHover: isDropHover || isOpen }]"
    >
        <input
            ref="inputField"
            type="text"
            class="m-input"
            :disabled="isDisabledInput"
            :class="[
                { 'm-input-disabled': isDisabledInput },
                `m-input-${type}`
            ]"
            v-model="localValue.label"
            v-bind="$attrs"
            autocomplete="off"
            :tabindex="tabindex"
            :title="type === 'danger' ? errorMessage : ''"
            @input="handleInput"
            @blur="onBlurInput"
        />
        <m-btn-no-text
            title="Mở rộng"
            @click="onToggleDropList"
            @blur="onCloseDropList"
            @mouseover="onMouseOverDropBtn"
            @mouseout="onMouseOutDropBtn"
            class="combo-toggle lg"
            tabindex="-1"
            :class="[
                {
                    'combo-toggle-anim': isOpen === true
                },
                icon
            ]"
        />
        <div
            ref="container"
            v-show="isOpen"
            class="combo-box__items"
            :class="position"
        >
            <ul>
                <li
                    v-if="comboboxItems.header"
                    class="combo-box-header"
                    v-html="
                        comboboxItems.header.render
                            ? comboboxItems.header.render()
                            : `<div class='color-text'>${
                                  comboboxItems.header.label || ''
                              }</div>`
                    "
                ></li>

                <li
                    class="combo-box-item color-green-100"
                    v-for="(item, id) of localComboItems"
                    :key="id"
                    :ref="id"
                    @click="onClickItem(item.value, item.label)"
                    :class="itemIsFocus === id && 'combo-box-item--focus'"
                    v-html="
                        comboboxItems.body.render
                            ? comboboxItems.body.render(item.value, item.label)
                            : `<span class = 'color-text'>${item.label}</span>`
                    "
                ></li>
                <m-loading v-if="isLoading" />

                <li
                    v-if="comboboxItems.footer"
                    class="combo-box-footer color-green-100 combo-box-item w-full"
                    @click="$emit('onClickFooter')"
                    v-html="
                        comboboxItems.footer.render
                            ? comboboxItems.footer.render(
                                  comboboxItems.footer.label
                              )
                            : `<span class='m-btn-icon icon-pencil icon-20'> ${comboboxItems.footer.label} </span>`
                    "
                >
                </li>
            </ul>
        </div>
        <div
            v-show="!isDisabledInput"
            :style="{ visibility: errorMessage ? 'visible' : 'hidden' }"
            class="danger--info"
        >
            {{ errorMessage || '...' }}</div
        >
    </div>
</template>
<script>
import { api } from '../../../js/utils/api';
export default {
    name: 'MComboBox',
    inject: ['onOpenDialogError'],
    props: {
        label: String,
        modelValue: Object, // 2-way binding

        comboboxItems: Object,

        fieldValue: {
            type: String,
            default: 'departmentId'
        },
        fieldLabel: {
            type: String,
            default: 'departmentName'
        },
        id: String,
        tabindex: String,
        position: {
            // vị trí xuất hiện của dropdown
            type: String,
            default: 'bottom right mt-8'
        },
        focusColor: {
            // màu sắc khi combobox được focus
            type: String,
            default: '#3395ff'
        },
        icon: {
            // icon của nút toggle drop list
            type: String,
            default: 'icon-down-solid icon-20'
        },
        isDisabledInput: { type: Boolean, default: false },

        required: Boolean
    },
    data() {
        return {
            isLoading: false,
            localValue: null,
            isOpen: false,
            isDropHover: false,
            localComboItems: [],
            type: null,
            errorMessage: null,
            timeoutClose: null,
            itemIsFocus: -1,
            debounceValidate: null,
            debounceFetchData: null
        };
    },
    /**
     * Áp dụng 2-way binding
     * @Author NTVu - MF1742 - 19/08/2023
     * Modified: NTVu - 23/08/2023: Thêm khởi tạo giá trị cho local combo items
     */
    created() {
        if (this.modelValue) {
            this.localValue = this.modelValue;
        }
        if (this.comboboxItems.body.url) {
            this.fetchData();
        } else {
            this.localComboItems = this.comboboxItems.body.items;
        }
    },
    methods: {
        /**
         * @author NTVU - 06/09/2023
         * Lấy dữ liệu từ url
         */
        async fetchData() {
            try {
                this.isLoading = true;
                const res = await api.get(
                    `${this.comboboxItems.body.url}/Filter`,
                    {
                        params: {
                            search: this.localValue.label || '',
                            page: 1,
                            pageSize: 100
                        }
                    }
                );

                this.localComboItems = res.data.data.map((de) => ({
                    value: de[this.fieldValue],
                    label: de[this.fieldLabel]
                }));
            } catch (error) {
                this.onOpenDialogError({
                    title: this.$Resource.Error,
                    content: error.message
                });
            } finally {
                this.isLoading = false;
            }
        },
        onClickItem(value, label) {
            this.localValue.value = value;
            this.localValue.label = label;
            this.$emit('update:modelValue', this.localValue);
        },
        /**
         * Khi hover qua dropToggleBtn thì hiện border xanh
         * @Author NTVu - MF1742 (28/08/2023)
         */
        onMouseOverDropBtn() {
            this.isDropHover = true;
        },
        /**
         * Khi leave dropToggleBtn thì hiện trở lại trạng thái border bình thường
         * @Author NTVu - MF1742 (28/08/2023)
         */
        onMouseOutDropBtn() {
            this.isDropHover = false;
        },
        /**
         * Khi blur, nếu drop list đang mở thì đóng lại
         * @Author NTVu - MF1742 (23/08/2023)
         */
        onBlurInput() {
            if (this.isOpen) this.onCloseDropList();
        },
        /**
         * Đóng, mở drop list
         * @Author NTVu - MF1742 - 19/08/2023
         */
        onToggleDropList() {
            this.itemIsFocus = -1;
            this.isOpen = !this.isOpen;
        },
        /**
         * Mở các options
         * @Author NTVu - MF1742 - 23/08/2023
         */
        onOpenDropList() {
            if (this.timeoutClose) {
                clearTimeout(this.timeoutClose);
                this.timeoutClose = null;
            }
            this.isOpen = true;
        },
        /**
         * Đóng drop list sau 300ms để người dùng có thể click các options
         * @Author NTVu - MF1742 - 19/08/2023
         */
        onCloseDropList() {
            if (this.timeoutClose) {
                clearTimeout(this.timeoutClose);
            }
            this.timeoutClose = setTimeout(() => {
                this.isOpen = false;
            }, 200);
        },
        /**
         * Focus vào input hiện tại
         * @Author NTVu - MF1742 - 19/08/2023
         */
        autoFocus() {
            this.$refs.inputField.focus();
            this.$refs.inputField.select();
        },
        /**
         * Khi người dùng nhập thì filter giá trị
         * @Author NTVu - MF1742 - 23/08/2023

         */
        handleInput() {
            if (this.comboboxItems.body.url) {
                if (!this.debounceFetchData) {
                    this.debounceFetchData = this.$Helper.debounce(
                        this.fetchData,
                        400
                    );
                }
                this.debounceFetchData();
            } else {
                this.localComboItems = this.comboboxItems.body.items.filter(
                    (el) => {
                        return (
                            this.$Helper
                                .removeVietnameseTone(el.value.toLowerCase())
                                .includes(
                                    this.$Helper.removeVietnameseTone(
                                        this.localValue.toLowerCase()
                                    )
                                ) ||
                            this.$Helper
                                .removeVietnameseTone(el.label.toLowerCase())
                                .includes(
                                    this.$Helper.removeVietnameseTone(
                                        this.localValue.toLowerCase()
                                    )
                                )
                        );
                    }
                );
            }
            this.$emit('update:modelValue', this.localValue);
            this.itemIsFocus = 0;
            this.onOpenDropList();
        },
        /**
         * Điều khiển khi người dùng bấm lên, xuống giữa các options
         * @Author NTVu - MF1742 - 23/08/2023
         * @param {KeyboardEvent} e
         */
        onInputKeyDown(e) {
            if (e.key === 'ArrowDown') {
                e.preventDefault();

                if (this.isOpen === false) this.isOpen = true;

                this.itemIsFocus += 1;

                if (this.itemIsFocus > this.localComboItems.length - 1) {
                    this.itemIsFocus = 0;
                    this.scrollToIndex(this.itemIsFocus);
                } else {
                    this.scrollToIndex(this.itemIsFocus - 1);
                }
                return;
            }
            if (e.key === 'ArrowUp') {
                e.preventDefault();
                if (this.isOpen === false) this.isOpen = true;
                this.itemIsFocus--;

                if (this.itemIsFocus < 0) {
                    this.itemIsFocus = this.localComboItems.length - 1;
                    this.scrollToIndex(this.itemIsFocus);
                } else {
                    this.scrollToIndex(this.itemIsFocus - 1);
                }
                return;
            }
            if (e.key !== 'Enter') {
                return;
            }
            this.isOpen = this.isOpen === false ? true : false;
            e.preventDefault();
            const currentItem = this.localComboItems[this.itemIsFocus];
            if (currentItem)
                this.onClickItem(currentItem.value, currentItem.label);
        },
        /**
         * Điều khiển scroll của drop down
         * @author NTVu (02/09/2023)
         * @param {Number} index
         */
        scrollToIndex(index) {
            const list = this.$refs.container;
            const item = this.$refs[index] && this.$refs[index][0];
            if (item && list) {
                list.scrollTop = item.offsetTop - list.offsetTop;
            }
        },

        /**
         * Hiển thị lỗi trên input
         * @Author NTVu - MF1742 (20/08/2023)
         * @param {String} message
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
            // this.type = 'valid';
            this.type = null;
            this.errorMessage = null;
        },
        /**
         * Validate dữ liệu của combo box
         * @Author NTVu - 23/08/2023
         * @param {boolean} toFocus - nếu bằng true, khi có lỗi, ô input sẽ tự động focus
         * @returns {Number} 0 - Dữ liệu không hợp lệ, 1 - Dữ liệu hợp lệ
         */
        async validate(toFocus = false) {
            if (this.required) {
                if (!this.onValidateRequired()) {
                    this.onShowError(
                        this.$Resource.errorFieldEmpty(this.label)
                    );
                    if (toFocus) this.autoFocus();
                    return 0;
                } else if (!(await this.onValidateInOptions())) {
                    if (toFocus) this.autoFocus();
                    this.onShowError(this.$Resource.ValueNotInOptions);
                    return 0;
                }
            }
            this.onValid();
            return 1;
        },
        /**
         * Kiểm tra điều kiện required
         * @Author NTVu - MF1742 (20/08/2023)
         */
        onValidateRequired() {
            return !!this.localValue.label;
        },
        /**
         * Kiểm tra giá trị ô input có nằm trong list options đã cho
         * @Author NTVu - 23/08/2023
         */
        async onValidateInOptions() {
            if (!this.localValue.label.trim()) {
                return false;
            }
            if (this.comboboxItems.body.url) {
                try {
                    const res = await api.get(
                        `${this.comboboxItems.body.url}/Name/${this.localValue.label}`
                    );
                    if (res.data.length < 1) {
                        return false;
                    }
                } catch (error) {
                    this.onOpenDialogError({
                        title: this.$Resource.Error,
                        content: error.message
                    });
                }
            } else {
                if (
                    !this.comboboxItems.body.items.find(
                        (el) => el.label === this.localValue
                    )
                ) {
                    return false;
                }
            }

            return true;
        }
    },
    /**
     * Sử dụn debounce để call api trước khi update
     * @author NTVu - 03/09/2023
     *
     */
    beforeUpdate() {
        if (this.localValue.label != null) {
            if (!this.debounceValidate) {
                this.debounceValidate = this.$Helper.debounce(
                    this.validate,
                    500
                );
            }
            this.debounceValidate();
        }
    },
    watch: {
        modelValue() {
            this.localValue = { ...this.modelValue };
        }
    }
};
</script>

<style scoped>
@import url('./comboBox.css');
@import url('../input/input.css');
</style>
