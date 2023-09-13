<template>
    <button>{{ title }}</button>
</template>

<script>
export default {
    name: "MISAButton",
    props: ['title']
}
</script>

<style scoped>
@import url(./MISAButton.css);
</style>