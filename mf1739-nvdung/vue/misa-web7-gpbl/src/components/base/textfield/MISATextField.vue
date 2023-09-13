<template>
  <!-- Ngoài ra, có thể thêm các class: m-textfield-icon, m-textfield-icon-action -->
  <div class="m-textfield">
    <m-label
      :lblRequired="textFieldRequired"
      :tooltip="lblTooltip"
      v-if="haveLabel"
    >
      <slot name="lbl-content"></slot>
    </m-label>
    <input
      v-bind:autofocus="isAutoFocused"
      autocomplete="true"
      ref="inputTextField"
      :tabindex="tabIndex"
      :type="inputType"
      :placeholder="placeholderTextField"
      v-on:mouseover="isHover = true"
      v-on:mouseleave="isHover = false"
      v-on:focusin="isActive = true"
      v-on:focusout="isActive = false"
      @input="validate"
      v-model="valueOutput"
    />
    <a
      v-on:click="this.isActive = true"
      v-on:mouseover="isHover = true"
      v-on:mouseleave="isHover = false"
      ><div class="mi mi-24 mi-search"></div
    ></a>
    <span class="warning" v-if="textFieldRequired && isError">
      <slot name="warning"></slot
    ></span>
  </div>
</template>
<script>
export default {
  name: "MISATextField",
  data() {
    return {
      isHover: false,
      isActive: false,
      isError: false,

      valueOutput: this.valueInput,
    };
  },
  props: {
    // Set tooltip cho label
    lblTooltip: {
      type: String,
      default: "",
      required: false,
    },
    // Hiển thị label
    haveLabel: {
      type: Boolean,
      default: true,
      required: false,
    },
    // Hiển thị label *required
    textFieldRequired: {
      type: Boolean,
      default: false,
      required: false,
    },
    inputType: {
      type: String,
      default: "text",
      required: false,
    },
    isAutoFocused: {
      type: Boolean,
      default: false,
      required: false,
    },
    placeholderTextField: {
      type: String,
      default: "",
      required: false,
    },
    tabIndex: {
      type: Number,
      default: 1,
      required: false,
    },
    valueInput: {
      type: String,
      default: "",
      required: false,
    },
    /* Mảng các hàm validate */
    validateFunctions: {
      type: Array,
      required: false,
    },
  },
  methods: {
    /**
     * Kiểm tra input rỗng
     * Author: NVDUNG (20/08/2023)
     */
    validate(event) {
      // Mảng lưu trữ các kết quả các hàm validate
      let boolArray = [];
      // Kiểm tra rỗng
      let isNotEmpty = this.$refs.inputTextField.value.trim() == "" ? false : true;
      boolArray.push(isEmpty);
      // Kiểm tra các hàm validate khác
      validateFunctions.forEach((item) => {
        let isValidFunction = item(event.target.value);
        boolArray.push(isValidFunction);
      });

      // Trả về kết quả chung
      this.isError = boolArray.some((value) => value === true);
    },

    /**
     * Set focus input
     * Author: NVDUNG (20/08/2023)
     */
    setFocusInput() {
      try {
        if (this.isAutoFocused) this.$refs.inputTextField.focus();
      } catch (error) {
        console.log(error);
      }
    },
  },
  watch: {
    /**
     * Gửi dữ liệu cho component cha khi data thay đổi
     * Author: NVDUNG (20/08/2023)
     */
    valueOutput(newValue) {
      // Tạo sự kiện để truyền giá trị mới cho component cha
      this.$emit("inputOnChange", this.valueOutput);
    },
  },
  mounted() {
    // Set focus input
    this.setFocusInput();
  },
};
</script>
<style scoped>
@import url(./text-field.css);
</style>
