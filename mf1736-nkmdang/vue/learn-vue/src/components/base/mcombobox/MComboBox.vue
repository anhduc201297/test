<template>
  <div class="combobox-container">
    <div>
      <label class="flex">
        {{ label }}
        <div class="icon__required" v-if="required">*</div>
      </label>
    </div>
    <div class="relative">
      <div
        class="combobox-box"
        :class="{ active: isActive, 'border-e61d1d': isMissingInput }"
      >
        <div class="combobox-input">
          <input
            type="text"
            :tabindex="tabindex"
            :disabled="disabled"
            :class="textClass"
            ref="inputText"
            :value="modelValue"
            @input="$emit('update:modelValue', $event.target.value)"
            @focus="onActive"
            @blur="onUnActive"
          />
        </div>
        <div class="combobox-sub-cont" @click="clickComboboxDropList">
          <div class="icons-align-center-box">
            <div class="icons icon__vdown"></div>
          </div>
        </div>
      </div>
      <div class="label__missing" v-if="isMissingInput">
        {{ label }} không được để trống
      </div>
      <div class="combobox-droplist-cont" v-if="isShowComboboxDropList">
        <div class="combobox-droplist-box">
          <div
            class="combobox-droplist-item"
            v-for="option in listOptionsCombobox"
            :key="option"
          >
            {{ option.Content }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: [
    "tabindex",
    "initValue",
    "disabled",
    "textClass",
    "label",
    "required",
    "missingInput",
    "modelValue",
    "listOptions",
  ],
  data() {
    return {
      isShowComboboxDropList: false,
      isActive: false,
      isMissingInput: this.missingInput,
      listOptionsCombobox: this.listOptions,
    };
  },
  methods: {
    /**
     * Author: Minh Đăng 5/9/2023
     * Feat: Ẩn hiện thị droplist
     */
    clickComboboxDropList() {
      this.isShowComboboxDropList = !this.isShowComboboxDropList;
    },
    /**
     * Active combobox chuyển màu border sang xanh
     */
    onActive() {
      this.isActive = true;
    },
    onUnActive() {
      this.isActive = false;
    },
    inputCombobox() {},
  },
  watch: {
    missingInput() {
      if (this.missingInput == true) {
        this.isMissingInput = this.missingInput;
      }
    },
    modelValue(newValue) {
      // console.log(newValue, newValue !== "");
      // if (newValue.replace(/\s+/g, "") !== "") {
      //   this.isMissingInput = false;
      // } else this.isMissingInput = true;
      // console.log(newValue.replace(/\s/g, "") !== "");
      // if (newValue.replace(/\s/g, "")) {
      //   this.isMissingInput = false;
      // } else this.isMissingInput = true;
      // console.log(newValue);
      if (newValue) {
        this.isMissingInput = false;
      } else {
        this.isMissingInput = true;
      }
    },
  },
};
</script>
<style scoped>
@import url(./mcombobox.css);
.active {
  background-color: #fff !important;
  border-color: #2ac012 !important;
}

.label__missing {
  color: #e61d1d;
  position: absolute;
  top: 100%;
}
.border-e61d1d {
  border-color: #e61d1d;
}
</style>
