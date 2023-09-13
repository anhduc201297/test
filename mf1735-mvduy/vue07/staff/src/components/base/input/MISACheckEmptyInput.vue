<template>
  <div style="position: relative">
    <input
      class="d-input"
      v-model="inputValue"
      @blur="isError++"
      :class="{ error: isError > 0 && inputValue === '' }"
      type="text"
    />
    <span v-show="isError > 0 && inputValue === ''" class="text-error"
      >{{content}}</span
    >
  </div>
</template>

<script>
export default {
  name: "MISACheckEmptyInput",
  props:['content'],
  data() {
    return {
      isError: 0,
      inputValue: "",
    };
  },
};
</script>

<style scoped>
@import url(./input.css);

.error {
  border-color: #e61d1d !important;
}
.text-error {
  font-weight: 500 !important;
  color: #dcdcdc;
  font-size: 12px;
  background-color: rgb(37, 37, 37);
  padding: 4px 0;
  width: 180px;
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: 0px 0px 6px rgba(6, 6, 6, 0.5);
  position: absolute;
  top: 30px;
  left: 20px;
}
</style>