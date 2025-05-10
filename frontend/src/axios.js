// src/axios.js
import axios from 'axios'

// 🔧 Détection dynamique de l'hôte courant (utile si frontend accède à backend via IP)
const host = window.location.hostname
const port = 5000 // Port du backend ASP.NET Core

// 🌐 Base URL complète
axios.defaults.baseURL = import.meta.env.VITE_API_BASE_URL

// 🔐 Intercepteur pour ajouter le token JWT automatiquement
axios.interceptors.request.use(config => {
  const token = localStorage.getItem('token')
  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config
}, error => {
  return Promise.reject(error)
})

// 🛡️ Optionnel : gestion des réponses d'erreur globales (401, 403, etc.)
axios.interceptors.response.use(
  response => response,
  error => {
    if (error.response?.status === 401) {
      console.warn('Non autorisé, token invalide ou expiré.')
      // Tu peux ici rediriger vers /login ou effacer le token
      localStorage.removeItem('token')
    }
    return Promise.reject(error)
  }
)

export default axios
