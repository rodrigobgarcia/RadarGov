<template>
  <div v-if="empresa" 
       :class="['p-6 h-full rounded-lg shadow-xl text-neutral-light transition-colors duration-300', empresa.previewColor || 'bg-green-dark-md']">
    <h2 class="text-2xl font-bold mb-4">{{ empresa.nome }}</h2>
    <p class="text-lg">CNPJ: {{ empresa.cnpj }}</p>
    <p class="mt-4">
        <span class="font-semibold">Conteúdo Nacional Preferido:</span> 
        <span :class="[empresa.prefereExigenciaConteudoNacional ? 'text-success' : 'text-error']">
            {{ empresa.prefereExigenciaConteudoNacional ? 'Sim' : 'Não' }}
        </span>
    </p>
    <Button @click="$emit('openModal', empresa)" variant="outline" class="mt-4 bg-neutral-light hover:bg-green-light text-green-dark-md">
        Ver Todas as Preferências
    </Button>
  </div>
  <div v-else class="h-full flex items-center justify-center bg-green-light-hover rounded-lg border border-green-light-md">
    <p class="text-green-dark-md">Selecione uma empresa na lista.</p>
  </div>
</template>

<script setup lang="ts">
import { Button } from '@/components/ui/button';
// import type { Empresa } from '@/types/Empresa';

defineProps<{
  empresa: any | null;
}>();

defineEmits(['openModal']);
</script>