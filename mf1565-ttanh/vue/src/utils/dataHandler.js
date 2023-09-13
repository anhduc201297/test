import exportFromJSON from "export-from-json";
import { Gender } from './enums';

/**
 * hiển thị giới tính phụ thuộc vào genderNumber
 * (0 là Nam, 1 là Nữ, 2 là Khác, các giá trị khác là không hợp lệ)
 * @param {int} genderNumber 
 * @returns 
 * create by: TTANH - 19/08/2023
 * modified by: TTANH - 20/08/2023
 */
export const getGender = (genderNumber) => {
    if (genderNumber === null) return '';
    switch (genderNumber) {
        case Gender.Male:
            return 'Nam';
        case Gender.Female:
            return 'Nữ';
        case Gender.Other:
            return 'Khác';
        default:
            return 'Không hợp lệ';
    }
};

/**
 * định dạng chuỗi ngày tháng năm theo format: 'dd/mm/yyyy'
 * @param {string} date 
 * @returns 
 * create by: TTANH - 18/08/2023
 * modified by: TTANH - 19/08/2023
 */
export const getDate = (date) => {
    if (date === '0001-01-01T00:00:00') return ''

    const inputDate = date ? new Date(date) : new Date();
    const day = inputDate.getDate().toString().padStart(2, '0');
    const month = (inputDate.getMonth() + 1).toString().padStart(2, '0');
    const year = inputDate.getFullYear().toString();

    const formattedDate = `${day}/${month}/${year}`;
    return formattedDate;
};

/**
 * Xuất dữ liệu từ JSON
 * @param {object} data 
 * @param {string} newFileName 
 * @param {string} fileExportType loại file muốn export (mặc định là .xls)
 * @returns dữ liệu đã được chuyển đổi
 * create by: TTANH - 07/09/2023
 */
export const exportDataFromJSON = (data, newFileName, fileExportType) => {
    if (!data) return;

    try {
        const fileName = newFileName || "exported-data";
        const exportType = exportFromJSON.types[fileExportType || "xls"];
        exportFromJSON({ data, fileName, exportType });
    } catch (err) {
        throw new Error("Parsing failed!");
    }

    return {
        exportDataFromJSON
    };
}