<script setup>
import { computed, onMounted, ref, useSlots, watch } from 'vue';

const props = defineProps({
    size: {
        type: String,
        default: 'medium',
        validator(val) {
            return ['large', 'medium', 'small'].includes(val);
        }
    },
    width: {
        type: String,
        default: 'medium',
        validator(val) {
            return ['full', 'extra-large', 'large', 'medium', 'small', 'extra-small'].includes(
                val
            );
        }
    },
    id: {
        type: String,
        required: true
    },
    tooltip: {
        type: Boolean,
        default: true
    },
    value: [String, Number],
    required: Boolean,
    firstFocus: Boolean,
    disabled: Boolean,
    class: String,
    title: String,
    placeholder: String,
    errMsgs: Object,
    pattern: RegExp
});

const emit = defineEmits(['update:input', 'addErrorMessage', 'removeErrorMessage']);

/**
 * Biến hasLabel và hasIcon kiểm tra xem Textfield có label và icon không.
 * Nếu có, thêm class 'has-label' cho label tag và 'has-icon' cho input tag.
 */
const slots = useSlots();
const hasLabel = slots.label;
const hasIcon = slots.icon;

const inputRef = ref(null);
const inputValue = ref(props.value);

// when mounted, focus the first input
onMounted(() => {
    if (props.firstFocus) inputRef.value.focus();
});

// overflow text
const isOverflowInput = ref(false);

/**
 * === CHỨC NĂNG VALIDATE INPUT ===
 */
// Check empty input
const isEmpty = computed(() => {
    return props.required && (!inputValue.value || !inputValue.value.trim());
});

// Check invalid input
const isInvalid = computed(() => {
    return props.pattern && !props.pattern.test(inputValue.value);
});

// trả về thông báo lỗi dựa vào isEmpty / isInvalid
const errorMessage = computed(() => {
    if (isEmpty.value) {
        return props.errMsgs.isEmpty;
    } else if (isInvalid.value) {
        return props.errMsgs.isInvalid;
    } else {
        return '';
    }
});

/**
 * Khi thay đổi giá trị input,
 * cho phép có thể hiển thị errorMessage nếu input trống hoặc không hợp lệ.
 * Emit dữ liệu inputValue lên TablePopup component.
 */
const showErrorInput = ref(false);
const showErrorMessage = ref(false);
const handleInput = (event) => {
    emit('update:input', event.target.value);
    inputValue.value = String(inputRef.value.value);

    if (isEmpty.value || isInvalid.value) {
        showErrorInput.value = true;
        showErrorMessage.value = true;
    }

    // nếu input vượt quá width, gán isOverflowInput = true để hiển thị tooltip khi hover.
    if (inputRef.value.scrollWidth > inputRef.value.clientWidth) isOverflowInput.value = true;
    else isOverflowInput.value = false;
};

//Khi thay đổi giá trị input, emit event addErrorMessage và thông báo lỗi
const handleChange = () => {
    if (props.errMsgs) {
        emit('removeErrorMessage', props.errMsgs.isEmpty);
        emit('removeErrorMessage', props.errMsgs.isInvalid);
        if (errorMessage.value) emit('addErrorMessage', errorMessage.value);
    }
};

onMounted(() => {
    if (props.errMsgs) {
        emit('removeErrorMessage', props.errMsgs.isEmpty);
        emit('removeErrorMessage', props.errMsgs.isInvalid);
        if (errorMessage.value) emit('addErrorMessage', errorMessage.value);
    }
});

// nếu trạng thái isEmpty / isInvalid thay đổi, thay đổi giá trị showErrorMessage
watch([isEmpty, isInvalid], () => (showErrorMessage.value = isEmpty.value || isInvalid.value));

/**
 * Dựa vào props size và width được truyền vào,
 * gán custom class để style cho mỗi size và width của input tương ứng.
 */
const inputGroupClass = computed(() => ['input-group', `input-width-${props.width}`]);
const inputClass = computed(() => [
    `input-${props.size}`,
    `input-width-${props.width}`,
    { 'input-has-icon': hasIcon },
    { 'input-error': showErrorInput.value && (isEmpty.value || isInvalid.value) },
    { 'input-overflow': props.tooltip && isOverflowInput.value },
    props.class
]);
</script>

<template>
    <div class="textfield">
        <div class="label-group" v-if="hasLabel">
            <label :for="id" :title="title">
                <slot name="label"></slot>
            </label>
            <span class="required-mark" v-if="required">&nbsp;*</span>
        </div>
        <div :class="inputGroupClass">
            <input
                type="text"
                :id="id"
                :placeholder="placeholder"
                :disabled="disabled"
                :class="inputClass"
                :value="inputValue"
                @input="handleInput($event)"
                @change="handleChange"
                ref="inputRef"
            />
            <i class="icon" v-if="hasIcon">
                <slot name="icon"></slot>
            </i>
        </div>
        <span class="tooltip" v-if="showErrorMessage || (tooltip && isOverflowInput)">
            {{ errorMessage ? errorMessage : inputValue }}
        </span>
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

.textfield {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
}

.input-small {
    height: $--input-small-height;
    padding: $--input-small-padding-y $--input-padding-x;
}
.input-medium {
    height: $--input-medium-height;
    padding: $--input-medium-padding-y $--input-padding-x;
}
.input-large {
    height: $--input-large-height;
    padding: $--input-large-padding-y $--input-padding-x;
}

.label-group {
    margin-bottom: 8px;
    label {
        @include font(14, 500);
        color: $--label-color;
    }
    .required-mark {
        color: $--label-required-mark-color;
    }
}

.input-group {
    input {
        @include font(14);
        font-family: var(--font-family-system);
        text-overflow: ellipsis;

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

    .icon {
        @include size(20px);
        position: absolute;
        top: 50%;
        right: 8px;
        transform: translateY(-50%);
    }
}

/* Style tooltip */
.tooltip {
    position: absolute;
    left: 16px;
    display: none;
    top: 52px;
    z-index: 5;

    @include font(12);
    padding: 4px;
    border-radius: 2px;
    color: $--error-message-color;
    background-color: $--error-message-bg-color;

    .input-group:has(.input-error:hover) + & {
        display: flex;
    }
    .input-group:has(.input-overflow:hover) + & {
        display: flex;
    }
}

/* === Width of input === */
.input-width-full {
    width: 100%;
}

.input-width-extra-large {
    width: 250px;
}
.input-width-large {
    width: 200px;
}
.input-width-medium {
    width: 180px;
}
.input-width-small {
    width: 150px;
}
.input-width-extra-small {
    width: 120px;
}
</style>
