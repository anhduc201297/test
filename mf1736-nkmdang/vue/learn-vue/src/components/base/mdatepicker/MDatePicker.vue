<template>
  <div class="datepicker-container">
    <div class="datepicker-box">
      <div class="datepicker-input">
        <input
          type="text"
          class=""
          v-model="modelDatePicker"
          :tabindex="tabindex"
        />
      </div>
      <div class="date-icon">
        <div class="icons-align-center-box">
          <div class="icons icon-calendar"></div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: ["initDate", "tabindex"],
  data() {
    return {
      modelDatePicker: this.initDate,
    };
  },
  watch: {
    modelDatePicker(newValue) {
      this.$emit("update:modelValue", newValue);
    },
  },
};
</script>
<style scoped>
@import url(./mdatepicker.css);
</style>
