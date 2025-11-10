import { Empresa } from "@/dtos/Empresa";
import { CrudServicoBase } from "./CrudServicoBase";


class EmpresaServico extends CrudServicoBase<Empresa> {
    constructor() {
        super("Empresa"); 
    }
}

export default new EmpresaServico(); 