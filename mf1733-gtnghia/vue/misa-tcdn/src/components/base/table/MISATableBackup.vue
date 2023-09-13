<template>
  <div class="content-container mt-8">
    <div class="content__header">
      <div class="content__header--start">
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
          />
          <i class="icon icon-search-detail i-search"></i>
        </div>
        <m-button
          :type="'icon'"
          class="btn-reload-data"
          :icon="'icon-reload'"
        />
      </div>
    </div>
    <div class="content__content scrollbar mt-8">
      <table class="table-pri">
        <thead>
          <slot name="thead"></slot>
        </thead>
        <tbody>
          <slot name="tbody"></slot>
        </tbody>
        <tfoot></tfoot>
      </table>
    </div>
    <div class="content__footer">
      <div class="content__footer--start">
        <div class="records-count">
          <span>Tổng số bản ghi: </span>
          <span><strong>100</strong></span>
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
  },
  data() {
    return {
    };
  },
  methods: {
  },
};
</script>

<style scoped>
@import url(./table);
</style>
