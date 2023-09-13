<template>
    <button :class="{'btn': btnText,'btn--secondary':isSecondary,'icon-btn':btnIcon && !btnText,'btn-with-icon':btnText && btnIcon}" :tabindex="tabIndex">
        <font-awesome-icon v-if="btnIcon" :icon="btnIcon" class="btn__icon"/>
        <div v-if="btnText" class="btn__text">{{ btnText }}</div>
    </button>
</template>

<script>
export default {
    name:"BaseButton",
    props:["btnText","btnIcon","isSecondary","tabIndex"]
}
</script>