<template>
  <div class="dialog-container">
    <div class="dialog">
      <div class="dialog__header">
        <div class="dialog__header--title">
          <p class="heading2">{{ dialog.title }}</p>
        </div>
        <div class="dialog__header--close">
          <m-button
            @click="onCloseDialog"
            :type="'icon'"
            :icon="'icon-close-s'"
            :size="'s'"
          />
        </div>
      </div>
      <div class="dialog__body">
        <i class="icon icon-warning-l mr-8"></i>
        <p class="body2">{{ dialog.message }}</p>
        <slot name="dialog__body"></slot>
      </div>
      <div v-if="dialog.type === 'feature'" class="dialog__footer">
        <div class="dialog__footer--option">
          <slot name="dialog__footer"></slot>
          <m-button
            :type="'extra'"
            :title="dialog.cancelTitle"
            class="mr-8"
            @click="onCloseDialog"
          />
          <m-button
            :type="'error'"
            :title="dialog.acceptTitle"
            @click="onAcceptDialog"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "m-dialog",
  props: {
    dialog: Object,
  },
  data() {
    return {};
  },
  methods: {
    /**
     * closeDialog
     * Created by: mf1733-gtnghia 1/9/2023
     */
    onCloseDialog() {
      this.$emitter.emit("closeDialog");
    },
    /**
     * closeDialog
     * Created by: mf1733-gtnghia 1/9/2023
     */
    onAcceptDialog() {
      this.$emitter.emit(this.dialog.onAccept);
      this.onCloseDialog();
    },
  },
  watch: {},
};
</script>

<style scoped>
@import url(./dialog.css);
</style>
