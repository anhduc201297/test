<template>
  <div class="main-container" v-if="isIndexPage">
    Quản lý khách hàng
    <div class="mt-8">
      <m-button :route="'report.detail'" :title="'Báo cáo'" :type="'route'">
      </m-button>
    </div>
    <div class="mt-8">
      <m-button
        :route="'report.detail.id'"
        :title="'Chi tiết báo cáo'"
        :type="'route'"
      >
      </m-button>
    </div>
  </div>
  <router-view v-else></router-view>
</template>

<script>
export default {
  name: "ReportIndex",
  data() {
    return {
      isIndexPage: true,
      numberOfSlashes: 1,
      isChecked: false,
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
      console.log(this.$route.name);
      console.log(this.$route.matched);
      console.log(this.$route.matched.length);
      if (this.$route.matched.length > 1) this.isIndexPage = false;
      else this.isIndexPage = true;
    },
  },
};
</script>

<style scoped></style>
