<template>
  <input class="d-radio-input" type="radio"/>
  <span>{{content}}</span>
</template>
  
  <script>
export default {
  name: "MISARadioInput",
  props: ["content"],
};
</script>
  
  <style scoped>
@import url(radio-input.css);
</style>