<template>
  <div class="d-toast">
    <div class="left-toast">
      <div
        class="icon"
        v-bind:class="{
          'icon-question': type === dialogType.Question,
          'icon-error': type === dialogType.Error,
          'icon-info': type === dialogType.Info,
          'icon-warning': type === dialogType.Warning,
          'icon-success':type === dialogType.Success,
        }"
      ></div>
      <div class="title-toast">{{ title }}</div>
      <p>{{ content }}</p>
    </div>
    <div class="right-toast">
      <div class="icon close-icon"></div>
    </div>
  </div>
</template>
  
  <script>
export default {
  name: "MISAToast",
  props: ["title", "type", "content"],
  methods: {},
  data() {
    return {
      dialogType: {
        Question: 1,
        Error: 2,
        Warning: 3,
        Info: 4,
        Success:5,
      },
    };
  },
};
</script>
  
  <style scoped>
@import url(toast.css);
</style>