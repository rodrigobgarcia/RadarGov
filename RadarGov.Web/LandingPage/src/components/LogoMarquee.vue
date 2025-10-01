<template>
  <div class="marquee-container">
    <div class="marquee-content" ref="marqueeContent">
      <slot></slot>
      <slot></slot>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue';

// A animação será controlada puramente por CSS,
// mas podemos usar o script para obter a largura do conteúdo, se necessário
// (embora a solução com repetição de slot e animação CSS pura seja mais simples e eficiente).

// Se você precisar de lógica de script, por exemplo, para pausar ao passar o mouse,
// você a colocaria aqui.
const marqueeContent = ref(null);
</script>

<style scoped>
/* 1. Container: Define a área visível, esconde o que ultrapassa */
.marquee-container {
  overflow: hidden;
  width: 100%;
  white-space: nowrap; /* Impede que os itens quebrem a linha */
  padding: 20px 0; /* Espaçamento vertical opcional */
  background-color: #253b7f; /* Fundo escuro como no vídeo */
}

/* 2. Conteúdo: Contém os itens do slot + a repetição. */
.marquee-content {
  display: flex;
  /* Cria o loop infinito movendo-se horizontalmente */
  animation: marquee-scroll 30s linear infinite;
}

/* Pausar a animação ao passar o mouse */
.marquee-container:hover .marquee-content {
  animation-play-state: paused;
}

/* 3. A Animação (Keyframes) */
@keyframes marquee-scroll {
  0% {
    /* Começa na posição normal */
    transform: translateX(0);
  }
  100% {
    /* Move para a esquerda em 50% de sua largura total.
       Isso move exatamente a largura de UMA cópia do conteúdo,
       fazendo o loop perfeito */
    transform: translateX(-50%);
  }
}

/* 4. Estilo dos Itens (Logos) */
:deep(.logo-item) {
  /* Os itens precisam ter largura suficiente e espaçamento */
  min-width: 240px; /* Exemplo de largura mínima para cada logo */
  height: 45px; /* Altura fixa */
  display: flex;
  align-items: center;
  justify-content: center;
  margin-right: 50px; /* Espaço entre os logos */
  flex-shrink: 0; /* Impede que os itens encolham */
  filter: grayscale(100%) brightness(1.5); /* Efeito de logo cinza/claro */
  transition: filter 0.3s ease; /* Transição suave para o hover */
  cursor: default;
}

:deep(.logo-item:hover) {
  filter: grayscale(0%) brightness(1); /* Logo colorida/mais clara ao passar o mouse */
}

/* Estilo para as imagens dentro dos itens, se for o caso */
:deep(.logo-item img) {
    max-height: 100%;
    width: auto;
}
</style>