<!-- EmpresaCadastro.vue -->
<template>
  <div class="p-4">
    <h2 class="text-2xl font-bold mb-6">Cadastro de Empresa</h2>

    <form @submit.prevent="salvarEmpresa" class="space-y-6">
      <!-- Dados básicos -->
      <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
        <div>
          <label class="block text-sm font-medium text-gray-700">Nome da Empresa</label>
          <input
            v-model="empresa.nome"
            type="text"
            required
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
        </div>

        <div>
          <label class="block text-sm font-medium text-gray-700">CNPJ</label>
          <input
            v-model="empresa.cnpj"
            v-mask="'##.###.###/####-##'"
            type="text"
            required
            maxlength="18"
            placeholder="00.000.000/0000-00"
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
        </div>

        <div>
          <label class="block text-sm font-medium text-gray-700">E-mail</label>
          <input
            v-model="empresa.email"
            type="email"
            required
            class="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500"
          />
        </div>
      </div>

      <!-- Preferências -->
      <div class="mt-8">
        <h3 class="text-lg font-medium text-gray-900 mb-4">Preferências de Licitação</h3>
        
        <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
          <!-- UFs -->
          <div>
            <div class="flex justify-between items-center">
              <label class="block text-sm font-medium text-gray-700">UFs de Interesse</label>
              <button 
                type="button"
                @click="selecionarTodas('ufs')"
                class="text-sm text-indigo-600 hover:text-indigo-900"
              >
                {{ empresa.ufsIdTerceiroPreferidas.length === ufsFormatted.length ? 'Limpar Seleção' : 'Selecionar Todas' }}
              </button>
            </div>
            <multi-select
              v-model="empresa.ufsIdTerceiroPreferidas"
              :options="ufsFormatted"
              label="sigla"
              track-by="idTerceiro"
              value-prop="idTerceiro"
              :multiple="true"
              :searchable="true"
              :close-on-select="false"
              :clear-on-select="false"
              :preserve-search="true"
              placeholder="Selecione as UFs"
              class="mt-1 multiselect-virtualized"
              mode="tags"
              :max-height="200"
              :limit="100"
              :min-chars="1"
            >
              <template v-slot:option="{ option }">
                <div class="flex items-center px-2 py-1">
                  <span class="text-sm font-medium">{{ option.sigla }}</span>
                </div>
              </template>
              <template v-slot:selected-option="{ option }">
                <span class="text-sm font-medium">{{ option.sigla }}</span>
              </template>
              <template v-slot:no-options>
                <span class="text-gray-500">Nenhuma UF encontrada</span>
              </template>
            </multi-select>
          </div>

          <!-- Municípios -->
          <div>
            <div class="flex justify-between items-center">
              <label class="block text-sm font-medium text-gray-700">Municípios de Interesse</label>
              <button 
                type="button"
                @click="selecionarTodas('municipios')"
                class="text-sm text-indigo-600 hover:text-indigo-900"
              >
                {{ empresa.municipiosIdTerceiroPreferidos.length === municipios.length ? 'Limpar Seleção' : 'Selecionar Todas' }}
              </button>
            </div>
            <multi-select
              v-model="empresa.municipiosIdTerceiroPreferidos"
              :options="municipios"
              label="nome"
              track-by="idTerceiro"
              value-prop="idTerceiro"
              :multiple="true"
              :searchable="true"
              mode="tags"
              :max-height="200"
              :limit="100"
              placeholder="Selecione os municípios"
              class="mt-1 multiselect-virtualized"
            >
              <template v-slot:option="{ option }">
                <div class="flex items-center px-2 py-1">
                  <span class="text-sm font-medium">{{ option.sigla }}</span>
                </div>
              </template>
            </multi-select>
          </div>

          <!-- Modalidades -->
          <div>
            <div class="flex justify-between items-center">
              <label class="block text-sm font-medium text-gray-700">Modalidades de Interesse</label>
              <button 
                type="button"
                @click="selecionarTodas('modalidades')"
                class="text-sm text-indigo-600 hover:text-indigo-900"
              >
                {{ empresa.modalidadesIdTerceiroPreferidas.length === modalidadesFormatted.length ? 'Limpar Seleção' : 'Selecionar Todas' }}
              </button>
            </div>
            <multi-select
              v-model="empresa.modalidadesIdTerceiroPreferidas"
              :options="modalidadesFormatted"
              label="nome"
              track-by="idTerceiro"
              value-prop="idTerceiro"
              :multiple="true"
              :searchable="true"
              mode="tags"
              :max-height="200"
              :limit="100"
              placeholder="Selecione as modalidades"
              class="mt-1 multiselect-virtualized"
            >
              <template v-slot:option="{ option }">
                <div class="flex items-center px-2 py-1">
                  <span class="text-sm font-medium">{{ option.nome }}</span>
                </div>
              </template>
              <template v-slot:selected-option="{ option }">
                <span class="text-sm font-medium">{{ option.nome }}</span>
              </template>
            </multi-select>
          </div>

          <!-- Órgãos -->
          <div>
            <div class="flex justify-between items-center">
              <label class="block text-sm font-medium text-gray-700">Órgãos de Interesse</label>
              <button 
                type="button"
                @click="selecionarTodas('orgaos')"
                class="text-sm text-indigo-600 hover:text-indigo-900"
              >
                {{ empresa.orgaosIdTerceiroPreferidos.length === orgaosFormatted.length ? 'Limpar Seleção' : 'Selecionar Todos' }}
              </button>
            </div>
            <multi-select
              v-model="empresa.orgaosIdTerceiroPreferidos"
              :options="orgaosFormatted"
              label="nome"
              track-by="idTerceiro"
              value-prop="idTerceiro"
              :multiple="true"
              :searchable="true"
              mode="tags"
              :max-height="200"
              :limit="100"
              placeholder="Selecione os órgãos"
              class="mt-1 multiselect-virtualized"
            >
              <template v-slot:option="{ option }">
                <div class="flex items-center px-2 py-1">
                  <span class="text-sm font-medium">{{ option.nome }}</span>
                </div>
              </template>
              <template v-slot:selected-option="{ option }">
                <span class="text-sm font-medium">{{ option.nome }}</span>
              </template>
            </multi-select>
          </div>

          <!-- Tipos -->
          <div>
            <div class="flex justify-between items-center">
              <label class="block text-sm font-medium text-gray-700">Tipos de Interesse</label>
              <button 
                type="button"
                @click="selecionarTodas('tipos')"
                class="text-sm text-indigo-600 hover:text-indigo-900"
              >
                {{ empresa.tiposIdTerceiroPreferidos.length === tiposFormatted.length ? 'Limpar Seleção' : 'Selecionar Todos' }}
              </button>
            </div>
            <multi-select
              v-model="empresa.tiposIdTerceiroPreferidos"
              :options="tiposFormatted"
              label="nome"
              track-by="idTerceiro"
              value-prop="idTerceiro"
              :multiple="true"
              :searchable="true"
              mode="tags"
              :max-height="200"
              :limit="100"
              placeholder="Selecione os tipos"
              class="mt-1 multiselect-virtualized"
            >
              <template v-slot:option="{ option }">
                <div class="flex items-center px-2 py-1">
                  <span class="text-sm font-medium">{{ option.nome }}</span>
                </div>
              </template>
              <template v-slot:selected-option="{ option }">
                <span class="text-sm font-medium">{{ option.nome }}</span>
              </template>
            </multi-select>
          </div>

          <!-- Fontes Orçamentárias -->
          <div>
            <div class="flex justify-between items-center">
              <label class="block text-sm font-medium text-gray-700">Fontes Orçamentárias</label>
              <button 
                type="button"
                @click="selecionarTodas('fontes')"
                class="text-sm text-indigo-600 hover:text-indigo-900"
              >
                {{ empresa.fontesOrcamentariasIdTerceiroPreferidas.length === fontesOrcamentariasFormatted.length ? 'Limpar Seleção' : 'Selecionar Todas' }}
              </button>
            </div>
            <multi-select
              v-model="empresa.fontesOrcamentariasIdTerceiroPreferidas"
              :options="fontesOrcamentariasFormatted"
              label="nome"
              track-by="idTerceiro"
              value-prop="idTerceiro"
              :multiple="true"
              :searchable="true"
              mode="tags"
              :max-height="200"
              :limit="100"
              placeholder="Selecione as fontes"
              class="mt-1 multiselect-virtualized"
            >
              <template v-slot:option="{ option }">
                <div class="flex items-center px-2 py-1">
                  <span class="text-sm font-medium">{{ option.nome }}</span>
                </div>
              </template>
              <template v-slot:selected-option="{ option }">
                <span class="text-sm font-medium">{{ option.nome }}</span>
              </template>
            </multi-select>
          </div>

          <!-- Poderes -->
          <div>
            <div class="flex justify-between items-center">
              <label class="block text-sm font-medium text-gray-700">Poderes</label>
              <button 
                type="button"
                @click="selecionarTodas('poderes')"
                class="text-sm text-indigo-600 hover:text-indigo-900"
              >
                {{ empresa.poderesIdTerceiroPreferidos.length === poderesFormatted.length ? 'Limpar Seleção' : 'Selecionar Todos' }}
              </button>
            </div>
            <multi-select
              v-model="empresa.poderesIdTerceiroPreferidos"
              :options="poderesFormatted"
              label="nome"
              track-by="idTerceiro"
              value-prop="idTerceiro"
              :multiple="true"
              :searchable="true"
              mode="tags"
              :max-height="200"
              :limit="100"
              placeholder="Selecione os poderes"
              class="mt-1 multiselect-virtualized"
            >
              <template v-slot:option="{ option }">
                <div class="flex items-center px-2 py-1">
                  <span class="text-sm font-medium">{{ option.nome }}</span>
                </div>
              </template>
              <template v-slot:selected-option="{ option }">
                <span class="text-sm font-medium">{{ option.nome }}</span>
              </template>
            </multi-select>
          </div>

          <!-- Tipos de Margem de Preferência -->
          <div>
            <div class="flex justify-between items-center">
              <label class="block text-sm font-medium text-gray-700">Tipos de Margem de Preferência</label>
              <button 
                type="button"
                @click="selecionarTodas('tiposMargem')"
                class="text-sm text-indigo-600 hover:text-indigo-900"
              >
                {{ empresa.tiposMargemPreferenciaIdTerceirosPreferidos.length === tiposMargemPreferenciaFormatted.length ? 'Limpar Seleção' : 'Selecionar Todos' }}
              </button>
            </div>
            <multi-select
              v-model="empresa.tiposMargemPreferenciaIdTerceirosPreferidos"
              :options="tiposMargemPreferenciaFormatted"
              label="nome"
              track-by="idTerceiro"
              value-prop="idTerceiro"
              :multiple="true"
              :searchable="true"
              mode="tags"
              :max-height="200"
              :limit="100"
              placeholder="Selecione os tipos"
              class="mt-1 multiselect-virtualized"
            >
              <template v-slot:option="{ option }">
                <div class="flex items-center px-2 py-1">
                  <span class="text-sm font-medium">{{ option.nome }}</span>
                </div>
              </template>
              <template v-slot:selected-option="{ option }">
                <span class="text-sm font-medium">{{ option.nome }}</span>
              </template>
            </multi-select>
          </div>
        </div>

        <!-- Exigência de Conteúdo Nacional -->
        <div class="mt-4">
          <div class="flex items-start">
            <div class="flex items-center h-5">
              <input
                v-model="empresa.prefereExigenciaConteudoNacional"
                type="checkbox"
                class="focus:ring-indigo-500 h-4 w-4 text-indigo-600 border-gray-300 rounded"
              />
            </div>
            <div class="ml-3 text-sm">
              <label class="font-medium text-gray-700">
                Preferência por Exigência de Conteúdo Nacional
              </label>
              <p class="text-gray-500">
                Marque se a empresa tem preferência ou requisito por licitações com exigência de Conteúdo Nacional
              </p>
            </div>
          </div>
        </div>
      </div>

      <!-- Botões -->
      <div class="flex justify-end space-x-4 mt-8">
        <button
          type="button"
          @click="$router.back()"
          class="px-4 py-2 border border-gray-300 rounded-md shadow-sm text-sm font-medium text-gray-700 bg-white hover:bg-gray-50"
        >
          Cancelar
        </button>
        <button
          type="submit"
          class="px-4 py-2 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-indigo-600 hover:bg-indigo-700"
        >
          Salvar
        </button>
      </div>
    </form>
  </div>
