<template>
    <div class="popup-container"> 
        <div class="popup-container">
          <div class="dialog">
            <div class="dialog__header">
              <div class="dialog__title">{{title}}</div>
              <div class="icon dialog__close" @click="closeDialog"></div>
            </div>
            <div class="dialog__content">
              <div
                class="dialog__icon" :class="typeIcon" 
              ></div>
              <span class="dialog__name"
                >{{ content }}
              </span>
            </div>
            <div
              class="dialog__button">
                <MISAButton 
                  v-if="showButton"
                  class="btn-cancel" 
                  :class="$MISAEnum.ButtonType.Normal"  
                  @click="closeDialog" 
                  :text="$MISAResource[$LangCode].CancelText">
                </MISAButton>

                <MISAButton 
                  :text="$MISAResource[$LangCode].AcceptText"
                  @click="onConfirm"
                  >
                </MISAButton>
            </div>
          </div>
        </div>
      </div>
</template>
  
<script>
export default {
    name: 'MISADialog',
    props: {
        title: String,
        content: String,
        typeIcon:String,
        onConfirm: Function,
        showButton: {
          type: Boolean,
          default: true 
        } 
    },
    methods: {
      /**
       * Hàm đóng DiaLog
       * CreateBy : NTKMY (20/08/2023)
      */
      closeDialog() {
      this.$emit('close');
    },
    
    }
};


</script>

<style scoped>
@import url('../../css/base/dialog.css');
@import url('../../css/base/icon.css');
</style>
  