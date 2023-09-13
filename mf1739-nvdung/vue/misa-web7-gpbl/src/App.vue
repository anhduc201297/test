<template>
  <TheSidebar></TheSidebar>
  <main>
    <TheHeader></TheHeader>
    <TheMainContent></TheMainContent>
  </main>
</template>

<script>
import TheSidebar from "./components/layout/thesidebar/TheSidebar.vue";
import TheHeader from "./components/layout/theheader/TheHeader.vue";
import TheMainContent from "./components/layout/themaincontent/TheMainContent.vue";
export default {
  components: {
    TheSidebar,
    TheHeader,
    TheMainContent,
  },
};
</script>

<style></style>
