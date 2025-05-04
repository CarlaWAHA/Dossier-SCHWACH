import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import Movie from '../views/Movie.vue'
import Login from '../Views/Login.vue'
import Register from '../Views/Register.vue'

const routes = [
  { path: '/', name: 'Home', component: Home },
  { path: '/movie/:id', name: 'Movie', component: Movie },
  { path: '/login', name: 'Login', component: Login },
  { path: '/register', name: 'Register', component: Register }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
