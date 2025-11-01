package com.pim.pimsuporte.models;

import java.io.Serializable;
import com.google.gson.annotations.SerializedName;

public class Categoria implements Serializable {

    @SerializedName("id_categoria")
    private int idCategoria;

    @SerializedName("categoria")
    private String nome;

    public Categoria() {
    }

    public int getIdCategoria() {
        return idCategoria;
    }

    public String getNome() {
        return nome;
    }

    // Setters
    public void setIdCategoria(int idCategoria) {
        this.idCategoria = idCategoria;
    }

    public void setNome(String nome) {
        this.nome = nome;
    }
}

