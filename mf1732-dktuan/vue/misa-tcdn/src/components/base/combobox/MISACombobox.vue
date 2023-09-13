<template>
    <div class="label">
        <label v-if="label" :title="labelTitle" class="label-name" for="">{{ label }}</label>
        <div v-if="isRequired" class="required">*</div>
    </div>
    <div class="combobox-wrapper">
        <m-loading v-if="isLoading"></m-loading>
        <v-combobox v-model="selectedItem" :items="myItems">
        </v-combobox>
    </div>
</template>

<script>
export default {
    name: "m-combobox",
    props: {
        label: String,
        labelTitle: String,
        isRequired: Boolean,
        items: Object,
        modelValue: null,
    },
    data() {
        return {
            test: "haha",
            isLoading: true,
            myItems: [],
            selectedItem: this.modelValue,
        }
    },
    methods: {
        /**
         * bind value đã chọn của combobox
         * @param {String} newVal 
         * Author: dktuan (4/09/2023)
         */
        bindSelectedValue(newVal) {
            this.$emit('update:modelValue', newVal)
        }
    },

    watch: {
        items(newVal) {
            this.myItems = Object.keys(newVal)
            this.isLoading = false
        },
        selectedItem(newVal) {
            newVal = this.items[newVal]
            this.bindSelectedValue(newVal)
        }
    },
}
</script>

<style scoped>
@import url(./combobox.css);
</style>