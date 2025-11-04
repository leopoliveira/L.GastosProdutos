import axios from 'axios';

// Shared axios defaults and interceptors
axios.defaults.timeout = 15000;

const http = axios;

http.interceptors.request.use((config) => {
  config.headers = {
    ...(config.headers || {}),
    'X-Requested-With': 'XMLHttpRequest',
  } as any;
  return config;
});

http.interceptors.response.use(
  (res) => res,
  (error) => {
    const normalized = {
      status: error?.response?.status ?? 0,
      message:
        error?.response?.data?.message || error?.message || 'Falha ao processar a requisição.',
      data: error?.response?.data,
    };
    return Promise.reject(normalized);
  }
);

export default http;
