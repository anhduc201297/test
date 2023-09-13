<template>
  <div class="loading-background">
    <div class="loader"></div>
  </div>
</template>

<script>
export default {
    name:'MISALoading',
    data() {
    return {
    
    };
  },
 
}
</script>

<style>
.loading-background {
  width: 100%;
  background: rgba(0, 0, 0, 0.1);
  position: fixed;
  left: 0;
  top: 0;
  height: 100%;
  z-index: 98;
  opacity: 1;
  display: flex;
  justify-content: center;
  align-content: center;
}
.loader {
  border: 4px solid #2CA01C;
  border-radius: 50%;
  border-top: 4px solid #ffffff;
  width: 40px;
  height: 40px;
  -webkit-animation: spin 2s linear infinite; /* Safari */
  animation: spin 2s linear infinite;
  position: relative;
  bottom: -45vh;
}

/* Safari */
@-webkit-keyframes spin {
  0% { -webkit-transform: rotate(0deg); }
  100% { -webkit-transform: rotate(360deg); }
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}
</style>