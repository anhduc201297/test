<template>
  <div class="container-form">
    <div class="m-form-background"></div>
    <div class="m-form---content">
      <header class="m-form--header">
        <div class="m-form--title">
          <div class="title-popup">Thông tin nhân viên</div>
          <m-checkbox nameCheckbox="khachhang">Là khách hàng</m-checkbox>
          <m-checkbox nameCheckbox="nhacungcap">Là nhà cung cấp</m-checkbox>
        </div>
        <div class="m-form-close">
          <div class="mi mi-24 mi-help" title="Trợ giúp"></div>
          <div class="mi mi-24 mi-close" title="Đóng" @click="closeForm"></div>
        </div>
      </header>
      <main class="m-form-content">
        <div class="form-content scroller">
          <div class="basic-info">
            <div class="basic-info-left">
              <div class="flex-row">
                <m-text-field
                  :haveLabel="true"
                  :textFieldRequired="true"
                  style="width: 48%"
                  :isAutoFocused="true"
                  :tabIndex="1"
                  :valueInput="this.dataNhanVien.MaNhanVien || ''"
                  @inputOnChange="textFieldOnChange($event, 'MaNhanVien')"
                >
                  <template v-slot:lbl-content>Mã</template>
                  <template v-slot:warning>Mã không được để trống</template>
                </m-text-field>

                <m-text-field
                  :haveLabel="true"
                  :textFieldRequired="true"
                  style="width: 48%"
                  :tabIndex="2"
                  :valueInput="this.dataNhanVien.HoTen"
                  @inputOnChange="textFieldOnChange($event, 'HoTen')"
                >
                  <template v-slot:lbl-content>Tên</template>
                  <template v-slot:warning>Tên không được để trống</template>
                </m-text-field>
              </div>

              <m-combobox
                :dataDropdownlist="[
                  'Hà Nội',
                  'TP Hồ Chính Minh',
                  'Hải Phòng',
                  'Hải Dương',
                ]"
                :comboboxRequired="true"
                :tabIndex="3"
              >
                <template v-slot:lbl-content>Đơn vị</template>
                <template v-slot:warning>Đơn vị không được để trống</template>
              </m-combobox>

              <m-text-field
                style="padding-top: 8px"
                :tabIndex="4"
                :valueInput="this.dataNhanVien.ChucDanh"
                @inputOnChange="textFieldOnChange($event, 'ChucDanh')"
              >
                <template v-slot:lbl-content>Chức danh</template>
              </m-text-field>
            </div>
            <div class="basic-info-right">
              <div class="flex-row">
                <m-text-field
                  :haveLabel="true"
                  inputType="date"
                  style="width: 33%"
                  :tabIndex="5"
                  :valueInput="this.dataNhanVien.NgaySinh"
                  @inputOnChange="textFieldOnChange($event, 'NgaySinh')"
                >
                  <template v-slot:lbl-content>Ngày sinh</template>
                </m-text-field>

                <m-radio
                  :tabIndex="6"
                  :listOptions="['Nam', 'Nữ', 'Khác']"
                  style="width: 66%; padding-left: 8px"
                  >Giới tính</m-radio
                >
              </div>
              <div class="flex-row">
                <m-text-field
                  lblTooltip="Số chứng minh nhân dân"
                  style="width: 48%"
                  :tabIndex="7"
                  :valueInput="this.dataNhanVien.CMND"
                  @inputOnChange="textFieldOnChange($event, 'CMND')"
                >
                  <template v-slot:lbl-content>Số CMND</template>
                </m-text-field>
                <m-text-field
                  inputType="date"
                  style="width: 48%"
                  :tabIndex="8"
                  @inputOnChange="textFieldOnChange($event, 'NgayCap')"
                >
                  <template v-slot:lbl-content>Ngày cấp</template>
                </m-text-field>
              </div>
              <m-text-field
                :tabIndex="9"
                @inputOnChange="textFieldOnChange($event, 'NoiCap')"
              >
                <template v-slot:lbl-content>Nơi cấp</template>
              </m-text-field>
            </div>
          </div>

          <div class="contact-info">
            <m-text-field
              style="padding-top: 8px"
              :tabIndex="10"
              @inputOnChange="textFieldOnChange($event, 'DiaChi')"
            >
              <template v-slot:lbl-content>Địa chỉ</template>
            </m-text-field>

            <div class="flex-row flex-left">
              <m-text-field
                style="padding-top: 8px"
                :tabIndex="11"
                @inputOnChange="textFieldOnChange($event, 'DienThoaiDiDong')"
              >
                <template v-slot:lbl-content title="Điện thoại di động"
                  >ĐT di động</template
                >
              </m-text-field>
              <m-text-field
                style="padding-top: 8px; padding-left: 8px"
                :tabIndex="12"
                @inputOnChange="textFieldOnChange($event, 'DienThoaiCoDinh')"
              >
                <template v-slot:lbl-content title="Điện thoại cố định"
                  >ĐT cố định</template
                >
              </m-text-field>

              <m-text-field
                style="padding-top: 8px; padding-left: 8px"
                :tabIndex="13"
                @inputOnChange="textFieldOnChange($event, 'Email')"
              >
                <template v-slot:lbl-content>Email</template>
              </m-text-field>
            </div>

            <div class="flex-row flex-left">
              <m-text-field
                style="padding-top: 8px"
                :tabIndex="14"
                :valueInput="this.dataNhanVien.SoTaiKhoan"
                @inputOnChange="textFieldOnChange($event, 'SoTaiKhoan')"
              >
                <template v-slot:lbl-content>Tài khoản ngân hàng</template>
              </m-text-field>
              <m-text-field
                style="padding-top: 8px; padding-left: 8px"
                :tabIndex="15"
                :valueInput="this.dataNhanVien.NganHang"
                @inputOnChange="textFieldOnChange($event, 'NganHang')"
              >
                <template v-slot:lbl-content>Tên ngân hàng</template>
              </m-text-field>
              <m-text-field
                style="padding-top: 8px; padding-left: 8px"
                :tabIndex="16"
                :valueInput="this.dataNhanVien.ChiNhanh"
                @inputOnChange="textFieldOnChange($event, 'ChiNhanh')"
              >
                <template v-slot:lbl-content>Chi nhánh</template>
              </m-text-field>
            </div>
          </div>
        </div>

        <div class="controll flex-row">
          <div class="left">
            <m-button class="m-btn-secondary" tabindex="17">Hủy</m-button>
          </div>
          <div class="right">
            <m-button
              class="m-btn-secondary"
              style="margin-right: 12px; /* zero */"
              tabindex="18"
              >Cất</m-button
            >
            <m-button tabindex="19" @click="openDialog">Cất và thêm</m-button>
          </div>
        </div>
      </main>
    </div>
  </div>
