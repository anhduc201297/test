class PopupForm {
    /**
     * Định nghĩa các mode có thể của Form
     * Author: NTVu - MF1742
     * Modified: NTVu - 16/08/2023
     */
    static PopupMode = {
        create: { label: 'Tạo mới nhân viên', action: 'post' },
        update: { label: 'Cập nhật thông tin', action: 'put' },
    };

    popUp;
    mode;
    /**
     * Load event mặc định cho popup form
     * Author: NTVu - MF1742
     * Modified: NTVu - 15/08/2023
     * @param {HTMLElement} popUp
     */
    constructor(popUp) {
        this.popUp = popUp;

        this.loadEvents();
    }

    /**
     * Load sự kiện cho các button trong popup
     * Author: NTVu - MF1742
     * Modified: NTVu - 17/08/2023
     */
    loadEvents() {
        const _this = this;
        try {
            $('.container-header .m-btn').click(() => {
                this.setMode(PopupForm.PopupMode.create);
                this.show();
            });
            $('.pop-up-container .icon-close').click(() => {
                this.hide();
            });

            ComboBox.loadEvents(
                $(this.popUp).find('.relative:has(.drop-toggle')[0]
            );

            window.addEventListener('keydown', (e) => {
                if (e.key === 'Escape') {
                    if ($(this.popUp).hasClass('m-display-none')) {
                        return;
                    }
                    this.hide();
                }
            });

            $(this.popUp)
                .find('.m-btn[type="submit"]')
                .click(() => {
                    _this.submitForm();
                });
        } catch (error) {
            console.log(error);
        }
    }

    /**
     * Show popup form thông tin nhân viên
     * Author: NTVu - MF1742
     * Modified: NTVu - 17/08/2023: Sửa lỗi hiện ra thì xóa các value của input text và date
     * @param {object} data
     *
     */
    show(data) {
        try {
            Common.onLoading();
            if (data || this.mode === PopupForm.PopupMode.update) {
                const inputsText = $(this.popUp).find('input[type=text]');
                [...inputsText].forEach((inp) => {
                    $(inp).val(data[$(inp).attr('name')]);
                });

                const inputsDate = $(this.popUp).find('input[type=date]');
                [...inputsDate].forEach((inp) => {
                    const value = Common.convertDate(data[$(inp).attr('name')]);
                    $(inp).val(value);
                });
                const inputsCheck = $(this.popUp).find('input[type=checkbox]');
                [...inputsCheck].forEach((el) => {
                    const value = data[$(el).attr('name')];
                    if (value) {
                        if ($(el).is(':checked')) {
                            return;
                        } else {
                            $(el).trigger('click');
                        }
                    } else {
                        if ($(el).is(':checked')) {
                            $(el).trigger('click');
                        }
                    }
                });
                const inputsRadio = $(this.popUp).find('input[type=radio]');
                [...inputsRadio].forEach((el) => {
                    const value = data[$(el).attr('name')];
                    const fieldName = $(el).attr('name') + 's';
                    if (
                        $(el).attr('value') ===
                        Common.fieldLabels[fieldName][parseInt(value)].value
                    ) {
                        $(el).prop('checked', true);
                    } else {
                        $(el).prop('checked', false);
                    }
                });
            } else {
                // Reset form
                this.popUp
                    .find(':is(input[type=text], input[type=date])')
                    .val('');
                this.popUp
                    .find(':is(input[type="checkbox"], input[type="radio"])')
                    .prop('checked', false);
                this.popUp.find('#male').prop('checked', true);
            }
            this.popUp.removeClass('m-display-none');
            const firstInput = $(this.popUp).find('.m-input')[0];
            $(firstInput).trigger('focus');
        } catch (error) {
            console.log(error);
        } finally {
            Common.endLoading();
        }
    }

    /**
     * Ẩn popupForm, reset các formItem đang bị cảnh báo
     * Author: NTVu - MF1742
     * Modified: NTVu - 15/08/2023: 13h30
     */
    hide() {
        this.popUp.addClass('m-display-none');
        $(this.popUp)
            .find('input')
            .prop('checked', false)
            .removeClass('m-input-danger')
            .blur(() => {});
    }

    /**
     * Set chế độ của form thông tin
     * Author: NTVu - MF1742
     * Modified: NTVu - 15/08/2023: 13h32
     * @param {Object} mode
     * */
    setMode(mode) {
        this.mode = mode;
        const title = mode.label;
        $(this.popUp).find('.pop-up-header h1').text(title);
    }

    /**
     * Validate các input trong một form
     * Author: NTVu - MF1742
     * Modified: 15/08/2023 - 12h50, by NTVu
     */
    validateForm() {
        const inputs = $(this.popUp).find('input[required]');
        let checker = true;
        for (let i = inputs.length - 1; i >= 0; i--) {
            if (!this.validateInput(inputs[i], (focus = true))) {
                checker = false;
            }
        }
        return checker;
    }

    /**
     * Validate 1 input, nếu focus=true thì nếu input bị lỗi, nó sẽ được focus
     * Author: NTVu - MF1742
     * Modified: 15/08/2023 - 12h50, by NTVu
     * @param {HTMLElement} input
     * @param {boolean} focus
     */
    validateInput(input, focus = false) {
        const value = $(input).val();
        const _this = this;
        if (value === undefined || value === '') {
            $(input).addClass('m-input-danger');
            if (focus) $(input).trigger('focus');
            $(input).blur(function () {
                _this.validateInput(this);
            });

            return false;
        } else {
            $(input).removeClass('m-input-danger');
            $(input).blur(() => {});
            return true;
        }
    }

    /**
     * Validate dữ liệu trong form, nếu thành công thì submit
     * Author: NTVu - MF1742
     * Modified: NTVu - 15/08/2023
     * @returns {void}
     */
    submitForm() {
        if (!this.validateForm()) {
            return;
        } else {
            console.log('Submit');
        }
    }
}
