import { fileURLToPath, URL } from 'node:url'
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import vueDevTools from 'vite-plugin-vue-devtools'

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
        target: 'http://localhost:5000',
        changeOrigin: true,
        secure: false
      }
    }
  },
  build: {
    outDir: '../backend/wwwroot',
    emptyOutDir: true,
    assetsDir: '',
    rollupOptions: {
      input: {
        main: fileURLToPath(new URL('./index.html', import.meta.url)), // Assurez-vous que le chemin est correct
      }
    }
  }
})
