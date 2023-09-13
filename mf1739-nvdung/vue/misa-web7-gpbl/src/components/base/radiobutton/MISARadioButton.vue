<template>
  <div id="m-rdo-gioitinh" class="m-rdo">
    <label><slot></slot></label>
    <div class="m-rdo-content">
      <div
        class="item"
        v-for="(item, index) in listOptions"
        :key="item"
        :class="{ 'm-rdo-selected': index === indexSelectedData }"
        v-on:click="itemClick(index)"
      >
        <a :tabindex="tabIndexRadio + index"><span></span></a>
        <label>{{ item }}</label>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "MISARadioButton",
  data() {
    return {
      indexSelectedData: -1,
    };
  },
  props: {
    listOptions: {
      type: Array,
      required: true,
    },
    tabIndexRadio: {
      type: Number,
      required: false,
    },
    indexSelected: {
      type: Number,
      default: -1,
      required: false,
    },
  },
  mounted() {
    // Thiết lập data
    if (this.indexSelected > -1) {
      this.indexSelectedData = this.indexSelected;
    }
  },
  watch: {
    indexSelectedData(newValue) {
      // Truyền dữ liệu về lại component cha
      this.$emit("changeValue", newValue);
    },
  },
  methods: {
    /**
     * Cập nhật đánh dấu selected của radio button
     * Author: NVDUNG (20/08/2023)
     */
    itemClick(index) {
      this.indexSelectedData = index;
    },
  },
};
</script>
<style scoped>
@import url(./radio-button.css);
</style>
