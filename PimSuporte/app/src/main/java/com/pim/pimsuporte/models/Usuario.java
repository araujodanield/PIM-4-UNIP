package com.pim.pimsuporte.models;
import java.io.Serializable;
import com.google.gson.annotations.SerializedName;

public class Usuario implements Serializable {

    // JSON: "id_usuario" -> Java: idUsuario
    @SerializedName("id_usuario")
    private int idUsuario;

    // JSON: "fk_tipo_usuario" -> Java: fkTipoUsuario
    @SerializedName("fk_tipo_usuario")
    private int fkTipoUsuario;

    // JSON: "tipo_usuario" -> Java: tipoUsuario
    @SerializedName("tipo_usuario")
    private String tipoUsuario;

    // JSON: "nome" -> Java: nome
    @SerializedName("nome")
    private String nome;

    // JSON: "email" -> Java: email
    @SerializedName("email")
    private String email;

    public Usuario() {
    }

    public int getIdUsuario() {
        return idUsuario;
    }

    public int getFkTipoUsuario() {
        return fkTipoUsuario;
    }

    public String getTipoUsuario() {
        return tipoUsuario;
    }

    public String getNome() {
        return nome;
    }

    public String getEmail() {
        return email;
    }


    public void setIdUsuario(int idUsuario) {
        this.idUsuario = idUsuario;
    }

    public void setFkTipoUsuario(int fkTipoUsuario) {
        this.fkTipoUsuario = fkTipoUsuario;
    }

    public void setTipoUsuario(String tipoUsuario) {
        this.tipoUsuario = tipoUsuario;
    }

    public void setNome(String nome) {
        this.nome = nome;
    }

    public void setEmail(String email) {
        this.email = email;
    }
}
