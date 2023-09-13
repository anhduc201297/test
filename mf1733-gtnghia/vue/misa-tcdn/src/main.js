import { createApp } from "vue";
import App from "./App.vue";
// IMPORT COMPONENTS
import MISAInput from "./components/base/input/MISAInput.vue";
import MISAModal from "./components/base/modal/MISAModal.vue";
import MISAPopup from "./components/base/popup/MISAPopup.vue";
import MISARadio from "./components/base/input/option/MISARadio.vue";
import MISATable from "./components/base/table/MISATable.vue";
import MISAToast from "./components/base/toast/MISAToast.vue";
import MISAToastMsg from "./components/base/toast/MISAToastMsg.vue";
import MISAButton from "./components/base/button/MISAButton.vue";
import MISADialog from "./components/base/dialog/MISADialog.vue";
import MISALoading from "./components/base/loading/MISALoading.vue";
import MISACheckbox from "./components/base/input/option/MISACheckbox.vue";
import MISAContextMenu from "./components/base/contextmenu/MISAContextMenu.vue";

// IMPORT JS
import MISAEnum from "./js/MISAEnum";
import MISAHelper from "./js/MISAHelper";
import MISAResource from "./js/MISAResource";

// IMPORT LIBRARY
import MSCombobox from "ms-combobox"
import vueRouter from './router/router.js'
import axios from "axios";
import emitter from "tiny-emitter/instance"
import store from "./store"

// IMPORT SERVICES
import sendRequest from "./services/sendRequest";

const app = createApp(App);
app.component("m-input", MISAInput);
app.component("m-modal", MISAModal);
app.component("m-popup", MISAPopup);
app.component("m-radio", MISARadio);
app.component("m-table", MISATable);
app.component("m-toast", MISAToast);
app.component("m-toast-msg", MISAToastMsg);
app.component("m-button", MISAButton);
app.component("m-dialog", MISADialog);
app.component("m-loading", MISALoading);
app.component("m-checkbox", MISACheckbox);
app.component("m-context-menu", MISAContextMenu);

app.component("m-combobox", MSCombobox)

app.config.globalProperties.$MISAHelper = MISAHelper
app.config.globalProperties.$MISAEnum = MISAEnum
app.config.globalProperties.$MISAResource = MISAResource
app.config.globalProperties.$LangCode = "VN"
app.config.globalProperties.$axios = axios
app.config.globalProperties.$emitter = emitter
app.config.globalProperties.$sendRequest = sendRequest

app.use(vueRouter)
app.use(store)
app.mount("#app");

export default app