</template>

<script setup>
import { ref, onMounted, watch, computed } from 'vue'
import { useRouter } from 'vue-router'
import { useToast } from 'vue-toastification'
import axios from 'axios'
import MultiSelect from '@vueform/multiselect'
import '@vueform/multiselect/themes/default.css'
import OpcoesServico from '../servicos/OpcoesServico'
import EmpresaServico from '../servicos/EmpresaServico'
import VueTheMask from 'vue-the-mask'

const router = useRouter()
const toast = useToast()

const empresa = ref({
  nome: '',
  cnpj: '',
  email: '',
  ufsIdTerceiroPreferidas: [], // Array de strings (siglas das UFs)
  municipiosIdTerceiroPreferidos: [],
  modalidadesIdTerceiroPreferidas: [],
  orgaosIdTerceiroPreferidos: [],
  tiposIdTerceiroPreferidos: [],
  fontesOrcamentariasIdTerceiroPreferidas: [],
  tiposMargemPreferenciaIdTerceirosPreferidos: [],
  poderesIdTerceiroPreferidos: [],
  prefereExigenciaConteudoNacional: false
})

// Referências para as opções
const ufs = ref([])
const municipios = ref([])
const modalidades = ref([])
const orgaos = ref([])
const tipos = ref([])
const fontesOrcamentarias = ref([])
const tiposMargemPreferencia = ref([])
const poderes = ref([])

