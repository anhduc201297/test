<template>
  <div class="employeelist">
    <TheHeader></TheHeader>
    <TheSidebar></TheSidebar>
    <TheMainContainer></TheMainContainer>
  </div>
</template>

<script>
import TheHeader from './components/layout/TheHeader.vue'
import TheSidebar from './components/layout/TheSidebar.vue'
import TheMainContainer from './components/layout/TheMainContainer.vue'
export default {
  name: 'App',
  components: {
    TheHeader, TheSidebar, TheMainContainer
  }
}
</script>

<style>
@import url("./css/main.css");
.employeelist {
  display: grid;
  grid-template-columns: 200px calc(100% - 200px);
  grid-template-rows: 56px calc(100% - 56px);
  width: 100vw;
  height: 100vh;
  grid-template-areas:
                  "area1 area2 "
                  "area1 area3 "
}
</style>
