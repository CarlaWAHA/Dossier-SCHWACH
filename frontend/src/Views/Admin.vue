<template>
  <div class="p-6 max-w-xl mx-auto">
    <h1 class="text-2xl font-bold mb-4">Ajouter un film</h1>
    <form @submit.prevent="submit">
      <input v-model="form.title" placeholder="Titre" class="input" />
      <input v-model="form.posterUrl" placeholder="URL de l'affiche" class="input" />
      <input v-model="form.trailerUrl" placeholder="URL de la bande-annonce" class="input" />
      <textarea v-model="form.summary" placeholder="Résumé" class="input"></textarea>
      <button type="submit" class="bg-blue-500 text-white px-4 py-2 mt-2 rounded">Ajouter</button>
    </form>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'

const form = ref({ title: '', posterUrl: '', trailerUrl: '', summary: '' })

const submit = async () => {
  await axios.post('http://localhost:5232/api/movies', form.value)
  alert('Film ajouté avec succès !')
  form.value = { title: '', posterUrl: '', trailerUrl: '', summary: '' }
}
</script>

<style scoped>
.input {
  display: block;
  margin-bottom: 10px;
  padding: 8px;
  width: 100%;
  border: 1px solid #ccc;
  border-radius: 4px;
}
</style>
