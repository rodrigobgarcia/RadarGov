import { createRouter, createWebHistory } from 'vue-router'
import Home from './views/Home.vue'
import Empresa from './views/Empresa.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/empresas',
      name: 'empresas',
      component: Empresa
    },
    {
      path: '/empresas/cadastro',
      name: 'empresaCadastro',
      component: () => import('./views/EmpresaCadastro.vue')
    }
  ],
})

export default router
