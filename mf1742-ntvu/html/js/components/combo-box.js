class ComboBox {
    /**
     * Load tất cả Combo box và thêm event mặc định: sự kiện focus thì hiện dropList
     * Author: NTVu - MF1742
     * Modified: 15/08/2023 - 10h53 AM by NTVu
     */
    constructor() {
        const comboBoxes = $('.drop-toggle').parent('.relative');

        for (const el of [...comboBoxes]) {
            ComboBox.loadEvents(el);
        }
    }

    /**
     * Thêm event show dropdown khi click vào button toggle
     * Author: NTVu - MF1742
     * Modified: 15/08/2023 - 10h48 AM by NTVu
     * @param {HTMLElement} comboBox
     * @param {CallableFunction} callback
     */
    static loadEvents(comboBox, callback = undefined) {
        const dropGroup = $(comboBox).find('.drop-down-group');
        const btn = $(comboBox).find('.drop-toggle');
        // show drop list
        $(btn).click(function () {
            if ($(dropGroup).css('display') === 'none') {
                $(dropGroup).css('display', 'block');
            } else {
                $(dropGroup).css('display', 'none');
            }
        });

        const inputDisplay = $(comboBox).find('.m-input');
        if (inputDisplay.length > 0) {
            const dropList = $(comboBox).find('.drop-down-list');
            dropList.on('click', '.drop-down-item', function () {
                const label = $(this).attr('valueLabel');
                const value = $(this).attr('value');
                if (label) {
                    $(inputDisplay).attr('data-value', value);
                    $(inputDisplay).val(label);
                    $(dropGroup).css('display', 'none');
                }
                if (callback) callback();
            });
        }

        $(document).on('click', (event) => {
            const btn = $(comboBox).find('.drop-toggle')[0];
            if (
                event.target !== btn &&
                !$(dropGroup)[0].contains(event.target) &&
                !btn.contains(event.target)
            ) {
                $(dropGroup).css('display', 'none');
            }
        });
    }
}
