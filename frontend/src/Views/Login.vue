<template>
  <div class="min-h-screen flex items-center justify-center bg-white">
    <div class="w-full max-w-md p-8 border rounded shadow-sm">
      <h1 class="text-2xl font-bold text-center mb-6">Bienvenue à nouveau</h1>

      <input
        v-model="username"
        placeholder="Adresse e-mail"
        class="input-style"
      />
      <input
        v-model="password"
        type="password"
        placeholder="Mot de passe"
        class="input-style"
      />
      <button @click="login" class="btn-style">Continuer</button>

      <p class="text-sm text-center mt-4">
        Vous n’avez pas encore de compte ?
        <router-link to="/register" class="text-green-600 hover:underline">Inscription</router-link>
      </p>

      <div class="my-4 border-t text-center relative">
        <span class="absolute bg-white px-2 -top-3 left-1/2 transform -translate-x-1/2 text-sm text-gray-500">ou</span>
      </div>

      <div class="space-y-2">
        <button class="oauth-button">Continuer avec Google</button>
        <button class="oauth-button">Continuer avec un compte Microsoft</button>
        <button class="oauth-button">Continuer avec Apple</button>
        <button class="oauth-button">Continuer avec un numéro de téléphone</button>
      </div>

      <p class="text-red-500 text-sm mt-4 text-center">{{ error }}</p>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'

const username = ref('')
const password = ref('')
const error = ref('')
const router = useRouter()

const login = async () => {
  try {
    const res = await axios.post('http://localhost:5000/api/auth/login', {
      username: username.value,
      password: password.value
    })
    localStorage.setItem('token', res.data.token)
    localStorage.setItem('username', username.value)
    router.push('/')
  } catch (err) {
    error.value = err.response?.data || 'Erreur serveur'
  }
}
</script>

<style scoped>
.input-style {
  display: block;
  width: 100%;
  padding: 12px;
  border: 1px solid #ccc;
  border-radius: 6px;
  margin-bottom: 16px;
  font-size: 14px;
}

.btn-style {
  width: 100%;
  background-color: #10b981;
  color: white;
  padding: 12px;
  border-radius: 6px;
  font-weight: 600;
  margin-bottom: 12px;
  transition: background-color 0.3s;
}
.btn-style:hover {
  background-color: #059669;
}

.oauth-button {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 6px;
  font-size: 14px;
  background-color: white;
  cursor: pointer;
  transition: background-color 0.3s;
}
.oauth-button:hover {
  background-color: #f3f4f6;
}
</style>
