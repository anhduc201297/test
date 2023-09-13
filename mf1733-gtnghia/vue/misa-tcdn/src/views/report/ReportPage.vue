<template>
  <div>
    <m-button :route="'report.child'" :title="'Báo cáo'" :type="'route'">
    </m-button>
    <!-- <router-view name="report"> </router-view> -->
    <router-view name="top"> </router-view>
    <router-view name="bottom"> </router-view>
  </div>
</template>

<script>
export default {
  name: "ReportPage",
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
      console.log(this.$route.matched.length);
      if (this.$route.matched.length > 1) this.isIndexPage = false;
      else this.isIndexPage = true;
    },
  },
};
</script>

<style scoped></style>
