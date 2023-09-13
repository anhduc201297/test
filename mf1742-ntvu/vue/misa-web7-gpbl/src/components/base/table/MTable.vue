<template>
    <div
        class="container-table"
        :style="[{ 'min-width': `max(100%, ${tableWidth}px)` }]"
    >
        <table class="m-table">
            <thead class="m-table-header">
                <th
                    v-for="(item, index) of headers"
                    :key="index"
                    :class="[`text-${item.format || 'left'}`, item.class || '']"
                    :fieldName="item.fieldName"
                    :width="item.width"
                    :title="item.title || ''"
                >
                    <m-check-box
                        :modelValue="isCheckedAll"
                        @update:modelValue="onChangeCheckedAll"
                        v-if="item.fieldName === 'checkbox'"
                    />
                    <span v-else>{{ item.label }}</span>
                </th>
            </thead>

            <tbody class="m-table-body">
                <m-table-row
                    v-for="(item, index) of data"
                    :key="item[this.uniqueField]"
                    :ref="item[this.uniqueField]"
                    :data="item"
                    :headerCols="headers"
                    @changeCheckStatus="onChangeCheckItem"
                    @dblClick="$emit('dblClickRow', index)"
                    @clickAction="$emit('clickAction', index)"
                    @un-mounted="itemCheckedUnMounted"
                ></m-table-row>
            </tbody>
        </table>
    </div>

    <!-- PAGING -->
    <div class="table-paging">
        <div>
            Tổng số:
            <span class="text-bold">{{ totalRecords }}</span>
            bản ghi
        </div>
        <nav class="table-pagination-controls">
            <m-combo-box
                :modelValue="recordsPerPage"
                @update:modelValue="setRecordsPerPage"
                :comboboxItems="comboBoxPageOptionConfig"
                position="top right mb-8"
                focusColor="#50b83c"
                isDisabledInput
            >
            </m-combo-box>

            <div class="table-paging__records">
                {{ currentRecords.from }}
                -
                <span class="text-bold">{{ currentRecords.to }}</span>
                bản ghi
            </div>
            <m-btn-no-text
                @click="onPrevPage"
                class="icon-left-16 icon-20"
                :class="{ 'm-btn--disabled': currentPage <= 1 }"
            ></m-btn-no-text>
            <m-btn-no-text
                @click="onNextPage"
                class="icon-right-16 icon-20"
                :class="{
                    'm-btn--disabled': currentPage >= totalPages
                }"
            ></m-btn-no-text>
        </nav>
    </div>
</template>

<script>
import MTableRow from './MTableRow.vue';

