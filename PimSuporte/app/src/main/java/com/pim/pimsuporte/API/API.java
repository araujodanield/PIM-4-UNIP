package com.pim.pimsuporte.API;

import com.pim.pimsuporte.models.Chamado;
import com.pim.pimsuporte.models.ChamadosUpdate;
import com.pim.pimsuporte.models.Categoria;
import com.pim.pimsuporte.models.Status;
import com.pim.pimsuporte.models.Usuario;
import com.pim.pimsuporte.models.RespostaIA;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface API {



    // Serve para listar todos os chamados
    @GET("api/chamados")
    Call<List<Chamado>> listarTodosChamados();

    //  Serve para buscar chamado por ID
    @GET("api/chamados/{id}")
    Call<Chamado> buscarChamadoPorId(@Path("id") int idChamado);

    //  Serve para atualizar chamado
    @PUT("api/chamados/{id}")
    Call<Void> atualizarChamado(
            @Path("id") int idChamado,
            @Body ChamadosUpdate chamadoUpdate
    );


    //  Serve para listar todas as respostas da IA
    @GET("api/respostas-ia")
    Call<List<RespostaIA>> listarRespostasIA();

    //  Serve para buscar respostas por ID do chamado
    @GET("api/respostas-ia/chamado/{id}")
    Call<List<RespostaIA>> buscarRespostasPorChamado(@Path("id") int idChamado);

    //  Serve para listar todos os usuários
    @GET("api/usuarios")
    Call<List<Usuario>> listarTodosUsuarios();

    //  Serve para listar todas as categorias
    @GET("api/categorias")
    Call<List<Categoria>> listarCategorias();

    //  Serve para listar todos os status
    @GET("api/status")
    Call<List<Status>> listarStatus();
}