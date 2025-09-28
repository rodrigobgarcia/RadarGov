<template>
  <div class="relative w-full min-h-[600px] flex flex-col justify-center items-center px-4 sm:px-6 lg:px-12 text-center">
    <div class="flex flex-col items-center gap-6 mb-10 max-w-4xl">
      <div
        class="inline-block px-4 py-1 rounded-full font-semibold text-white text-sm sm:text-base
               bg-gradient-to-r from-[#DAA520] via-[#FFD700] to-[#DAA520]"
      >
        Inteligência potencializada por IA
      </div>

      <h1
        class="font-extrabold text-gray-700 text-4xl sm:text-5xl lg:text-6xl leading-tight sm:leading-tight"
      >
        Descubra oportunidades de Licitações com
        <div>
          <span class="text-[#253b7f]">Precisão Inteligente</span>
        </div>
      </h1>

      <p class="text-base sm:text-lg lg:text-xl font-medium text-gray-600">
        Transforme a sua busca por Licitações com a nossa plataforma que identifica,
        busca e sugere oportunidades de Licitação relevantes para o seu negócio.
      </p>
    </div>

    <div class="flex flex-col sm:flex-row gap-4 w-full sm:w-auto justify-center items-center z-15">
      <BaseButton @click="scrollToSection('contato')">
        Comece seu Teste Gratuito
      </BaseButton>
      <BaseButton variant="outline" @click="scrollToSection('planos')">
        Conheça os planos
      </BaseButton>
    </div>

    <div class="custom-shape-divider-bottom-1759069318">
      <svg
        data-name="Layer 1"
        xmlns="http://www.w3.org/2000/svg"
        viewBox="0 0 1200 120"
        preserveAspectRatio="none"
      >
        <path
          d="M321.39,56.44c58-10.79,114.16-30.13,172-41.86,
          82.39-16.72,168.19-17.73,250.45-.39C823.78,31,
          906.67,72,985.66,92.83c70.05,18.48,
          146.53,26.09,214.34,3V0H0V27.35A600.21,
          600.21,0,0,0,321.39,56.44Z"
          class="shape-fill"
        />
      </svg>
    </div>
  </div>
</template>


<script setup>
import BaseButton from '../Button.vue'
/**
 * Função para rolar suavemente para uma seção específica da página.
 * É a mesma função usada no seu Navbar e Footer.
 * *NOTA: Esta função é necessária no script para que os botões funcionem.*
 * @param {string} id O ID do elemento para onde rolar (ex: 'planos', 'contato').
 */
function scrollToSection(id) {
    const element = document.getElementById(id)
    if (element) {
        element.scrollIntoView({ behavior: 'smooth', block: 'start' })
    } else if (id === 'topo') {
        window.scrollTo({ top: 0, behavior: 'smooth' })
    }
}
</script>
<style>
.custom-shape-divider-bottom-1759069318 {
    position: absolute;
    bottom: 0;
    left: 0;
    width: 100%;
    overflow: hidden;
    line-height: 0;
    transform: rotate(180deg);
}

.custom-shape-divider-bottom-1759069318 svg {
    position: relative;
    display: block;
    width: calc(111% + 1.3px);
    height: 138px;
}

.custom-shape-divider-bottom-1759069318 .shape-fill {
    fill: #F3F6FC;
}
</style>