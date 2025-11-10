<template>
  <div class="p-8 bg-neutral-100 min-h-screen">
    <!-- Cabeçalho -->
    <div class="flex justify-between items-center mb-6">
      <h1 class="text-2xl font-semibold text-neutral-800 tracking-tight text-green-primary">
        Lista de Empresas
      </h1>
    </div>

    <!-- Barra de pesquisa e botão -->
    <div class="flex gap-2 mb-3 justify-between items-center">
      <div class="relative w-80">
        <Search class="absolute left-3 top-2.5 h-4 w-4 text-neutral-500" />
        <input
          v-model="filtro"
          type="text"
          placeholder="Buscar por nome, e-mail ou CNPJ..."
          class="w-full pl-9 pr-4 py-2 rounded-lg border border-neutral-300 text-neutral-800 placeholder-neutral-400
          focus:outline-none focus:border-neutral-500 focus:ring-1 focus:ring-neutral-400 transition"
        />
      </div>

      <Button @click="irParaCadastro" class="bg-green-primary text-white hover:bg-green-dark">
        Adicionar Empresa
      </Button>
    </div>

    <!-- Layout principal -->
    <div class="grid grid-cols-3 gap-6 h-[75vh]">
      <!-- COLUNA TABELA -->
      <div class="col-span-2 flex flex-col bg-white border border-neutral-200 rounded-2xl shadow-sm overflow-hidden">
        <div class="flex-1 overflow-y-auto">
          <Table class="w-full text-left">
            <TableHeader class="bg-neutral-50 text-neutral-700 sticky top-0 border-b border-neutral-200">
              <TableRow>
                <TableHead class="px-[16px] py-[4px] font-medium">Nome</TableHead>
                <TableHead class="px-[16px] py-[4px] font-medium">Email</TableHead>
                <TableHead class="px-[16px] py-[4px] font-medium">CNPJ</TableHead>
                <TableHead class="px-[16px] py-[4px] text-end font-medium"></TableHead>
              </TableRow>
            </TableHeader>

            <TableBody>
              <TableRow
                v-for="empresa in empresasFiltradasPaginadas"
                :key="empresa.id"
                @click="selectEmpresa(empresa)"
                :class="[
                  'cursor-pointer transition-colors duration-150 select-none',
                  selectedEmpresa && selectedEmpresa.id === empresa.id
                    ? 'bg-[#E8F3D8] hover:bg-[#E8F3D8]'
                    : 'hover:bg-neutral-50'
                ]"
              >
                <TableCell class="px-[16px] py-[4px] text-neutral-800">
                  {{ empresa.nome }}
                </TableCell>
                <TableCell class="px-[16px] py-[4px] text-neutral-800">
                  {{ empresa.email }}
                </TableCell>
                <TableCell class="px-[16px] py-[4px] text-neutral-800">
                  {{ empresa.cnpj }}
                </TableCell>

                <!-- Coluna de ações -->
                <TableCell class="px-[16px] py-[4px] text-end">
                  <DropdownMenu>
                    <DropdownMenuTrigger as-child>
                      <Button
                        variant="ghost"
                        size="icon"
                        class="shadow-none"
                      >
                        <MoreVertical class="h-4 w-4" />
                      </Button>
                    </DropdownMenuTrigger>

                    <DropdownMenuContent class="w-auto bg-white border border-neutral-200 shadow-md rounded-lg">
                      <DropdownMenuItem
                        @click="abrirModalEdicao(empresa)"
                        class="flex items-center gap-2 text-neutral-700 hover:bg-green-hover-v1"
                      >
                        <Pencil class="h-4 w-auto" /> Editar
                      </DropdownMenuItem>
                      <DropdownMenuItem
                        @click="abrirDialogExclusao(empresa)"
                        class="flex items-center gap-2 text-neutral-700 hover:bg-green-hover-v1"
                      >
                        <Trash2 class="h-4 w-auto" /> Excluir
                      </DropdownMenuItem>
                    </DropdownMenuContent>
                  </DropdownMenu>
                </TableCell>
              </TableRow>
            </TableBody>
          </Table>
        </div>

        <!-- Paginação -->
        <div class="flex justify-center items-center gap-3 px-6 py-3 bg-neutral-50 border-t border-neutral-200">
          <Button
            variant="outline"
            size="icon"
            class="border-neutral-300 text-neutral-700 hover:border-neutral-500 hover:text-neutral-800"
            :disabled="currentPage === 1"
            @click="previousPage"
          >
            <ChevronLeft class="h-4 w-4" />
          </Button>

          <span class="text-sm text-neutral-700 font-medium">
            Página {{ currentPage }} de {{ totalPages }}
          </span>

          <Button
            variant="outline"
            size="icon"
            class="border-neutral-300 text-neutral-700 hover:border-neutral-500 hover:text-neutral-800"
            :disabled="currentPage === totalPages"
            @click="nextPage"
          >
            <ChevronRight class="h-4 w-4" />
          </Button>
        </div>
      </div>

      <!-- COLUNA DETALHE -->
      <div class="col-span-1">
        <EmpresaPreview :empresa="selectedEmpresa" @open-modal="openModal" />
      </div>
    </div>

    <!-- Modal de Cadastro/Edição -->
    <EmpresaPreferenciasDialog :is-open="isModalOpen" :empresa="modalEmpresa" @close="isModalOpen = false" />

    <!-- Dialog de exclusão -->
    <Dialog v-model:open="isDialogOpen">
      <DialogContent class="max-w-sm">
        <DialogHeader>
          <DialogTitle class="text-green-primary">Excluir Empresa</DialogTitle>
          <DialogDescription>
            Tem certeza de que deseja excluir <strong>{{ empresaParaExcluir?.nome }}</strong>?
            Essa ação não poderá ser desfeita.
          </DialogDescription>
        </DialogHeader>
        <div class="flex justify-end gap-2 mt-4">
          <Button variant="outline" @click="isDialogOpen = false">Cancelar</Button>
          <Button class="bg-emerald-600 text-white hover:bg-emerald-700" @click="confirmarExclusao">
            Confirmar
          </Button>
        </div>
      </DialogContent>
    </Dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import {
  Search, MoreVertical, Pencil, Trash2, ChevronLeft, ChevronRight
} from 'lucide-vue-next'
import { Button } from '../components/ui/button'
import {
  DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuTrigger
} from '../components/ui/dropdown-menu'
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '../components/ui/table'
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogDescription } from '../components/ui/dialog'
import { useToast } from '../components/ui/toast/use-toast'
import EmpresaPreview from '../components/Empresa/EmpresaPreview.vue'
import EmpresaPreferenciasDialog from '../components/Empresa/EmpresaPreferenciasDialog.vue'
import EmpresaServico from '../servicos/EmpresaServico'
import type { Empresa } from '@/dtos/Empresa'

