import { Licitacao } from '@/dtos/Licitacao'
import { CrudServicoBase } from './CrudServicoBase'
import http from './http-common'

class LicitacaoServico extends CrudServicoBase<Licitacao> {
  constructor() {
    super('Licitacao')
  }

  public async obterRecomendadas(empresaId: number) {
    return http.get<Licitacao[]>(`/Licitacao/recomendacoes?empresaId=${empresaId}`)
  }
}

export default new LicitacaoServico()
