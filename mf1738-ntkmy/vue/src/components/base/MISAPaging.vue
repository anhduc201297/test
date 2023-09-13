<template>
    <!-- Paging -->
    <div class="table__detail--paging">
        <div class="table__record--dropdown">
          <div class="table__record">
            20 bản ghi trên 1 trang
            <div class="icon table__record-icon"></div>
          </div>
          <div class="paging--dropdown">
            <a href="#">10 bản ghi trên 1 trang</a>
            <a href="#">20 bản ghi trên 1 trang</a>
            <a href="#">30 bản ghi trên 1 trang</a>
            <a href="#">50 bản ghi trên 1 trang</a>
            <a href="#">100 bản ghi trên 1 trang</a>
          </div>
        </div>
        <ul class="pagination">
          <li class="page-item"><a class="page-link" href="#">Trước</a></li>
          <li class="page-item"><a class="page-link" href="#">1</a></li>
          <li class="page-item"><a class="page-link" href="#">2</a></li>
          <li class="page-item"><a class="page-link" href="#">3</a></li>
          <li class="page-item"><a class="page-link" href="#">4</a></li>
          <li class="page-item"><a class="page-link" href="#">5</a></li>
          <li class="page-item"><a class="page-link" href="#">Sau</a></li>
        </ul>
      </div>
</template>
  
<script>
export default {
    name: 'MISAPaging',
};
</script>

<style scoped>
@import url('../../css/base/paging.css');
</style>
  