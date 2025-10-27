<template>
  <div class="flex h-screen bg-gray-50 text-gray-700">
    <!-- TABELA -->
    <div class="w-2/3 p-8 overflow-y-auto">
      <div class="flex items-center justify-between mb-6">
        <h1 class="text-3xl font-semibold text-[#253b7f]">
          Modalidades de Licitação
        </h1>
      </div>

      <div class="overflow-hidden rounded-2xl shadow-md bg-white">
        <table class="min-w-full text-left">
          <thead class="bg-[#F3F6FC] text-[#253b7f] uppercase text-sm tracking-wider">
            <tr>
              <th class="px-6 py-3">ID</th>
              <th class="px-6 py-3">Nome</th>
              <!-- <th class="px-6 py-3 text-right">Total</th> -->
              <th class="px-6 py-3 text-right">Ações</th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="modalidade in modalidades"
              :key="modalidade.id"
              @click="selectItem(modalidade)"
              class="cursor-pointer transition"
              :class="selectedItem?.id === modalidade.id
                ? 'bg-[#F3F6FC]'
                : 'hover:bg-[#F3F6FC]/70'"
            >
              <td class="px-6 py-4 border-t border-gray-100 font-medium">
                {{ modalidade.id }}
              </td>
              <td class="px-6 py-4 border-t border-gray-100">
                {{ modalidade.nome }}
              </td>
              <!-- <td class="px-6 py-4 border-t border-gray-100 text-right font-semibold text-gray-600">
                {{ modalidade.total.toLocaleString('pt-BR') }}
              </td> -->
              <td class="px-6 py-4 text-right">
                <div class="relative inline-block text-left">
                  <button
                    @click.stop="toggleMenu(modalidade.id)"
                    class="text-gray-400 hover:text-[#253b7f] transition"
                  >
                    ⋮
                  </button>
                  <div
                    v-if="openMenu === modalidade.id"
                    class="absolute right-0 mt-2 w-36 bg-white rounded-lg shadow-xl z-10"
                  >
                    <ul class="py-1 text-sm">
                      <!-- <li
                        @click="editarItem(modalidade)"
                        class="px-4 py-2 hover:bg-[#F3F6FC] cursor-pointer flex items-center gap-2"
                      >
                        Editar
                      </li> -->
                      <li
                        @click="excluirItem(modalidade)"
                        class="px-4 py-2 hover:bg-red-50 cursor-pointer text-red-500 flex items-center gap-2"
                      >
                        Excluir
                      </li>
                    </ul>
                  </div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <!-- PREVIEW -->
    <div class="w-1/3 p-8 flex flex-col">
      <div
        v-if="selectedItem"
        class="bg-white shadow-lg rounded-2xl p-6 border-t-4 border-[#253b7f] transition-all"
      >
        <div class="flex items-center justify-between mb-4">
          <h2 class="text-xl font-semibold text-[#253b7f]">
            Detalhes da Modalidade
          </h2>
        </div>

        <div class="space-y-3 text-gray-700">
          <p>
            <span class="font-medium text-[#253b7f]">ID:</span>
            {{ selectedItem.id }}
          </p>
          <p>
            <span class="font-medium text-[#253b7f]">Nome:</span>
            {{ selectedItem.nome }}
          </p>
          <p>
            <span class="font-medium text-[#253b7f]">Total:</span>
            {{ selectedItem.total.toLocaleString('pt-BR') }}
          </p>
        </div>

        <!-- <div class="mt-6 flex justify-end">
          <button
            @click="editarItem(selectedItem)"
            class="bg-[#253b7f] hover:bg-[#1f3270] text-white font-medium px-4 py-2 rounded-lg shadow-sm transition"
          >
            Editar
          </button>
        </div> -->
      </div>

      <div
        v-else
        class="flex items-center justify-center h-full text-gray-400 italic"
      >
        Selecione uma modalidade para ver os detalhes
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from "vue";

const modalidades = ref([
  { id: "8", nome: "Dispensa", total: 1475374 },
  { id: "6", nome: "Pregão - Eletrônico", total: 726695 },
  { id: "9", nome: "Inexigibilidade", total: 426348 },
  { id: "4", nome: "Concorrência - Eletrônica", total: 82883 },
  { id: "12", nome: "Credenciamento", total: 30901 },
  { id: "7", nome: "Pregão - Presencial", total: 18742 },
  { id: "5", nome: "Concorrência - Presencial", total: 6037 },
  { id: "1", nome: "Leilão - Eletrônico", total: 2245 },
  { id: "13", nome: "Leilão - Presencial", total: 709 },
  { id: "11", nome: "Pré-qualificação", total: 689 },
]);

const selectedItem = ref(null);
const openMenu = ref(null);

function selectItem(item) {
  selectedItem.value = item;
  openMenu.value = null;
}

function toggleMenu(id) {
  openMenu.value = openMenu.value === id ? null : id;
}

function editarItem(item) {
  alert(`Editar modalidade: ${item.nome}`);
  openMenu.value = null;
}

function excluirItem(item) {
  if (confirm(`Tem certeza que deseja excluir "${item.nome}"?`)) {
    modalidades.value = modalidades.value.filter((m) => m.id !== item.id);
    if (selectedItem.value?.id === item.id) selectedItem.value = null;
  }
  openMenu.value = null;
}
</script>

<style scoped>
::-webkit-scrollbar {
  width: 6px;
}
::-webkit-scrollbar-thumb {
  background-color: #c3c3c3;
  border-radius: 3px;
}
</style>
