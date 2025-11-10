import { EntidadeBase } from "./EntidadeBase";

export interface Empresa extends EntidadeBase {
  nome: string;
  cnpj: string;
  email: string;
  orgaosIdTerceiroPreferidos: string[];
  municipiosIdTerceiroPreferidos: string[];
  modalidadesIdTerceiroPreferidas: string[];
  tiposIdTerceiroPreferidos: string[];
  ufsIdTerceiroPreferidas: string[];
  fontesOrcamentariasIdTerceiroPreferidas: string[];
  tiposMargemPreferenciaIdTerceirosPreferidos: string[];
  poderesIdTerceiroPreferidos: string[];
  prefereExigenciaConteudoNacional: boolean;
}