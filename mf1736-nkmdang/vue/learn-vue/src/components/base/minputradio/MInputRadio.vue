<template>
  <div class="radio-container flex align-center">
    <div class="radio-input-box">
      <input
        ref="radioInput"
        type="radio"
        class="radio-input"
        :name="name"
        :tabindex="tabindex"
        :value="value"
        @input="$emit('update:modelValue', $event.target.value)"
      />
    </div>
    <div class="radio-box border-focus">
      <div class="radio-body bg-focus"></div>
    </div>
  </div>
</template>
<script>
export default {
  props: ["name", "tabindex", "valueInput", "valueChecked", "value"],
  methods: {
    /** Author: Minh Đăng
     * Feat: Tích focus checkbox 20/8/2023
     * */
    onFocus() {
      // console.log(this.isFocus)
      this.isChecked = true;
    },
  },
  data() {
    return {
      isFocus: false,
      isChecked: false,
      modelRadio: "",
    };
  },
  /**
   * Author: Minh Đăng 30/08/2023
   * Feat: Tự tích nếu mang giá trị đúng
   */
  mounted() {
    if (this.value == this.valueChecked) {
      this.$refs.radioInput.checked = true;
    }
  },
  watch: {
    /**
     * Author: Minh Đăng 31/08/2023
     * Feat: Kiểm tra giá trị valueChecked thay đổi để tích
     */
    // valueChecked() {
    //   if (this.valueInput == this.valueChecked) {
    //     this.$refs.radioInput.checked = true;
    //   } else if (
    //     this.valueChecked != "Nam" &&
    //     this.valueChecked != "Nữ" &&
    //     this.valueInput == "Khác"
    //   ) {
    //     this.$refs.radioInput.checked = true;
    //   }
    // },
  },
};
</script>
<style scoped>
@import url(./minputradio.css);

.border-focus {
  border-color: #2ca012;
  /* box-shadow: 0 0 10px #2ca012; */
}

.bg-focus {
  background-color: #2ca012;
}
</style>
