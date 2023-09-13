class Dialog {
    /**
     * Định nghĩa có kiểu dialog notify có thể có, được dùng để cài đặt dialog
     * Author: NTVu - MF1742
     * Modified: NTVu - 16/08/2023
     */
    static DialogTypes = {
        success: { value: 1, label: 'S' },
        warning: {
            value: 2,
            label: 'W',
            btn: `<button class="m-btn m-btn--sub text-bold">Hủy</button>`,
        },
    };

    dialog;

    /**
     * Khởi tạo Dialog
     * Author: NTVu - MF1742
     * Modified: NTVu - 15/08/2023
     * @param {HTMLElement} dialog
     */
    constructor(dialog) {
        this.dialog = dialog;
    }

    /**
     * Hiện dialog thông báo theo tin nhắn, loại cảnh báo, đồng thời thêm sự kiện cho dialog
     * Author: NTVu - MF1742
     * Modified: NTVu - 15/08/2023
     * @param {string} message
     * @param {number} type
     * @param {CallableFunction} callback
     * @param {OptionalEffectTiming} timeout
     */
    show(message, type, callback, timeout = 5000) {
        $(this.dialog).find('.noti-container').text(message);
        this.loadEvents(type, callback, timeout);
        $(this.dialog).removeClass('m-display-none');
    }

    /**
     * Load các sự kiện cho dialog
     * Author: NTVu - MF1742
     * Modified: NTVu - 16/08/2023
     * @param {number} type
     * @param {CallableFunction} callback
     * @param {OptionalEffectTiming} timeout
     */
    loadEvents(type, callback, timeout) {
        const t = setTimeout(() => {
            this.hide();
        }, timeout);

        $(this.dialog)
            .find('.noti-timeout')
            .css('animation-duration', `${timeout / 1000}s`);
        $('body').append(this.dialog);

        $(this.dialog)
            .find('.pop-footer .m-btn--primary')
            .click(() => {
                clearTimeout(t);
                this.hide();
                callback();
            });

        if (type === Dialog.DialogTypes.warning.value) {
            const btn = $(Dialog.DialogTypes.warning.btn);
            $(btn).click(() => {
                this.hide();
                clearTimeout(t);
            });
            $(this.dialog).find('.btns-left').empty().append(btn);
        }

        window.addEventListener('keydown', (e) => {
            if (e.key === 'Escape') {
                this.hide();
                clearTimeout(t);
            }
        });
    }

    /**
     * Ẩn dialog
     * Author: NTVu - MF1742
     * Modified: NTVu - 15/08/2023
     */
    hide() {
        $(this.dialog).addClass('m-display-none');
    }
}
