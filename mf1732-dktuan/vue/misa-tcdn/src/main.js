import { createApp } from 'vue'
import App from './App.vue'
import MISAButton from './components/base/button/MISAButton.vue';
import MISAToast from './components/base/toast/MISAToast.vue';
import MISATable from './components/base/table/MISATable.vue';
import MISADialog from './components/base/dialog/MISADialog.vue';
import MISAInput from './components/base/input/MISAInput.vue'
import MISAModal from './components/base/modal/MISAModal.vue';
import MISALoading from './components/base/loading/MISALoading.vue'
import MISACombobox from './components/base/combobox/MISACombobox.vue'
import enums from './js/MISAEnum';
import resource from './js/MISAResource';
import router from './routes/routes';
import emitter from 'tiny-emitter/instance'
import helper from './js/MISAHelper';
import vuetify from './plugins/vuetify';

var app = createApp(App)
app.component('m-button', MISAButton)
app.component('m-toast', MISAToast)
app.component('m-table', MISATable)
app.component('m-dialog', MISADialog)
app.component('m-input', MISAInput)
app.component('m-modal', MISAModal)
app.component('m-loading', MISALoading)
app.component('m-combobox', MISACombobox)

app.config.globalProperties.$enum = enums
app.config.globalProperties.$message = resource
app.config.globalProperties.$helper=helper
app.config.globalProperties.$lang = "VN"
app.config.globalProperties.$emitter = emitter

app.use(router)
app.use(vuetify)

app.mount('#app')


export default app