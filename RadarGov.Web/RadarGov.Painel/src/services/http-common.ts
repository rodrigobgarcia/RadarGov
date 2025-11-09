import axios, { AxiosInstance } from "axios";

const apiClient: AxiosInstance = axios.create({
  baseURL: "http://localhost:5000/api", 
  headers: {
    "Content-type": "application/json",
    // 'Authorization': `Bearer ${localStorage.getItem('token')}`
  },
});

export default apiClient;