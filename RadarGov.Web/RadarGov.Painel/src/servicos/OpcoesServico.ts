import { EntidadeBase } from '@/dtos/EntidadeBase';
import { AxiosResponse } from 'axios';
import http from './http-common';

// Interface para representar os tipos de retorno das opções
interface OpcaoBase extends EntidadeBase {
    nome: string;
}

// Criando uma instância base do serviço


class OpcoesServico {
    protected readonly baseUrl: string;
    /**
       * @param nomeBaseDaRota O nome da rota do seu Controller (ex: 'Usuarios', 'Produtos').
       */
    constructor(nomeBaseDaRota: string) {
        this.baseUrl = nomeBaseDaRota;
    }

    async obterUfs(): Promise<AxiosResponse<OpcaoBase[]>> {
        const response = await http.get<OpcaoBase[]>(`/opcoes/ufs`);
        return response;
    }

    async obterModalidades(): Promise<AxiosResponse<OpcaoBase[]>> {
        const response = await http.get<OpcaoBase[]>(`/opcoes/modalidades`);
        return response;
    }

    async obterOrgaos(): Promise<AxiosResponse<OpcaoBase[]>> {
        const response = await http.get<OpcaoBase[]>(`/opcoes/orgaos`);
        return response;
    }

    async obterMunicipiosPorUf(uf: string): Promise<AxiosResponse<OpcaoBase[]>> {
        const response = await http.get<OpcaoBase[]>(`/opcoes/municipios?uf=${uf}`);
        return response;
    }

    async obterTipos(): Promise<AxiosResponse<OpcaoBase[]>> {
        const response = await http.get<OpcaoBase[]>(`/opcoes/tipos`);
        return response;
    }

    async obterFontesOrcamentarias(): Promise<AxiosResponse<OpcaoBase[]>> {
        const response = await http.get<OpcaoBase[]>(`/opcoes/fontes-orcamentarias`);
        return response;
    }

    async obterTiposMargemPreferencia(): Promise<AxiosResponse<OpcaoBase[]>> {
        const response = await http.get<OpcaoBase[]>(`/opcoes/tipos-margem-preferencia`);
        return response;
    }

    async obterPoderes(): Promise<AxiosResponse<OpcaoBase[]>> {
        const response = await http.get<OpcaoBase[]>(`/opcoes/poderes`);
        return response;
    }
};


export default new OpcoesServico(http.defaults.baseURL);