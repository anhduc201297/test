<template>
  <div class="d-checkbox">
    <input type="checkbox"/>
    <span>{{ content }}</span>
  </div>
</template>

<script>
export default {
  name: "MISACheckboxInput",
  props: ["content"],
  data() {
    return {
      
    };
  },
  methods: {
    
  },
};
</script>

<style scoped>
.d-checkbox {
  display: flex;
  align-items: center;
}
.d-checkbox input {
  appearance: none;
  width: 18px;
  height: 18px;
  margin: 0 6px;
  transform: translate(0, 2px);
  border: 2px solid #9E9E9E !important;
  border-radius: 3px !important;
  cursor: pointer;
}

.d-checkbox input:checked {
  appearance: checkbox;
}
</style>
