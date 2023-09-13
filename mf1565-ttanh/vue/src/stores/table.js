import { defineStore } from 'pinia';
import { computed, ref } from 'vue';

import * as EmployeeDataService from '@/services/employeeDataService';

/**
 * tạo store quản lý trạng thái nhân viên
 * create by: TTANH - 04/09/2023
 * modified by: TTANH - 05/09/2023
 */
export const useTableStore = defineStore('tableState', () => {
    const INIT_RECORD_PER_PAGE = 20;
    const INIT_CURRENT_PAGE = 1;
    const INIT_OFFSET_VALUE = 0;

    // số bản ghi mỗi trang
    const recordPerPage = ref(INIT_RECORD_PER_PAGE);

    // trang hiện tại
    const currentPage = ref(INIT_CURRENT_PAGE);

    // hàng bắt đầu truy xuất
    const offsetValue = ref(INIT_OFFSET_VALUE)

    // tổng số bản ghi
    const totalRecord = ref(0);

    // tổng số bản ghi
    const getTotalRecord = async () => {
        const response = await EmployeeDataService.getAll()
        totalRecord.value = response.data.result.length
    }

    // số trang
    const totalPages = computed(() => {
        return Math.ceil(totalRecord.value / recordPerPage.value)
    })

    /**
     * đi đến trang trước
     * created by: TTANH - 04/09/2023
     * modified by: TTANH - 05/09/2023 
     */
    const toPreviousPage = () => {
        currentPage.value--;
        offsetValue.value = (currentPage.value - 1) * recordPerPage.value
        console.log(`page: ${currentPage.value} - offset: ${offsetValue.value}`);
    }

    /**
     * đi đến trang kế tiếp
     * created by: TTANH - 04/09/2023
     * modified by: TTANH - 05/09/2023
    */
    const toNextPage = () => {
        currentPage.value++;
        offsetValue.value = (currentPage.value - 1) * recordPerPage.value
        console.log(`page: ${currentPage.value} - offset: ${offsetValue.value}`);
    }

    /**
     * đi đến trang bất kỳ
     * @param {int} page 
     * created by: TTANH - 06/09/2023
     */
    const toPage = (page) => {
        currentPage.value = page;
        offsetValue.value = (currentPage.value - 1) * recordPerPage.value
        console.log(`page: ${currentPage.value} - offset: ${offsetValue.value}`);
    }

    return {
        INIT_RECORD_PER_PAGE,
        INIT_CURRENT_PAGE,
        INIT_OFFSET_VALUE,
        totalRecord,
        recordPerPage,
        currentPage,
        totalPages,
        offsetValue,
        getTotalRecord,
        toPage,
        toNextPage,
        toPreviousPage
    };
});
