<template>
    <!-- Input Checkbox -->
    <input
    type="checkbox"
    :id="id"
    class="input__checkbox"
    :checked="value"
    v-bind="$attrs"
  />
</template>
  
<script>
export default {
    name: 'MISAInputCheckbox',
    props: {
      id: String,
      value: Boolean,
  },
};
</script>

<style scoped>
@import url('../../../css/base/input.css');
</style>
  