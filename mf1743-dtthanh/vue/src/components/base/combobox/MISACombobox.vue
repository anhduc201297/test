<template>
    <div class="m-combobox">
        <div class="combobox-input-control" :class="inputClass">
            <input type="text" class="combobox-input" v-model="value" :placeholder="placeholder" @input="onInput"
                @click="showCombobox" />
            <div class="combobox-icon-down" @click="showCombobox">
                <div class="icon"></div>
            </div>
        </div>
        <div class="combobox-dropdown-list" v-if="isShowResult">
            <div class="combobox-dropdown-item" v-for="(item, index) in result" :key="index" @click="selectItem(item)">
                {{ item }}
            </div>
        </div>
    </div>
</template>
<script>
export default {
    props: ["modelValue", "placeholder", "result", "dropdownItems ", "inputClass"],
    emits: ["update:modelValue"],
    data() {
        return {
            value: this.modelValue,
            isShowResult: false,
            department: [],
        };
    },
    methods: {
        /**
         * Chọn item trong phần option
         * AUTHOR: DTTHANH (30/08/2023)
         * @param {String} item 
         */
        selectItem(item) {
            this.value = item
            this.isShowResult = false;
            this.$emit('update:modelValue', this.value);
        },
        
        /**
         * Đóng mở combobox
         * AUTHOR: DTTHANH (30/08/2023)
         */
        async showCombobox() {
            this.isShowResult = !this.isShowResult;
        },

        /**
         * Bắt sự kiện khi người dùng nhập input
         * AUTHOR: DTTHANH (30/08/2023)
         */
        onInput() {
            if (this.value != "") this.isShowResult = true;
            else this.isShowResult = false;
            this.$emit("update:modelValue", this.value);
        },
    },
};
</script>
  
<style scoped>
@import url(./combobox.css);
</style>
  