<script setup>
import { computed } from 'vue';

const props = defineProps({
    class: { type: String, required: true }
});

const iconClass = computed(() => ['icon', props.class]);
</script>

<template>
    <div :class="iconClass"></div>
</template>

<style lang="scss" scoped>
@import '@/styles/mixins.scss';

$--icon-size-default: 20px;
.icon {
    @include size($--icon-size-default);
    @include centerAbsolute();
    display: inline-block;
    background: url('@/assets/images/sprites.svg') no-repeat;
}
</style>
