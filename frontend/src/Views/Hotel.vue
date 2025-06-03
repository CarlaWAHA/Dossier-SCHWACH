<template>
  <div class="w-screen h-screen bg-black flex items-center justify-center relative overflow-hidden">
    <!-- Bouton -->
    <button
      v-if="!started"
      @click="startScenes"
      class="bg-white text-black px-6 py-3 rounded shadow-lg"
    >
      Dirigez-vous vers votre chambre
    </button>

    <!-- Scène active -->
    <div v-if="started" class="w-full h-full absolute top-0 left-0">
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
          sound: '/hotel/sons/elevatordoor.wav',
          text: "C'était une longue route je vais me reposer dans ma chambre"
        },
        {
          img: '/hotel/images/face_ascenseur2.jpg',
          sound: '',
          text: "Cet ascenseur m'a jamais inspiré confiance..."
        },
        {
          img: '/hotel/images/face_ascenseur3.jpg',
          sound: '/hotel/sons/step.wav.mp3',
          text: "Il faut que j'aille au 1er étage"
        },
        {
          img: '/hotel/images/panneau_ascenseur1.jpg',
          sound: '/hotel/sons/elevator.wav',
          text: "Je devrai vivement"
        },
        {
          img: '/hotel/images/panneau_ascenseur2.jpg',
          sound: '/hotel/sons/fin.mp3',
          text: "Il me semble avoir laissé des médicaments pour mes trouble"
        },
        {
          img: '/hotel/images/attente_ascenseur.jpg',
          sound: '',
          text: "Je les ai laissés dans le tiroir de la table de nuit"
        },
        {
          img: '/hotel/images/attente_ascenseur2.jpg',
          sound: '/hotel/sons/step.wav.mp3',
          text: "Tiens ? La femmme de ménage habituelle n'est pas là ? Comment elle s'appellait déjà..."
        },
        {
          img: '/hotel/images/porte.jpg',
          sound: '/hotel/sons/keys.wav',
          text: "C'est étrange que toutes les affaires de ménage ont étés laissées dans le couloir..."
        },
        {
          img: '/hotel/images/portevuepres.jpg',
          sound: '/hotel/sons/dooropen.wav',
          text: "..."
        },
        {
          img: '/hotel/images/vue_meuble.jpg',
          sound: '/hotel/sons/step.wav.mp3',
          text: "Je prends mes cachets et au dodo"
        },
        {
          img: '/hotel/images/meuble.jpg',
          sound: '/hotel/sons/drawer.wav',
          text: ""
        },
        {
          img: '/hotel/images/meuble_ouvert.jpg',
          sound: '/hotel/sons/lumieredisjoncte.mp3',
          text: "Mes cachets ne sont plus là"
        },
        {
          img: '/hotel/images/chambrenuit.png',
          sound: '',
          text: "Il y a quelque chose qui arrive..."
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
      .then(() => {
        console.log(" Son joué :", soundPath)
      })
      .catch((err) => {
        console.warn(" Échec lecture son :", soundPath, err)
      })
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
