<template>
  <div class="popup-background"></div>
  <div class="delete-notify-dialog">
    <div class="header-dialog">
      <div class="title">{{ title }}</div>
      <div class="icon close-icon" @click="onClose"></div>
    </div>
    <div class="content-dialog">
      <div
        class="icon"
        v-bind:class="{
          'icon-question': type === dialogType.Question,
          'icon-error': type === dialogType.Error,
          'icon-info': type === dialogType.Info,
          'icon-warning': type === dialogType.Warning,
        }"
      ></div>
      <div>{{ content }}</div>
    </div>
    <div class="footer-dialog">
      <button class="btn-normal" @click="onClose">Không</button>
      <button class="btn-primary"  @click="confirmAction">Có</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "MISADialog",
  props: { content: String, type: Number, title: String ,
    confirmAction: Function},
  methods: {
    /*
     * Gọi đến sự kiện onClose trong TheContent.vue
     * Author: mf1735-duy (20/08/2023)
     */
    onClose() {
      this.$emit("onClose");
    },

  },
  data() {
    return {
      dialogType: {
        Question: 1,
        Error: 2,
        Warning: 3,
        Info: 4,
      },
      value: false,
    };
  },
};
</script>

<style scoped>
@import url(dialog.css);
</style>