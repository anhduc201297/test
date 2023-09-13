<script setup>
defineProps({
    id: {
        type: String,
        required: true
    },
    name: {
        type: String,
        required: true
    },
    value: {
        type: Number,
        required: true
    },
    checked: {
        type: Boolean
    },
    disabled: {
        type: Boolean,
        default: false
    }
});

const emit = defineEmits(['checkRadio']);

const handleChange = (event) => {
    if (event.target.checked) {
        emit('checkRadio', event.target.value);
    }
};
</script>

<template>
    <div class="radio-button">
        <div class="radio-input">
            <input
                type="radio"
                :name="name"
                :id="id"
                :value="value"
                :checked="checked"
                @change="handleChange"
            />
            <span class="checkmark"></span>
        </div>
        <label :for="id">
            <slot name="label"></slot>
        </label>
    </div>
</template>

<style lang="scss" scoped>
@import '@/styles/mixins.scss';

$--label-color: rgb(var(--c-gray-900));
$--label-required-mark-color: rgb(var(--c-red-500));

$--input-small-height: 32px;
$--input-small-padding-y: 6px;

$--input-medium-height: 36px;
$--input-medium-padding-y: 8px;

$--input-large-height: 40px;
$--input-large-padding-y: 10px;

$--input-padding-x: 12px;
$--input-border-color: rgb(var(--c-gray-300));
$--input-placeholder-color: rgb(var(--c-gray-500));
$--input-hover-bg-color: rgb(var(--c-gray-100));
$--input-error-border-color: rgb(var(--c-red-500));

$--error-message-color: rgb(var(--c-white));
$--error-message-bg-color: rgb(var(--c-gray-900));

.radio-button {
    display: flex;
    align-items: center;
}

.radio-input {
    display: flex;
}

input {
    @include font(14);
    font-family: var(--font-family-system);

    border-radius: 4px;
    border: 1px solid $--input-border-color;
    outline: none;

    &::placeholder {
        @include font(14);
        color: $--input-placeholder-color;
    }
    &.input-error {
        border-color: $--input-error-border-color !important;
        &:hover,
        &:focus {
            border-color: $--input-error-border-color !important;
        }
    }

    &:not(.input-error):hover {
        background-color: $--input-hover-bg-color;
    }

    &:not(.input-error):focus {
        border-color: rgb(var(--c-primary));
    }

    &.input-has-icon {
        padding-right: 32px;
    }
}

label {
    @include font(14);
}

.icon {
    position: absolute;
    top: 50%;
    right: 8px;
    transform: translateY(-50%);
}

/* Styles for default input */
.radio-input > input[type='radio'] {
    @include size(18px);
    margin: 0;

    /* hide but also checked default input */
    opacity: 0;
    z-index: 3;
    cursor: pointer;
}

/* Style for checkmark */
.radio-input > .checkmark {
    @include size(18px);
    @include centerAbsolute('both');

    border-radius: 999px;
    border: 1px solid rgb(var(--c-gray-400));
    background-color: rgb(var(--c-white));
}

/* When input is checked, change background-color of checkmark*/
.radio-input > input[type='radio']:checked + .checkmark {
    border-color: rgb(var(--c-primary));
}

/* When input is checked, display check icon */
.radio-input > input[type='radio']:checked + .checkmark::before {
    @include centerAbsolute('both');
    @include size(10px);

    content: '';
    display: block;
    background: rgb(var(--c-primary));
    border-radius: 999px;
}

/* Style for label */
.radio-button label {
    display: inline-block;
    padding-left: 8px;
    cursor: pointer;
}
</style>
