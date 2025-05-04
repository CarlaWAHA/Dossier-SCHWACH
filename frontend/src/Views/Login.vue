<template>
  <form @submit.prevent="login" class="flex flex-col gap-4 max-w-md mx-auto mt-32">
    <input v-model="username" placeholder="Nom d'utilisateur" class="px-4 py-2 border rounded" />
    <input type="password" v-model="password" placeholder="Mot de passe" class="px-4 py-2 border rounded" />
    <button type="submit" class="bg-blue-600 text-white py-2 rounded hover:bg-blue-700">
      Connexion
    </button>
  </form>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'

const username = ref('')
const password = ref('')
const router = useRouter()

const login = async () => {
  try {
    const res = await axios.post('http://localhost:5232/api/auth/login', {
      username: username.value,
      password: password.value
    })

    localStorage.setItem('token', res.data.token)
    localStorage.setItem('username', username.value)

    alert('Connexion réussie ✅')
    router.push('/') // redirection
  } catch (err) {
    console.error('Erreur de connexion', err)
    alert('Erreur de connexion ❌')
  }
}
</script>

