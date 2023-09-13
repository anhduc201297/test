import { createApp } from 'vue'
import App from './App.vue'
/* import the fontawesome core */
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
/* import specific icons */
import {fas} from '@fortawesome/free-solid-svg-icons'

import helper from './js/helper'
import resource from './js/resource'
import enums from './js/enum'
import axios from 'axios'
import emitter from 'tiny-emitter/instance'
import router from './router/router'

import BaseToast from '../src/components/base/BaseToast.vue'
import BaseButton from '../src/components/base/BaseButton.vue'
import BaseInput from '../src/components/base/BaseInput.vue'
import BaseCombobox from '../src/components/base/BaseCombobox.vue'
import BaseCheckbox from '../src/components/base/BaseCheckbox.vue'
import BaseLoading from '../src/components/base/BaseLoading.vue'
import BaseDialog from '../src/components/base/BaseDialog.vue'


/* add icons to the library */
library.add(fas);

const app = createApp(App);

app.component('font-awesome-icon', FontAwesomeIcon);
app.component('base-toast', BaseToast);
app.component('base-button', BaseButton);
app.component('base-input', BaseInput);
app.component('base-combobox', BaseCombobox);
app.component('base-checkbox', BaseCheckbox);
app.component('base-loading', BaseLoading);
app.component('base-dialog', BaseDialog);

app.use(router);
app.config.globalProperties.$helper = helper;
app.config.globalProperties.$resource = resource;
app.config.globalProperties.$LangCode = "VN";
app.config.globalProperties.$enums = enums;
app.config.globalProperties.$axios = axios;
app.config.globalProperties.$emitter = emitter;
app.mount('#app');
