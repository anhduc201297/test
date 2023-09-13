<template>
  <div
    ref="comboboxElement"
    id="m-cbo-donvi"
    :class="{
      'm-combobox': true,
      'm-combobox-hover': isHover,
      'm-combobox-active': isActive,
    }"
  >
    <m-label :lblRequired="comboboxRequired" v-if="haveLabel">
      <slot name="lbl-content"></slot>
    </m-label>
    <div class="m-combobox-content">
      <input
        ref="inputCombobox"
        type="text"
        style="width: calc(100% - 40px)"
        @mouseover="isHover = true"
        @mouseleave="isHover = false"
        @focusin="isActive = true"
        @focusout="isActive = false"
        @click.stop
        @change="comboboxChangValue()"
        @keyup="filterComboboxFunction()"
        :tabindex="tabIndexInput"
      />
      <a
        v-on:click="showHideDropdown()"
        v-on:mouseover="isHover = true"
        v-on:mouseleave="isHover = false"
        :tabindex="tabIndexButton"
      >
        <div
          v-if="isShow"
          class="mi mi-16 mi-arrow-dropdown mi-arrow-dropdown--open"
        ></div>
        <div
          v-else
          class="mi mi-16 mi-arrow-dropdown mi-arrow-dropdown--close"
        ></div>
      </a>
    </div>
    <!-- dropdown -->
    <MISADropdownList
      v-bind:class="{ show: isShow }"
      :items="dataDropdownlist"
      :dropup="dropup"
      :valueInputCombobox="valueInputCombobox"
      @dropdownItemSelected="itemSelected"
    ></MISADropdownList>
    <span class="warning" v-if="comboboxRequired && isError">
      <slot name="warning"></slot>
    </span>
  </div>
</template>

<script>
import MISADropdownList from "../dropdownlist/MISADropdownList.vue";
export default {
  name: "MISACombobox",
  data() {
    return {
      isHover: false,
      isActive: false,
      isShow: false,
      isError: false,
    };
  },
  props: {
    // item selected
    valueInputCombobox: { type: String, default: "", required: false },

    haveLabel: {
      type: Boolean,
      default: true,
      required: false,
    },

    dataDropdownlist: {
      type: Array,
      required: true,
    },
    comboboxRequired: {
      type: Boolean,
      default: false,
      required: false,
    },
    dropup: {
      type: Boolean,
      default: false,
      required: false,
    },
    tabIndexInput: {
      type: Number,
      default: 1,
      required: false,
    },
    tabIndexButton: {
      type: Number,
      default: 2,
      required: false,
    },
  },

  components: {
    MISADropdownList,
  },
  methods: {
    /**
     * Ẩn/hiện dropdownlist và active
     * Author: NVDUNG (18/08/2023)
     */
    showHideDropdown() {
      // Chuyển đổi trạng thái isShow
      this.isShow = !this.isShow;
      // Active
      this.isActive = true;
    },

    /**
     * Hiển thị itemSelected từ dropdownlist sang input
     * Author: NVDUNG (18/08/2023)
     */
    itemSelected(newValue) {
      /* Nhận dữ liệu được truyền từ component con gửi đến
      component cha qua emit sự kiện dropdownItemSelected
      - itemValue là dữ liệu được truyền từ dropdownlist
      */
      try {
        // Lấy thẻ input từ DOM qua reference
        this.$refs.inputCombobox.value = newValue;
        // Phát sự kiện comboboxChangeValue
        this.$emit("comboboxChangeValue", newValue);
        // Đóng dropdownlist
        this.isShow = false;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Kiểm ra giá trị khi thay đổi và trả về component cha
     * Author: NVDUNG (18/08/2023)
     */
    comboboxChangValue() {
      // Kiểm tra rỗng
      this.validate();
      // Trả về component cha
      this.$emit("comboboxChangeValue", this.$refs.inputCombobox.value);
    },

    /**
     * Kiểm tra input rỗng
     * Author: NVDUNG (18/08/2023)
     */
    validate() {
      try {
        this.isError = this.$refs.inputCombobox.value == "" ? true : false;
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Lọc các giá trị từ input
     * Author: NVDUNG (18/08/2023)
     */
    filterComboboxFunction() {},

    /**
     * Đóng dropdownlist và deactive khi click bên ngoài combox
     * Author: NVDUNG (18/08/2023)
     */
    // clickOutCombobox() {
    //   try {
    //     // Lưu tạm đối tượng vue instance
    //     var vueInstance = this;
    //     // console.log(vueInstance.$refs);
    //     // console.log("Bên ngoài:" + vueInstance.$refs.comboboxElement.innerHTML);
    //     // Lấy combox từ DOM qua reference
    //     let combobox = vueInstance.$refs.comboboxElement;
    //     document.onclick = function (event) {
    //       // console.log("cbo: " + combobox.innerHTML);
    //       // Kiểm tra click bên ngoài combobox
    //       if (!combobox.contains(event.target)) {
    //         // Đóng dropdownlist
    //         vueInstance.isShow = false;
    //         // Hủy active
    //         vueInstance.isActive = false;
    //       }
    //       // console.log("Phần tử kích hoạt: " + event.target.innerHTML);
    //     };
    //   } catch (error) {
    //     console.log(error);
    //   }
    // },
  },

  mounted() {
    // Tạo input.value ban đầu
    if (this.valueInputCombobox != "") {
      this.$refs.inputCombobox.value = this.valueInputCombobox;
    }

    // this.clickOutCombobox();
  },
};
</script>
<style scoped>
@import url(./combobox.css);
</style>
