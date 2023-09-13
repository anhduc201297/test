<template>
  <div :style="{ width: width, height: height, margin: margin }">
    <div class="relative">
      <label :class="[labelClass, hasToolTip]" @click="clickLabelForInput"
        >{{ label }}
        <div class="icon__required" v-if="required">*</div>
      </label>
      <!-- <input
        class="minput--text"
        :class="{ input__missing: isMissingInput }"
        :type="type"
        ref="inputFocus"
        @input="$emit('update:modelValue', $event.target.value)"
      /> -->
      <input
        class="minput--text"
        :class="{ input__missing: isMissingInput }"
        @keydown="checkInputEmpty"
        :type="type"
        ref="inputFocus"
        :value="modelValue"
        @input="$emit('update:modelValue', $event.target.value)"
      />
      <div class="tooltip-cont">
        <div class="tooltip-icon-box">
          <div class="tooltip-icon"></div>
        </div>
        <div class="tooltip-box">
          {{ tooltip }}
        </div>
      </div>
      <div class="label__missing" v-if="isMissingInput">
        {{ label }} không được để trống
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: [
    "type",
    "initValue",
    "label",
    "tooltip",
    "required",
    "width",
    "height",
    "labelClass",
    "margin",
    "focus",
    "modelValue",
    "missingInput",
  ],
  data() {
    return {
      hasToolTip: "",
      isMissingInput: false,
      valueInput: "",
      modelValueInput: this.modelValue,
    };
  },
  methods: {
    clickLabelForInput() {
      this.$refs.inputFocus.focus();
    },
  },

  /**
   * Author: Minh Đăng 30/08/2023
   * Feat: Focus vào thẻ input khi thẻ input được đưa vào DOM
   */
  mounted() {
    if (this.focus === true) {
      this.$refs.inputFocus.focus();
    }
    if (this.tooltip) {
      this.hasToolTip = "has-tooltip";
    }
    if (this.initValue) {
      this.valueInput = this.initValue;
    }
  },
  watch: {
    modelValue(newValue) {
      if (this.required == "true") {
        if (newValue !== "") {
          this.isMissingInput = false;
        } else {
          this.isMissingInput = true;
        }
      }
    },
    missingInput() {
      if (this.missingInput === true) {
        this.isMissingInput = true;
      }
    },
  },
};
</script>

<style scoped>
/* CSS cho thẻ label */
label {
  display: flex;
  margin-bottom: 8px;
}
.has-tooltip:hover ~ .tooltip-cont {
  display: block;
}
/* CSS cho thẻ label */

.minput--text {
  width: 100%;
  height: 36px;
  padding: 0px 8px;
  border-radius: 4px;
  border: 1px solid #e0e0e0;
}

.minput--text::placeholder {
  color: #999999;
  font-size: 14px;
}

.minput--text:hover {
  background-color: #f6f6f6;
  /* cursor: pointer; */
  border-color: #ebebeb;
}

.minput--text:focus {
  border: 1px solid #50b83c;
  outline: none;
}

:root {
  --color-tooltip: #333;
  --height-tooltip-icon: 8px;
}
.tooltip-cont {
  display: none;
  position: absolute;
  min-width: 100px;
  width: 100%;
  top: 100%;
  left: 50%;
  transform: translateX(-50%);
  /* max-width: 100%; */
  z-index: 3;
}

.tooltip-icon-box {
  width: 100%;
  display: flex;
  justify-content: center;
  margin-bottom: -4px;
}

.tooltip-icon {
  transform: rotate(45deg);
  width: var(--height-tooltip-icon);
  height: var(--height-tooltip-icon);
  background-color: var(--color-tooltip);
}

.tooltip-box {
  border-radius: 4px;
  /* width: auto; */
  text-align: center;
  background-color: var(--color-tooltip);
  color: #ddd;
  padding: 3px 5px;
}

.input__missing {
  border-color: #e61d1d;
}

.label__missing {
  color: #e61d1d;
  position: absolute;
  top: 100%;
}
</style>
