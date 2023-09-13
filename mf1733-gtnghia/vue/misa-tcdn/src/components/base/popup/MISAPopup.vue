<template>
  <div class="popup">
    <div class="popup__header">
      <div class="popup__header--title">
        <p class="heading2">{{ title }}</p>
      </div>
      <div class="popup__header--close">
        <m-button @click="onClose" :type="'icon'" :icon="'icon-close'" />
      </div>
    </div>
    <div class="popup__body">
      <p class="body2">{{ message }}</p>
      <slot name="popup__body"></slot>
    </div>
    <div class="popup__footer">
      <div class="popup__footer--option">
        <slot name="popup__footer"></slot>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "m-popup",
  props: {
    title: String,
    message: String,
    
  },
  data() {
    return {};
  },
  methods: {
    onClose() {
      this.$emit("onClose")
    }
  },
};
</script>

<style scoped>
@import url(./popup.css);
</style>
