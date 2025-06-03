<template>
  <div class="font-inter w-full min-h-screen bg-transparent">
    <!-- NAVBAR -->
    <div
      :class="['scroll-header', isScrolled ? 'bg-black' : 'bg-transparent']"
      class="fixed top-0 left-0 w-full z-50 flex justify-between items-center px-6 py-4 text-white transition-all duration-300"
    >
      <div class="text-2xl font-extrabold tracking-widest text-black">DOSSIER SCHWACH</div>
      <div class="flex gap-6 items-center text-lg ">
        <router-link to="/" class="hover:underline text-white">Accueil</router-link>
        <button @click="goToHotel" class="hover:underline text-white">PointAndClick</button>
        <router-link to="/anubis" class="hover:underline text-white">Expérience Anubis</router-link>
        <button v-if="showAdminButton" @click="goToAdmin" class="hover:underline text-red-400 text-white">Admin</button>
      </div>
      <div class="relative">
        <div v-if="isLoggedIn" class="group cursor-pointer">
          <div class="flex items-center gap-2">
            <span>{{ connectedUser }}</span>
          </div>
          <div class="absolute right-0 mt-2 hidden group-hover:block bg-black text-white text-sm py-2 px-4 rounded shadow-lg z-50">
            <ul class="space-y-2">
              <li><a href="#" class="hover:underline">Gérer les profils</a></li>
              <li><a href="#" class="hover:underline">Transférer un profil</a></li>
              <li><a href="#" class="hover:underline">Compte</a></li>
              <li><a href="#" class="hover:underline">Centre d’aide</a></li>
              <li><button @click="logout" class="text-red-500 hover:underline">Se déconnecter</button></li>
            </ul>
          </div>
        </div>
        <div v-else class="flex gap-4 text-lg">
          <router-link to="/login" class="hover:underline text-white">Connection</router-link>
          <router-link to="/register" class="hover:underline text-white">Inscription</router-link>
        </div>
      </div>
    </div>

    <!-- Bannière -->
    <section class="w-full">
  <img
    src="/posters/inception1.jpg"
    alt="Bannière"
    class="w-full md:h-[755px] object-cover"
  />
</section>

    <!-- Films et équipe -->
    <section class="px-6 py-2 -mt-24">
      <div class="flex flex-col items-center justify-center gap-14">
        <div
          v-for="movie in movies"
          :key="movie.id"
          class="bg-white text-black shadow-lg hover:shadow-2xl transform hover:scale-105 transition-all max-w-6xl w-full min-h-[700px] flex flex-col justify-center mx-auto px-8"
        >
          <h3 class="text-4xl font-bold text-blue-600 mb-10 text-center">Notre équipe</h3>

          <div class="grid grid-cols-2 md:grid-cols-4 gap-8 justify-items-center">
            <div v-for="person in team" :key="person.name" class="flex flex-col items-center">
              <img :src="person.image" :alt="person.name" class="w-32 h-32 rounded-full object-cover" />
              <p class="text-center mt-2 text-sm">{{ person.name }}<br />{{ person.role }}</p>
            </div>
          </div>

          <router-link
            :to="`/movie/${movie.id}`"
            class="block bg-red-600 text-white px-6 py-3 mt-8 rounded hover:bg-red-700 transition text-center mx-auto w-fit"
          >
            Voir +
          </router-link>

          <p class="italic text-lg text-gray-600 mt-6 text-center leading-relaxed px-4">
            Genre : horreur
          </p>
        </div>
      </div>
    </section>

    <!-- Footer -->
    <footer class="bg-black py-16 border-t mt-40">
      <div class="max-w-3xl mx-auto text-center px-6">
        <h2 class="text-4xl font-bold mb-8 text-white">Contact</h2>
        <div class="p-8 rounded-xl shadow-md bg-gray-900">
          <p class="mb-4 text-white text-lg"><strong>Instagram :</strong> dossier.schwach</p>
          <p class="mb-4 text-white text-lg"><strong>Email :</strong> dossier.schwach@gmail.com</p>
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
import { ref, onMounted, onBeforeUnmount } from 'vue'
import axios from 'axios'
import { useRouter } from 'vue-router'

const movies = ref([])
const showAdminButton = ref(false)
const isLoggedIn = ref(false)
const connectedUser = ref('')
const isScrolled = ref(false)
const router = useRouter()

const team = [
  { name: 'Zoe Michaely', role: 'Scénariste et Me Productrice', image: '/posters/Zoe.jpg' },
  { name: 'Victor Munsch', role: 'Acteur principal, Ingénieur son', image: '/posters/Victor.jpg' },
  { name: 'Carla', role: 'Actrice, Développeuse Web', image: '/posters/Carla.jpg' },
  { name: 'Julie', role: 'Actrice, Développeuse Web', image: '/posters/Julie.jpg' },
  { name: 'Louna', role: 'Actrice, Communication', image: '/posters/Louna.jpg' },
  { name: 'Loane', role: 'Acteur, Animateur 3D/2D', image: '/posters/Louane.jpg' },
  { name: 'Lyna', role: 'Actrice, Animatrice 3D/2D', image: '/posters/Lyna.jpg' },
  { name: 'Anas', role: 'Développeur Web', image: '/posters/Anas.jpg' },
  { name: 'Karl', role: 'Acteur, Audiovisuel', image: '/posters/Karl.jpg' },
  { name: 'Julien', role: 'Acteur, Animateur 3D/2D', image: '/posters/Julien.jpg' },
  { name: 'Loic', role: 'Acteur, Développeur Web', image: '/posters/Loic.jpg' }
]

const handleScroll = () => {
  isScrolled.value = window.scrollY > 50
}

onMounted(async () => {
  window.addEventListener('scroll', handleScroll)

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

  const myIP = await axios.get('https://api.ipify.org?format=json')
  if (myIP.data.ip === '79.85.149.201') {
    showAdminButton.value = true
  }
})

onBeforeUnmount(() => {
  window.removeEventListener('scroll', handleScroll)
})

function goToAdmin() {
  router.push('/admin')
}

function goToHotel() {
  router.push('/hotel')
}

function logout() {
  localStorage.removeItem('token')
  localStorage.removeItem('username')
  window.location.reload()
}
</script>

<style scoped>
.scroll-header {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  z-index: 9999;
  transition: background-color 0.3s ease, color 0.3s ease;
  backdrop-filter: blur(10px);
}
</style>
