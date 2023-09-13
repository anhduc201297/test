<template>
  <div class="d-combobox">
    <div class="combobox-input">
      <input v-model="selectedOption" type="text"
       @blur="isError++"
       :class="{ error: isError>0 && selectedOption === '' }"
       />
      <div
        class="btn-dropdown"
        @click="onChangeShow"
        :class="{ 'icon-rotate': showOptions }"
      >
        <div class="icon icon-arrow-select"></div>
      </div>
    </div>
    <span v-show="isError>0 && selectedOption === ''" class="text-error">{{content}}</span>
    <div v-show="showOptions" class="options">
      <div
        class="option"
        v-for="(option, index) in options"
        :key="index"
        @click="selectOption(option)"
      >
        {{ option }}
      </div>
    </div>
  </div>
</template>
  
  <script>
export default {
  name: "MISACombobox",
  props: {
    options: Array,
    content:String
  },
  methods: {
    /*
     * Click thay đổi trang thái hiển thị của Options
     * Author: mf1735-duy (20/08/2023)
     */
    onChangeShow() {
      this.showOptions = !this.showOptions;
    },
    /*
     * Click để binding value input và đóng Options
     * Author: mf1735-duy (20/08/2023)
     */
    selectOption(option) {
      this.selectedOption = option;
      this.showOptions = false;
    },
  },
  data() {
    return {
      showOptions: false,
      selectedOption: "",
      isError: 0,
    };
  },
};
</script>
  
<style scoped>
@import url(combobox.css);
</style>
  