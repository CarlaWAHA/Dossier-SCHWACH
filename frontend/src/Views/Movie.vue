<template>
  <div class="w-full min-h-screen px-2 pt-6 pb-44 text-gray-900 bg-gray-70 font-inter">
    <h1 class="text-3xl font-bold mb-6 text-center">Dossier Schwach</h1>

    <div class="flex flex-col md:flex-row items-center justify-center gap-8 mb-10 w-full">
      <div class="w-full md:w-[55vw] h-[500px]">
        <iframe :src="movie.trailerUrl" title="Film complet" class="w-full h-full rounded shadow-xl" allowfullscreen></iframe>
      </div>
      <div class="w-full md:w-[30vw] h-[500px]">
        <img :src="movie.posterUrl" :alt="movie.title" class="w-full h-full object-cover shadow-xl" />
      </div>
    </div>

    <div class="flex justify-center items-center gap-10 text-lg mb-8">
      <button @click="toggleLike" class="flex items-center gap-2 transition" :class="userLiked ? 'text-blue-600' : 'text-gray-700 hover:text-blue-600'">
        <span class="text-2xl">👍</span> <span class="font-semibold">{{ likeCount }}</span>
      </button>
      <button @click="toggleDislike" class="flex items-center gap-2 transition" :class="userDisliked ? 'text-red-600' : 'text-gray-700 hover:text-red-600'">
        <span class="text-2xl">👎</span> <span class="font-semibold">{{ dislikeCount }}</span>
      </button>
      <div class="flex items-center gap-2 text-gray-700">
        <span class="text-2xl">👁️</span> <span class="font-semibold">{{ views }}</span>
      </div>
    </div>

    <p class="text-lg text-center text-gray-700 mb-10">Dans un hotel, des jeunes personnes vont vivre des phénomènes étranges.</p>

    <h2 class="text-2xl font-semibold mb-2">Acteurs</h2>
    <ul class="list-disc ml-6 mb-6">
      <li v-for="actor in movie.actors" :key="actor.id" class="text-blue-600 hover:underline cursor-pointer" @click="selectedActor = actor">{{ actor.name }}</li>
    </ul>

    <ActorDetails :actor="selectedActor" @close="selectedActor = null" class="mb-6" />

    <div class="mb-12">
      <h2 class="text-2xl font-semibold mb-4">Commentaires ({{ topLevelComments.length }})</h2>
      <div v-if="isLoggedIn" class="flex flex-col md:flex-row gap-2 mb-6">
        <input v-model="newComment.author" placeholder="Votre nom" class="border border-gray-300 px-4 py-2 rounded w-full md:w-1/4" readonly />
        <textarea v-model="newComment.content" placeholder="Votre commentaire" class="border border-gray-300 px-4 py-2 rounded w-full md:flex-1 resize-none" rows="3"></textarea>
        <button @click="submitComment" class="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 transition">Envoyer</button>
      </div>
      <p v-else class="text-red-600 font-semibold mb-4">Connectez-vous pour commenter ce film.</p>

      <div v-for="comment in topLevelComments" :key="comment.id" class="mb-6">
        <div class="flex items-start">
          <div class="w-10 h-10 rounded-full bg-blue-600 text-white flex items-center justify-center font-bold text-lg mr-4">
            {{ comment.author.charAt(0).toUpperCase() }}
          </div>
          <div class="bg-gray-100 rounded-xl px-4 py-3 shadow-sm w-full">
            <div class="flex justify-between items-center mb-1">
              <span class="font-semibold text-gray-800">{{ comment.author }}</span>
              <span class="text-sm text-gray-500">{{ new Date(comment.createdAt).toLocaleString() }}</span>
            </div>
            <p class="text-gray-700 leading-relaxed">{{ comment.content }}</p>
            <button v-if="isLoggedIn" class="text-blue-600 text-sm mt-2 hover:underline" @click="replyToComment(comment.id)">
              {{ replyingTo === comment.id ? 'Annuler' : 'Répondre' }}
            </button>
            <div v-if="isLoggedIn && replyingTo === comment.id" class="mt-4 space-y-2">
              <input v-model="newComment.author" placeholder="Votre nom" class="border border-gray-300 px-4 py-1 rounded w-1/2" readonly />
              <textarea v-model="newComment.content" placeholder="Votre réponse" class="border border-gray-300 px-4 py-2 rounded w-full resize-none" rows="2"></textarea>
              <button @click="submitComment" class="bg-blue-600 text-white px-4 py-1 rounded hover:bg-blue-700">Envoyer la réponse</button>
            </div>
            <div v-for="reply in getReplies(comment.id)" :key="reply.id" class="ml-6 mt-4 pl-4 border-l-2 border-gray-200">
              <strong>{{ reply.author }}</strong>
              <p class="text-gray-700">{{ reply.content }}</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue'
