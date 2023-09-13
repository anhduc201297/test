import axios from 'axios';
import Resource from '../MResource';
import { getLangCode } from './localStorage';
/**
 * Khởi tạo cách truyền và xử lí Rest-API
 * @Author NTVu - MF1742 - 23/08/2023
 * @param {import('axios').CreateAxiosDefaults} config
 * @param {{auth: boolean, silent: boolean}} param2
 * @returns {import('axios').AxiosInstance}
 */
export const createApiInstance = (config, { auth = true, silent } = {}) => {
    const api = axios.create(config);

    api.interceptors.request.use(
        (config) => {
            if (auth && config?.headers) {
                //     config.headers['Authorization'] = 'Bearer ' + getAccessToken();
            }
            return config;
        },
        (error) => {
            Promise.reject(error);
        }
    );
    api.interceptors.response.use(
        (response) => response,
        /**
         * Xử lí các trường hợp lỗi
         * @author NTVu - 06/09/2023
         * @param {import('axios').AxiosError} error
         * @returns {{message: string, data: object}} data
         */
        (error) => {
            if (!silent) {
                console.log(error);
            }

            const { value } = getLangCode();

            if (!error.response) {
                return Promise.reject({
                    message: Resource[value].ErrorText,
                    data: null
                });
            }
            const { status } = error.response;
            let { userMsg } = error.response.data;

            switch (status) {
                case 400: {
                    if (!userMsg) {
                        userMsg = Resource[value].DataNotValid;
                    }
                    break;
                }

                case 401: {
                    if (!userMsg) {
                        userMsg = Resource[value].Unauthorized;
                    }
                    break;
                }
                case 403: {
                    if (!userMsg) {
                        userMsg = Resource[value].Forbidden;
                    }
                    break;
                }
                case 404: {
                    if (!userMsg) {
                        userMsg = Resource[value].NotFound;
                    }
                    break;
                }
                case 405: {
                    if (!userMsg) {
                        userMsg = Resource[value].MethodNotAllowed;
                    }
                    break;
                }
                case 409: {
                    if (!userMsg) {
                        userMsg = Resource[value].ErrorConflict;
                    }
                    break;
                }
                case 415: {
                    if (!userMsg) {
                        userMsg = Resource[value].UnsupportedMediaType;
                    }
                    break;
                }
                case 500: {
                    if (!userMsg) {
                        userMsg = Resource[value].ErrorText;
                    }
                    break;
                }
                default:
                    userMsg = Resource[value].ErrorText;
            }
            return Promise.reject({
                message: userMsg,
                data: error.response.data?.data
            });
        }
    );

    return api;
};
/**
 * @Author NTVu - MF1742 - 23/08/2023
 * @description Tạo một apiInstance với content là json
 */
export const api = createApiInstance(
    {
        baseURL: `${process.env.VUE_APP_API_END_POINT}/api/v1`,
        headers: {
            'Content-Type': 'application/json',
            Accept: '*/*'
        }
    },
    { auth: false }
);
