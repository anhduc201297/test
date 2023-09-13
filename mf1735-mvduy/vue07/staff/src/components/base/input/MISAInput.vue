<template>
        <input class="d-input" type="text" v-model="outputValue" />
</template>

<script>
export default {
    name: 'MISAInput',
    props: [
        'inputValue'
    ],
     /*
     * Ràng buộc outputValue với inputValue
     * Author: mf1735-duy (20/08/2023)
     */
    created(){
        this.outputValue = this.inputValue;
    },
     /*
     * Theo dõi sự thay đổi của ouputValue
     * Author: mf1735-duy (20/08/2023)
     */
   
    data() {
    return {
        outputValue: '',
    }
  },
}
</script>

<style scoped>
@import url(./input.css);
.text-error{
    font-weight: 500 !important;
    color: #E61D1D;
    font-size: 12px;
}
</style>