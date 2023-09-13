<template>
  <TheHeader/>
  <TheSidebar/>
  <TheContent/>
  <m-loading v-if="showLoading"/>
  
</template>

<script>

import TheHeader from './components/layout/TheHeader.vue';
import TheContent from './components/layout/TheContent.vue';
import TheSidebar from './components/layout/TheSidebar.vue';

export default {
  name: 'App',
  components: {
    TheContent, TheHeader, TheSidebar
  },
  provide:{
    showToast: false,
  }, 
  created(){
    this.$emitter.on("onShowLoading", this.onShowLoading);
  },
  beforeUnmount(){
    this.$emitter.off("onShowLoading", this.onShowLoading);
  },
  methods: {
    onShowLoading: function(){
      this.showLoading = !this.showLoading;
    }
  },
  data() {
    return {
      showLoading:false,
    }
  },
}
</script>

<style>
@import url(./css/main.css);
</style>
