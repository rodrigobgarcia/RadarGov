<template>
  <Dialog :open="isOpen" @update:open="$emit('close')">
    <DialogContent class="sm:max-w-[800px] max-h-[90vh] overflow-y-auto">
      <DialogHeader>
        <DialogTitle class="text-green-dark">Preferências de Licitação</DialogTitle>
        <DialogDescription class="text-green-dark-md">
          Detalhes das preferências de filtro para a empresa **{{ empresa?.nome }}** ({{ empresa?.cnpj }}).
        </DialogDescription>
      </DialogHeader>

      <div class="space-y-6 pt-4">
        <div class="flex items-center space-x-2">
          <span class="font-semibold text-green-dark">Conteúdo Nacional:</span>
          <Badge :variant="empresa?.prefereExigenciaConteudoNacional ? 'default' : 'secondary'"
                 :class="empresa?.prefereExigenciaConteudoNacional ? 'bg-green-primary text-neutral-light hover:bg-green-dark' : 'bg-green-light-hover text-green-dark-md'">
            {{ empresa?.prefereExigenciaConteudoNacional ? "SIM (Preferido)" : "NÃO (Indiferente)" }}
          </Badge>
        </div>

        <div v-for="(pref, index) in preferencias" :key="index">
          <template v-if="pref.list && pref.list.length > 0">
            <h3 class="text-lg font-semibold mb-2 border-b pb-1 text-green-dark">{{ pref.label }}</h3>
            <div class="flex flex-wrap gap-2">
              <Badge v-for="(item, i) in pref.list" :key="i" variant="outline" class="text-xs border-green-light-md text-green-dark-md bg-green-light-hover">
                {{ item }}
              </Badge>
            </div>
          </template>
        </div>
      </div>
    </DialogContent>
  </Dialog>
</template>

<script setup lang="ts">
import { computed } from 'vue';
import { Dialog, DialogContent, DialogDescription, DialogHeader, DialogTitle } from '../ui/dialog';
import { Badge } from '../ui/badge';
// Importe a interface da sua Empresa (se estiver usando TypeScript)
// import type { Empresa } from '@/types/Empresa'; 

const props = defineProps<{
  isOpen: boolean;
  empresa: any | null; // Usando 'any' ou a interface Empresa
}>();

defineEmits(['close']);

const preferencias = computed(() => {
  if (!props.empresa) return [];
  
  // Mapeamento das propriedades da sua classe C#
  return [
    { label: "Órgãos", list: props.empresa.orgaosIdTerceiroPreferidos },
    { label: "Municípios", list: props.empresa.municipiosIdTerceiroPreferidos },
    { label: "Modalidades", list: props.empresa.modalidadesIdTerceiroPreferidas },
    { label: "Tipos", list: props.empresa.tiposIdTerceiroPreferidos },
    { label: "UFs", list: props.empresa.ufsIdTerceiroPreferidas },
    { label: "Fontes Orçamentárias", list: props.empresa.fontesOrcamentariasIdTerceiroPreferidas },
    { label: "Tipos Margem Preferência", list: props.empresa.tiposMargemPreferenciaIdTerceirosPreferidos },
    { label: "Poderes", list: props.empresa.poderesIdTerceiroPreferidos },
  ];
});
</script>