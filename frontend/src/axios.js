import axios from 'axios'

// Base API dynamique selon environnement
axios.defaults.baseURL =
  import.meta.env.MODE === 'production'
    ? '/api'
    : 'http://localhost:5000/api'

// Ajout automatique du token dans les headers si présent
axios.interceptors.request.use(config => {
  const token = localStorage.getItem('token')
  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
})

export default axios