const empresas = ref<Empresa[]>([])
const selectedEmpresa = ref<Empresa | null>(null)
const isModalOpen = ref(false)
const modalEmpresa = ref<Empresa | null>(null)
const isDialogOpen = ref(false)
const empresaParaExcluir = ref<Empresa | null>(null)
const currentPage = ref(1)
const itemsPerPage = 10
const filtro = ref('')
const { toast } = useToast()

const router = useRouter()

const totalPages = computed(() => Math.ceil(empresas.value.length / itemsPerPage))
const nextPage = () => currentPage.value < totalPages.value && currentPage.value++
const previousPage = () => currentPage.value > 1 && currentPage.value--

const irParaCadastro = () => {
  router.push('/empresas/cadastro')
}

const carregarEmpresas = async () => {
  try {
    const response = await EmpresaServico.obterOData()
    empresas.value = response.data
    selectedEmpresa.value = empresas.value[0] || null
  } catch (error) {
    toast({
      title: 'Erro ao carregar empresas',
      description: 'Não foi possível obter os dados do servidor.',
      variant: 'destructive'
    })
  }
}

const selectEmpresa = (empresa: Empresa) => (selectedEmpresa.value = empresa)

const abrirModalEdicao = (empresa: Empresa) => {
  modalEmpresa.value = { ...empresa }
  isModalOpen.value = true
}

const abrirModalNovaEmpresa = () => {
  modalEmpresa.value = { id: 0, nome: '', email: '', cnpj: '' }
  isModalOpen.value = true
}

const abrirDialogExclusao = (empresa: Empresa) => {
  empresaParaExcluir.value = empresa
  isDialogOpen.value = true
}

const confirmarExclusao = async () => {
  if (!empresaParaExcluir.value) return
  try {
    await EmpresaServico.remover(empresaParaExcluir.value.id)
    toast({
      title: 'Empresa excluída',
      description: `"${empresaParaExcluir.value.nome}" foi removida com sucesso.`,
      variant: 'success'
    })
    isDialogOpen.value = false
    carregarEmpresas()
  } catch (error) {
    toast({
      title: 'Erro ao excluir',
      description: 'Ocorreu um erro ao tentar excluir a empresa.',
      variant: 'destructive'
    })
  }
}

const empresasFiltradas = computed(() => {
  if (!filtro.value.trim()) return empresas.value
  const termo = filtro.value.toLowerCase()
  return empresas.value.filter(
    e =>
      e.nome.toLowerCase().includes(termo) ||
      e.email.toLowerCase().includes(termo) ||
      e.cnpj.toLowerCase().includes(termo)
  )
})

const empresasFiltradasPaginadas = computed(() => {
  const start = (currentPage.value - 1) * itemsPerPage
  return empresasFiltradas.value.slice(start, start + itemsPerPage)
})

onMounted(carregarEmpresas)
</script>
