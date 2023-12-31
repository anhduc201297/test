import { createApp } from "vue";
import App from "./App.vue";
import MToastMessage from "./components/base/mtoastmessage/MToastMessage.vue";
import MToastMessageGroup from "./components/base/mtoastmessage/MToastMessageGroup.vue";
import TheSidebar from "./components/layout/sidebar/TheSidebar.vue";
import TheHeader from "./components/layout/header/TheHeader.vue";
import MInputText from "./components/base/minputtext/MInputText.vue";
import MComboBox from "./components/base/mcombobox/MComboBox.vue";
import MDatePicker from "./components/base/mdatepicker/MDatePicker.vue";
import MButton from "./components/base/mbutton/MButton.vue";
import MInputRadio from "./components/base/minputradio/MInputRadio.vue";
import MNotificationDialog from "./components/base/mdialog/MNotificationDialog.vue";
import MAcceptDialog from "./components/base/mdialog/MAcceptDialog.vue";
import MInputCheckbox from "./components/base/minputcheckbox/MInputCheckbox.vue";
import MIsLoading from "./components/base/misloading/MIsLoading.vue";
import MSCombobox from "ms-combobox";
import MForm from "./components/base/mform/MForm.vue";
import ErrorPage from "./views/error/Error.vue";
import emitter from "tiny-emitter/instance";
import resource from "./js/resource";
import baseEnum from "./js/enum";
import helper from "./js/helper";
import router from "./router/router";
import axios from "./js/axios.js";
// App.component
const app = createApp(App);

app.component("MToastMessage", MToastMessage);
app.component("MToastMessageGroup", MToastMessageGroup);
app.component("TheSidebar", TheSidebar);
app.component("TheHeader", TheHeader);
app.component("m-input-text", MInputText);
app.component("MComboBox", MComboBox);
app.component("MDatePicker", MDatePicker);
app.component("MButton", MButton);
app.component("MInputRadio", MInputRadio);
app.component("MNotificationDialog", MNotificationDialog);
app.component("MAcceptDialog", MAcceptDialog);
app.component("MInputCheckbox", MInputCheckbox);
app.component("MIsLoading", MIsLoading);
app.component("MSCombobox", MSCombobox);
app.component("MForm", MForm);
app.component("ErrorPage", ErrorPage);
app.config.globalProperties.$resource = resource;
app.config.globalProperties.$languageCode = "VN";
app.config.globalProperties.$helper = helper;
app.config.globalProperties.$baseEnum = baseEnum;
app.config.globalProperties.$emitter = emitter;
app.config.globalProperties.$axios = axios;
app.use(router);
app.mount("#app");
