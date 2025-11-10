import { CrudServicoBase } from "./CrudServicoBase";
import http from "./http-common";
import { Usuario } from "@/dtos/Usuario";


class UsuarioService extends CrudServicoBase<Usuario> {
    constructor() {
        super("Usuarios"); 
    }

    public async buscarPorEmail(email: string) {
        return http.get<Usuario[]>(`/${this.baseUrl}/PorEmail?email=${email}`);
    }
}

export default new UsuarioService();