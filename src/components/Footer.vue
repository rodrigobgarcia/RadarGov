<template>
  <footer
    class="flex justify-center items-center w-full py-6 px-4 sm:px-6 md:px-10 lg:px-[100px] text-lg font-sans bg-[#e6eef9] text-gray-700"
  >
    <div class="flex flex-col md:flex-row justify-between items-center md:items-start w-full max-w-[1200px] text-sm gap-4 md:gap-0">
      
      <div class="flex flex-col items-center md:items-start gap-2 text-center md:text-left">
        <img src="../assets/logo.png" alt="Logo RadarGov" class="h-8" />
        <div class="text-gray-500 font-normal">
          {{ currentYear }} | Todos os direitos reservados
        </div>
      </div>

      <!-- LINKS -->
      <div class="flex flex-wrap justify-center items-center md:justify-end gap-4 md:gap-10 font-medium text-center md:text-right">
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