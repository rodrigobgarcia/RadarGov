import { EntidadeBase } from "@/dtos/EntidadeBase";
import http from "./http-common";
import { AxiosResponse } from "axios";

/**
 * Serviço base para operações de consulta (GET, OData).
 * @param TEntity O tipo da Entidade (ex: Usuario, Produto) que deve estender EntidadeBase.
 */
export abstract class ConsultaServicoBase<TEntity extends EntidadeBase> {
  protected readonly baseUrl: string;

  /**
   * @param nomeBaseDaRota O nome da rota do seu Controller (ex: 'Usuarios', 'Produtos').
   */
  constructor(nomeBaseDaRota: string) {
    this.baseUrl = nomeBaseDaRota;
  }

  /**
   * Retorna todos os registros da entidade. (GET /api/{nomeBaseDaRota})
   */
  public async obterTodos(): Promise<AxiosResponse<TEntity[]>> {
    return http.get<TEntity[]>(`/${this.baseUrl}`);
  }

  /**
   * Retorna os dados da entidade com suporte a OData. (GET /api/{nomeBaseDaRota}/OData)
   * A URL de requisição deve incluir a query OData (ex: /OData?$filter=...)
   */
  public async obterOData(query: string = ""): Promise<AxiosResponse<TEntity[]>> {
    return http.get<TEntity[]>(`/${this.baseUrl}/OData${query}`);
  }

  /**
   * Retorna uma entidade pelo ID. (GET /api/{nomeBaseDaRota}/{id})
   */
  public async obterPorId(id: number | string): Promise<AxiosResponse<TEntity>> {
    return http.get<TEntity>(`/${this.baseUrl}/${id}`);
  }
}