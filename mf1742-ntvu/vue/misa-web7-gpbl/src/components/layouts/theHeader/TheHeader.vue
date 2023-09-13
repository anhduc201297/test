<template>
    <header class="header">
        <div class="header-left">
            <div v-if="isOpenSidebar" id="hideSidebar-controller">
                <m-btn-no-text
                    @click="$emit('onCloseSidebar')"
                    title="Rút gọn"
                    class="icon-menu"
                />
            </div>
            <div class="header-title">
                <h1
                    ref="companyName"
                    title="Công ty tnhh sản xuất - thương mại - dịch vụ qui phúc"
                >
                    {{ companyName }}
                </h1>
                <div>
                    <m-btn-no-text class="icon-down-regular" />
                </div>
            </div>
        </div>
        <div class="header-info">
            <div class="header-lang">
                <m-combo-box
                    :modelValue="language"
                    @update:modelValue="onChangeLangCode"
                    :comboboxItems="languageOptions"
                    isDisabledInput
                />
            </div>
            <a title="Thông báo" class="header-nav-btn">
                <m-btn-no-text class="icon-bell lg icon-24" />
            </a>
            <div class="relative color-grey-400">
                <button
                    @click="onToggleDropdown"
                    @blur="onCloseDropdown"
                    :style="{ color: 'transparent' }"
                    class="drop-toggle header-user-info"
                >
                    <div class="user-avatar avatar-32">
                        <img
                            src="../../../assets/icons/avatar-default.svg"
                            alt="Avatar"
                        />
                    </div>
                    <span class="user-name color-text">Nguyễn Thế Vũ</span>

                    <m-btn-no-text class="icon-down-regular icon-20-btn-s" />
                </button>
                <div
                    v-show="isShowDropdown"
                    class="drop-down-group bottom right"
                >
                    <div class="text-center pt-16">
                        <div class="user-avatar avatar-56">
                            <img
                                src="../../../assets/img/avatar-owner.png"
                                alt="Avatar"
                            />
                        </div>
                    </div>

                    <div class="mt-8">
                        <h1 class="text-center color-text m-text-head-3"
                            >Nguyễn Thế Vũ</h1
                        >
                        <p class="m-text-sub color-grey-400 text-center">
                            Vunguyenaz1309@gmail.com
                        </p>
                    </div>
                    <a
                        title="(SMECloud) Công ty cổ phần Vô cùng kỳ diệu"
                        class="mt-8 color-grey-200 header__user_2-icon icon-car icon-24"
                    >
                        <span class="color-text">
                            (SMECloud) Công ty cổ phần Vô cùng kỳ diệu
                        </span>

                        <m-btn-no-text class="bg-white lg icon-right icon-16" />
                    </a>

                    <a
                        title="Test hiệu năng Database"
                        class="mt-8 color-grey-200 header__user_2-icon icon-database icon-20"
                    >
                        <span class="color-text">Test hiệu năng DB</span>
                        <m-btn-no-text class="bg-white lg icon-right icon-16" />
                    </a>

                    <ul class="mt-8 drop-down-list">
                        <li
                            class="drop-down-item-icon color-grey-200 m-btn m-btn-icon icon-cash icon-24"
                        >
                            <span class="color-text"
                                >Giấy phép &amp; Thanh toán</span
                            >
                        </li>
                        <li
                            class="drop-down-item-icon color-grey-200 m-btn m-btn-icon icon-repository icon-20"
                        >
                            <span class="color-text">Đổi mật khẩu</span>
                        </li>
                        <li
                            class="drop-down-item-icon color-grey-200 m-btn m-btn-icon icon-tool icon-32"
                        >
                            <span class="color-text">Thiết lập tài khoản</span>
                        </li>
                        <li
                            class="drop-down-item-icon color-grey-200 m-btn m-btn-icon icon-tax icon-20"
                        >
                            <span class="color-text">Thiết lập bảo mật</span>
                        </li>
                        <li
                            class="drop-down-item-icon color-grey-200 m-btn m-btn-icon icon-cart icon-20"
                        >
                            <span class="color-text"
                                >Giới thiệu - Tích điểm</span
                            >
                        </li>
                        <li
                            class="drop-down-item-icon color-grey-200 m-btn m-btn-icon icon-order icon-20"
                        >
                            <span class="color-text">Thiết lập bảo mật</span>
                        </li>
                        <li
                            class="drop-down-item-icon color-grey-200 m-btn m-btn-icon icon-car icon-20"
                        >
                            <span class="color-text">Thỏa thuận sử dụng</span>
                        </li>
                        <li
                            class="drop-down-item-icon color-grey-200 m-btn m-btn-icon icon-general icon-20"
                        >
                            <span class="color-text">
                                Chính sách quyền riêng tư
                            </span>
                        </li>
                    </ul>
                    <div class="mt-8 line-1"></div>
                    <a
                        class="mt-4 drop-down-row drop-down-item color-grey-200 drop-item-center"
                    >
                        <span class="m-btn m-btn-icon icon-logout icon-16">
                            <span class="color-text">Đăng xuất</span>
                        </span>
                    </a>
                </div>
            </div>
        </div>
    </header>
