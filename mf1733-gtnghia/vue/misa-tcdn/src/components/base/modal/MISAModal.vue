<template>
  <div class="modal-container">
    <div class="modal">
      <div class="modal__header">
        <div class="header__start">
          <div class="header__title mr-24">
            <p class="heading1">{{ title }}</p>
          </div>
        </div>
        <div class="header__end">
          <slot name="modal__header--end"></slot>
          <m-button
            :type="'icon'"
            class="btn-modal-help"
            :icon="'icon-help'"
            :title="'helper'"
            @click="onResetForm"
          />
          <m-button
            :type="'icon'"
            class="btn-modal-close"
            :icon="'icon-close'"
            @click="onCloseForm"
          />
        </div>
      </div>
      <div class="modal__body mb-8">
        <slot name="modal__body"></slot>
      </div>
      <div class="modal__footer">
        <div class="footer__start">
          <m-button :type="'extra'" :title="'Hủy'" @click="onCloseForm" />
          <slot name="modal__footer--start"> </slot>
        </div>
        <div class="footer__end">
          <slot name="modal__footer--end"></slot>
          <m-button
            :title="titleSubmitBtn"
            @click="onSubmitForm"
            class="mr-8"
          />
          <m-button :title="titleSubmitAndNewBtn" @click="onSubmitAndNewForm" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    URL: String,
    method: String,
    title: String,
    titleSubmitBtn: {
      type: String,
      default: "Xác nhận",
    },
    titleSubmitAndNewBtn: {
      type: String,
      default: "Xác nhận",
    },
    data: Object,
    msgSuccess: String,
  },
  data() {
    return {
      isShowModal: false,
    };
  },
  methods: {
    /**
     * Send Event onResetForm
     * Created by: mf1733-gtnghia - 21/8/2023
     */
    onResetForm() {
      this.$emit("onResetForm");
    },
    /**
     * Send Event onCloseForm
     * Created by: mf1733-gtnghia - 21/8/2023
     */
    onCloseForm() {
      this.$emit("onCloseForm");
    },
    /**
     * Handle Submit Button
     * Created by: mf1733-gtnghia - 29/8/2023
     */
    async sendForm() {
      console.log(this.data);
      const request = await this.$sendRequest(this.URL, this.method, this.data);
      if (request === 1) {
        // Case: SUCCESS
        this.$emitter.emit(
          "addToast",
          this.$MISAEnum.ToastStatus.success,
          this.msgSuccess
        );
        return 1;
      } else {
        // Case: ERROR
        const error = this.$MISAHelper.handleErrorResponse(request);
        console.log(error);
        this.$emitter.emit(
          "showDialog",
          error.status,
          error.title,
          error.message
        );
        return 0;
      }
    },
    /**
     * Handle Submit and new Button
     * Created by: mf1733-gtnghia - 29/8/2023
     */
    async onSubmitAndNewForm() {
      const result = await this.sendForm();
      if (result === 1) {
        this.$emit("onSubmitAndNewForm");
      }
    },
    /**
     * Handle Submit Button
     * Created by: mf1733-gtnghia - 29/8/2023
     */
    async onSubmitForm() {
      const result = await this.sendForm();
      if (result === 1) {
        this.$emit("onSubmitForm");
      }
    },
  },
};
</script>

<style scoped>
@import url(./modal.css);
</style>
