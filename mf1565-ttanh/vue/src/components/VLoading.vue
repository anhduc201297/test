<template>
    <div class="loading-circle">
        <div class="spinner"></div>
    </div>
</template>

<style lang="scss" scoped>
@import '@/styles/mixins.scss';
.loading-circle {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100%;
    .spinner {
        @include size(40px);
        border-radius: 999px;
        border-width: 4px;
        border-style: solid;
        border-top-color: rgba(0, 0, 0, 0.1);
        border-left-color: rgb(var(--c-primary));
        border-right-color: rgb(var(--c-primary));
        border-bottom-color: rgb(var(--c-primary));
        animation: spin 1s linear infinite;
    }
}

@keyframes spin {
    0% {
        transform: rotate(0deg);
    }
    100% {
        transform: rotate(360deg);
    }
}
</style>
