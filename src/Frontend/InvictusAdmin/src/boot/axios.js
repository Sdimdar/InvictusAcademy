import { boot } from 'quasar/wrappers'
import axios from 'axios'
import qs from 'qs'

// Be careful when using SSR for cross-request state pollution
// due to creating a Singleton instance here;
// If any client changes this (global) instance, it might be a
// good idea to move this instance creation inside of the
// "export default () => {}" function below (which runs individually
// for each client)
const api = axios.create({
  baseURL: process.env.GATEWAY,
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
//admin
export const login = (payload) => api.post('/AdminPanel/Accounts/Login', payload);
export const fetchLoginedUserData = () => api.get("/AdminPanel/Accounts/GetAdminData");
export const fetchlogout = () => api.post("/AdminPanel/Accounts/LogOff");

// usersData
export const fetchUsersData = (pageNumber, pageSize) => api.get('/AdminPanel/Users/GetAllRegisteredUsers', { params:{ pageSize: pageSize, pageNumber: pageNumber } });
export const fetchUsersCount = () => api.get('/AdminPanel/Users/GetUsersCount');

// requestsData
export const fetchAllRequest = (pageNumber, pageSize) => api.get('/AdminPanel/Requests/GetAll', { params:{ pageSize: pageSize, pageNumber: pageNumber } });
export const fetchRequestsCount = () => api.get('/AdminPanel/Requests/GetRequestsCount');

export const managerComment = (payload) => api.post('/AdminPanel/Requests/ManagerComment', payload);
export const changeCalled = (payload) => api.post('/AdminPanel/Requests/ChangeCalled', payload);
export const createAdmin = (payload) => api.post('/AdminPanel/Admins/CreateAdmin', payload);

//modules
export const createModule = (payload) => api.post('/AdminPanel/Modules/Create', payload);
export const fetchAllModules = () => api.get('/AdminPanel/Modules/GetAll');
export const fetchModulesCount = () => api.get('/AdminPanel/Modules/GetModulesCount');
export const deleteModule = (payload) => api.post('/AdminPanel/Modules/Delete', payload);
export const updateModule = (payload) => api.post('/AdminPanel/Modules/Update', payload);
export const fetchModuleById = (id) => api.get(`/AdminPanel/Modules/GetById?id=${id}`);
export const fetchModuleByFilterString = (string) => api.get(`/AdminPanel/Modules/GetByFilterString?filteredString=${string}`);

//articles
export const addNewArticle = (payload) => api.post('/AdminPanel/Modules/AddArticles', payload);
export const addTest = (payload) => api.post('/AdminPanel/Modules/AddTest', payload);

//payments
export const getPaymentsByParams = (paymentData) => api.get(`/AdminPanel/Payment/GetWithParametersPayment`, {params:{UserId:paymentData.userId, CourseId:paymentData.courseId, Status:paymentData.status}})
export const confirmPaymentById = (payload)=> api.post(`/AdminPanel/Payment/Confirm`, payload);
export const rejectPayment = (payload) => api.post(`/AdminPanel/Payment/Reject`, payload)

//courses
export const createCourse = (courseData) => api.post('/AdminPanel/Courses/CreateCourse', courseData);
export const editCourse = (courseData) => api.post('/AdminPanel/Courses/EditCourse', courseData);
export const changeCourseModules = (courseModulesData) => api.post('/AdminPanel/Courses/ChangeAllModules', courseModulesData);
export const insertModules = (modulesData) => api.post('/AdminPanel/Courses/InsertModules', modulesData);
export const getAllModules = () => api.get('/AdminPanel/Modules/GetAll')
export const getAllCourses = () => api.get('/AdminPanel/Courses/GetCourses', { params: { type: 4 } })
export const getCourse = (courseId) => api.get('/AdminPanel/Courses/GetCourse', { params: { id: courseId } })
export const getCourseModulesId = (courseId) => api.get('/AdminPanel/Courses/GetCourseModulesId', { params: { CourseId: courseId } })
export const getModulesByListId = (modulesId) => api.get('/AdminPanel/Modules/GetByListOfId', {
    params: { ModulesId: modulesId },
    paramsSerializer: params => {
        return qs.stringify(params, { arrayFormat: "repeat" })
    }

});
