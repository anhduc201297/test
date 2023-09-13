import { createApp } from 'vue'
import App from './App.vue'
import MISAButton from './components/base/button/MISAButton.vue'
import MISAButtonNoText from './components/base/button/MISAButtonNoText.vue'
import MISADialog from './components/base/dialog/MISADialog.vue'
import MISAInput from './components/base/input/MISAInput.vue'
import MISACheckboxInput from './components/base/input/MISACheckboxInput.vue'
import MISARadioInput from './components/base/input/MISARadioInput.vue'
import MISACheckEmptyInput from './components/base/input/MISACheckEmptyInput.vue'
// import MISACombobox from './components/base/combobox/MISACombobox.vue'
import MISAForm from './components/base/form/MISAForm.vue'
import MISAToast from './components/base/toast/MISAToast.vue'
import MISALoading from './components/base/loading/MISALoading.vue'
import commonJS from './js/common'
import helperJS from './js/helper'
import resourceJS from './js/resource'
import enumJS from './js/enum'
import MSCombobox from 'ms-combobox'
import axios from 'axios'
import vueRouter from './router/router.js' 
import emitter from 'tiny-emitter/instance'

const app = createApp(App);
app.component("m-button", MISAButton);
app.component("m-button-notext", MISAButtonNoText);
app.component("m-dialog", MISADialog);
app.component("m-input", MISAInput);
app.component("m-checkbox", MISACheckboxInput);
app.component("m-radio", MISARadioInput);
app.component("m-check-input", MISACheckEmptyInput);
app.component("m-combobox", MSCombobox);
app.component("m-form", MISAForm);
app.component("m-toast", MISAToast);
app.component("m-loading", MISALoading);


app.config.globalProperties.$commonJS= commonJS;
app.config.globalProperties.$helperJS= helperJS;
app.config.globalProperties.$resourceJS= resourceJS;
app.config.globalProperties.$enumJS= enumJS;
app.config.globalProperties.$axios= axios;
app.config.globalProperties.$emitter= emitter;
app.use(vueRouter)
app.mount('#app')
