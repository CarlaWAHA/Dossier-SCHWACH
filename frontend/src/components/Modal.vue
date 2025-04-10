<!-- frontend/src/components/Modal.vue -->
<template>
  <div
    v-if="show"
    class="fixed inset-0 bg-black bg-opacity-60 flex items-center justify-center z-50"
    @click.self="close"
  >
    <div class="bg-white p-6 rounded-xl w-full max-w-lg shadow-lg relative">
      <button
        @click="close"
        class="absolute top-2 right-2 text-red-600 font-bold text-xl"
      >
        &times;
      </button>
      <slot />
    </div>
  </div>
</template>

<script setup>
import { defineProps, defineEmits } from 'vue'

const props = defineProps({
  show: Boolean,
})
const emit = defineEmits(['close'])

function close() {
  emit('close')
}
</script>
