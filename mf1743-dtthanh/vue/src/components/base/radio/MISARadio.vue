<template>
    <label class="center" for="">
        <input type="radio" :name="name"/>
        <span>{{ text }}</span>
    </label>
</template>

<script>
export default {
    name: "MISARadio",
    props: {
        name: {
            type:String,
        },
        text: {
            type:String,
        },

    }

}
</script>

<style scoped>
@import url(./radio.css);
</style>