<template>
  <div class="gender-select" v-for="(item, index) in dataGender" :key="index">
    <input type="radio" :value="item" v-model="picked" />
    <span class="span-label">{{ item }}</span>
  </div>
</template>

<script>
export default {
  props: ["dataGender", "modelValue", "isUpdateData"],
  name: "MISAInputRadio",
  data() {
    return {
      picked: null,
    };
  },
  created() {
    this.picked = this.modelValue;
  },
  updated() {
    if (this.isUpdateData === true) {
      this.$emit("update:modelValue", this.picked);
    }
  },
};
</script>

<style scoped>
@import url(./MISAInput.css);
</style>