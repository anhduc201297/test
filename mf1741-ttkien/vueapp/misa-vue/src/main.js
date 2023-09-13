import { createApp } from 'vue'

import App from './App.vue'
import MISAButton from './components/bases/button/MISAButton.vue'
import MISAInput from './components/bases/input/MISAInput.vue'
import MISAInputIcon from './components/bases/input/MISAInputIcon.vue'
import MISAInputCheckbox from './components/bases/input/MISAInputCheckbox.vue'
import MISAInputRadio from './components/bases/input/MISAInputRadio.vue'
import MISADroplist from './components/bases/droplist/MISADroplist.vue'
import MISAIcon from './components/bases/icon/MISAIcon.vue'
import MISATableData from './components/bases/table-data/MISATableData.vue'
import MISAForm from './components/bases/form/MISAForm.vue'
import MISADialog from './components/bases/dialog/MISADialog.vue'
import MISALoading from './components/bases/loading/MISALoading.vue'
import MISAToastMessage from './components/bases/toast-message/MISAToastMessage.vue'
import MISACombobox from '../node_modules/ms-combobox'

import helper from './js/helper'
import MISAResource from './js/MISAResource'
import MISAEnum from './js/MISAEnum'
import axios from 'axios'
import vueRouter from './router/router'

const app = createApp(App)

app.component("m-button", MISAButton)
app.component("m-input", MISAInput)
app.component("m-input-icon", MISAInputIcon)
app.component("m-input-checkbox", MISAInputCheckbox)
app.component("m-input-radio", MISAInputRadio)
app.component("m-droplist", MISADroplist)
app.component("m-icon", MISAIcon)
app.component("m-table-data", MISATableData)
app.component('m-form', MISAForm)
app.component('m-dialog', MISADialog)
app.component("m-loading", MISALoading)
app.component("m-toast-message", MISAToastMessage)
app.component("m-combobox", MISACombobox)

app.config.globalProperties.$helper = helper
app.config.globalProperties.$MISAResource = MISAResource
app.config.globalProperties.$MISAEnum = MISAEnum
app.config.globalProperties.$axios = axios

app.use(vueRouter)

app.mount('#app')

export default app