// Função auxiliar para formatar e ordenar opções
const formatOptions = (array, useIdAsSigla = false) => {
  const formatted = array?.map(item => ({
    id: item.id,
    idTerceiro: item.idTerceiro,
    nome: item.nome || item.idTerceiro, // Nome para exibição
    sigla: useIdAsSigla ? item.idTerceiro : item.nome || item.idTerceiro, // Para ordenação
    value: item.idTerceiro // Valor que será salvo
  })) || [];
  
  return formatted.sort((a, b) => (a.nome || '').localeCompare(b.nome || ''));
};

// Computed properties para os selects
const ufsFormatted = computed(() => formatOptions(ufs.value, true))
const modalidadesFormatted = computed(() => formatOptions(modalidades.value, false))
const orgaosFormatted = computed(() => formatOptions(orgaos.value, false))
const tiposFormatted = computed(() => formatOptions(tipos.value, false))
const fontesOrcamentariasFormatted = computed(() => formatOptions(fontesOrcamentarias.value, false))
const tiposMargemPreferenciaFormatted = computed(() => formatOptions(tiposMargemPreferencia.value, false))
const poderesFormatted = computed(() => formatOptions(poderes.value, false))

// Carrega as opções ao montar o componente
const carregarOpcoes = async () => {
  console.info('🔄 Iniciando carregamento das opções...');
  
  try {
    // Função auxiliar para carregar um endpoint específico
    const carregarEndpoint = async (funcao, nome) => {
      try {
        console.info(`⏳ Carregando ${nome}...`);
        const response = await funcao();
        console.info(`✅ ${nome} carregado:`, response?.data?.length || 0, 'itens');
        return response;
      } catch (error) {
        console.warn(`⚠️ Erro ao carregar ${nome}:`, error.message);
        toast.warning(`Não foi possível carregar ${nome}`);
        return { data: [] };
      }
    };

    // Carrega todos os endpoints em paralelo
    const [
      ufsResponse,
      modalidadesResponse,
      orgaosResponse,
      tiposResponse,
      fontesResponse,
      tiposMargemResponse,
      poderesResponse
    ] = await Promise.all([
      carregarEndpoint(OpcoesServico.obterUfs, 'UFs'),
      carregarEndpoint(OpcoesServico.obterModalidades, 'Modalidades'),
      carregarEndpoint(OpcoesServico.obterOrgaos, 'Órgãos'),
      carregarEndpoint(OpcoesServico.obterTipos, 'Tipos'),
      carregarEndpoint(OpcoesServico.obterFontesOrcamentarias, 'Fontes Orçamentárias'),
      carregarEndpoint(OpcoesServico.obterTiposMargemPreferencia, 'Tipos de Margem'),
      carregarEndpoint(OpcoesServico.obterPoderes, 'Poderes')
    ]);

    console.log('Dados brutos recebidos:', {
      ufs: ufsResponse?.data,
      modalidades: modalidadesResponse?.data,
      orgaos: orgaosResponse?.data,
      tipos: tiposResponse?.data,
      fontes: fontesResponse?.data,
      tiposMargemPreferencia: tiposMargemResponse?.data,
      poderes: poderesResponse?.data
    });

    // Função para filtrar itens de teste
    const filtrarItensValidos = (items = []) => {
      if (!Array.isArray(items)) {
        console.error('Dados recebidos não são um array:', items);
        return [];
      }
      return items.filter(item => !item.nome?.includes('TesteDireto'));
    };

    // Função para validar e processar dados
    const processarDados = (response, nome) => {
      if (!response?.data) {
        console.warn(`⚠️ Dados inválidos para ${nome}:`, response);
        return [];
      }

      const dados = Array.isArray(response.data) ? response.data : [];
      const dadosValidos = dados.filter(item => 
        item && 
        typeof item === 'object' && 
        'id' in item && 
        'nome' in item &&
        !item.nome?.includes('TesteDireto')
      );

      if (dadosValidos.length < dados.length) {
        console.warn(`⚠️ ${dados.length - dadosValidos.length} itens inválidos removidos de ${nome}`);
      }

      return dadosValidos;
    };

    // Atribuir dados processados para cada ref
    ufs.value = processarDados(ufsResponse, 'UFs');
    modalidades.value = processarDados(modalidadesResponse, 'Modalidades');
    orgaos.value = processarDados(orgaosResponse, 'Órgãos');
    tipos.value = processarDados(tiposResponse, 'Tipos');
    fontesOrcamentarias.value = processarDados(fontesResponse, 'Fontes Orçamentárias');
    tiposMargemPreferencia.value = processarDados(tiposMargemResponse, 'Tipos de Margem');
    poderes.value = processarDados(poderesResponse, 'Poderes');

    // Log de resumo do carregamento
    console.info('📊 Resumo do carregamento:', {
      ufs: ufs.value.length,
      modalidades: modalidades.value.length,
      orgaos: orgaos.value.length,
      tipos: tipos.value.length,
      fontes: fontesOrcamentarias.value.length,
      tiposMargemPreferencia: tiposMargemPreferencia.value.length,
      poderes: poderes.value.length
    });
  } catch (error) {
    toast.error('Erro ao carregar as opções')
    console.error('Erro ao carregar opções:', error)
    if (error.response) {
      console.error('Resposta do servidor:', error.response.data)
      console.error('Status:', error.response.status)
    }
  }
};

