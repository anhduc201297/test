<template>
  <div class="main-container" v-if="isIndexPage">
    Quản lý Nhân viên
    <div class="mt-8">
      <m-button
        :route="'employee.list'"
        :title="'Danh sách nhân viên'"
        :type="'route'"
      >
      </m-button>
    </div>
    <div class="mt-8">
      <m-button
        :route="'employee.list.form'"
        :title="'Chi tiết nhân viên'"
        :type="'route'"
      >
      </m-button>
    </div>
  </div>
  <router-view v-else></router-view>
</template>

<script>
export default {
  name: "EmployeeIndex",
  data() {
    return {
      isIndexPage: true,
      numberOfSlashes: 1,
    };
  },
  beforeMount() {
    this.renderRouterview();
  },
  beforeUpdate() {
    this.renderRouterview();
  },
  methods: {
    renderRouterview() {
      if (this.$route.matched.length > 1) this.isIndexPage = false;
      else this.isIndexPage = true;
    },
  },
};
</script>

<style scoped></style>
