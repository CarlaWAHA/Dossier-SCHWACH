import { fileURLToPath, URL } from 'node:url'
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueDevTools from 'vite-plugin-vue-devtools'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    vueDevTools()
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    proxy: {
      '/api': {
        target: 'http://localhost:5000', // ✅ ton backend écoute ici
        changeOrigin: true,
        secure: false
      }
    }
  },
  build: {
    outDir: '../backend/wwwroot', // ✅ sortie du frontend dans le dossier static de .NET
    emptyOutDir: true,             // ❗ nettoie le dossier avant chaque build
    assetsDir: '',                 // ❗ empêche Vite de mettre assets dans un sous-dossier (facilite la lecture des chemins)
  }
})
