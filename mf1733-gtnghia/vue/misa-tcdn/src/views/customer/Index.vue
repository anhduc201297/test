<template>
  <div class="main-container" v-if="isIndexPage">
    Quản lý khách hàng
    <div class="mt-8">
      <m-button
        :route="'customer.list'"
        :title="'Danh sách khách hàng'"
        :type="'route'"
      >
      </m-button>
    </div>
    <div class="mt-8">
      <m-button
        :route="'customer.list.form'"
        :title="'Thêm mới khách hàng'"
        :type="'route'"
      >
      </m-button>
    </div>
  </div>
  <router-view v-else></router-view>  
</template>

<script>
export default {
  name: "CustomerIndex",
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
      // console.log(this.$route);
      // console.log(this.$route.matched[0].name);
      // console.log(this.$route.matched.length);
      if (this.$route.matched.length > 1) this.isIndexPage = false;
      else this.isIndexPage = true;
    },
  },
};
</script>

<style scoped></style>
