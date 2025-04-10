<template>
  <div class="p-6">
    <h1 class="text-3xl font-bold mb-6">Films disponibles</h1>

    <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-6">
      <div
        v-for="movie in movies"
        :key="movie.id"
        class="bg-white rounded-xl shadow hover:shadow-lg transition duration-300"
      >
        <img
          :src="movie.posterUrl"
          :alt="movie.title"
          class="rounded-t-xl w-full h-64 object-cover"
        />
        <div class="p-4">
          <h2 class="text-xl font-semibold mb-2">{{ movie.title }}</h2>
          <router-link
            :to="`/movie/${movie.id}`"
            class="inline-block bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 transition"
          >
            Voir +
          </router-link>

          <!-- 🎞️ Résumé -->
          <p class="italic mt-2">Résumé</p>
          <p class="text-gray-700 mb-2">
            Inception est une immersion dans l’univers complexe des rêves. Ce chef-d'œuvre de science-fiction explore la frontière entre la réalité et l'imaginaire, où chaque niveau de rêve est une énigme à résoudre. Une œuvre intellectuelle et visuellement époustouflante.
          </p>

          <!-- 👥 Équipe -->
          <p class="italic mt-4">Équipe du film</p>
          <ul class="text-gray-800 text-sm list-disc ml-4">
            <li><strong>Réalisateur :</strong> Christopher Nolan</li>
            <li><strong>Scénariste :</strong> Christopher Nolan</li>
            <li><strong>Productrice :</strong> Emma Thomas</li>
            <li><strong>Musique :</strong> Hans Zimmer</li>
            <li><strong>Directeur photo :</strong> Wally Pfister</li>
          </ul>
        </div>
      </div>
    </div>
  </div>

  <!-- Section Contact remaniée -->
  <section class="mt-12 border-t pt-6 relative" v-if="showContact">
    <h2 class="text-2xl font-bold mb-4">Contact</h2>

    <div class="bg-gray-100 p-6 rounded-xl shadow-md max-w-xl">
      <h3 class="text-xl font-semibold mb-4">📬 Coordonnées</h3>
      <p class="mb-2"><strong>Instagram :</strong> Dossier : Schwach</p>
      <p class="mb-2"><strong>Email :</strong> dossier.schwach@gmail.com</p>
      <p class="mb-4">
        👉 <a
          href="https://fr.tipeee.com/dossier-schwach-court-metrage-ydays"
          class="text-blue-600 hover:underline"
          target="_blank"
        >
          Vous pouvez nous soutenir avec un don
        </a>
      </p>
    </div>
  </section>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'

const movies = ref([])
const showContact = ref(true)

onMounted(async () => {
  const res = await axios.get('/api/movies')
  movies.value = res.data
})
</script>
