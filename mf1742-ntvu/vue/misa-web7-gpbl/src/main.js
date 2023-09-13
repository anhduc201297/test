import { createApp } from 'vue';
import App from './App.vue';
import MButton from './components/base/button/MButton.vue';
import MButtonNoText from './components/base/button/MButtonNoText.vue';
import MLoading from './components/base/loading/MLoading.vue';
import MInputText from './components/base/input/MInputText.vue';
import MInputNumber from './components/base/input/MInputNumber.vue';
import MDialog from './components/base/dialog/MDialog.vue';
import MRadio from './components/base/input/MRadio.vue';
import MComboBox from './components/base/comboBox/MComboBox.vue';
import MCheckBox from './components/base/checkbox/MCheckBox.vue';
import MMask from './components/base/mask/MMask.vue';
import MResource from './js/MResource';
import MEnum from './js/MEnum';
import helper from './js/helper';
import router from './router';
import { getLangCode } from './js/utils/localStorage';

const app = createApp(App);
const { value } = getLangCode();

app.component('m-button', MButton);
app.component('m-btn-no-text', MButtonNoText);
app.component('m-loading', MLoading);
app.component('m-input-text', MInputText);
app.component('m-input-number', MInputNumber);
app.component('m-dialog', MDialog);
app.component('m-radio', MRadio);
app.component('m-combo-box', MComboBox);
app.component('m-check-box', MCheckBox);
app.component('m-mask', MMask);

app.config.globalProperties.$MEnum = MEnum;
app.config.globalProperties.$Helper = helper;
app.config.globalProperties.$Resource = MResource[value];

app.use(router);
app.mount('#appMain');
export default app;
