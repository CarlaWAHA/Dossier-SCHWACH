<template>
  <div class="anubis-wrapper">
    <header>
      <div class="logo">
        <img src="/posters/AnuB.png" alt="Logo AnuB" class="logo-img" />
      </div>
      <div class="search-bar">
        <input v-model="searchQuery" @keyup.enter="searchPage" placeholder="Rechercher une page..." />
        <button @click="searchPage">Rechercher</button>
      </div>
      <p v-if="error" class="error-message">Page introuvable</p>
    </header>

    <div class="container">
      <h1>Hiérarchie du groupe</h1>
      <p>Grand Monarque : //////////</p>

      <h2>Conseil</h2>
      <p>Sous-Chef : ASCALON</p>
      <p>Scientifique en chef : LONGINUS</p>
      <p>Chef de la sécurité : BALMUNG</p>
      <p>Chef de Terrain : GUNGNIR</p>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Hierarchie',
  data() {
    return {
      searchQuery: '',
      error: false,
      pages: {
        repertoire: '/IS/repertoire.html',
        guide: '/Doc/Guide.html',
        index: '/Doc/Index.html',
        hierarchie: '/hierarchie',
        necronomicon: '/Doc/Necronomicon.html',
        autentification: '/Doc/Autentification.html',
        faussepiste: '/Doc/FaussePiste.html'
        // Ajoute d'autres correspondances ici si nécessaire
      }
    };
  },
  methods: {
    normalizeString(str) {
      return str.toLowerCase().normalize("NFD").replace(/\p{Diacritic}/gu, "");
    },
    searchPage() {
      const normalized = this.normalizeString(this.searchQuery.trim());
      this.error = false;

      if (this.pages[normalized]) {
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
  margin-top: 140px;
  padding: 2rem;
  border: 2px solid red;
  box-shadow: 0 0 20px red;
  max-width: 700px;
  margin-left: auto;
  margin-right: auto;
}
h1, h2 {
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
