<template>
  <div class="anubis-detail px-6 py-20 text-white font-comic">
    <router-link to="/anubis" class="text-red-500 hover:underline">Retour</router-link>

    <div v-if="entity" class="mt-8 max-w-4xl mx-auto bg-gray-900 p-8 rounded shadow-xl border border-red-600">
      <h1 class="text-4xl font-bold text-red-500 mb-4">{{ entity.title }}</h1>
      <p class="mb-2"><strong>Nom attribué :</strong> {{ entity.name }}</p>
      <p class="mb-2"><strong>Classification :</strong> {{ entity.classification }}</p>
      <p class="mb-6"><strong>Niveau de danger :</strong> {{ entity.danger }}</p>
      <p v-for="(paragraph, index) in entity.description" :key="index" class="mb-4">{{ paragraph }}</p>
    </div>

    <p v-else class="text-center text-red-500">Entité introuvable.</p>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const entity = ref(null)

const data = [
  {
    id: '0000',
    title: 'IS-0000',
    name: 'Celui qui précédait tout',
    classification: 'Inclassable',
    danger: 'Inclassable',
    description: [
      'Informations restreintes. Ce contenu est considéré comme hautement sensible.',
      'Les origines demeurent inconnues. Plusieurs théories parlent d'une proto-conscience.',
      'L'entité échappe à toute forme de détection, d'observation ou de définition.'
    ]
  },
  {
    id: '0023',
    title: 'IS-0023',
    name: 'Parasomnia',
    classification: 'PR-8',
    danger: 'Élevé',
    description: [
      'Capacité de contrôle mental à distance.',
      'Confinement réalisé grâce à BALMUNG et ASCALON.',
      'Maintenu dans une chambre bloquant les émissions mentales.'
    ]
  },
  // ➕ Ajoute ici les autres entités comme IS-0039, IS-0113, etc.
]

onMounted(() => {
  const id = route.params.id.replace('IS-', '')
  entity.value = data.find(e => e.id === id)
})
</script>

<style scoped>
.anubis-detail {
  min-height: 100vh;
  background-color: #000;
}
.font-comic {
  font-family: 'Comic Neue', cursive;
}
</style>
