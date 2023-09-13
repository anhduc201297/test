<template>
  <input
    type="checkbox"
    v-model="checkBoxArray"
    v-if="this.checkBoxArray.length < 2 || this.checkBoxArray === true"
  />
  <div class="box-icon box-input-checkbox checkbox-checked" v-else>
    <!-- <div class="div-icon-check"></div> -->
    <div class="div-icon-minus"></div>
  </div>
  <!-- <div class="box-icon box-input-checkbox" v-else :id="id">
    <div class="icon-img icon-check"></div>
    <div class="div-icon-check"></div>
  </div> -->
</template>

<script>
export default {
  name: "MISAInputCheckbox",
  props: ["thead", "modelValue"],
  data() {
    return {
      checkBoxArray: [],
    };
  },
  methods: {},
  watch: {
    checkBoxArray(newValue) {
      this.$emit("update:modelValue", newValue);
    },
  },
  updated() {
    this.checkBoxArray = this.modelValue;
  },
};
</script>

<style scoped>
@import url(./MISAInput.css);
</style>