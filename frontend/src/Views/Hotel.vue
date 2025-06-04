<template>
  <div class="w-screen h-screen bg-black flex items-center justify-center relative overflow-hidden">
    <button
      v-if="!started"
      @click="startScenes"
      class="bg-white text-black px-6 py-3 rounded shadow-lg z-10"
    >
      Dirigez-vous vers votre chambre
    </button>

    <div v-if="started" class="w-full h-full absolute top-0 left-0">
      <div class="w-full h-full relative">
        <img
          :src="currentSceneData.img"
          :alt="currentSceneData.text"
          class="w-full h-full object-cover"
        />

        <div
          v-for="(zone, index) in currentSceneData.hotspots || []"
          :key="index"
          class="absolute z-20"
          :style="{
            left: zone.x + '%',
            top: zone.y + '%',
            width: zone.width + '%',
            height: zone.height + '%',
            cursor: 'pointer'
          }"
          @click="goToScene(zone.targetScene, zone.sound)"
        ></div>

        <input
          v-if="showCodeInput"
          v-model="userCode"
          @keyup.enter="checkCode"
          placeholder="écoutez les lumières"
          class="absolute bottom-[20%] left-1/2 transform -translate-x-1/2 p-2 text-xl rounded text-center z-30"
        />

        <div
          v-if="displayedText"
          class="absolute bottom-0 w-full bg-black bg-opacity-70 text-white text-center py-4 px-4 text-base sm:text-lg leading-relaxed font-mono z-20"
        >
          {{ displayedText }}
        </div>

        <div
          v-if="passwordRevealed"
          class="absolute bottom-[10%] left-1/2 transform -translate-x-1/2 text-white text-2xl bg-black bg-opacity-70 p-4 rounded z-30"
        >
          {{ revealedPassword }}
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
      displayedText: '',
      textInterval: null,
      autoTimer: null,
      showCodeInput: false,
      userCode: '',
      passwordRevealed: false,
      revealedPassword: '',
      scenes: [
        {
          img: '/hotel/images/face_ascenseur1.jpg',
          sound: '',
          text: "C'était une longue route je vais me reposer dans ma chambre",
          hotspots: [
            { x: 73, y: 70, width: 2, height: 6, targetScene: 1, sound: '/hotel/sons/elevatordoor.wav' }
          ]
        },
        {
          img: '/hotel/images/face_ascenseur2.jpg',
          sound: '',
          text: "Cet ascenseur m'a jamais inspiré confiance...",
          autoAdvance: 4000
        },
        {
          img: '/hotel/images/face_ascenseur3.jpg',
          sound: '/hotel/sons/entree.mp3',
          text: "Il faut que j'aille au 1er étage",
          hotspots: [
            { x: 46, y: 30, width: 15, height: 65, targetScene: 3, sound: '/hotel/sons/step.wav' }
          ]
        },
        {
          img: '/hotel/images/panneau_ascenseur1.jpg',
          sound: '/hotel/sons/fin.mp3',
          text: "Je devrai vivement aller me reposer",
          hotspots: [
            { x: 38.5, y: 28.5, width: 4, height: 8, targetScene: 4, sound: '/hotel/sons/elevator.wav' }
          ]
        },
        {
          img: '/hotel/images/panneau_ascenseur2.jpg',
          sound: '/hotel/sons/fin.mp3',
          text: "Il me semble avoir laissé des médicaments pour mes troubles de sommeil",
          autoAdvance: 8000
        },
        {
          img: '/hotel/images/attente_ascenseur.jpg',
          sound: '/hotel/sons/fin.mp3',
          text: "Je les ai laissés dans le tiroir de la table de nuit",
          autoAdvance: 5000
        },
        {
          img: '/hotel/images/attente_ascenseur2.jpg',
          sound: '/hotel/sons/music.wav.mp3',
          text: "Tiens ? La femmme de ménage habituelle n'est pas là ? Comment elle s'appellait déjà...",
          hotspots: [
            { x: 55, y: 30, width: 10, height: 55, targetScene: 7, sound: '/hotel/sons/step.wav' }
          ]
        },
        {
          img: '/hotel/images/porte.jpg',
          sound: '/hotel/sons/fin.mp3',
          text: "C'est étrange que toutes les affaires de ménage ont étés laissées dans le couloir...",
          hotspots: [
            { x: 28, y: 29, width: 8, height: 16, targetScene: 8, sound: '/hotel/sons/keys.wav' }
          ]
        },
        {
          img: '/hotel/images/portevuepres.jpg',
          sound: '/hotel/sons/fin.wav',
          text: "...",
          hotspots: [
            { x: 35, y: 80, width: 10, height: 20, targetScene: 9, sound: '/hotel/sons/dooropen.wav' }
          ]
        },
        {
          img: '/hotel/images/vue_meuble.jpg',
          sound: '/hotel/sons/fin.wav.mp3',
          text: "Je prends mes cachets et au dodo",
          hotspots: [
            { x: 36, y: 40, width: 10, height: 20, targetScene: 10, sound: '/hotel/sons/step.wav' }
          ]
        },
        {
          img: '/hotel/images/meuble.jpg',
          sound: '/hotel/sons/fin.wav',
          text: "",
          hotspots: [
            { x: 47, y: 70, width: 10, height: 20, targetScene: 11, sound: '/hotel/sons/drawer.wav' }
          ]
        },
        {
          img: '/hotel/images/meuble_ouvert.jpg',
          sound: '/hotel/sons/fin.mp3',
          text: "Mes cachets ne sont plus là",
          hotspots: [
            { x: 30, y: 70, width: 40, height: 20, targetScene: 12, sound: '/hotel/sons/lumieredisjoncte.mp3' }
          ]
        },
        {
          img: '/hotel/images/chambrenuit.png',
          sound: '/hotel/sons/fin.mp3',
          text: 'Il y a quelque chose qui arrive...',
          autoAdvance: 2000,
          isFinal: true
        }
      ]
    };
  },
  computed: {
    currentSceneData() {
      return this.scenes[this.currentScene];
    }
  },
  watch: {
    currentScene(newIndex) {
      this.showScene(newIndex);
    }
  },
  methods: {
    startScenes() {
      this.started = true;
      this.showScene(this.currentScene);
    },
    goToScene(index, sound) {
      this.currentScene = index;
      if (sound) this.playSound(sound);
    },
    playSound(soundPath) {
      if (soundPath) {
        const audio = new Audio(soundPath);
        audio.play().catch(err => console.warn('Erreur audio :', err));
      }
    },
    showScene(index) {
      clearInterval(this.textInterval);
      clearTimeout(this.autoTimer);
      const scene = this.scenes[index];

      this.displayedText = '';
      this.passwordRevealed = false;
      this.userCode = '';
      this.showCodeInput = false;
      this.revealedPassword = '';

      this.typeText(scene.text);

      if (scene.sound) this.playSound(scene.sound);

      if (scene.autoAdvance) {
        this.autoTimer = setTimeout(() => {
          if (scene.isFinal) {
            this.showCodeInput = true;
          } else {
            this.currentScene++;
          }
        }, scene.autoAdvance);
      }
    },
    typeText(text) {
      let i = 0;
      this.displayedText = '';
      this.textInterval = setInterval(() => {
        if (i < text.length) {
          this.displayedText += text[i++];
        } else {
          clearInterval(this.textInterval);
        }
      }, 40);
    },
    checkCode() {
  const expected = 'HIDD';
  if (this.userCode.trim().toUpperCase() === expected) {
    this.revealPassword();
  } else {
    this.revealedPassword = 'Code incorrect';
    this.passwordRevealed = true;

    // Redirection après 5 secondes aussi en cas de code incorrect
    setTimeout(() => {
      this.$router.push('/');
    }, 5000);
  }
},
    revealPassword() {
  const finalCode = atob('OVIzWDdRMg=='); // "9R3X7Q2"
  this.revealedPassword = '';
  this.passwordRevealed = true;
  let i = 0;

  const interval = setInterval(() => {
    if (i < finalCode.length) {
      this.revealedPassword += finalCode.charAt(i++);
    } else {
      clearInterval(interval);

      // Après 5 secondes, redirection vers la page d'accueil
      setTimeout(() => {
        this.$router.push('/'); // Assurez-vous que le nom de la route d'accueil est bien "home"
      }, 5000);
    }
  }, 150);
    }
  }
};
</script>

<style scoped>
html, body {
  margin: 0;
  padding: 0;
  height: 100%;
  font-family: 'Courier New', Courier, monospace;
  overflow: hidden;
}
</style>
