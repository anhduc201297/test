<script setup>
const props = defineProps({
    id: String,
    label: String,
    checked: Boolean
});
</script>

<template>
    <div class="checkbox">
        <div class="checkbox-input">
            <input type="checkbox" :id="id" :checked="props.checked" />
            <span class="checkmark"></span>
        </div>
        <label :for="id" v-if="label">{{ label }}</label>
    </div>
</template>

<style lang="scss" scoped>
@import '@/styles/mixins.scss';
/* Styles for checkbox */
.checkbox {
    position: relative;
    display: flex;
    align-items: center;
}

/* Styles for checkbox-input */
.checkbox-input {
    position: relative;
    display: flex;
}

/* Styles for default input */
.checkbox-input > input[type='checkbox'] {
    margin: 0;
    opacity: 0;

    @include size(18px);
    z-index: 3;
    cursor: pointer;
}

/* Style for checkmark */
.checkbox-input > .checkmark {
    @include centerAbsolute('both');
    @include size(18px);
    border: 1px solid rgb(var(--c-gray-400));
    border-radius: 4px;
}

/* When input is checked, change background-color of checkmark*/
.checkbox-input > input[type='checkbox']:checked + .checkmark {
    background-color: rgb(var(--c-primary));
    border: none;
}

/* When input is checked, display check icon */
.checkbox-input > input[type='checkbox']:checked + .checkmark::before {
    @include centerAbsolute('both');

    content: '';
    display: block;
    transform: translate(-50%, -50%) scale(80%);

    /* Get check icon in file sprites */
    background: url('@/assets/images/sprites.svg') no-repeat -1273px -363px;
    width: 14px;
    height: 11px;
    filter: brightness(200%);
}

/* Style for label */
.checkbox label {
    display: inline-block;
    position: relative;
    padding-left: 8px;
    cursor: pointer;
    -webkit-user-select: none;
    user-select: none;
}
</style>