// Invoca o carregamento quando o componente é montado
onMounted(() => {
  console.warn('Componente montado');
  alert('Componente montado');
  carregarOpcoes();
});

// Carrega municípios quando UF é selecionada
watch(() => empresa.value.ufsIdTerceiroPreferidas, async (newUfs) => {
  if (!newUfs?.length) {
    console.info('🏙️ Limpando lista de municípios (nenhuma UF selecionada)');
    municipios.value = [];
    return;
  }

  console.info(`🔄 Carregando municípios para ${newUfs.length} UF(s)...`);
  
  try {
    const promisesComNomes = newUfs.map(ufId => {
      const ufNome = ufs.value.find(u => u.id === ufId)?.nome || ufId;
      return {
        promise: OpcoesServico.obterMunicipiosPorUf(ufId),
        ufNome
      };
    });

    const resultados = await Promise.allSettled(
      promisesComNomes.map(({ promise }) => promise)
    );

    const municipiosPorUf = resultados.map((resultado, index) => {
      const { ufNome } = promisesComNomes[index];
      
      if (resultado.status === 'rejected') {
        console.warn(`⚠️ Erro ao carregar municípios de ${ufNome}:`, resultado.reason);
        return [];
      }

      const dados = resultado.value?.data || [];
      const dadosValidos = dados.filter(m => 
        m && 
        typeof m === 'object' && 
        'id' in m && 
        'nome' in m &&
        !m.nome?.includes('TesteDireto')
      );

      console.info(`✅ ${dadosValidos.length} municípios carregados de ${ufNome}`);
      return dadosValidos;
    });

    // Combina todos os municípios em uma única lista e remove duplicatas por ID
    const municipiosUnicos = municipiosPorUf.flat().reduce((acc, atual) => {
      if (!acc.some(m => m.id === atual.id)) {
        acc.push(atual);
      }
      return acc;
    }, []);

    municipios.value = municipiosUnicos;
    console.info('📊 Total de municípios carregados:', municipiosUnicos.length);
    
  } catch (error) {
    console.error('❌ Erro ao carregar municípios:', error);
    toast.error('Erro ao carregar municípios. Por favor, tente novamente.');
  }
}, { immediate: false })

