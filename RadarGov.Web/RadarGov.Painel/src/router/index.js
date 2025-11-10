import { createRouter, createWebHistory } from 'vue-router'
import Home from './views/Home.vue'
import Empresa from './views/Empresa.vue'
import EmpresaCadastro from '../views/EmpresaCadastro.vue'

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
      name: 'empresas-cadastro',
      component: EmpresaCadastro
    }
  ],
})

export default router
