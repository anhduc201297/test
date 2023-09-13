<template>
    <!-- Tooltip -->
    <span class="tooltiptext">
        {{ tooltip }}
    </span>
</template>
  
<script>
export default {
    name: 'MISATooltip',
    props:["tooltip"]
};
</script>

<style scoped>
@import url('../../css/base/tooltip.css');
</style>
  