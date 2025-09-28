<template>
  <div
    class="flex justify-between items-center w-full fixed top-0 left-0 h-16 text-xl font-sans px-[100px] transition-colors duration-300 z-1000"
    :class="isScrolled ? 'bg-[#f3f6fc]/95 shadow-md' : 'bg-transparent'"
  >
    <div class="flex items-center gap-2 cursor-pointer" @click="scrollToSection('topo')">
      <img src="../assets/logo.png" alt="Logo RadarGov" class="h-10" />
    </div>

    <div class="flex gap-8 text-gray-600 text-xl">
      <a
        href="#planos"
        @click.prevent="scrollToSection('planos')"
        class="cursor-pointer hover:text-[#253b7f] transition-colors"
      >
        Planos
      </a>
      <a
        href="#integracoes"
        @click.prevent="scrollToSection('integracoes')"
        class="cursor-pointer hover:text-[#253b7f] transition-colors"
      >
        Integrações
      </a>
      <a
        href="#vantagens"
        @click.prevent="scrollToSection('vantagens')"
        class="cursor-pointer hover:text-[#253b7f] transition-colors"
      >
        Vantagens
      </a>
      <a
        href="#contato"
        @click.prevent="scrollToSection('contato')"
        class="cursor-pointer hover:text-[#253b7f] transition-colors"
      >
        Contato
      </a>
    </div>

    <div class="flex gap-8 items-center">
      <BaseButton @click="scrollToSection('contato')">
        Comece já!
      </BaseButton>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, onBeforeUnmount } from 'vue'
import BaseButton from './Button.vue'

const isScrolled = ref(false)

function handleScroll() {
  isScrolled.value = window.scrollY > 0
}

/**
 * Função para rolar suavemente para uma seção específica da página.
 * @param {string} id O ID do elemento para onde rolar (ex: 'planos', 'contato').
 */
function scrollToSection(id) {
  const element = document.getElementById(id)
  if (element) {
    element.scrollIntoView({ behavior: 'smooth' })
  } else if (id === 'topo') {
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }
}

onMounted(() => {
  window.addEventListener('scroll', handleScroll)
})
onBeforeUnmount(() => {
  window.removeEventListener('scroll', handleScroll)
})
</script>