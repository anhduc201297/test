<template>
  <div class="">
    <TheHeader></TheHeader>
    <div class="container">
      <TheSidebar></TheSidebar>
      <TheMainContent></TheMainContent>
    </div>
  </div>
</template>
<!-- eslint-disable -->
<script>
import TheHeader from '@/components/layout/TheHeader.vue'
import TheMainContent from '@/components/layout/TheMainContent.vue'
import TheSidebar from '@/components/layout/TheSidebar.vue'
export default {
  name: 'App',
  components: {
    TheHeader,
    TheMainContent,
    TheSidebar
  }
}
</script>

<style>
@import url(./css/main.css);
</style>


