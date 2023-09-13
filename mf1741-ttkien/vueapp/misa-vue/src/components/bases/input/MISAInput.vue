<template>
  <div class="box-input">
    <input
      type="text"
      v-model="valueOutput"
      :ref="refValue"
      :class="{ 'input-error': inputError }"
    />
  </div>
</template>

<script>
export default {
  name: "MISAInput",
  props: ["refValue", "isUpdateData", "modelValue", "inputError"],
  data() {
    return {
      valueOutput: null,
    };
  },
  created() {
    this.valueOutput = this.modelValue;
  },
  mounted() {
    /**
     * author: ttkien
     * 27/08/2023
     * autofocus vào thẻ input đầu tiên
     */
    if (this.$refs.EmployeeCode !== undefined) {
      this.$refs.EmployeeCode.focus();
    }
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

<style scoped>
@import url(./MISAInput.css);
</style>