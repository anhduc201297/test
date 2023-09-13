<template>
    <!-- Input Button -->
    <button :class="type" class="tooltip">{{ text }} 
        <MISATooltip v-if="showTooltip" :tooltip="tooltip"></MISATooltip>
    </button>
</template>
  
<script>
import MISAEnum from '../../js/MISAEnum';
import MISATooltip from './MISATooltip.vue';
export default {
    name: 'MISAButton',
    props: {
        text: String,
        type: {
            type: String,
            default: MISAEnum.ButtonType.Success,
        },
        tooltip: String,
        showTooltip: Boolean,
    },
    data() {
        return {};
    },
    methods: {},
    components: { MISATooltip }
};
</script>

<style scoped>
@import url('../../css/base/button.css');
@import url("../../css/base/tooltip.css");
</style>
  