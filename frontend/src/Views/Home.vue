<template>
  <div class="font-comic absolute inset-0 w-full h-full object-cover bg-transparent">
    <!-- 🎬 Bouton Admin -->
    <button
      v-if="showAdminButton"
      @click="goToAdmin"
      class="fixed top-6 right-6 bg-red-600 text-white py-2 px-4 rounded-lg hover:bg-red-700 shadow-lg z-50">
      Admin
    </button>

    <!-- 🎬 Onglets haut droite -->
    <div class="fixed top-6 right-36 flex gap-6 z-40">
      <router-link to="/reservation" class="text-white text-lg hover:scale-125 transition-transform duration-300">
        Réserver une projection
      </router-link>
      <router-link to="/equipe" class="text-white text-lg hover:scale-125 transition-transform duration-300">
        Équipe projet
      </router-link>
      <router-link v-if="!isLoggedIn" to="/login" class="text-white text-lg hover:scale-125 transition-transform duration-300">
        Connexion
      </router-link>
      <router-link v-if="!isLoggedIn" to="/register" class="text-white text-lg hover:scale-125 transition-transform duration-300">
        Inscription
      </router-link>
    </div>

    <!-- 🔐 Info Connexion -->
    <div v-if="isLoggedIn" class="fixed top-6 left-6 text-white font-bold z-50">
      ✅ Connecté : {{ connectedUser }}
    </div>
    <button
      v-if="isLoggedIn"
      @click="logout"
      class="fixed top-6 left-56 bg-red-600 text-white px-3 py-1 rounded hover:bg-red-700 z-50">
      Déconnexion
    </button>

    <!-- 🎬 Bannière principale -->
    <section>
      <img src="/posters/schwach.jpg" alt="Bannière" class="absolute inset-0 w-full md:h-[525px] object-cover" />
      <div class="absolute inset-0 flex flex-col items-center justify-center">
        <h1 class="text-white text-5xl md:text-7xl font-extrabold drop-shadow-md">
          🎬 Dossier Schwach
        </h1>
      </div>
    </section>

    <!-- 🎞️ Section Films + Équipe -->
    <section class="px-6 pt-[580px] pb-44">
      <div class="flex flex-col items-center justify-center gap-14">
        <div
          v-for="movie in movies"
          :key="movie.id"
          class="bg-white shadow-lg hover:shadow-2xl transform hover:scale-105 transition-all max-w-6xl w-full min-h-[700px] flex flex-col justify-center mx-auto px-8">
          
          <!-- Titre -->
          <h3 class="text-4xl font-bold text-blue-800 mb-10 text-center">{{ movie.title }}</h3>
          
          <!-- Photos équipe -->
          <div class="grid grid-cols-2 md:grid-cols-4 gap-8 justify-items-center">
            <div v-for="person in team" :key="person.name" class="flex flex-col items-center">
              <img :src="person.image" :alt="person.name" class="w-32 h-32 rounded-full object-cover" />
              <p class="text-center mt-2 text-sm">{{ person.name }}<br />{{ person.role }}</p>
            </div>
          </div>

          <!-- Bouton voir + -->
          <router-link
            :to="`/movie/${movie.id}`"
            class="block bg-red-600 text-white px-6 py-3 mt-8 rounded hover:bg-red-700 transition text-center mx-auto w-fit">
            Voir +
          </router-link>

          <!-- Résumé -->
          <p class="italic text-lg text-gray-600 mt-6 text-center leading-relaxed px-4">
            {{ movie.summary.substring(0, 200) }}...
          </p>
        </div>
      </div>
    </section>

    <!-- 📫 Footer Contact -->
    <footer class="bg-black py-16 border-t mt-40">
      <div class="max-w-3xl mx-auto text-center px-6">
        <h2 class="text-4xl font-bold mb-8 text-white">📫 Contact</h2>
        <div class="p-8 rounded-xl shadow-md bg-gray-900">
          <p class="mb-4 text-white text-lg">📸 <strong>Instagram :</strong> dossier.schwach</p>
          <p class="mb-4 text-white text-lg">📧 <strong>Email :</strong> dossier.schwach@gmail.com</p>
          <p class="mt-6 text-blue-400 hover:underline">
            <a href="https://fr.tipeee.com/dossier-schwach-court-metrage-ydays" target="_blank">
              Soutenir avec un don
            </a>
          </p>
        </div>
      </div>
    </footer>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'

const movies = ref([])
const showAdminButton = ref(false)
const isLoggedIn = ref(false)
const connectedUser = ref('')
const router = useRouter()

const team = [
  { name: 'Zoe Michaely', role: 'Scénariste et Me Productrice', image: '/posters/Zoe.jpg' },
  { name: 'Victor Munsch', role: 'Acteur principal, Ingénieur son', image: '/posters/Victor.jpg' },
  { name: 'Carla', role: 'Actrice, Développeuse Web', image: '/posters/Carla.jpg' },
  { name: 'Julie', role: 'Actrice, Développeuse Web', image: '/posters/Julie.jpg' },
  { name: 'Louna', role: 'Actrice, Communication', image: '/posters/Louna.jpg' },
  { name: 'Louane', role: 'Actrice, Animatrice 3D/2D', image: '/posters/Louane.jpg' },
  { name: 'Lyna', role: 'Actrice, Perchman', image: '/posters/Lyna.jpg' },
  { name: 'Anas', role: 'Développeur Web', image: '/posters/Anas.jpg' },
  { name: 'Karl', role: 'Acteur, Audiovisuel', image: '/posters/Karl.jpg' },
  { name: 'Julien', role: 'Acteur, Animateur 3D/2D', image: '/posters/Julien.jpg' }
]

onMounted(async () => {
  try {
    const res = await axios.get('/api/movies')
    movies.value = res.data
  } catch (err) {
    console.error('Erreur de récupération des films', err)
  }

  const token = localStorage.getItem('token')
  if (token) {
    isLoggedIn.value = true
    connectedUser.value = localStorage.getItem('username')
  }

  // Facultatif : bouton admin visible selon IP ou rôle (à implémenter selon ton besoin)
  const myIP = await axios.get('https://api.ipify.org?format=json')
  if (myIP.data.ip === '79.85.149.201') {
    showAdminButton.value = true
  }
})

function goToAdmin() {
  router.push('/admin')
}

function logout() {
  localStorage.removeItem('token')
  localStorage.removeItem('username')
  window.location.reload()
}
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Comic+Neue:wght@700&display=swap');
.font-comic {
  font-family: 'Comic Neue', cursive;
}
</style>
