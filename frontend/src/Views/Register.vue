<template>
  <div class="p-10 max-w-md mx-auto">
    <h2 class="text-2xl font-bold mb-4">Inscription</h2>
    <input v-model="username" placeholder="Nom d'utilisateur" class="input" />
    <input v-model="password" type="password" placeholder="Mot de passe" class="input" />
    <button @click="register" class="btn">S'inscrire</button>
    <p class="text-red-500 mt-2">{{ error }}</p>
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
.input {
  display: block;
  width: 100%;
  padding: 8px;
  border: 1px solid #ccc;
  margin-bottom: 16px;
}
.btn {
  background-color: #2563eb;
  color: white;
  padding: 10px 16px;
  border-radius: 6px;
}
</style>
