<template>
  <div class="p-6 max-w-xl mx-auto">
    <h1 class="text-2xl font-bold mb-4">Ajouter un film</h1>
    <form @submit.prevent="submit" enctype="multipart/form-data">
      <input v-model="form.title" placeholder="Titre" class="input" />

      <textarea v-model="form.summary" placeholder="Résumé" class="input"></textarea>

      <label class="block mb-2 font-semibold">Affiche (image)</label>
      <input type="file" @change="handlePoster" accept="image/*" class="input" />

      <label class="block mb-2 font-semibold">Bande-annonce (vidéo)</label>
      <input type="file" @change="handleTrailer" accept="video/*" class="input" />

      <button type="submit" class="bg-blue-500 text-white px-4 py-2 mt-4 rounded">
        Ajouter
      </button>
    </form>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'

const form = ref({
  title: '',
  summary: '',
  posterFile: null,
  trailerFile: null
})

const handlePoster = (e) => {
  form.value.posterFile = e.target.files[0]
}

const handleTrailer = (e) => {
  form.value.trailerFile = e.target.files[0]
}

const submit = async () => {
  if (!form.value.posterFile || !form.value.trailerFile) {
    alert('Merci de sélectionner une affiche et une vidéo.')
    return
  }

  const formData = new FormData()
  formData.append('Title', form.value.title)
  formData.append('Summary', form.value.summary)
  formData.append('PosterFile', form.value.posterFile)
  formData.append('TrailerFile', form.value.trailerFile)

  try {
    await axios.post('http://localhost:5232/api/movies/upload', formData, {
      headers: { 'Content-Type': 'multipart/form-data' }
    })

    alert('Film ajouté avec succès 🎉')
    form.value = { title: '', summary: '', posterFile: null, trailerFile: null }
  } catch (error) {
    console.error('Erreur lors de l’ajout du film :', error)
    alert('Erreur lors de l’ajout.')
  }
}
</script>

<style scoped>
.input {
  @apply block mb-4 px-4 py-2 w-full border border-gray-300 rounded focus:outline-none focus:ring-2 focus:ring-blue-400;
}
button {
  @apply bg-blue-600 text-white font-semibold px-4 py-2 rounded transition;
}
button:hover {
  @apply bg-blue-700;
}
</style>
