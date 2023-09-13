<template>
  <div class="table-content">
    <table class="d-table">
      <thead>
        <tr>
          <th class="text-center" style="width: 20px">
            <input
              class="d-checkbox icon"
              :class="{'icon-checked':checkedAll}"
              type="checkbox"
              v-model="checkedAll"
              @change="checkedAllCheckbox"
            />
          </th>
          <th class="text-left" style="width: 140px">MÃ NHÂN VIÊN</th>
          <th class="text-left">TÊN NHÂN VIÊN</th>
          <th class="text-left" style="width: 100px">GIỚI TÍNH</th>
          <th class="text-center">NĂM SINH</th>
          <th class="text-left" title="Số chứng minh nhân dân">SỐ CMND</th>
          <th class="text-left">CHỨC DANH</th>
          <th class="text-left">TÊN ĐƠN VỊ</th>
          <th class="text-left">SỐ TÀI KHOẢN</th>
          <th class="text-left">TÊN NGÂN HÀNG</th>
          <th class="text-left">CHI NHÁNH TK NGÂN HÀNG</th>
          <th class="text-center" style="width: 120px">CHỨC NĂNG</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(staff, index) in staffs" :key="staff.CustomerID">
          <td class="text-center" style="width: 20px">
            <input
              class="d-checkbox icon"
              :class="{'icon-checked':checked[index]}"
              type="checkbox"
              v-model="checked[index]"
            />

          </td>
          <td class="text-left" style="width: 70px">
            {{ staff.CustomerCode }}
          </td>
          <td class="text-left">{{ staff.FullName }}</td>
          <td class="text-left" style="width: 40px">
            {{ $commonJS.formatGender(staff.Gender) }}
          </td>
          <td class="text-center">
            {{ $commonJS.formatDate(staff.DateOfBirth) || "" }}
          </td>
          <td class="text-left">123456789</td>
          <td class="text-left">Fresher</td>
          <td class="text-left">{{ staff.CompanyName || "" }}</td>
          <td class="text-left">{{ staff.DebitAmount || "" }}</td>
          <td class="text-left"></td>
          <td class="text-left"></td>

          <td class="text-center" style="width: 50px">
            Sửa
            <div class="down-select">
              <div class="down-select-button">
                <div class="icon icon-triangle-down"></div>
              </div>
              <div class="select-content">
                <a href="#">Nhân bản</a>
                <a href="#" @click="onShow">Xóa</a>
                <a href="#">Ngừng sử dụng</a>
              </div>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
    
    <script>
export default {
  name: "MISAStaffTable",
  created() {
    /*
     * Lấy dữ liệu
     * Author: mf1735-duy (24/08/2023)
     */
    try {
      fetch("https://cukcuk.manhnv.net/api/v1/customers")
        .then((res) => res.json())
        .then((data) => {
          this.staffs = data;
          
        });
    } catch (error) {
      console.log(error);
    }
  },

  methods: {
    /*
     * Gọi đến sự kiện onShowDialog trong TheContent.vue
     * Author: mf1735-duy (20/08/2023)
     */
    onShow() {
      this.$emit("onShowDialog");
    },

    checkedAllCheckbox() {
      for (let i = 0; i < this.staffs.length; i++) {
        this.checked[i] = this.checkedAll;
      }
    },
  },
  data() {
    return {
      checkedAll: false,
      checked: [],
      staffs: [],
    };
  },
};
</script>
    
   <style scoped>
@import url(table.css);

.d-checkbox {
  appearance: none;
  width: 20px;
  height: 20px;
  margin: 0 6px;
  transform: translate(0, 2px);
  border: 2px solid #9e9e9e ;
  border-radius: 3px !important;
  cursor: pointer;  
}

.d-checkbox:checked {
  background-color:#067933;
  border-color: #067933;
  filter: brightness(1.5);
}
</style>