export default {
    name: 'MTable',
    components: {
        MTableRow
    },
    emits: [
        'setRecordsPerPage',
        'changePage',
        'changeAmountItemChecked',
        'dblClickRow',
        'clickAction'
    ],
    props: {
        headers: Array, // các cột
        data: Array,
        uniqueField: String, // trường có giá trị duy nhất, VD: employeeId
        currentPage: { type: Number, default: 1 },
        pageSizeOptions: {
            type: Array,
            default: () => [
                {
                    value: 10,
                    label: '10 bản tin trên 1 trang'
                }
            ]
        },
        recordsPerPage: {
            type: Object,
            default: () => ({ value: 10, label: '10 bản tin trên 1 trang' })
        },
        totalRecords: { type: Number, default: 0 }
    },
    data() {
        return {
            isCheckedAll: false,
            amountItemChecked: 0
        };
    },

    methods: {
        /**
         * Thông báo cập nhật số lg item đc check
         * @author NTVu - 07/09/2023
         */
        emitChangeCheckedAmount() {
            this.$emit('changeAmountItemChecked', this.amountItemChecked);
        },
        /**
         * Lắng nghe thay đổi check all
         * @author NTVu - 28/08/2023
         */
        onChangeCheckedAll(value) {
            this.setAllItemCheck(value);
            this.emitChangeCheckedAmount();
        },
        /**
         * Thay đổi trạng thái check của tất cả item
         * @author NTVu - 06/09/2023
         * @param {boolean} value
         */
        setAllItemCheck(value) {
            this.isCheckedAll = value;
            this.amountItemChecked = value
                ? Math.min(this.recordsPerPage.value, this.data.length)
                : 0;

            this.data.forEach((item) => {
                const currRef = item[this.uniqueField];

                this.$refs[currRef][0].setCheckStatus(value);
            });
        },

        /**
         * Khi row checked status thay đổi -> thay đổi số lượng checked
         * @Author NTVu - MF1742 - 20/08/2023
         */
        onChangeCheckItem(isChecked) {
            if (isChecked) {
                this.amountItemChecked += 1;
                if (
                    this.amountItemChecked ===
                    Math.min(this.recordsPerPage.value, this.data.length)
                ) {
                    this.isCheckedAll = true;
                }
            } else {
                this.amountItemChecked--;
                this.isCheckedAll = false;
            }
            this.emitChangeCheckedAmount();
        },
        /**
         * Giảm checked amount khi 1 row đang checked bị unmount
         * @author NTVu - 08/09/2023
         */
        itemCheckedUnMounted() {
            this.amountItemChecked--;
            this.emitChangeCheckedAmount();
        },

        /**
         * Lấy ra id của các row đang được check
         * @author NTVu - 25/08/2023
         * @returns {string[]} ids
         */
        getAllItemChecked() {
            const ids = [];
            this.data.forEach((item) => {
                const currRef = item[this.uniqueField];
                if (this.$refs[currRef][0].getCheckedStatus()) {
                    ids.push(item[this.uniqueField]);
                }
            });
            return ids;
        },
        /**
         * Xử lí sự kiện chuyển đến trang kế tiếp nếu có
         * Đồng thời gửi sự kiện 'changePage' với trang kế tiếp
         
         * @author NTVu - MF1742
         * @modified 06/09/2023
         */
        onNextPage() {
            if (this.currentPage + 1 <= this.totalPages)
                this.$emit('changePage', this.currentPage + 1);
        },

        /**
         * Xử lí sự kiện chuyển đến trang trước đó nếu có
         * Đồng thời gửi sự kiện 'onChangePage' với trang trước đó
         * @author NTVu - MF1742
         * @modified 06/09/2023
         */
        onPrevPage() {
            if (this.currentPage - 1 > 0)
                this.$emit('changePage', this.currentPage - 1);
        },
        /**
         * Emit thay đổi số bản ghi trên trang
         * @author NTVu - 06/09/2023
         */
        setRecordsPerPage({ value, label }) {
            if (
                this.amountItemChecked === value ||
                this.amountItemChecked === this.data.length
            ) {
                this.isCheckedAll = true;
            }
            this.$emit('setRecordsPerPage', { value, label });
        }
    },

    created() {
        // cấu hình comboBox để truyền vào MComboBox pageOptions
        this.comboBoxPageOptionConfig = {
            body: {
                items: this.pageSizeOptions
            }
        };
    },
    computed: {
        /**
         * Tính độ rộng của bảng báo cáo để cài đặt style cho bảng
         * @Author NTVu - MF1742 - 20/08/2023
         */
        tableWidth() {
            return this.headers.reduce((value, col) => {
                value += parseInt(col.width);
                return value;
            }, 0);
        },
        /**
         * Tính tổng số trang khi thay đổi pageSize
         * @Author NTVu - MF1742 - 20/08/2023
         */
        totalPages() {
            return Math.ceil(this.totalRecords / this.recordsPerPage.value);
        },
        /**
         * tính số bản ghi đang được chiếu
         * @author NTVu - 08/09/2023
         */
        currentRecords() {
            return {
                from: (this.currentPage - 1) * this.recordsPerPage.value + 1,
                to: this.currentPage * this.recordsPerPage.value
            };
        }
    },
    /**
     * Thay đổi trạng thái check header khi table được update
     * @author NTVu - 09/09/2023
     */
    updated() {
        this.isCheckedAll = this.amountItemChecked === this.data.length;
    }
};
</script>

<style>
@import url('./table.css');
</style>
