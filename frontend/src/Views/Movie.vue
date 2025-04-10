<template>
  <div class="p-6 max-w-4xl mx-auto text-gray-900 font-sans">
    <h1 class="text-4xl font-bold mb-4">{{ movie.title }}</h1>
    <img :src="movie.posterUrl" :alt="movie.title" class="rounded w-full h-96 object-cover mb-4" />
    <p class="text-gray-700 mb-6">{{ movie.summary }}</p>

    <!-- Vidéo locale -->
    <iframe
      v-if="movie.trailerUrl"
      :src="movie.trailerUrl"
      title="Bande-annonce de {{ movie.title }}"
      class="w-full h-72 rounded mb-6"
    ></iframe>

    <!-- Acteurs -->
    <h2 class="text-2xl font-semibold mb-2">Acteurs</h2>
    <ul class="list-disc ml-6 mb-6">
      <li
        v-for="actor in movie.actors"
        :key="actor.id"
        class="text-blue-600 hover:underline cursor-pointer"
        @click="selectedActor = actor"
      >
        {{ actor.name }}
      </li>
    </ul>

    <!-- Détails de l'acteur -->
    <ActorDetails
      :actor="selectedActor"
      @close="selectedActor = null"
      class="mb-6"
    />

    <!-- Commentaires -->
    <div class="mb-12">
      <h2 class="text-2xl font-semibold mb-4">Commentaires ({{ movie.comments?.length || 0 }})</h2>

      <!-- Formulaire -->
      <div class="flex flex-col md:flex-row gap-2 mb-6">
        <input
          v-model="newComment.author"
          placeholder="Votre nom"
          class="border border-gray-300 px-4 py-2 rounded w-full md:w-1/4"
        />
        <textarea
          v-model="newComment.content"
          placeholder="Votre commentaire"
          class="border border-gray-300 px-4 py-2 rounded w-full md:flex-1 resize-none"
          rows="3"
        ></textarea>
        <button
          @click="submitComment"
          class="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 transition"
        >
          Envoyer
        </button>
      </div>

      <!-- Liste des commentaires -->
      <div v-for="comment in movie.comments" :key="comment.id" class="flex items-start mb-6">
          <!-- Avatar -->
          <div class="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-lg mr-4">
            {{ comment.author.charAt(0).toUpperCase() }}
          </div>

            <!-- Bulle de commentaire -->
          <div class="bg-gray-100 rounded-xl px-4 py-3 shadow-sm w-full">
            <div class="flex justify-between items-center mb-1">
            <span class="font-semibold text-gray-800">{{ comment.author }}</span>
          <span class="text-sm text-gray-500">{{ new Date(comment.createdAt).toLocaleString() }}</span>
          </div>
    <p class="text-gray-700 leading-relaxed">{{ comment.content }}</p>
  </div>
</div>
    </div>

    <!-- Note -->
    <h2 class="text-2xl font-semibold">Note moyenne</h2>
    <p class="text-lg">{{ averageRating }}</p>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import axios from 'axios'
import ActorDetails from '@/components/ActorDetails.vue'

const route = useRoute()
const movie = ref({})
const averageRating = ref(0)
const selectedActor = ref(null)

const newComment = ref({
  author: '',
  content: ''
})

onMounted(async () => {
  await fetchMovie()
})

const fetchMovie = async () => {
  const res = await axios.get(`/api/movies/${route.params.id}`)
  movie.value = res.data
  const ratings = res.data.ratings || []
  if (ratings.length > 0) {
    averageRating.value = (
      ratings.reduce((sum, r) => sum + r.score, 0) / ratings.length
    ).toFixed(1)
  }
}

const submitComment = async () => {
  if (!newComment.value.author || !newComment.value.content) {
    alert('Veuillez remplir les deux champs.')
    return
  }

  await axios.post(`/api/movies/${route.params.id}/comments`, newComment.value)

  // Recharge les données du film avec les nouveaux commentaires
  await fetchMovie()

  // Reset du formulaire
  newComment.value = { author: '', content: '' }
}

</script>