// Salva a empresa
// Função para selecionar todas as opções de um determinado tipo
const selecionarTodas = (tipo) => {
  const opcoesMap = {
    ufs: { ref: empresa.value.ufsIdTerceiroPreferidas, options: ufsFormatted.value },
    municipios: { ref: empresa.value.municipiosIdTerceiroPreferidos, options: municipios.value },
    modalidades: { ref: empresa.value.modalidadesIdTerceiroPreferidas, options: modalidadesFormatted.value },
    orgaos: { ref: empresa.value.orgaosIdTerceiroPreferidos, options: orgaosFormatted.value },
    tipos: { ref: empresa.value.tiposIdTerceiroPreferidos, options: tiposFormatted.value },
    fontes: { ref: empresa.value.fontesOrcamentariasIdTerceiroPreferidas, options: fontesOrcamentariasFormatted.value },
    tiposMargem: { ref: empresa.value.tiposMargemPreferenciaIdTerceirosPreferidos, options: tiposMargemPreferenciaFormatted.value },
    poderes: { ref: empresa.value.poderesIdTerceiroPreferidos, options: poderesFormatted.value }
  };

  const opcao = opcoesMap[tipo];
  if (!opcao) return;

  // Se já estiver tudo selecionado, limpa a seleção
  const todosIds = opcao.options.map(o => o.idTerceiro);
  const jaEstaoTodosSelecionados = todosIds.every(id => opcao.ref.includes(id));

  if (jaEstaoTodosSelecionados) {
    opcao.ref.length = 0; // Limpa o array
  } else {
    opcao.ref.push(...todosIds);
  }
};

