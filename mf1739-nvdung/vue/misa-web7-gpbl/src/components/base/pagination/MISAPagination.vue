<template>
  <div class="ms-pagination" :class="{ disabled: isDisabled }">
    <div class="left-pagination">
      Tổng số <b>{{ totalRecord }}</b> bản ghi
    </div>
    <div class="right-pagination">
      <div class="record-in-page">
        <!-- combobox record in page -->
        <m-combobox
          :haveLabel="false"
          valueInputCombobox="20 bản ghi"
          :dataDropdownlist="['20 bản ghi', '30 bản ghi', '40 bản ghi']"
          :dropup="true"
          @comboboxChangeValue="changePageSize"
        ></m-combobox>
      </div>

      <div class="paging">
        <b>{{ pageCurrent }}</b> - <b>{{ pageEnd }}</b> trang
      </div>
      <a
        class="previous-page"
        :class="{ disabled: pageCurrent == 1 }"
        @click="pageCurrent = pageCurrent - 1"
      >
        <div class="mi mi-24 mi-previous-page"></div>
      </a>
      <a
        class="next-page"
        :class="{ disabled: pageCurrent == pageEnd }"
        @click="pageCurrent = pageCurrent + 1"
      >
        <div class="mi mi-24 mi-next-page"></div>
      </a>
    </div>
  </div>
</template>
<script>
export default {
  name: "MISAPagination",
  data() {
    return {
      pageSize: 20,
      pageCurrent: 1,
    };
  },
  props: {
    totalRecord: {
      // number of employees
      type: Number,
      required: true,
    },
    isDisabled: {
      type: Boolean,
      default: false,
      required: false,
    },
  },
  computed: {
    // Trang cuối cùng
    pageEnd() {
      return Math.ceil(this.totalRecord / this.pageSize);
    },
  },
  watch: {
    /**
     * Tạo sự kiện truyền pageCurrent mới để set dataTable mới
     * Author: NVDUNG (25/08/2023)
     * @param {*} newValue
     */
    pageCurrent(newValue) {
      this.$emit("changePageCurrent", newValue);
    },
  },
  methods: {
    /**
     * Thay đổi PageSize
     * Author: NVDUNG (25/08/2023)
     */
    changePageSize(newPageSize) {
      try {
        this.pageCurrent = 1;
        // newPageSize là một chuỗi "20 bản ghi"
        let newPageSizeNumber = parseInt(newPageSize.split(" ")[0]);
        this.pageSize = newPageSizeNumber;
        // Phát sự kiện changePageSize để cập nhật dataTable
        this.$emit("changePageSize", newPageSizeNumber);
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>
<style scoped>
@import url(./pagination.css);
</style>
