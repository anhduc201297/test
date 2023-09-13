import { createApp } from 'vue';
import App from './App.vue';
import MISAButton from '@/components/base/button/MISAButton.vue';
import MISAForm from '@/components/base/form/MISAForm.vue';
import MISAInput from '@/components/base/input/MISAInput.vue';
import MISARadio from '@/components/base/radio/MISARadio.vue';
import MISACheckbox from '@/components/base/checkbox/MISACheckbox.vue';
import MISAInputFull from '@/components/base/input/MISAInputFull.vue';
import MISAIcon from '@/components/base/icon/MISAIcon.vue';
import MISASidebarItem from '@/components/base/sidebar-item/MISASidebarItem.vue'
import MISADialog from '@/components/base/dialog/MISADialog.vue';
import MISAToast from '@/components/base/toast/MISAToast.vue';
import MISACombobox from '@/components/base/combobox/MISACombobox.vue';
import MISAEnum from './js/enum.js';
import helper from './js/helper.js';
import resources from './js/resources.js';
import handle from './js/handle.js';
import axios from 'axios';
import routers from './router/index.js';
import emitter from 'tiny-emitter/instance'


const app = createApp(App);

app.component('m-button', MISAButton);
app.component('m-form', MISAForm);
app.component('m-input', MISAInput);
app.component('m-radio', MISARadio);
app.component('m-checkbox', MISACheckbox);
app.component('m-input-full', MISAInputFull);
app.component('m-icon', MISAIcon);
app.component('m-sidebar-item', MISASidebarItem);
app.component('m-dialog', MISADialog);
app.component('m-toast', MISAToast);
app.component('m-combobox', MISACombobox);

app.config.globalProperties.$enum = MISAEnum;
app.config.globalProperties.$helper = helper;
app.config.globalProperties.$resources = resources;
app.config.globalProperties.$handle = handle;
app.config.globalProperties.$LangCode = 'VN';
app.config.globalProperties.$axios = axios;
app.config.globalProperties.$emitter = emitter;

app.use(routers);

app.mount('#app');

export default app;