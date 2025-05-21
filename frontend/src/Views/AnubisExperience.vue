<template>
  <div class="anubis-wrapper">
    <header>
      <div class="logo">
        <img src="/posters/AnuB.png" alt="Logo AnuB" class="logo-img" />
      </div>
      <div class="search-bar">
        <input
          v-model="searchQuery"
          @keyup.enter="searchPage"
          placeholder="Rechercher une page..."
        />
        <button @click="searchPage">Rechercher</button>
      </div>
      <p v-if="error" class="error-message">Page introuvable</p>
    </header>

    <div class="container">
      <h1>BIENVENUE DANS ANUBIS</h1>
      <p>Félicitations, vous avez été sélectionné pour rejoindre l'Organisation ANUBIS. Ce n'est pas un emploi ordinaire.</p>
      <p>C'est un engagement total envers une cause qui dépasse l'entendement humain.</p>
      <p>Nous ne sommes pas une simple organisation de recherche. Nous sommes les gardiens de l'inexplicable.</p>
      <p>Nous protégeons le monde des phénomènes qui pourraient anéantir notre réalité.</p>
      <p>Nous explorons les mystères interdits.</p>
      <p>À partir d'aujourd'hui, votre ancienne vie n'existe plus.</p>

      <br><br />
      <p>Pour rechercher les documents, notez dans la barre de recherche :</p>
      <p><strong>HIERARCHIE</strong></p>
    </div>
  </div>
</template>

<script>
export default {
  name: 'AnubisExperience',
  data() {
    return {
      searchQuery: '',
      error: false,
      pages: {
        'hierarchie': '/hierarchie', // Utilisation de routes SPA
        'damocles': '/damocles',
        'titanomachie': '/titanomachie',
        'ragnarok': '/ragnarok',
        'osiris': '/osiris',
        'genese': '/genese',
        'nyerlatotep': '/nyerlatotep',
        'shandraha': '/shandraha',
        'longinus': '/longinus',
        'gungnir': '/gungnir',
        'balmung': '/balmung',
        'ascalone': '/ascalone',
        'durandal': '/durandal',
        'is-1989': '/is-1989',
        'is-2010': '/is-2010',
        'is-1919': '/is-1919',
        'is-1306': '/is-1306',
        'is-1033': '/is-1033',
        'is-0413': '/is-0413',
        'is-0113': '/is-0113',
        'is-0039': '/is-0039',
        'is-0023': '/is-0023',
        'is-0000': '/is-0000',
        'guide': '/guide',
        'autentification': '/autentification',
        'faussepiste': '/faussepiste',
        'necronomicon': '/necronomicon'
      }
    };
  },
  methods: {
    normalizeString(str) {
      return str.toLowerCase().normalize("NFD").replace(/\p{Diacritic}/gu, "");
    },
    searchPage() {
      const normalized = this.normalizeString(this.searchQuery.trim());
      if (this.pages[normalized]) {
        this.error = false;
        this.$router.push(this.pages[normalized]);
      } else {
        this.error = true;
      }
    }
  }
};
</script>

<style scoped>
.anubis-wrapper {
  background-color: black;
  color: red;
  font-family: 'Courier New', Courier, monospace;
  min-height: 100vh;
  padding-top: 100px;
  text-align: center;
}

header {
  position: fixed;
  top: 0;
  width: 100%;
  height: 100px;
  background-color: black;
  border-bottom: 2px solid red;
  display: flex;
  align-items: center;
  justify-content: space-around;
  z-index: 1000;
}

.logo-img {
  height: 60px;
}

.search-bar input,
.search-bar button {
  padding: 8px;
  font-size: 16px;
  border: 2px solid red;
  background-color: black;
  color: red;
  border-radius: 4px;
}

.search-bar button:hover {
  background-color: red;
  color: black;
}

.container {
  border: 2px solid red;
  padding: 40px;
  box-shadow: 0 0 20px red;
  max-width: 800px;
  margin: auto;
  margin-top: 2rem;
}

h1 {
  font-size: 2.5em;
  color: red;
  margin-bottom: 1rem;
}

p {
  font-size: 1.1rem;
  margin-bottom: 1rem;
}

.error-message {
  color: red;
  text-align: center;
}
</style>