</template>
<script>
export default {
  name: "EmployeeDetails",
  data() {
    return {
      dataNhanVien: {
        MaNhanVien: "",
        HoTen: "",
        GioiTinh: "",
        NgaySinh: "",
        CMND: "",
        NgayCap: "",
        NoiCap: "",
        DiaChi: "",
        ChucDanh: "",
        DonVi: "",
        SoTaiKhoan: "",
        NganHang: "",
        ChiNhanh: "",
        DienThoaiDiDong: "",
        DienThoaiCoDinh: "",
        Email: "",
      },
    };
  },
  props: {
    nhanVien: {
      type: Object,
      required: false,
    },
  },
  created() {
    if (this.nhanVien) this.dataNhanVien = this.nhanVien;
  },
  methods: {
    /**
     * Gửi sự kiện closeForm cho component cha để đóng form
     * Author: NVDUNG (20/08/2023)
     */
    closeForm() {
      this.$emit("closeForm");
    },

    /**
     * Nhận dữ liệu từ sự kiện inputOnChange để lưu vào data
     * Author: NVDUNG (20/08/2023)
     */
    textFieldOnChange(newValue, dataAttr) {
      try {
        console.log(newValue, dataAttr);
        this.dataNhanVien[dataAttr] = newValue;
        console.log("mã:" + this.dataNhanVien.MaNhanVien);
        console.log("Tên:" + this.dataNhanVien.HoTen);
      } catch (error) {
        console.log(error);
      }
    },

    /**
     * Mở dialog thông báo
     * Author: NVDUNG (22/08/2023)
     */
    openDialog() {
      this.$emit("openDialog");
    },
  },
};
</script>
<style scoped>
@import url(./employee-detail.css);
</style>
