import { EntidadeBase } from './EntidadeBase'

export interface Licitacao extends EntidadeBase {
  titulo: string
  descricao?: string
  itemUrl?: string
  valorGlobal?: number
  dataPublicacaoPncp?: string | null
  orgaoIdTerceiro?: string | null
  municipioIdTerceiro?: string | null
  modalidadeIdTerceiro?: string | null
  ufIdTerceiro?: string | null
  exigenciaConteudoNacional?: boolean
}
