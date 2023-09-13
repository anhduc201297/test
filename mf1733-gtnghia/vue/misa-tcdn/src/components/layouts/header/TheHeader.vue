<template>
  <div class="header">
    <div class="header__start">
      <div class="logo-container">
        <m-button
          :type="'icon'"
          class="btn-menu-option"
          :icon="'icon-menu-option'"
          @click="toggleSidebar"
        />
        <a class="btn-homepage ml-8" href="#">
          <img
            class="img-logo-homepage"
            src="@/assets/img/Logo_Module_TiengViet_White.66947422.svg"
            alt=""
          />
        </a>
      </div>
    </div>
    <div class="header__center">
      <m-button
        :type="'icon'"
        class="btn-menu-bar mr-8"
        :icon="'icon-menu-bar'"
      />
      <div class="cur-company dropdown-m">
        <select name="" id="" class="dropdown-m-select">
          <option value="quiphuc">
            CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - DỊCH VỤ QUI PHÚC
          </option>
          <option value="single">
            CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - MỘT THÀNH VIÊN
          </option>
          <option value="apple">CÔNG TY TNHH APPLE</option>
        </select>
      </div>
    </div>
    <div class="header__end">
      <select
        class="dropdown-m-select mr-8"
        name=""
        id="user-lang"
        @change="onChangeLanguage"
        v-model="language"
        ref="userLang"
      >
        <option value="VN">Tiếng việt</option>
        <option value="EN">English</option>
      </select>
      <m-button class="mr-8" :type="'icon'" :icon="'icon-bell-light'" />
      <div class="user">
        <div class="user-avatar">
          <img class="img-avatar" src="@/assets/img/avatar.png" alt="" />
        </div>
        <div class="user-name">
          <p>Giang Trung Nghĩa</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import app from '@/main.js'
export default {
  name: "TheHeader",
  props: {},
  data() {
    return {
      language: "VN",
      isShowSidebar: true,
    };
  },
  methods: {
    // Bật tắt sidebar
    toggleSidebar() {
      this.$emit("toggleSidebar")
    },
    onChangeLanguage() {
      const value = this.$refs.userLang.value
      console.log(value);
      app.config.globalProperties.$LangCode = value
    }
  },
};
</script>

<style scoped>
@import url(./header.css);
</style>
