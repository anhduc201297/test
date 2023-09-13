<template>
    <!-- Input Radio -->
    <div class="gender__checkbox">
            <input
            name="radio"
            type="radio"
            class="input__radio"
            v-model="checked"
            :value="value"
        />
        <div class="gender__name">{{label}}</div>
    </div>
</template>
  
<script>
export default {
    name: 'MISAInputRadio',
    props:["value", "label"]
};
</script>

<style scoped>
@import url('../../../css/base/input.css');
.gender__checkbox {
    display: flex;
    gap: 8px;
    align-items: center;
}
</style>
  