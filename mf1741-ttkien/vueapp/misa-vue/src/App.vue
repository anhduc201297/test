<template>
  <div id="main">
    <TheSideBar></TheSideBar>
    <TheContainer></TheContainer>
  </div>
</template>

<script>
import TheSideBar from "./components/layout/sidebar/TheSidebar.vue";
import TheContainer from "./components/layout/container/TheContainer.vue";

export default {
  name: "App",
  components: {
    TheSideBar,
    TheContainer,
  },
};
</script>

<style>
@import url(./App.css);
@import url(./components/bases/input/ms-combobox.css);
</style>
