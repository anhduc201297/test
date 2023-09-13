<template>
    <tr :class="{ selected: isChecked }">
        <td
            v-for="(item, index) of headerCols"
            :key="index"
            @dblclick="onDblclick(item.fieldName)"
            :class="[`text-${item.format}`, item.class || '']"
        >
            <span v-if="item.isConstant">
                {{
                    $Resource[$Helper.capitalizeString(item.fieldName)][
                        data[item.fieldName]
                    ]
                }}
            </span>

            <m-check-box
                v-else-if="item.fieldName === 'checkbox'"
                :modelValue="isChecked"
                @update:modelValue="changeCheckedStatus"
            />
            <div v-else-if="item.fieldName === 'action'" class="action-col">
                <m-button
                    @click="$emit('clickAction')"
                    :type="'link'"
                    class="action-edit small focus-border"
                    >{{ item.text }}
                </m-button>

                <m-dropdown
                    @clickItem="onClickAction"
                    :dropItems="actionItems"
                />
            </div>
            <span v-else-if="item.formatType">
                {{ computeFormatCol(item.formatType, data[item.fieldName]) }}
            </span>
            <span v-else>
                {{ data[item.fieldName] }}
            </span>
        </td>
    </tr>
</template>
<script>
import MDropdown from '../dropdown/MDropdown.vue';
export default {
    name: 'MTableRow',
    components: {
        MDropdown
    },
    emits: ['changeCheckStatus', 'dblClick', 'clickAction', 'unMounted'],
    props: {
        data: Object,

        headerCols: Array
    },
    data() {
        return {
            isChecked: false,
            items: null,

            onChangeChecked: null
        };
    },
    /**
     * Thiết lập các items cho dropdown được truyền từ table
     * @author NTVu (23/08/2023)
     */
    created() {
        // Lấy ra cột action
        const colAction = this.headerCols.find(
            (col) => col.fieldName == 'action'
        );

        // Cấu hình để truyền vào dropdown
        this.actionItems = {
            body: {
                items: colAction.actions || []
            }
        };
    },
    methods: {
        /**
         * Sự kiện click vào các action item
         * @author NTVu - 07/09/2023
         * @param {Number} value
         */
        onClickAction(value) {
            this.actionItems.body.items[value].action(this.data);
        },
        /**
         *
         */
        onDblclick(fieldName) {
            if (fieldName == 'checkbox' || fieldName == 'action') return;
            this.$emit('dblClick');
        },
        /**
         * Cập nhật trạng thái check của row hiện tại
         * @author NTVu - 28/08/2023
         */
        changeCheckedStatus(value) {
            this.isChecked = value;
            this.$emit('changeCheckStatus', value);
        },
        /**
         * Thay đổi giá trạng thái checked
         * @param {boolean} value
         */
        setCheckStatus(value) {
            this.isChecked = value;
        },
        /**
         * Lấy trạng thái checkbox hiện tại
         * @Author NTVu - MF1742 - 20/08/2023
         */
        getCheckedStatus() {
            return this.isChecked;
        },
        /**
         * Format cột có formatType
         * @author NTVu 07/09/2023
         */
        computeFormatCol(formatType, value) {
            if (!value) return '';
            const [type, format] = formatType.split('|');
            if (type == 'date') {
                return this.$Helper.formatDate(value, format);
            } else {
                return value;
            }
        }
    },

    /**
     * Nếu bị loại bỏ -> emit lên để giảm số lượng checked amount
     * @author NTVu - 08/09/2023
     */
    beforeUnmount() {
        if (this.isChecked) {
            this.$emit('unMounted');
        }
    }
};
</script>
<style scoped>
@import url('./table.css');
</style>
