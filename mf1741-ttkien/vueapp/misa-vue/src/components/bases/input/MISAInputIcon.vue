<template>
  <div class="box-input">
    <input
      type="text"
      class="input-icon"
      :placeholder="placeholder"
      v-model="valueOutput"
      :ref="refValue"
    />
    <m-icon
      :boxIconType="boxIconType"
      :iconType="iconType"
      :tooltip="tooltip"
    ></m-icon>
  </div>
</template>

<script>
export default {
  name: "MISAInputIcon",
  props: [
    "placeholder",
    "boxIconType",
    "iconType",
    "tooltip",
    "modelValue",
    "isUpdateData",
  ],
  data() {
    return {
      valueOutput: null,
    };
  },
  created() {
    this.valueOutput = this.modelValue;
  },
  updated() {
    /**
     * author: ttkien
     * 30/08/2023
     * update dữ liệu mới vào form
     */
    if (this.isUpdateData === true) {
      this.$emit("update:modelValue", this.valueOutput);
    }
  },
};
</script>

<style>
@import url(./MISAInput.css);
</style>