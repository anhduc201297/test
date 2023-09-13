<template>
    <!-- Input DateTime -->
    <input
        class="input__datetime"
        type="date"
        id="datetime"
        v-model="datetime"
      />
</template>
  
<script>
    export default {
        name: 'MISAInputDateTime',
        data() {
            return {
                datetime: '', 
            };
        },
}
</script>
<style scoped>
@import url('../../../css/base/input.css');
</style>
  