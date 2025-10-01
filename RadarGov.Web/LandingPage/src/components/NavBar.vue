<template>
  <div
    class="fixed top-0 left-0 w-full h-16 px-6 flex justify-between items-center text-xl font-sans transition-colors duration-300 z-50"
    :class="isScrolled ? 'bg-[#f3f6fc]/95 shadow-md' : 'bg-transparent'"
  >
    <div class="flex items-center gap-2 cursor-pointer" @click="scrollToSection('topo')">
      <img src="../assets/logo.png" alt="Logo RadarGov" class="h-10" />
    </div>

    <button class="md:hidden text-gray-700 focus:outline-none" @click="toggleMobileMenu">
      <span class="material-symbols-outlined">menu</span>
    </button>

    <div class="hidden md:flex gap-8 text-gray-600 text-xl">
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

    <div class="hidden md:flex gap-8 items-center">
      <BaseButton @click="scrollToSection('contato')">
        Comece já!
      </BaseButton>
    </div>
  </div>

  <!-- MOBILE MENU -->
  <div
    v-if="showMobileMenu"
    class="fixed top-16 left-0 w-full bg-[#f3f6fc]/95 shadow-md md:hidden z-40"
  >
    <div class="flex flex-col gap-4 py-4 px-6 text-gray-700 text-lg">
      <a @click.prevent="handleMobileNav('planos')" class="hover:text-[#253b7f]">Planos</a>
      <a @click.prevent="handleMobileNav('integracoes')" class="hover:text-[#253b7f]">Integrações</a>
      <a @click.prevent="handleMobileNav('vantagens')" class="hover:text-[#253b7f]">Vantagens</a>
      <a @click.prevent="handleMobileNav('contato')" class="hover:text-[#253b7f]">Contato</a>
      <BaseButton @click="handleMobileNav('contato')">Comece já!</BaseButton>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, onBeforeUnmount } from 'vue'
import BaseButton from './Button.vue'

const isScrolled = ref(false)
const showMobileMenu = ref(false)

function handleScroll() {
  isScrolled.value = window.scrollY > 0
}

function scrollToSection(id) {
  const element = document.getElementById(id)
  if (element) {
    element.scrollIntoView({ behavior: 'smooth' })
  } else if (id === 'topo') {
    window.scrollTo({ top: 0, behavior: 'smooth' })
  }
}

function toggleMobileMenu() {
  showMobileMenu.value = !showMobileMenu.value
}

function handleMobileNav(id) {
  scrollToSection(id)
  showMobileMenu.value = false
}

onMounted(() => {
  window.addEventListener('scroll', handleScroll)
})

onBeforeUnmount(() => {
  window.removeEventListener('scroll', handleScroll)
})
</script>
