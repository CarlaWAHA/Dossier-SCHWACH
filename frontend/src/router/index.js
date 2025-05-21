import { createRouter, createWebHistory } from 'vue-router'
import Home from '@/Views/Home.vue'
import Login from '@/Views/Login.vue'
import Register from '@/Views/Register.vue'
import Movie from '@/Views/Movie.vue'
import Hotel from '@/Views/Hotel.vue'
import AnubisExperience from '@/Views/AnubisExperience.vue' // ✅ Import manquant
import Hierarchie from '@/Views/Hierarchie.vue';

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
    component: Hotel
  },
  {
    path: '/anubis',
    name: 'Anubis',
    component: AnubisExperience // ✅ Composant maintenant bien reconnu
  },
  { path: '/hierarchie', name: 'hierarchie', component: Hierarchie }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
