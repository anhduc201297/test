<template>
  <div>
    <m-button
      class="mr-8"
      :route="'customer.list'"
      :title="'Danh sách khách hàng'"
      :type="'route'"
    >
    </m-button>
    <m-button
      class="mr-8"
      :route="'employee.list'"
      :title="'Danh sách nhân viên'"
      :type="'route'"
    >
    </m-button>
  </div>
</template>

<script>
export default {
  name: "HomePage",
};
</script>

<style scoped></style>
