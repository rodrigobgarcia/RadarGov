import { createRouter, createWebHistory } from 'vue-router'
import Home from './views/Home.vue'
import Empresa from './views/Empresa.vue'
import Licitacao from './views/Licitacao.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/licitacoes',
      name: 'licitacoes',
      component: Licitacao
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
