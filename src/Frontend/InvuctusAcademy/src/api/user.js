import api from "../requests.js";

export const fetchUserData = (id) => api.get(`/User/GetUserData/${id}`);
