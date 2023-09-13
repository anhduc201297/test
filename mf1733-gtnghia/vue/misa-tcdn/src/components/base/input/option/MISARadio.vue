<template>
  <div class="input-container">
    <div v-if="label" class="label-box" :class="{ required: isRequired }">
      <label :name="name" :for="name" :title="tooltips">{{ label }}</label>
    </div>
    <div class="input-box" :class="`radio-box-${direction}`">
      <div
        class="radio-item"
        v-for="(option, index) in options"
        :key="index"
      >
        <input
          class="input-radio"
          type="radio"
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
</template>

<script>
export default {
  name: "m-radio",
  props: {
    modelValue: String,
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
  },
  created() {
    this.valueOutput = this.modelValue;
  },
  watch: {
    /**
     * mf1733-gtnghia 22/8/2023
     * create: valueOutput passing data
     */
    valueOutput: function (newValue) {
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
