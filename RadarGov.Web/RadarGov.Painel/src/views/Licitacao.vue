<template>
  <div class="p-8 bg-neutral-100 min-h-screen">
    <div class="flex justify-between items-center mb-6">
      <h1 class="text-2xl font-semibold text-neutral-800 tracking-tight text-green-primary">
        Lista de Licitações (Recomendações)
      </h1>
    </div>

    <div class="flex gap-2 mb-3 justify-between items-center">
      <div class="relative w-80">
        <input
          v-model="filtro"
          type="text"
          placeholder="Buscar por título, órgão ou município..."
          class="w-full pl-3 pr-4 py-2 rounded-lg border border-neutral-300 text-neutral-800 placeholder-neutral-400
          focus:outline-none focus:border-neutral-500 focus:ring-1 focus:ring-neutral-400 transition"
        />
      </div>

      <div class="flex items-center gap-2">
        <label class="text-sm text-neutral-700">Empresa:</label>
        <select v-model.number="selectedEmpresaId" @change="carregarRecomendacoes" class="px-3 py-2 border rounded">
          <option v-for="e in empresas" :key="e.id" :value="e.id">{{ e.nome }}</option>
        </select>
      </div>
    </div>

    <div class="grid grid-cols-3 gap-6 h-[75vh]">
      <div class="col-span-2 flex flex-col bg-white border border-neutral-200 rounded-2xl shadow-sm overflow-hidden">
        <div class="flex-1 overflow-y-auto">
          <table class="w-full text-left">
            <thead class="bg-neutral-50 text-neutral-700 sticky top-0 border-b border-neutral-200">
              <tr>
                <th class="px-4 py-2 font-medium">Título</th>
                <th class="px-4 py-2 font-medium">Órgão</th>
                <th class="px-4 py-2 font-medium">Data</th>
                <th class="px-4 py-2 text-end font-medium"></th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="licitacao in licitacoesFiltradasPaginadas" :key="licitacao.id" @click="selectLicitacao(licitacao)" :class="['cursor-pointer', selectedLicitacao && selectedLicitacao.id === licitacao.id ? 'bg-[#E8F3D8]' : 'hover:bg-neutral-50']">
                <td class="px-4 py-2">{{ licitacao.titulo }}</td>
                <td class="px-4 py-2">{{ licitacao.orgaoIdTerceiro }}</td>
                <td class="px-4 py-2">{{ formatDate(licitacao.dataPublicacaoPncp) }}</td>
                <td class="px-4 py-2 text-end">
                  <a :href="licitacao.itemUrl" target="_blank" class="text-sm text-blue-600">Abrir</a>
                </td>
              </tr>
            </tbody>
          </table>
        </div>

        <div class="flex justify-center items-center gap-3 px-6 py-3 bg-neutral-50 border-t border-neutral-200">
          <button @click="previousPage" :disabled="currentPage === 1" class="px-2 py-1 border rounded">Anterior</button>
          <span class="text-sm text-neutral-700 font-medium">Página {{ currentPage }} de {{ totalPages }}</span>
          <button @click="nextPage" :disabled="currentPage === totalPages" class="px-2 py-1 border rounded">Próxima</button>
        </div>
      </div>

      <div class="col-span-1">
        <div class="bg-white border border-neutral-200 rounded-2xl p-4">
          <h3 class="text-lg font-semibold text-neutral-800">Detalhes</h3>
          <div v-if="selectedLicitacao" class="mt-3 text-sm text-neutral-700">
            <p><strong>Título:</strong> {{ selectedLicitacao.titulo }}</p>
            <p><strong>Órgão:</strong> {{ selectedLicitacao.orgaoIdTerceiro }}</p>
            <p><strong>Município:</strong> {{ selectedLicitacao.municipioIdTerceiro }}</p>
            <p><strong>Data:</strong> {{ formatDate(selectedLicitacao.dataPublicacaoPncp) }}</p>
            <p class="mt-2"><strong>Descrição:</strong></p>
            <p class="text-sm">{{ selectedLicitacao.descricao }}</p>
          </div>
          <div v-else class="mt-3 text-sm text-neutral-500">Selecione uma licitação para ver detalhes.</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import LicitacaoServico from '../servicos/LicitacaoServico'
import EmpresaServico from '../servicos/EmpresaServico'
import type { Licitacao } from '@/dtos/Licitacao'
import type { Empresa } from '@/dtos/Empresa'

const licitacoes = ref<Licitacao[]>([])
const empresas = ref<Empresa[]>([])
const selectedLicitacao = ref<Licitacao | null>(null)
const selectedEmpresaId = ref<number | null>(null)
const currentPage = ref(1)
const itemsPerPage = 10
const filtro = ref('')

const totalPages = computed(() => Math.max(1, Math.ceil(licitacoes.value.length / itemsPerPage)))
const nextPage = () => currentPage.value < totalPages.value && currentPage.value++
const previousPage = () => currentPage.value > 1 && currentPage.value--

const carregarEmpresas = async () => {
    try {
      const response = await EmpresaServico.obterOData()
      // Retorno OData possui a propriedade 'items'
      empresas.value = response.data.items || []
      selectedEmpresaId.value = empresas.value[0]?.id ?? null
    } catch (e) {
    console.error(e)
  }
}

const carregarRecomendacoes = async () => {
  if (!selectedEmpresaId.value) return
  try {
    const response = await LicitacaoServico.obterRecomendadas(selectedEmpresaId.value)
    licitacoes.value = response.data
    selectedLicitacao.value = licitacoes.value[0] || null
    currentPage.value = 1
  } catch (e) {
    console.error(e)
  }
}

const selectLicitacao = (l: Licitacao) => (selectedLicitacao.value = l)

const licitacoesFiltradas = computed(() => {
  if (!filtro.value.trim()) return licitacoes.value
  const termo = filtro.value.toLowerCase()
  return licitacoes.value.filter(l => (l.titulo || '').toLowerCase().includes(termo) || (l.orgaoIdTerceiro || '').toLowerCase().includes(termo) || (l.municipioIdTerceiro || '').toLowerCase().includes(termo))
})

const licitacoesFiltradasPaginadas = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage
  return licitacoesFiltradas.value.slice(start, start + itemsPerPage)
})

const formatDate = (d?: string | null) => {
  if (!d) return ''
  try {
    return new Date(d).toLocaleDateString()
  } catch {
    return d
  }
}

onMounted(async () => {
  await carregarEmpresas()
  await carregarRecomendacoes()
})
</script>
