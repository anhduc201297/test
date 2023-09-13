<template>
  <div v-if="type === 'multiple'" class="input-container">
    <div v-if="label" class="label-box" :class="{ required: isRequired }">
      <label :name="name" :for="name" :title="tooltips">{{ label }}</label>
    </div>
    <div class="input-box" :class="`checkbox-box-${direction}`">
      <div
        class="checkbox-item"
        v-for="(option, index) in options"
        :key="index"
      >
        <input
          class="input-checkbox"
          type="checkbox"
          :name="name"
          :id="option.value"
          :value="option.value"
          v-model="valueOutput"
        />
        <label :for="option.value">{{ option.label }}</label>
      </div>
    </div>
    <div class="input-error mt-8">
      <p class="text-red">{{ label }} không được để trống</p>
    </div>
  </div>
  <input
    v-else-if="type === 'single'"
    class="checkbox-item-16 input-checkbox"
    type="checkbox"
    :name="name"
    v-model="valueOutput"
  />
</template>

<script>
export default {
  name: "m-checkbox",
  props: {
    modelValue: Array,
    name: String,
    label: String,
    tooltips: String,
    isRequired: Boolean,
    direction: {
      type: String,
      default: "horizontal",
    },
    options: {
      type: Array,
    },
    type: {
      default: "single",
      // multiple
    },
  },
  created() {
    this.valueOutput = this.modelValue;
  },
  watch: {
    /**
     * mf1733-gtnghia 22/8/2023
     * create: valueOutput passing data
     */
    valueOutput: function (newValue, oldValue) {
      console.log(newValue + " " + oldValue);
      this.$emit("update:modelValue", newValue);
    },
  },
  data() {
    return {
      valueOutput: null,
    };
  },
};
</script>

<style scoped>
@import url(../input.css);
</style>
