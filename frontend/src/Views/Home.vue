<template>
  <div class="font-comic absolute inset-0 w-full h-full object-cover bg-white">
    <!-- 🎬 Header Style BD -->
    <section>
      <img
        src="/posters/schwach.jpg"
        alt="Bannière"
        class="absolute inset-0 w-full md:h-[525px] object-cover"
      />
      <div class="absolute inset-0 flex items-center justify-center">
        <h1 class="text-white text-5xl md:text-7xl font-extrabold drop-shadow-md flex items-center gap-4">
          🎬 Dossier Schwach
        </h1>
      </div>
    </section>

    <!-- 🎞️ Section Films élargie et plus haute -->
    <section class="px-6 pt-[480px] pb-44">
      <div class="flex flex-col items-center justify-center gap-14">
        <div
          v-for="movie in movies"
          :key="movie.id"
          class="bg-white shadow-lg hover:shadow-2xl transform hover:scale-105 transition-all max-w-4xl w-full min-h-[700px] flex flex-col justify-center"
        >
          <div class="p-8">
            <h3 class="text-4xl font-bold text-blue-800 mb-6 text-center">Schwach</h3>
            <router-link
              :to="`/movie/${movie.id}`"
              class="block bg-red-600 text-white px-6 py-3 rounded hover:bg-red-700 transition text-center mx-auto w-fit"
            >
              Voir +
            </router-link>
            <p class="italic text-lg text-gray-600 mt-6 text-center leading-relaxed px-4">
              {{ movie.summary.substring(0, 200) }}...
            </p>
          </div>
        </div>
      </div>
    </section>

    <!-- 📫 Contact toujours bien espacé -->
    <footer class="bg-gray-300 py-12 border-t mt-40">
      <div class="max-w-xl mx-auto text-center px-4">
        <h2 class="text-3xl font-bold mb-4">📫 Contact</h2>
        <div class="bg-white p-6 rounded-xl shadow-md">
          <p class="mb-2"><strong>📸 Instagram :</strong> Dossier : Schwach</p>
          <p class="mb-2"><strong>📧 Email :</strong> dossier.schwach@gmail.com</p>
          <p class="mt-3 text-blue-600 hover:underline">
            👉
            <a
              href="https://fr.tipeee.com/dossier-schwach-court-metrage-ydays"
              target="_blank"
            >
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

const movies = ref([])

onMounted(async () => {
  const res = await axios.get('/api/movies')
  movies.value = res.data
})
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Comic+Neue:wght@700&display=swap');

.font-comic {
  font-family: 'Comic Neue', cursive;
}
</style>
