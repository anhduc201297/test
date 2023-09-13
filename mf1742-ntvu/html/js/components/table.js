class Table {
    table;
    recordsPerPage = 10;
    numOfPages = 0;
    page = 1;
    numOfRecords = 0;
    data;
    originalData;
    actions;
    popupForm;
    pagingEl;
    /**
     * Khởi tạo bảng và load các sự kiện
     * Author: NTVu - MF1742
     * Modified: NTVu - 16/08/2023
     * @param {HTMLElement} table
     * @param {Array} data
     * @param {EventListenerOrEventListenerObject} actions
     * @param {PopupForm} popupForm
     * @param {} dialog
     */
    constructor(table, data, actions, popupForm) {
        this.table = table;
        this.pagingEl = $(table)
            .parents('.container-table-wrapper')
            .find('.table-paging')[0];
        this.data = data;
        this.originalData = data;
        this.numOfRecords = data.length;
        this.numOfPages = Math.ceil(this.numOfRecords / this.recordsPerPage);
        this.actions = actions;
        this.popupForm = popupForm;

        this.switchPage();
        this.loadEvents();
        this.displayNumOfRecords();
    }

    /**
     * Thêm sự kiện dblclick cho từng dòng trong bảng, khi dblclick thì hiện form thông tin
     * Author: NTVu - MF1742
     * Modified: NTVu - 15/08/2023: 13h10
     */
    loadEvents() {
        const _this = this;
        // Reset searchValue action
        $('#refreshBtn').click(function () {
            $('#searchText').val('');
        });

        // Paging actions
        this.loadSearchEvent();
        this.loadEventsCheckBox();
        $(this.pagingEl)
            .find('#previous-page')
            .click(function (e) {
                e.preventDefault();
                if (_this.page <= 1) return;
                _this.page -= 1;
                _this.switchPage();
            });
        $(this.pagingEl)
            .find('#next-page')
            .click(function (e) {
                e.preventDefault();
                if (_this.page >= _this.numOfPages) return;
                _this.page += 1;
                _this.switchPage();
            });

        // ComboBox hiển thị số bản ghi trên trang
        const recordsPerPageCtrl = $(this.pagingEl)
            .find('.table-pagination-controls .drop-toggle')
            .parent('.relative')[0];

        ComboBox.loadEvents(recordsPerPageCtrl, () => {
            const value = $(this.pagingEl)
                .find('.table-pagination-controls .m-input[type="text"]')
                .attr('data-value');
            if (value) {
                this.recordsPerPage = parseInt(value);
                this.numOfPages = Math.ceil(
                    this.numOfRecords / this.recordsPerPage
                );
                this.page = 1;
                this.switchPage();
            }
        });
    }

    /**
     * Tạo bảng dựa vào table và data được truyền vào
     * Author: NTVu - MF1742
     * Modified: NTVu - 15/08/2023: 13h06
     */
    buildTable() {
        this.numOfRecords = this.data.length;
        this.numOfPages = Math.ceil(this.numOfRecords / this.recordsPerPage);
        const tableData = this.data.slice(
            (this.page - 1) * this.recordsPerPage,
            this.page * this.recordsPerPage
        );

        const tableBody = $(this.table).find('.m-table-body').empty();
        const tableHeader = $(this.table).find('thead th');
        const numCols = tableHeader.length;
        const dropList = `<ul class="drop-down-list">
                            <li action='duplicate' class="drop-down-item color-drop-green">
                                <span class="color-text">Nhân bản</span>
                            </li>
                            <li action='remove' class="drop-down-item color-drop-green">
                                <span class="color-text">Xóa</span>
                            </li>
                            <li action='stopUse' class="drop-down-item color-drop-green">
                                <span class="color-text">Ngưng sử dụng</span>
                            </li>
                        </ul>`;
        tableData.forEach((staff) => {
            const elRow = $('<tr></tr>');
            const checkboxEl = $(`<td class="text-center">
                                    <label for='${staff.id}' class="m-input-checkbox-wrap checkbox-to-green">
                                        <input type="checkbox" hidden class="m-input-checkbox" name='${staff.id}'
                                            id='${staff.id}'>
                                        <label for='${staff.id}'
                                            class="m-show-border color-grey-400  checkbox-display m-btn m-only-icon-btn-s icon-20">
                                        </label>
                                    </label>
                                </td>`);
            elRow.append(checkboxEl);
            for (let i = 1; i < numCols - 1; i++) {
                const cellName = $(tableHeader[i]).attr('fieldName');
                const className = $(tableHeader[i]).attr('class');
                const label = $(tableHeader[i]).attr('fieldLabel');

                if (cellName) {
                    if (label) {
                        const cellEl = `<td class= '${className}'>${
                            Common.fieldLabels[label][staff[cellName]].label
                        }</td>`;
                        elRow.append(cellEl);
                    } else {
                        const cellEl = `<td class= '${className}'>${staff[cellName]}</td>`;
                        elRow.append(cellEl);
                    }
                }
            }
            const comboEl = $(` <div
                                    class="relative color-blue-500"
                                > </div>`);
            comboEl.append(
                $(`<button
                    class="drop-toggle drop-toggle-anim m-btn m-btn--link m-only-icon-btn icon-16 icon-down-solid focus-border"
                    ></button>`)
            );
            const dropDownEl = $(
                ' <div class="m-display-none drop-down-group m-drop-down-bottom m-right"></div>'
            );

            dropDownEl.append($(dropList));

            comboEl.append(dropDownEl);
            $(dropDownEl).data('staffId', staff.staffId);
            const actionsCol = $(`<td class="text-center action-col sticky r-0">
                                    <button
                                        class="m-btn m-btn-small m-btn--link m-btn m-btn-small m-btn--link focus-border action-edit">
                                        Sửa</button>
                                </td>`);
            actionsCol.append(comboEl);
            elRow.append(actionsCol);
            elRow.data(staff);
            tableBody.append(elRow);
        });
        this.loadPages();
        this.loadEventsOfTable();
    }

    /**
     * Load sự kiện cho các dòng trong table
     * Author: NTVu - MF1742
     * Modified: NTVu - 16/08/2023
     */
    loadEventsOfTable() {
        const _this = this;

        const onClickUpdate = function (employeeData) {
            _this.popupForm.setMode(PopupForm.PopupMode.update);
            _this.popupForm.show(employeeData);
        };
        $(this.table)
            .find('tbody')
            .on('dblclick', 'tr', function () {
                onClickUpdate($(this).data());
            });
        $(this.table)
            .find('tbody tr .action-edit')
            .on('click', function () {
                const data = $(this).parents('tr').data();
                onClickUpdate(data);
            });

        $(this.table)
            .find('[action]')
            .click(function () {
                const data = $(this).parents('tr').data();
                _this.actions[$(this).attr('action')](data.name);
            });

        const cbBoxes = $(this.table).find('.drop-toggle').parent('.relative');
        [...cbBoxes].forEach((el) => {
            ComboBox.loadEvents(el);
        });
    }

    /**
     * Chuyển trang
     * Author: NTVu - MF1742
     * Modified: NTVu - 16/08/2023
     */
    switchPage() {
        this.buildTable();
        $(this.pagingEl)
            .find(`.table-list-pages a[value]`)
            .removeClass('table-page-num-selected');
        $(this.pagingEl)
            .find(`.table-list-pages a[value=${this.page}]`)
            .addClass('table-page-num-selected');

        $(this.pagingEl)
            .find('.table-page-num-control > [id]')
            .removeClass('m-btn--disabled');

        if (this.page <= 1) {
            $(this.pagingEl).find('#previous-page').addClass('m-btn--disabled');
        }
        if (this.page >= this.numOfPages) {
            $(this.pagingEl).find('#next-page').addClass('m-btn--disabled');
        }
        $(this.table).find('#all').prop('checked', false);
    }

    /**
     * Load các trang cho table
     * Author: NTVu - MF1742
     * Modified: NTVu - 16/08/2023
     */
    loadPages() {
        const pages = $(this.table)
            .parents('.container-table-wrapper')
            .find('.table-list-pages')
            .empty();

        const maxPages = Math.min(this.numOfPages, this.page + 3);
        for (let i = Math.max(1, maxPages - 3); i <= maxPages; i++) {
            $(pages).append(
                $(`<a
            value="${i}"
            class="focus-border m-btn m-btn-small m-btn--link"
        >
            ${i}
        </a>`)
            );
        }
        const _this = this;
        $(this.pagingEl)
            .find('.table-list-pages a[value]')
            .click(function (e) {
                e.preventDefault();
                _this.page = parseInt($(this).attr('value'));
                _this.switchPage();
            });
    }

    /**
     * Load sự kiện cho các nút checkboxes
     * Author: NTVu - MF1742
     * Modified: NTVu - 16/08/2023
     */
    loadEventsCheckBox() {
        const _this = this;
        $(this.table)
            .find('#all')
            .change(function () {
                const all = this;
                const rows = $(_this.table).find('tbody input[type=checkbox]');
                const checkedRows = $(_this.table).find(
                    'tbody input[type=checkbox]:is(:checked)'
                );

                if (checkedRows.length < rows.length) {
                    $(rows)
                        .prop('checked', true)
                        .click(function () {
                            if ($(all).is(':checked')) {
                                $(all).prop('checked', false);
                            }
                        });
                } else {
                    $(checkedRows).prop('checked', false);
                }
            });
    }

    /**
     * Load sự kiện tìm kiếm
     * Author: NTVu - MF1742
     * Modified: NTVu - 16/08/2023
     */
    loadSearchEvent() {
        const _this = this;
        const searchEl = $('#searchText');
        $(searchEl).on('change', function () {
            _this.search($(this).val());
        });
        $('#refreshBtn').click(function () {
            $('#searchText').val('');
            _this.search('');
        });
    }

    /**
     * Tìm kiếm dữ liệu dựa trên mã và tên
     * Author: NTVu - MF1742
     * Modified: NTVu - 16/08/2023
     * @param {string} keyword
     */
    search(keyword) {
        const res = this.originalData.filter((v) => {
            const staffId = v.id.toString().toLowerCase();
            const staffName = Common.convertToUnaccented(v.name);
            const key = Common.convertToUnaccented(keyword.toString().trim());

            return staffId.includes(key) || staffName.includes(key);
        });
        this.data = res;
        this.switchPage();
        this.displayNumOfRecords();
    }
    /**
     * Hiển thị tổng số bản ghi lên giao diện
     * Author: NTVu - MF1742
     * Modified: NTVu - 16/08/2023
     */
    displayNumOfRecords() {
        $(this.pagingEl)
            .find('.table-amount .text-bold')
            .text(this.numOfRecords);
    }
}
