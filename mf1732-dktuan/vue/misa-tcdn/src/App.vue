<template>
  <MISAHeader @onExtend="handleExtendSidebar"></MISAHeader>
  <MISASidebar :class="{'shortened': shortened}"></MISASidebar>
  <MISAContainer></MISAContainer>
</template>

<script>
import MISAHeader from '@/components/layout/header/TheHeader.vue'
import MISAContainer from '@/components/layout/container/TheContainer.vue'
import MISASidebar from '@/components/layout/sidebar/TheSidebar.vue'
export default {
  name: 'App',
  components: {
    MISAContainer,
    MISAHeader,
    MISASidebar
  },
  methods: {
    /**
   * mở rộng hoặc thu gọn sidebar
   * Author: dktuan (19/08/2023)
   */
    handleExtendSidebar() {
        this.shortened=!this.shortened
    }
  },

  data: () => ({
    shortened: false,
  }),
}
</script>

<style scoped>
@import url(./css/main.css);
</style>