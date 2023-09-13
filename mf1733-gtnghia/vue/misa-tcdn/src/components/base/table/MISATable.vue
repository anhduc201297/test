<template>
  <div class="content-container mt-8">
    <div class="content__header">
      <div class="content__header--start">
        <div v-show="countSelectedOptions()" class="option-box">
          <span class="mr-24">Đã chọn: {{ countSelectedOptions() }}</span>
          <m-button
            :type="'extra'"
            :title="'Bỏ chọn'"
            :size="'s'"
            class="mr-8"
            @click="onUnCheckAll"
          />
          <m-button
            :type="'error'"
            :title="'Xóa tất cả'"
            :size="'s'"
            @click="onShowDeleteDialog"
          />
        </div>
        <slot name="header--start"></slot>
      </div>
      <div class="content__header--center">
        <slot name="header--center"></slot>
      </div>
      <div class="content__header--end">
        <slot name="header--end"></slot>
        <div class="search-box mr-8">
          <m-input
            :name="'search'"
            :placeholder="placeholderSeachbox"
            :size="'s'"
            :padding="'pr-24'"
          />
          <i class="icon icon-search-detail i-search"></i>
        </div>
        <m-button
          :type="'icon'"
          class="btn-reload-data"
          :icon="'icon-reload'"
          @click="reloadData"
        />
      </div>
    </div>
    <div class="content__content scrollbar mt-8">
      <table class="table-pri">
        <thead>
          <tr class="table-pri__row">
            <th
              v-for="(title, index) in tableTitle"
              :key="index"
              class="table-pri__title"
              :class="title.align"
            >
              <input
                v-if="index === 0"
                class="id table-checkbox checkbox-item-16"
                type="checkbox"
                v-model="isCheckedAll"
                @click="onCheckAll"
              />
              {{ title.title }}
            </th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(data, indexRow) in tableData"
            :key="data[tableField[0].label]"
            class="table-pri__row"
          >
            <td
              v-for="(field, index) in tableField"
              :key="index"
              class="table-pri__body"
              :class="field.align"
            >
              <input
                v-if="index === 0"
                class="id table-checkbox checkbox-item-16"
                type="checkbox"
                v-model="checkedBoxes[indexRow]"
              />
              {{ formatField(data[field.label], field.label) }}
            </td>
            <td class="table-pri__body">
              <div class="combo-box-s">
                <m-button
                  class="btn-employee-change"
                  :eid="data[tableField[0].label]"
                  :type="'link'"
                  :title="'Sửa'"
                  :size="'s'"
                />
                <m-button
                  class="btn-context-menu"
                  :eid="data[tableField[0].label]"
                  :type="'icon'"
                  :icon="'icon-arrow-blue-solid-bottom'"
                  :title="'Chức năng khác'"
                  :size="'s'"
                />
              </div>
            </td>
          </tr>
        </tbody>
        <tfoot></tfoot>
      </table>
    </div>
    <div class="content__footer">
      <div class="content__footer--start">
        <div class="records-count">
          <span>Tổng số bản ghi: </span>
          <span
            ><strong>{{ this.tableData.length }}</strong></span
          >
        </div>
      </div>
      <div class="content__footer--end">
        <div class="title-record">
          <p>Bản ghi/Trang</p>
        </div>
        <div class="num-record-option">
          <select class="num-record" name="" id="">
            <option value="10" selected>10</option>
            <option value="20">20</option>
            <option value="30">30</option>
          </select>
        </div>
        <div class="cur-record">
          <p class="subheader">1 - 9 bản ghi</p>
        </div>
        <div class="page-controller">
          <m-button
            :type="'icon'"
            class="page-down"
            :icon="'icon-arrow-blue-left'"
          />
          <m-button
            :type="'icon'"
            class="page-up"
            :icon="'icon-arrow-blue-right'"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    placeholderSeachbox: String,
    tableTitle: Array,
    tableData: Array,
    tableField: Array,
    tableKey: String,
    API: Object,
  },
  data() {
    return {
      checkedBoxes: Array(this.tableData.length).fill(false),
      isCheckedAll: false,
    };
  },
  created() {
    this.$emitter.on("confirmDelete", this.deleteCheckedRows);
  },
  beforeUnmount() {
    this.$emitter.off("confirmDelete", this.deleteCheckedRows);
  },
  methods: {
    /**
     * Function filter number of checked box
     * Created by: mf1733-gtnghia 3/9/2023
     */
    countSelectedOptions() {
      return this.checkedBoxes.filter((e) => e === true).length;
    },
    /**
     * toggle check all button
     * Created by: mf1733-gtnghia 3/9/2023
     */
    onCheckAll() {
      if (!this.isCheckedAll === true) {
        this.checkedBoxes.fill(true);
      } else {
        this.checkedBoxes.fill(false);
      }
    },
    /**
     * Un check all button
     * Created by: mf1733-gtnghia 3/9/2023
     */
    onUnCheckAll() {
      this.isCheckedAll = false;
      this.checkedBoxes.fill(false);
    },
    /**
     * Format data field
     * Created by: mf1733-gtnghia 3/9/2023
     */ 
    formatField(item, field) {
      if (field === "DateOfBirth") {
        item = this.$MISAHelper.formatDate(item);
      }
      if (field === "Gender") {
        item = this.$MISAHelper.formatGender(item);
      }
      return item;
    },
    /**
     * Show delete dialog
     * Created by: mf1733-gtnghia 3/9/2023
     */
    onShowDeleteDialog() {
      this.$emitter.emit(
        "showDialog",
        "error",
        "Xóa tất cả",
        "Bạn có muốn xóa tất cả các bản ghi đã lựa chọn",
        "feature",
        "confirmDelete"
      );
    },
    /**
     * Delete selected rows
     * Created by: mf1733-gtnghia 3/9/2023
     */
    async deleteCheckedRows() {
      const filteredIndexCheckedboxes = this.checkedBoxes.reduce(
        (indexArray, value, index) => {
          if (value === true) {
            indexArray.push(index);
          }
          return indexArray;
        },
        []
      );
      for (let i = 0; i < filteredIndexCheckedboxes.length; i++) {
        const element = filteredIndexCheckedboxes[i];
        const row = this.tableData[element];
        await this.$sendRequest(
          `${this.API.delete}${row[this.tableKey]}`,
          "DELETE"
        );
      }

      this.reloadData();
    },
    /**
     * Send reload event
     * Created by: mf1733-gtnghia 3/9/2023
     */
    reloadData() {
      this.$emit("reloadData");
    },
  },
  watch: {
    checkedBoxes: {
      /**
       * Synchronize check-single button and check-all button
       * Created by: mf1733-gtnghia 3/9/2023
       */
      handler() {
        if (this.countSelectedOptions() === this.checkedBoxes.length) {
          this.isCheckedAll = true;
        } else {
          this.isCheckedAll = false;
        }
      },
      deep: true,
    },
  },
  computed: {},
};
</script>

<style scoped>
@import url(./table);
</style>
