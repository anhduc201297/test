import { createApp } from 'vue'
import App from './App.vue'
import MISATooltip from './components/base/MISATooltip.vue'
import MISAButton from './components/base/MISAButton.vue'
import MISAPaging from './components/base/MISAPaging.vue'
import MISAInputText from './components/base/MISAInput/MISAInputText.vue'
import MISAInputCheckbox from './components/base/MISAInput/MISAInputCheckbox.vue'
import MISAInputRadio from './components/base/MISAInput/MISAInputRadio.vue'
import MISAInputDateTime from './components/base/MISAInput/MISAInputDateTime.vue'
import MISALoading from './components/base/MISALoading.vue'
import MISADialog from './components/base/MISADialog.vue'
import helper from './js/helper'
import MISAResource from './js/MISAResource'
import MISAEnum from './js/MISAEnum'
import MISAToast from './components/base/MISAToast'
import axios from 'axios'
import emitter from 'tiny-emitter/instance'
import vueRouter from './router/router';


const app = createApp(App)

app.component("MISATooltip", MISATooltip);
app.component("MISAButton", MISAButton);
app.component("MISAPaging", MISAPaging);
app.component("MISAInputText", MISAInputText);
app.component("MISAInputCheckbox", MISAInputCheckbox);
app.component("MISAInputRadio", MISAInputRadio);
app.component("MISAInputDateTime", MISAInputDateTime);
app.component("MISADialog", MISADialog);
app.component("MISALoading", MISALoading);
app.component("MISAToast", MISAToast);

app.config.globalProperties.$helper = helper;
app.config.globalProperties.$MISAResource = MISAResource;
app.config.globalProperties.$MISAEnum = MISAEnum;
app.config.globalProperties.$LangCode = 'VN';
app.config.globalProperties.$axios = axios;
app.config.globalProperties.$emitter = emitter;

app.use(vueRouter);

app.mount('#app')

export default app;