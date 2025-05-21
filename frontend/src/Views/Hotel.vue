<template>
  <div class="w-screen h-screen overflow-hidden">
    <button
      v-if="!started"
      @click="startScenes"
      class="absolute top-4 left-1/2 transform -translate-x-1/2 bg-blue-600 text-white px-4 py-2 rounded z-10"
    >
      Dirigez-vous vers votre chambre
    </button>
<div
  class="absolute bottom-10 w-full bg-black bg-opacity-70 text-white text-center py-6 px-6 text-lg sm:text-xl font-mono leading-relaxed"
>
  {{ currentSceneData.text }}
</div>

    <div v-if="started" class="w-full h-full">
      <div
        v-if="currentSceneData"
        @click="nextScene"
        class="w-full h-full relative cursor-pointer"
      >
        <img
          :src="currentSceneData.img"
          :alt="currentSceneData.text"
          class="w-full h-full object-cover"
        />
        <div
          class="absolute bottom-0 w-full bg-black bg-opacity-70 text-white text-center py-4 px-4 text-base sm:text-lg leading-relaxed font-mono"
        >
          {{ currentSceneData.text }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      started: false,
      currentScene: 0,
      scenes: [
        {
          img: '/hotel/images/face_ascenseur1.jpg',
          sound: '',
          text: "C'était une longue route je vais me reposer dans ma chambre"
        },
        {
          img: '/hotel/images/face_ascenseur2.jpg',
          sound: '/hotel/sons/elevatordoor.wav',
          text: "Cet ascenseur m'a jamais inspiré confiance..."
        },
        {
          img: '/hotel/images/face_ascenseur3.jpg',
          sound: '/hotel/sons/entree.mp3',
          text: "Il faut que j'aille au 1er étage"
        },
        {
          img: '/hotel/images/panneau_ascenseur1.jpg',
          sound: '',
          text: "Je devrai vivement"
        },
        {
          img: '/hotel/images/panneau_ascenseur2.jpg',
          sound: '',
          text: "Il me semble avoir laissé des médicaments pour mes trouble"
        },
        {
          img: '/hotel/images/ascenseur_ferme.jpg',
          sound: '',
          text: "Je les ai laissés dans le tiroir de la table de nuit"
        },
        {
          img: '/hotel/images/porte.jpg',
          sound: '',
          text: "C'est étrange que toutes les affaires de ménage ont étés laissé"
        },
        {
          img: '/hotel/images/poignee.jpg',
          sound: '',
          text: "..."
        },
        {
          img: '/hotel/images/vue_meuble.jpg',
          sound: '',
          text: ""
        },
        {
          img: '/hotel/images/meuble_ouvert.jpg',
          sound: '',
          text: "Mes cachets ne sont plus là"
        },
        {
          img: '/hotel/images/vue_couloir.jpg',
          sound: '',
          text: "Tiens ? La femme de ménage habituelle n'est pas là ? Comment elle s'appelait déjà..."
        }
      ]
    }
  },
  computed: {
    currentSceneData() {
      return this.scenes[this.currentScene]
    }
  },
  methods: {
    startScenes() {
      this.started = true
      this.playSound(this.currentSceneData.sound)
    },
    nextScene() {
      if (this.currentScene < this.scenes.length - 1) {
        this.currentScene++
        this.playSound(this.currentSceneData.sound)
      }
    },
    playSound(soundPath) {
      if (soundPath) {
        const audio = new Audio(soundPath)
        audio.play()
      }
    }
  }
}
</script>

<style scoped>
html,
body {
  margin: 0;
  padding: 0;
  height: 100%;
}
</style>
