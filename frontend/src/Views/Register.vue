<template>
  <form @submit.prevent="register">
    <input v-model="username" placeholder="Nom d'utilisateur" />
    <input type="password" v-model="password" placeholder="Mot de passe" />
    <button type="submit">S'inscrire</button>
  </form>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'

const username = ref('')
const password = ref('')

const register = async () => {
  await axios.post('/api/auth/register', { username: username.value, password: password.value })
  alert('Inscription réussie')
}
</script>
