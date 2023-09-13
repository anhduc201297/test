<script setup>
import { ref, watch, computed } from 'vue';

const props = defineProps({
    options: {
        type: Array,
        default: () => []
    }
});

// Declare reactive variables for selected value and filter query
const selectedValue = ref('');
const filterQuery = ref('');

// Declare computed property for filtered options
const filteredOptions = computed(() => {
    return props.options.filter((option) => {
        return option.label.toLowerCase().startsWith(filterQuery.value.toLowerCase());
    });
});

// Watch for changes to the selected value and emit an event
watch(selectedValue, (newValue, oldValue) => {
    if (newValue !== oldValue) {
        this.$emit('update:selectedValue', newValue);
    }
});

// Define methods for handling input and option selection
const handleInput = (event) => {
    filterQuery.value = event.target.value;
};

const handleOptionClick = (option) => {
    selectedValue.value = option.value;
    filterQuery.value = '';
};
</script>

<template>
    <div>
        <input type="text" v-model="filterQuery" @input="handleInput" />
        <ul>
            <li
                v-for="option in filteredOptions"
                :key="option.value"
                @click="handleOptionClick(option)"
            >
                {{ option.label }}
            </li>
        </ul>
    </div>
</template>