</template>
<script>
import app from '@/main';
import MResource from '../../../js/MResource';
import { setLangCode, getLangCode } from '../../../js/utils/localStorage';
export default {
    name: 'TheHeader',
    props: {
        isOpenSidebar: Boolean
    },
    data() {
        return {
            isShowDropdown: false,
            languageOptions: null,
            language: {
                value: '',
                label: ''
            },
            companyName: 'Công ty tnhh sản xuất - thương mại - dịch vụ qui phúc'
        };
    },
    /**
     * Khởi tạo các options languages
     */
    created() {
        this.languageOptions = {
            body: {
                items: [
                    {
                        label: 'Tiếng Việt',
                        value: 'VN'
                    },
                    {
                        label: 'EngLish',
                        value: 'EN'
                    }
                ]
            }
        };
        const { label } = getLangCode();
        this.language.label = label;
    },
    methods: {
        /**
         * Lắng nghe thay đổi view width để thay đổi company name
         * @author NTVu (28-08-2023)
         */
        onResize() {
            const companyNameEl = this.$refs.companyName;
            if (companyNameEl) {
                this.companyName =
                    companyNameEl.offsetWidth > companyNameEl.scrollWidth
                        ? 'Công ty tnhh sản xuất - thương mại - dịch vụ qui phúc'
                        : 'Công ty tnhh sản xuất - thương mại...';
            }
        },
        /**
         * Thay đổi ngôn ngữ
         * @author NTVu (28-08-2023)
         * @param {{value: string, label: string}} param - Language Code
         */
        onChangeLangCode({ value, label }) {
            setLangCode({ value, label });

            app.config.globalProperties.$Resource = MResource[value];
        },
        /**
         * Hàm toggle thanh options
         * @Author NTVu - MF1742
         * Modified: NTVu - 19/08/2023
         */
        onToggleDropdown() {
            this.isShowDropdown = !this.isShowDropdown;
        },
        /**
         * Hàm đóng thanh options sau 300ms (để người dùng có thể click vào các options)
         * @Author NTVu - MF1742
         * Modified: NTVu - 19/08/2023
         */
        onCloseDropdown() {
            setTimeout(() => {
                this.isShowDropdown = false;
            }, 300);
        }
    },
    /**
     * Thêm sự kiện onResize để thay đổi company name
     * @author NTVu - 29/08/2023
     */
    beforeMount() {
        window.addEventListener('resize', this.onResize);
    },
    /**
     * Xóa sự kiện onResize để thay đổi company name
     * @author NTVu - 29/08/2023
     */
    beforeUnmount() {
        window.removeEventListener('resize', this.onResize);
    }
};
</script>
<style scoped>
@import url('./header.css');
@import url('../../base/comboBox/comboBox.css');
</style>
