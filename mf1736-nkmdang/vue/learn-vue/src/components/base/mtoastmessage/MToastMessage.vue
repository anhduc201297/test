<template>
  <div class="toast-message-box" v-if="showToastMessage">
    <div class="toast-message-icon">
      <div class="icons" :class="toastMesContent.Icon"></div>
    </div>
    <div class="toast-message-desc">
      <span class="toast-message-status" :class="toastMesContent.TextClass">
        {{ toastMesContent.Status }}
      </span>
      {{ toastMesContent.Desc }}
    </div>
    <div class="toast-message-closebox">
      <div class="toast-message-option">
        <a href="">{{ toastMesContent.OptionAction }}</a>
      </div>
      <div class="toast-message-close" @click="clickClose">
        <i class="icons icon__miniclose"></i>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: ["onClose", "toastMesContent"],
  methods: {
    clickClose() {
      // this.$emitter.emit("onHideToastMessage");
      this.showToastMessage = false;
    },
  },
  // beforeMount() {
  //   console.log(this.onClose)
  // },
  data() {
    return {
      showToastMessage: true,
    };
  },
};
</script>
<style scoped>
@import url(./mtoastmessage.css);

.text-50b83c {
  color: #50b83c;
  font-weight: bold;
}

.text-DE3618 {
  color: #de3618;
}

.text-F49342 {
  color: #f49342;
}

.icon__miniclose {
  background-position: -1177px -362px;
  width: 13px;
  height: 12px;
}

.ti-close {
  color: #000;
}
.icon__error {
  background-position: -1136px -467px;
  width: 24px;
  height: 23px;
}

.icon__warning {
  background-position: -593px -145px;
  width: 22px;
  height: 23px;
}
</style>
