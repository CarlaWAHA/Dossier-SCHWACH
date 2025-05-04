<template>
  <div v-if="actor" class="p-4 border rounded bg-gray-100 shadow-md">
    <h3 class="text-lg font-semibold text-blue-800">{{ actor.name }}</h3>
    <p class="text-gray-700 mt-2">{{ actor.bio }}</p>
    <button
      @click="$emit('close')"
      class="mt-4 text-sm text-red-600 hover:underline"
    >
      Fermer
    </button>
  </div>
</template>

<script setup>
defineProps({
  actor: Object
})
</script>
