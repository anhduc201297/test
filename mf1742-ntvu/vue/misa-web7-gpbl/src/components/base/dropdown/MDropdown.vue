<template>
    <div class="dropdown-wrapper">
        <m-btn-no-text
            @click="onToggleDropList"
            @blur="onCloseDropList"
            class="drop-toggle focus-outline"
            :style="{ 'outline-color': isOpen ? focusColor : 'transparent' }"
            :class="[
                {
                    'drop-toggle-rotate': isOpen
                },
                icon
            ]"
        />
        <div v-show="isOpen" class="drop-down-group" :class="position">
            <ul class="drop-down-list">
                <li
                    v-if="dropItems.header"
                    class="drop-down-header"
                    v-html="dropItems.header.render()"
                ></li>
                <li
                    class="drop-down-item color-green-100"
                    v-for="(item, id) of dropItems.body.items"
                    :key="id"
                    @click="$emit('clickItem', item.value)"
                    v-html="
                        dropItems.body.render
                            ? dropItems.body.render(item.value, item.label)
                            : ` <span class='color-text'>${item.label}</span>`
                    "
                ></li>

                <li
                    v-if="dropItems.footer"
                    class="drop-down-footer color-green-100 drop-down-item"
                    v-html="dropItems.footer.render(dropItems.footer.label)"
                >
                </li>
            </ul>
        </div>
    </div>
</template>
<script>
export default {
    name: 'MDropdown',
    emits: ['clickItem'],
    props: {
        dropItems: {
            type: Object,
            default: () => ({
                body: {
                    items: [
                        {
                            label: '',
                            value: ''
                        }
                    ]
                }
            })
        },

        position: {
            // vị trí xuất hiện của dropdown
            type: String,
            default: 'bottom right'
        },
        focusColor: {
            // màu sắc khi combobox được focus
            type: String,
            default: '#3395ff'
        },
        icon: {
            // icon của nút toggle drop list
            type: String,
            default: 'icon-down-solid icon-20-btn-s'
        }
    },
    data() {
        return {
            isOpen: false,
            timeoutClose: null
        };
    },
    methods: {
        /**
         * Đóng, mở drop list
         * @Author NTVu - MF1742 - 23/08/2023
         */
        onToggleDropList() {
            this.isOpen = !this.isOpen;
        },

        /**
         * Đóng drop list sau 200ms để người dùng có thể click các options
         * @Author NTVu - MF1742 - 23/08/2023
         */
        onCloseDropList() {
            if (this.timeoutClose) {
                clearTimeout(this.timeoutClose);
            }
            this.timeoutClose = setTimeout(() => {
                this.isOpen = false;
            }, 200);
        }
    }
};
</script>

<style scoped>
@import url('./dropdown.css');
</style>
