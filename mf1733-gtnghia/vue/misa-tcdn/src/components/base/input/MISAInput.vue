<template>
  <div class="input-container">
    <div v-if="label" class="label-box" :class="{ required: isRequired }">
      <label :name="name" :for="name" :title="tooltips">{{ label }}</label>
    </div>
    <input
      :class="{
        'input-field-s': size === 's',
        'input-field-m': size === 'm',
        'input-field-l': size === 'l',
      }"
      :type="type"
      :name="name"
      :placeholder="placeholder"
      v-model="valueOutput"
      :required="isRequired"
    />
    <div class="input-error mt-8">
      <p class="text-red">{{ label }} không được để trống</p>
    </div>
  </div>
</template>

<script>
export default {
  name: "m-input",
  props: {
    name: String,
    label: String,
    tooltips: String,
    padding: String,
    type: {
      type: String,
      default: "text",
    },
    placeholder: String,
    modelValue: String,
    isRequired: {
      type: Boolean,
      default: false,
    },
    size: {
      type: String,
      default: "m",
    },
  },
  created() {
    this.valueOutput = this.modelValue;
  },
  watch: {
    /**
     * mf1733-gtnghia 21/8/2023
     * create: valueOutput passing data
     */
    valueOutput: function (newValue) {
      // console.log(newValue + " " + oldValue);
      this.$emit("update:modelValue", newValue);
    },
  },
  data() {
    return {
      valueOutput: null,
    };
  },
  methods: {

  },
  computed: {},
};
</script>

<style scoped>
@import url(./input.css);
</style>
