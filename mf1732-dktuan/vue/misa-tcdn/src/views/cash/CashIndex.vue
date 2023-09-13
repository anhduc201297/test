<template>
    <EmployeeForm :classes="'inside'"/>
</template>

<script>
import EmployeeForm from '../employee/EmployeeForm.vue';

export default {
    name: "CashIndex",
    components: { EmployeeForm },
}
</script>

<style scoped>
    div {
        font-size: 22.5px;
    }
</style>