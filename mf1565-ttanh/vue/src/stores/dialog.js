import { defineStore } from 'pinia';
import { ref } from 'vue';

export const useDialogStore = defineStore('dialogState', () => {
    const current = ref('')

    // hiển thị dialog theo name
    const show = (name) => {
        if (name) current.value = name
    }

    // ẩn dialog
    const hide = () => {
        current.value = ''
    };

    return {
        current,
        show,
        hide,
    };
});
