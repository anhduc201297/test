<template>
    <section class="content-section">
        <header class="table__header">
            <div class="select-action-wrapper" v-if="checkList.length > 0">
                <span>Đã chọn {{ checkList.length }}</span>
                <m-button @click="removeCheck" :class="'outline'">Bỏ chọn</m-button>
                <m-button :class="'outline'">
                    <span class="icon garbage-icon"></span>
                    Xóa tất cả
                </m-button>
            </div>
            <div class="search-empl">
                <div class="search-box">
                    <span class="search-icon"></span>
                    <input class="text-input search-input" :placeHolder="searchPlaceHolder" type="text">
                </div>
                <span @click="refreshRecords" title="Làm mới" class="reload-icon"></span>
                <span class="icon export-icon" title="Xuất file Excel"></span>
            </div>
        </header>
        <div class="table-container">
            <table class="table">
                <tr class="table-header">
                    <th class="check-col">
                        <input ref="checkAll" @input="setCheckAll" class="checkbox" type="checkbox" name="check-all">
                    </th>
                    <!-- <slot name="theaders"></slot> -->
                    <th v-for="header in headers" :key="header.employeeCode" :class="header.class" :title="header.tooltip">
                        {{ header.title }}
                    </th>
                    <th class="edit-btn">Chức năng</th>
                </tr>
                <tr v-for="tdata in data" :key="tdata.code">
                    <td class="check-col">
                        <input @input="handleCheck" class="checkbox" ref="check" type="checkbox" name="check-all"
                            :value="tdata.EmployeeCode">
                    </td>
                    <td v-for="td in Object.keys(headers)" :key="td.code">
                        {{ tdata[td]}}
                    </td>
                    <td class="blue-color edit-btn">
                        <v-menu transition="slide-y-transition">
                            <template #activator="{ props }">
                                <div @click="editEmpl" class="submenu-container">
                                    Sửa
                                    <span class="arrow-down-icon" v-bind="props"></span>
                                </div>
                            </template>
                            <v-list>
                                <v-list-item>
                                    <v-list-item-title>Nhân bản</v-list-item-title>
                                </v-list-item>
                                <v-list-item>
                                    <v-list-item-title>Xóa</v-list-item-title>
                                </v-list-item>
                                <v-list-item>
                                    <v-list-item-title>Ngừng sử dụng</v-list-item-title>
                                </v-list-item>
                            </v-list>
                        </v-menu>
                    </td>
                </tr>
            </table>
        </div>
        <div class="table--empty" v-if="data.length < 1">
            {{ noRecordMsg }}
        </div>
        <m-loading v-if="showLoading"></m-loading>
        <div class="pagination">
            <div class="total">Tổng số: <strong>{{ data.length }}</strong> bản ghi</div>
            <v-menu transition="slide-y-transition">
                <template #activator="{ props }">
                    <div class="row-per-page" v-bind="props">
                        <span class="value">Số bản ghi/trang: <span class="number">{{ recordPerPage
                        }}</span></span>
                        <span class="icon triangle-down"></span>
                    </div>
                </template>
                <v-list @update:selected="changeRecordPerPage">
                    <v-list-item :value="10">
                        <v-list-item-title :value="10">10 bản ghi/trang</v-list-item-title>
                    </v-list-item>
                    <v-list-item :value="20">
                        <v-list-item-title>20 bản ghi/trang</v-list-item-title>
                    </v-list-item>
                    <v-list-item :value="30">
                        <v-list-item-title>30 bản ghi/trang</v-list-item-title>
                    </v-list-item>
                    <v-list-item :value="50">
                        <v-list-item-title>50 bản ghi/trang</v-list-item-title>
                    </v-list-item>
                    <v-list-item :value="100">
                        <v-list-item-title>100 bản ghi/trang</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-menu>
            <div class="current-page">
                <strong>{{ (currentPage - 1) * recordPerPage + 1 }}</strong> -
                <strong>{{ (currentPage - 1) * recordPerPage + data.length }}</strong> bản ghi
            </div>
            <div class="page-container">
                <span @click="prevPage" class="icon left-arrow-icon" :class="{ 'disabled': currentPage === 1 }"></span>
                <span @click="nextPage" class="icon right-arrow-icon"
                    :class="{ 'disabled': currentPage === Math.ceil(totalRecords / recordPerPage) }"></span>
            </div>
        </div>
    </section>
</template>

<script>

export default {
    name: "m-table",
    props: ["searchPlaceHolder", "showLoading", "data", "totalRecords", "headers"],
    methods: {
        /**
         * đổi số bản ghi trên trang
         * Author: dktuan (4/09/2023)
         */
        changeRecordPerPage(e) {
            this.recordPerPage = e[0]
        },
        /**
         * chuyển trang kế
         * Author: dktuan (4/09/2023)
         */
        nextPage() {
            this.currentPage += 1;
            this.currentPage = Math.min(Math.ceil(this.totalRecords / this.recordPerPage), this.currentPage)
            this.$emit('loadNewPage', this.recordPerPage, this.currentPage)
        },
        /**
         * chuyển trang trước
         * Author: dktuan (4/09/2023)
         */
        prevPage() {
            this.currentPage -= 1;
            this.currentPage = Math.max(1, this.currentPage)
            this.$emit('loadNewPage', this.recordPerPage, this.currentPage)
        },
        /**
         * xử lý chọn checkbox
         * Author: dktuan (4/09/2023)
         */
        handleCheck(e) {
            if (this.checkList.includes(e.target.value)) {
                this.checkList = this.checkList.filter(val => val !== e.target.value)
            }
            else this.checkList = [...this.checkList, e.target.value]
        },
        /**
         * bỏ chọn checkbox
         * Author: dktuan (4/09/2023)
         */
        removeCheck() {
            this.checkList = []
            this.$refs.check.forEach(element => {
                element.checked = false
            });
        },
        /**
         * chọn tất cả checkbox
         * Author: dktuan (4/09/2023)
         */
        setCheckAll() {
            if (this.$refs.checkAll.checked === true) {
                this.checkList = this.data.map(e => e.EmployeeCode)
            } else {
                this.checkList = []
            }
            // if(this.$refs.checkAll.checked) {}
            this.$refs.check.forEach(element => {
                element.checked = this.$refs.checkAll.checked
            });
        },
        /**
         * emit cho component cha làm mới các bản ghi
         * Author: dktuan (19/08/2023)
         */
        refreshRecords() {
            this.$emit("refreshRecords")
        }

    },
    data() {
        return {
            recordPerPage: 10,
            currentPage: 1,
            checkList: [],
            noRecordMsg: this.$message[this.$lang].table.NO_RECORD
        }
    },
    watch: {
        checkList(newVal) {
            if (newVal.length < this.data.length) {
                this.$refs.checkAll.checked = false
            } else {
                this.$refs.checkAll.checked = true
            }
        }
    },
}
</script>

<style scoped>
@import url(./table.css);
</style>