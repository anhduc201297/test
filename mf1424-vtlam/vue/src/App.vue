<template>
  <TheHeader />
  <TheSideBar />
  <TheMainContent />
</template>

<script>
import TheHeader from './components/layout/TheHeader.vue'
import TheSideBar from './components/layout/TheSideBar.vue'
import TheMainContent from './components/layout/TheMainContent.vue'

export default {
  name: 'App',
  components: {
    TheHeader, TheSideBar, TheMainContent
  }
}
</script>

<style>
@import url(./css/main.css)
</style>
