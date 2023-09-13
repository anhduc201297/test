<template>
    <header class="header center">
        <div class="header-main center">
            <div class="header-grid center">
                <div class="header-grid__icon"></div>
            </div>
            <div class="header-logo">
                <img class="header-logo__img" src="@/assets/img/Logo_Module_TiengViet_White.66947422.svg" alt="" />
            </div>
        </div>
        <div class="header-nav">
            <ul class="header-nav-list center">
                <li class="header-nav-item center">
                    <select ref="cbxLanguage" @change="onChangeLanguage" name="" id="cbxLanguage">
                        <option value="VN">{{ this.$resources[this.$LangCode].Language.Vn }}</option>
                        <option value="EN">{{ this.$resources[this.$LangCode].Language.En }}</option>
                    </select>
                </li>
                <li data-tooltip="Cloud" class="header-nav-item center">
                    <div class="header-item__cloud"></div>
                </li>
                <li data-tooltip="Thông báo" class="tooltip header-nav-item center">
                    <div class="header-item__ring"></div>
                </li>
                <li data-tooltip="Trợ giúp" class="header-nav-item center">
                    <div class="header-item__help"></div>
                </li>
                <li class="header-nav-item center">
                    <div class="header-profile">
                        <div class="header-profile-icon"></div>
                    </div>
                </li>
            </ul>

        </div>
    </header>
</template>

<script>
import app from '@/main.js';
export default {
    name: "TheHeader",
    methods: {
        onChangeLanguage() {
            const value = this.$refs.cbxLanguage.value
            app.config.globalProperties.$LangCode = value
        }
    }
}
</script>

<style scoped></style>