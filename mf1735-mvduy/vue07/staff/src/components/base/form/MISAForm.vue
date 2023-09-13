<template>
    <div class="d-form-popup">
      <div class="d-detail-form">
        <div class="header-form">
          <div class="left-header">
            <span style="font-size: 24px; font-weight: 700"
              >{{ title }}</span
            >
            <slot name="left-header"></slot>
          </div>
          <div class="right-header">
            <div class="icon help-icon"></div>
            <div class="icon close-icon" @click="onClose"></div>
          </div>
        </div>
        <div class="body-form">
            <slot name="body-form"></slot>
        </div>
  
        <div class="footer-form">
            <slot name="footer-form"></slot>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: "MISAForm",
    methods: {
     
    },
    data() {
      return {
      }
    },
  };
  </script>
  
  <style scoped>
  @import url(form.css);
  </style>