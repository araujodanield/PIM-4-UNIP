package com.pim.pimsuporte.models;

import com.google.gson.annotations.SerializedName;

public class ChamadosUpdate {

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

    @SerializedName("titulo")
    private String titulo;

    @SerializedName("descricao")
    private String descricao;

    @SerializedName("resolvido_IA")
    private boolean resolvidoIA;

    @SerializedName("comentario_tecnico")
    private String comentarioTecnico;

    @SerializedName("data_abertura")
    private String dataAbertura;

    @SerializedName("data_encerramento")
    private String dataEncerramento;

    // Construtor vazio
    public ChamadosUpdate() {
    }

    // Getters
    public int getFkUsuario() { return fkUsuario; }
    public int getFkCategoria() { return fkCategoria; }
    public int getFkPrioridade() { return fkPrioridade; }
    public int getFkStatus() { return fkStatus; }
    public int getFkTecnico() { return fkTecnico; }
    public int getFkAvaliacao() { return fkAvaliacao; }
    public String getTitulo() { return titulo; }
    public String getDescricao() { return descricao; }
    public boolean isResolvidoIA() { return resolvidoIA; }
    public String getComentarioTecnico() { return comentarioTecnico; }
    public String getDataAbertura() { return dataAbertura; }
    public String getDataEncerramento() { return dataEncerramento; }
    // Setters
    public void setFkUsuario(int fkUsuario) {
        this.fkUsuario = fkUsuario;
    }

    public void setFkCategoria(int fkCategoria) {
        this.fkCategoria = fkCategoria;
    }

    public void setFkPrioridade(int fkPrioridade) {
        this.fkPrioridade = fkPrioridade;
    }

    public void setFkStatus(int fkStatus) {
        this.fkStatus = fkStatus;
    }

    public void setFkTecnico(int fkTecnico) {
        this.fkTecnico = fkTecnico;
    }

    public void setFkAvaliacao(int fkAvaliacao) {
        this.fkAvaliacao = fkAvaliacao;
    }

    public void setTitulo(String titulo) {
        this.titulo = titulo;
    }

    public void setDescricao(String descricao) {
        this.descricao = descricao;
    }

    public void setResolvidoIA(boolean resolvidoIA) {
        this.resolvidoIA = resolvidoIA;
    }

    public void setComentarioTecnico(String comentarioTecnico) {
        this.comentarioTecnico = comentarioTecnico;
    }

    public void setDataAbertura(String dataAbertura) {
        this.dataAbertura = dataAbertura;
    }

    public void setDataEncerramento(String dataEncerramento) {
        this.dataEncerramento = dataEncerramento;
    }
}