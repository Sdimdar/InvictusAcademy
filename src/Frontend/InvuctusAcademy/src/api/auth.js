import api from "../requests";

export const login = (payload) =>
  api.post("/User/Login", payload);

export const register = (payload) =>
  api.post("/User/Register", payload);
