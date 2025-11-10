<template>
  <div v-if="empresa" class="relative flex flex-col h-full rounded-xl bg-white border border-neutral-200 shadow-sm overflow-hidden transition-all duration-300">
    <!-- Cabeçalho -->
    <div class="px-6 py-5 border-b border-neutral-100 flex items-center justify-between">
      <h2 class="text-lg font-semibold text-neutral-800 truncate">{{ empresa.nome }}</h2>
      <span class="text-sm text-neutral-500 font-medium">{{ empresa.cnpj }}</span>
    </div>

    <!-- Corpo -->
    <div class="flex-1 p-6 space-y-5 text-neutral-700">
      <!-- Campo com borda lateral -->
      <div class="pl-4 border-l-4 border-[#E8F3D8]">
        <p class="text-sm text-neutral-500 mb-1">CNPJ</p>
        <p class="text-base font-medium text-neutral-800">{{ empresa.cnpj }}</p>
      </div>

      <div class="pl-4 border-l-4" :class="empresa.prefereExigenciaConteudoNacional ? 'border-[#E8F3D8]' : 'border-red-400/70'">
        <p class="text-sm text-neutral-500 mb-1">Conteúdo Nacional Preferido</p>
        <p
          class="inline-flex items-center gap-2 text-sm font-medium"
          :class="empresa.prefereExigenciaConteudoNacional ? 'text-green-600' : 'text-red-500'"
        >
          <span class="inline-block w-2.5 h-2.5 rounded-full"
                :class="empresa.prefereExigenciaConteudoNacional ? 'bg-[#1f5405]' : 'bg-red-400'"></span>
          {{ empresa.prefereExigenciaConteudoNacional ? 'Sim' : 'Não' }}
        </p>
      </div>
    </div>

    <!-- Rodapé -->
    <div class="px-6 py-4 border-t border-neutral-100 flex justify-end bg-neutral-50/50">
      <Button
        @click="$emit('openModal', empresa)"
        class="text-green-700 border border-[#1f5405] hover:bg-[#1f5405] transition-all rounded-lg px-5 py-2 text-sm font-medium"
      >
        Ver todas as preferências
      </Button>
    </div>
  </div>

  <!-- Placeholder -->
  <div v-else class="h-full flex flex-col items-center justify-center bg-neutral-50 border border-neutral-200 rounded-xl text-center p-8 text-neutral-500">
    <p class="text-base">Selecione uma empresa na lista para visualizar os detalhes.</p>
  </div>
</template>

<script setup lang="ts">
import { Button } from '@/components/ui/button'

defineProps<{
  empresa: any | null
}>()

defineEmits(['openModal'])
</script>
