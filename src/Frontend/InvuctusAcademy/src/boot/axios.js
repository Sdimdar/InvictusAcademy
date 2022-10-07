import { boot } from 'quasar/wrappers'
import axios from 'axios'

// Be careful when using SSR for cross-request state pollution
// due to creating a Singleton instance here;
// If any client changes this (global) instance, it might be a
// good idea to move this instance creation inside of the
// "export default () => {}" function below (which runs individually
// for each client)
const api = axios.create({
  baseURL: "https://localhost:7210",
  timeout: 30000,
});

api.defaults.headers.common["Content-Type"] = "application/json";
api.defaults.withCredentials = true;

function errorHandler(error) {
  if (error.response?.status === 401) {
    localStorage.removeItem("ticket");
    window.location = "/";
  }
  return Promise.reject(error);
}

api.interceptors.request.use((config) => {
  return config;
}, errorHandler);

api.interceptors.response.use((response) => {
  return response;
}, errorHandler);

export default boot(({ app }) => {
  // for use inside Vue files (Options API) through this.$axios and this.$api

  app.config.globalProperties.$axios = axios
  // ^ ^ ^ this will allow you to use this.$axios (for Vue Options API form)
  //       so you won't necessarily have to import axios in each vue file

  app.config.globalProperties.$api = api
  // ^ ^ ^ this will allow you to use this.$api (for Vue Options API form)
  //       so you can easily perform requests against your app's API
})

export { api }

export const fetchUserData = (email) => api.get('/User/GetUserData', { params: { email: email } });
export const fetchUsersData = (filterString, pageSize, page) => api.get('/User/GetUsersData', { params:{ filterString: filterString, pageSize: pageSize, page: page } });
export const login = (payload) => api.post("/User/Login", payload);
export const register = (payload) => api.post("/User/Register", payload);
export const fetchLoginedUserData = () => api.get('/User/GetLoginedUserData');
export const fetchlogout = () => api.post("/User/Logout");
export const createRequest = (payload) => api.post('/Request/Create', payload);
export const fetchAllRequest = () => api.get('/Request/GetAll');