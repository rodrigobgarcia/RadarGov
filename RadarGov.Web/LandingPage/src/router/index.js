import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Opportunities from '@/views/Opportunities.vue'
import Login  from '@/views/Login.vue'
import EnterpriseRegister from '@/views/EnterpriseRegister.vue'
import Modalidade from '@/views/Modalidade.vue'
import UserRegister from '@/views/UserRegister.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/oportunidades',
      name: 'oportunidades',
      component: Opportunities
    },
    {
      path: '/login',
      name: 'login',
      component: Login
    },
    {
      path: '/enterpriseRegister',
      name: 'enterpriseRegister',
      component: EnterpriseRegister
    },
    {
      path: '/userRegister',
      name: 'userRegister',
      component: UserRegister
    },
    {
      path: '/modalidade',
      name: 'modalidade',
      component: Modalidade
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue'),
    },
  ],
})

export default router
