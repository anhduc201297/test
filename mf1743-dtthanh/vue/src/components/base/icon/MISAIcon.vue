<template>
    <div class="icon-24">
        <div :class="['icon',`icon--${icon}`]"></div>
    </div>
</template>

<script>
export default {
    name:"MISAIcon",
    props: {
        icon: {
            type:String,
        },
    }
}
</script>

<style scoped></style>