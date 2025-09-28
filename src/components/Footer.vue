<template>
  <footer
    class="flex justify-center items-center w-full py-6 px-[20px] md:px-[100px] text-lg font-sans bg-[#e6eef9] text-gray-700"
  >
    <div class="flex justify-between items-center w-full max-w-[1200px] text-sm">

      <div class="flex gap-2 flex-col">
        <div class="flex items-center gap-2">
          <!-- <span class="text-xl font-bold text-gray-900">RadarGov</span> -->
          <img src="../assets/logo.png" alt="Logo RadarGov" class="h-8" />
        </div>
        
        <div class="text-gray-500 font-normal">
          {{ currentYear }} | Todos os direitos reservados
        </div>
      </div>

      <div class="flex gap-10 font-medium">
        <a
          href="#topo"
          @click.prevent="scrollToSection('topo')"
          class="hover:text-[#253b7f] transition-colors"
        >
          Início
        </a>
        <a
          href="#planos"
          @click.prevent="scrollToSection('planos')"
          class="hover:text-[#253b7f] transition-colors"
        >
          Conheça nossos planos
        </a>
        <a
          href="#vantagens"
          @click.prevent="scrollToSection('vantagens')"
          class="hover:text-[#253b7f] transition-colors"
        >
          Saiba das vantagens
        </a>
        <a
          href="#sobre"
          @click.prevent="scrollToSection('sobre')"
          class="hover:text-[#253b7f] transition-colors"
        >
          Sobre nós
        </a>
      </div>
    </div>
  </footer>
</template>

<script setup>
import { computed } from 'vue'

// Propriedade computada para o ano atual
const currentYear = computed(() => {
  return new Date().getFullYear()
})

/**
 * Função para rolar suavemente para uma seção específica da página.
 * É a mesma função do seu Navbar.
 * @param {string} id O ID do elemento para onde rolar (ex: 'planos', 'contato').
 */
function scrollToSection(id) {
  const element = document.getElementById(id)
  if (element) {
    element.scrollIntoView({ behavior: 'smooth', block: 'start' })
  } else if (id === 'topo' || id === 'início') {
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }
}
</script>