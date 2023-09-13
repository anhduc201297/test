import { defineStore } from 'pinia';
import { ref } from 'vue';

export const useToastMessageStore = defineStore('toastMessageState', () => {
    const isShow = ref(false)
    const init = {
        type: '',
        text: ''
    }

    const current = ref(init)

    // show toast message và đặt thời gian hiển thị là 5 giây.
    const show = (type, text) => {
        if (type) current.value.type = type
        if (text) current.value.text = text
        isShow.value = true
        setTimeout(() => isShow.value = false, 5000);
    }

    // ẩn toast message
    const hide = () => {
        current.value = init
        isShow.value = false
    };

    return {
        current,
        isShow,
        show,
        hide,
    };
});
