package com.pim.pimsuporte.models;

import java.io.Serializable;
import java.util.Date;
import com.google.gson.annotations.SerializedName;

public class Chamado implements Serializable {


    @SerializedName("id_chamado")
    private int idChamado;


    @SerializedName("fk_usuario")
    private int fkUsuario;
    @SerializedName("fk_categoria")
    private int fkCategoria;
    @SerializedName("fk_prioridade")
    private int fkPrioridade;
    @SerializedName("fk_status")
    private int fkStatus;
    @SerializedName("fk_tecnico")
    private int fkTecnico;
    @SerializedName("fk_avaliacao")
    private int fkAvaliacao;


    @SerializedName("usuario_emissor")
    private String usuarioEmissor;
    @SerializedName("categoria")
    private String categoria;
    @SerializedName("prioridade")
    private String prioridade;
    @SerializedName("status")
    private String status;
    @SerializedName("tecnico")
    private String tecnico; // Nome do técnico responsável
    @SerializedName("avaliacao")
    private String avaliacao;


    @SerializedName("titulo")
    private String titulo;
    @SerializedName("descricao")
    private String descricao;
    @SerializedName("resolvido_ia")
    private boolean resolvidoIA;
    @SerializedName("comentario_tecnico")
    private String comentarioTecnico;


    @SerializedName("data_abertura")
    private Date dataAbertura;
    @SerializedName("data_encerramento")
    private Date dataEncerramento;


    public Chamado() {
    }

    public int getFkUsuario() {
        return fkUsuario;
    }

    public int getFkCategoria() {
        return fkCategoria;
    }

    public int getFkPrioridade() {
        return fkPrioridade;
    }

    public int getFkStatus() {
        return fkStatus;
    }

    public int getFkTecnico() {
        return fkTecnico;
    }

    public int getFkAvaliacao() {
        return fkAvaliacao;
    }

    public int getIdChamado() {
        return idChamado;
    }

    public String getUsuarioEmissor() {
        return usuarioEmissor;
    }

    public String getDescricao() {
        return descricao;
    }

    public Date getDataAbertura() {
        return dataAbertura;
    }


    public Date getDataEncerramento() {
        return dataEncerramento;
    }

    public String getTitulo() {
        return titulo;
    }

    public String getStatus() {
        return status;
    }


    public String getCategoria() {
        return categoria;
    }

    public String getPrioridade() {
        return prioridade;
    }
}