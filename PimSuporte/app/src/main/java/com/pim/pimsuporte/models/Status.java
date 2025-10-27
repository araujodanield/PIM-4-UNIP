package com.pim.pimsuporte.models;

import java.io.Serializable;
import com.google.gson.annotations.SerializedName;

public class Status implements Serializable {

    @SerializedName("id_status")
    private int idStatus;

    @SerializedName("status")
    private String nome;

    public int getIdStatus() {
        return idStatus;
    }

    public String getNome() {
        return nome;
    }

    public void setIdStatus(int idStatus) {
        this.idStatus = idStatus;
    }

    public void setNome(String nome) {
        this.nome = nome;
    }

}
