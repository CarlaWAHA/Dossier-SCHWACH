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
  }
},
    revealPassword() {
      const finalCode = atob('OVIzWDdRMg=='); // "Mot de passe de connexion : 9R3X7Q2"
      this.revealedPassword = '';
      this.passwordRevealed = true;
      let i = 0;
      const interval = setInterval(() => {
        if (i < finalCode.length) {
          this.revealedPassword += finalCode.charAt(i++);
        } else {
          clearInterval(interval);
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
