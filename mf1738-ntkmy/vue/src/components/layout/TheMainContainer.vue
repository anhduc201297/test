<template>
  <!-- Main Container -->
  <div class="content-main">
    <router-view></router-view>
  </div>
</template>

<script>
export default {
    name: 'TheMainContainer',
}
</script>

<style scoped>
@import url("../../css/layout/main-container.css");
@import url('../../css/base/icon.css');
@import url('../../css/base/table.css');
</style>
