/**
 * chuyển đổi chuỗi định dạng format: 'dd/mm/yyyy' thành Date object.
 * @param {string} string 
 * @returns Date object
 * create by: TTANH - 18/08/2023
 * modified by: TTANH - 18/08/2023
 */
export const stringToDate = (string) => {
    if (!string) return ''
    const [day, month, year] = string.split('/').map(Number);
    const convertedDate = new Date(year, month - 1, day);
    return convertedDate;
};