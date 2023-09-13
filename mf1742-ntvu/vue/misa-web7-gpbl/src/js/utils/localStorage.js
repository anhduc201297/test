/**
 * Lưu mã ngôn ngữ vào localStorage
 * @param {object} code - Mã ngôn ngữ cần lưu
 * @param {string} code.value
 * @param {string} code.label
 * @author NTVu - MF1742
 * @modified 6/09/2023
 */
export function setLangCode({ value, label }) {
    localStorage.setItem('langCode', JSON.stringify({ value, label }));
}

/**
 * Lấy mã ngôn ngữ từ localStorage
 * @returns {{value: string, label: string}} code - Mã ngôn ngữ đã lưu, hoặc null nếu không có mã nào được lưu
 * @author NTVu - MF1742
 * @modified 6/09/2023
 */
export function getLangCode() {
    const code = localStorage.getItem('langCode');
    if (code) {
        return JSON.parse(code);
    } else {
        return { value: 'VN', label: 'Tiếng Việt' };
    }
}