const salvarEmpresa = async () => {
  try {
    EmpresaServico.adicionar(empresa.value);
    toast.success('Empresa cadastrada com sucesso!')
    router.push('/empresas')
  } catch (error) {
    toast.error('Erro ao cadastrar empresa')
    console.error('Erro ao salvar empresa:', error)
  }
}
</script>

<style scoped>
.multiselect-virtualized {
  --ms-max-height: 200px;
  --ms-options-height: 200px;
}

.multiselect-virtualized :deep(.multiselect-dropdown) {
  max-height: var(--ms-max-height);
  overflow-y: auto;
  scrollbar-width: thin;
}

.multiselect-virtualized :deep(.multiselect-options) {
  height: var(--ms-options-height);
  max-height: var(--ms-options-height);
}

.multiselect-virtualized :deep(.multiselect-tags) {
  flex-wrap: wrap;
  max-height: 80px;
  overflow-y: auto;
  scrollbar-width: thin;
  padding: 2px;
}

.multiselect-virtualized :deep(.multiselect-tag) {
  margin: 2px;
}

/* Estiliza a scrollbar para melhor UX */
.multiselect-virtualized :deep(*::-webkit-scrollbar) {
  width: 6px;
  height: 6px;
}

.multiselect-virtualized :deep(*::-webkit-scrollbar-track) {
  background: #f1f1f1;
  border-radius: 4px;
}

.multiselect-virtualized :deep(*::-webkit-scrollbar-thumb) {
  background: #888;
  border-radius: 4px;
}

.multiselect-virtualized :deep(*::-webkit-scrollbar-thumb:hover) {
  background: #555;
}
</style>