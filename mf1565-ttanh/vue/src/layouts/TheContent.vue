<script setup>
import { VButton } from '@/components';
import { DataTable, EmployeeForm } from '@/features';
import { useEmployeeStore } from '@/stores/employee';

const Employee = useEmployeeStore();
</script>

<template>
    <div id="content">
        <div class="content-header">
            <div class="heading-1">Nhân viên</div>
            <VButton type="primary" size="medium" @click="Employee.details.create">
                Thêm mới nhân viên
            </VButton>
            <EmployeeForm v-if="Employee.isShowNewDetails"></EmployeeForm>
        </div>
        <DataTable />
    </div>
</template>

<style lang="scss" scoped>
#content {
    max-height: var(--main-content-height);
    height: 100%;
    padding: var(--main-content-padding-y) var(--main-content-padding-x);
    background-color: rgb(var(--c-gray-100));
    .content-header {
        height: var(--main-content-header-height);
        display: flex;
        justify-content: space-between;
        align-items: center;
    }
}
</style>
