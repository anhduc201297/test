<script setup>
import { computed } from 'vue';

const props = defineProps({
    type: {
        type: String,
        default: 'primary',
        validator(val) {
            return ['primary', 'secondary', 'outline', 'icon'].includes(val);
        }
    },
    size: {
        type: String,
        default: 'medium',
        validator(val) {
            return ['extra-large', 'large', 'medium', 'small'].includes(val);
        }
    },
    colorScheme: {
        type: String,
        default: 'primary',
        validator(val) {
            return ['primary', 'red'].includes(val);
        }
    },
    class: String || Object || Array,
    disabled: Boolean
});

/**
 * Based on the type and size props passed in,
 * assign classes to style each corresponding type and size of button.
 */
const buttonClass = computed(() => [
    'button',
    `button-${props.type}`,
    `button-${props.size}`,
    `button-${props.colorScheme}-colorscheme`,
    props.class
]);

/**
 * If the disabled prop is passed in, set the disabled state for the button.
 */
const isDisabled = computed(() => props.disabled);
</script>

<template>
    <button :class="buttonClass" :disabled="isDisabled">
        <slot name="icon"></slot>
        <slot></slot>
    </button>
</template>

<style scoped lang="scss">
@import '@/styles/mixins.scss';

$--button-min-width: 80px;
$--button-padding-x: 16px;

$--button-small-height: 32px;
$--button-small-padding-y: 7px;

$--button-medium-height: 36px;
$--button-medium-padding-y: 9px;

$--button-large-height: 40px;
$--button-large-padding-y: 11px;

$--button-extra-large-height: 48px;
$--button-extra-large-padding-y: 15px;

$--button-primary-color: rgb(var(--c-white));
$--button-primary-bg-color: rgb(var(--c-primary));
$--button-primary-hover-bg-color: rgb(var(--c-light-green-500));
$--button-primary-pressed-bg-color: rgb(var(--c-green-600));

$--button-secondary-color: rgb(var(--c-primary));
$--button-secondary-bg-color: rgb(var(--c-white));
$--button-secondary-hover-bg-color: rgb(var(--c-gray-100));
$--button-secondary-pressed-bg-color: rgb(var(--c-gray-200));

$--button-outline-color: rgb(var(--c-gray-900));
$--button-outline-bg-color: rgb(var(--c-white));
$--button-outline-border-color: rgb(var(--c-gray-300));
$--button-outline-hover-bg-color: rgb(var(--c-gray-100));
$--button-outline-pressed-bg-color: rgb(var(--c-gray-200));

$--button-icon-color: rgb(var(--c-green-500));
$--button-icon-bg-color: rgb(var(--c-light-green-100));
$--button-icon-pressed-bg-color: rgb(var(--c-green-500));
$--button-icon-padding-x: 10px;
$--button-icon-padding-y: 8px;

$--button-red-colorscheme-color: rgb(var(--c-white));
$--button-red-colorscheme-bg-color: rgb(var(--c-red-500));
$--button-red-colorscheme-hover-bg-color: rgb(var(--c-red-400));
$--button-red-colorscheme-pressed-bg-color: rgb(var(--c-red-600), 0.8);

.button-small {
    height: $--button-small-height;
    padding: $--button-small-padding-y $--button-padding-x;
}
.button-medium {
    height: $--button-medium-height;
    padding: $--button-medium-padding-y $--button-padding-x;
}
.button-large {
    height: $--button-large-height;
    padding: $--button-large-padding-y $--button-padding-x;
}
.button-large {
    height: $--button-large-height;
    padding: $--button-large-padding-y $--button-padding-x;
}
.button-extra-large {
    height: $--button-extra-large-height;
    padding: $--button-extra-large-padding-y $--button-padding-x;
}

.button {
    display: flex;
    justify-content: center;
    align-items: center;
    border-radius: 4px;

    @include font(14);
    font-family: var(--font-family-system);
    font-weight: 500;
    cursor: pointer;

    &:not(.button-icon) {
        min-width: $--button-min-width;
    }
}

.button-primary {
    color: $--button-primary-color;
    background-color: $--button-primary-bg-color;
    border: none;
    &:hover:not(:disabled) {
        background-color: $--button-primary-hover-bg-color;
    }
    &:active:not(:disabled) {
        background-color: $--button-primary-pressed-bg-color;
    }
}

.button-secondary {
    color: $--button-secondary-color;
    background-color: $--button-secondary-bg-color;
    border: none;
    &:hover:not(:disabled) {
        background-color: $--button-secondary-hover-bg-color;
    }
    &:active:not(:disabled) {
        background-color: $--button-secondary-pressed-bg-color;
    }
}

.button-outline {
    border: 1px solid $--button-outline-border-color;
    color: $--button-outline-color;
    background-color: $--button-outline-bg-color;
    &:hover:not(:disabled) {
        background-color: $--button-outline-hover-bg-color;
    }
    &:active:not(:disabled) {
        background-color: $--button-outline-pressed-bg-color;
    }
}

.button-icon {
    padding: $--button-icon-padding-y $--button-icon-padding-x !important;
    color: $--button-icon-color;
    background-color: $--button-icon-bg-color;
    border: none;
    &:active:not(:disabled) {
        background-color: $--button-icon-pressed-bg-color;
    }
}

.button:disabled {
    cursor: not-allowed;
    opacity: 0.56;
}

.button-red-colorscheme {
    color: $--button-red-colorscheme-color;
    background-color: $--button-red-colorscheme-bg-color;
    border: none;
    &:hover:not(:disabled) {
        background-color: $--button-red-colorscheme-hover-bg-color;
    }
    &:active:not(:disabled) {
        background-color: $--button-red-colorscheme-pressed-bg-color;
    }
}
</style>
