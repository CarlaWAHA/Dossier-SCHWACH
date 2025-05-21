<template>
  <div class="anubis-container px-4 py-10 text-white">
    <router-link to="/anubis" class="text-blue-400 hover:underline mb-6 block">
      ← Retour aux archives
    </router-link>

    <div v-if="entity">
      <h1 class="text-4xl font-bold mb-4">{{ entity.id }} - {{ entity.name }}</h1>
      <p class="mb-2"><strong>Classification :</strong> {{ entity.classification }}</p>
      <p class="mb-2"><strong>Niveau de danger :</strong> {{ entity.danger }}</p>
      <p class="mb-6" v-for="(paragraph, i) in entity.description" :key="i">
        {{ paragraph }}
      </p>
    </div>

    <div v-else class="text-red-400">
      Entité introuvable.
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const entityId = route.params.id.toLowerCase()

const entities = [
  {
    id: 'IS-0000',
    name: 'Celui qui précédait tout',
    classification: 'Inclassable',
    danger: 'Inclassable',
    description: [
      "Informations restreintes. Ce contenu est considéré comme hautement sensible...",
      "Les origines de IS-0000 demeurent inconnues...",
      "Aucune tentative de confinement n'a jamais été couronnée de succès...",
      "Le contenu concernant IS-0000 a été bloqué."
    ]
  },
  {
    id: 'IS-0023',
    name: 'Parasomnia',
    classification: 'PR-8',
    danger: 'PR-8',
    description: [
      "IS-0023 a représenté un défi majeur lors de sa capture...",
      "Parasomnia est aujourd'hui maintenu dans une chambre isolée..."
    ]
  },
  // Ajouter d'autres entités ici
]

const entity = ref(entities.find(e => e.id.toLowerCase() === entityId))
</script>

<style scoped>
.anubis-container {
  font-family: 'Comic Neue', cursive;
  background-color: #0a0a0a;
  min-height: 100vh;
}
</style>
