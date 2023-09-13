// import css
import './assets/css/main.css';

import { createApp } from "vue";
import App from "./App.vue";

// import libs
import emitter from 'tiny-emitter/instance'
import axios from "axios";
// import components global
import MISAButton from "./components/base/buttons/MISAButton.vue";
import MISALabelVue from './components/base/label/MISALabel.vue';
import MISATextField from "./components/base/textfield/MISATextField.vue";
import MISACombobox from "./Components/base/combobox/MISACombobox.vue";
import MISACheckbox from "./Components/base/checkbox/MISACheckbox.vue";
import MISARadioButton from "./Components/base/radiobutton/MISARadioButton.vue";
import MISADialogVue from './components/base/dialog/MISADialog.vue';
import MISALoading from './components/base/loading/MISALoading.vue';
import MISAToast from './components/base/toast/MISAToast.vue';
import MISAForm from './components/base/form/MISAForm.vue';

// import /js 
import MISAEnum from "./js/enum.js";
import MISAHelper from "./js/helper.js";
import MISAResource from "./js/resource.js";
import repository from "./js/repository.js";

// import router
import router from "./router/index.js";

const app = createApp(App);
app.component('m-button', MISAButton);
app.component('m-label', MISALabelVue);
app.component('m-text-field', MISATextField);
app.component('m-combobox', MISACombobox);
app.component('m-checkbox', MISACheckbox);
app.component('m-radio', MISARadioButton);
app.component('m-dialog', MISADialogVue);
app.component('m-loading', MISALoading);
app.component('m-toast', MISAToast);
app.component('m-form', MISAForm);

// Tạo biến globals
app.config.globalProperties.$enum = MISAEnum;
app.config.globalProperties.$helper = MISAHelper;
app.config.globalProperties.$resource = MISAResource;
app.config.globalProperties.$axios = axios;
app.config.globalProperties.$repository = repository;
app.config.globalProperties.$emitter = emitter;

// Thêm router
app.use(router);

app.mount("#app");