import { useRoute } from 'vue-router'
import axios from 'axios'
import ActorDetails from '@/components/ActorDetails.vue'

const route = useRoute()
const movie = ref({})
const averageRating = ref(0)
const selectedActor = ref(null)
const likeCount = ref(0)
const dislikeCount = ref(0)
const views = ref(0)
const userLiked = ref(false)
const userDisliked = ref(false)
const replyingTo = ref(null)

const isLoggedIn = ref(!!localStorage.getItem('token'))
const username = ref(localStorage.getItem('username') || '')

const newComment = ref({ author: username.value, content: '' })

const topLevelComments = computed(() => (movie.value.comments || []).filter(comment => !comment.parentId))
const getReplies = (parentId) => (movie.value.comments || []).filter(comment => comment.parentId === parentId)

onMounted(async () => {
  await fetchMovie()
  await incrementView()
})

const fetchMovie = async () => {
  const res = await axios.get(`/api/movies/${route.params.id}`)
  movie.value = res.data
  likeCount.value = res.data.likes || 0
  dislikeCount.value = res.data.dislikes || 0
  views.value = res.data.views || 0

  const ratings = res.data.ratings || []
  if (ratings.length > 0) {
    averageRating.value = (ratings.reduce((sum, r) => sum + r.score, 0) / ratings.length).toFixed(1)
  }
}

const toggleLike = async () => {
  if (userLiked.value) {
    likeCount.value--
    userLiked.value = false
    await axios.post(`/api/movies/${route.params.id}/unlike`)
  } else {
    if (userDisliked.value) {
      dislikeCount.value--
      userDisliked.value = false
      await axios.post(`/api/movies/${route.params.id}/undislike`)
    }
    likeCount.value++
    userLiked.value = true
    await axios.post(`/api/movies/${route.params.id}/like`)
  }
}

const toggleDislike = async () => {
  if (userDisliked.value) {
    dislikeCount.value--
    userDisliked.value = false
    await axios.post(`/api/movies/${route.params.id}/undislike`)
  } else {
    if (userLiked.value) {
      likeCount.value--
      userLiked.value = false
      await axios.post(`/api/movies/${route.params.id}/unlike`)
    }
    dislikeCount.value++
    userDisliked.value = true
    await axios.post(`/api/movies/${route.params.id}/dislike`)
  }
}

const incrementView = async () => {
  const res = await axios.post(`/api/movies/${route.params.id}/view`)
  views.value = res.data.views
}

const replyToComment = (id) => {
  replyingTo.value = replyingTo.value === id ? null : id
}

const submitComment = async () => {
  if (!newComment.value.author || !newComment.value.content) {
    alert('Veuillez remplir les deux champs.')
    return
  }

  const payload = { ...newComment.value, parentId: replyingTo.value }
  await axios.post(`/api/movies/${route.params.id}/comments`, payload)
  await fetchMovie()
  newComment.value = { author: username.value, content: '' }
  replyingTo.value = null
}
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Inter:wght@400;600;700&display=swap');
.font-inter {
  font-family: 'Inter', sans-serif;
}
</style>
