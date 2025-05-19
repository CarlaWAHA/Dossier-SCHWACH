<template>
  <div class="min-h-screen flex items-center justify-center bg-white">
    <div class="w-full max-w-md p-8 border rounded shadow-sm">
      <h1 class="text-2xl font-bold text-center mb-6">Créer un compte</h1>

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
      <button @click="register" class="btn-style">S'inscrire</button>

      <p class="text-sm text-center mt-4">
        Vous avez déjà un compte ?
        <router-link to="/login" class="text-green-600 hover:underline">Connexion</router-link>
      </p>

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

const register = async () => {
  try {
    await axios.post('http://localhost:5000/api/auth/register', {
      username: username.value,
      password: password.value
    })
    router.push('/login')
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
</style>
