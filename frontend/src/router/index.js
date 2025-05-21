import { createRouter, createWebHistory } from 'vue-router'
import Home from '@/Views/Home.vue'
import Login from '@/Views/Login.vue'
import Register from '@/Views/Register.vue'
import Movie from '@/Views/Movie.vue'
import Hotel from '@/Views/Hotel.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/register',
    name: 'Register',
    component: Register
  },
  {
    path: '/movie/:id',
    name: 'Movie',
    component: Movie
  },
  {
    path: '/hotel',
    name: 'Hotel',
    component: Hotel // ✅ route directe, pas besoin d'import dynamique ici
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
