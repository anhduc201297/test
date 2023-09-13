<template>
  <div
    class="m-dropdown"
    :class="{ 'm-dropdown-up': dropup }"
    id="cbo_ddl_donvi"
  >
    <div class="scroller">
      <a
        v-for="item in items"
        :key="item"
        v-bind:class="{ selected: item == itemSelected }"
        @:click="ddlSelectedItem(item)"
        >{{ item }}
        <div class="mi mi-24 mi-tick"></div>
      </a>
    </div>
  </div>
</template>

<script>
export default {
  name: "MISADropdownList",
  data() {
    return {
      itemSelected: "",
    };
  },
  props: {
    valueInputCombobox: {
      type: String,
      required: false,
    },
    items: {
      type: Array,
      required: true,
    },
    dropup: {
      type: Boolean,
      default: false,
      required: false,
    },
  },
  created() {
    // Cập nhật itemSelected nếu có
    if (this.valueInputCombobox != "") {
      this.itemSelected = this.valueInputCombobox;
    }
  },

  methods: {
    /**
     * Truyền item được chọn đến component cha.
     * Author: NVDUNG (18/08/2023).
     */
    ddlSelectedItem(itemValue) {
      // Nhận giá trị cho thuộc tính itemSelected
      this.itemSelected = itemValue;

      // Gửi giá trị itemSelected cho component cha
      // Gửi nhiều giá trị khác nhau, thì đóng gói thành 1 object
      this.$emit("dropdownItemSelected", itemValue);
    },
  },
};
</script>

<style scoped>
@import url(./dropdownlist.css);
</style>
