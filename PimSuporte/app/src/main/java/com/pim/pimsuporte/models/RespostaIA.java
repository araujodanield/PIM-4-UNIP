package com.pim.pimsuporte.models;

import com.google.gson.annotations.SerializedName;
import java.util.Date;

public class RespostaIA {

    @SerializedName("id_resposta")
    private int idRespostaIA;

    @SerializedName("fk_chamado")
    private int fkChamado;

    @SerializedName("resposta")
    private String resposta;

    @SerializedName("data_resposta")
    private Date dataResposta;

    public RespostaIA() {
    }

    public int getIdRespostaIA() {
        return idRespostaIA;
    }

    public void setIdRespostaIA(int idRespostaIA) {
        this.idRespostaIA = idRespostaIA;
    }

    public int getFkChamado() {
        return fkChamado;
    }

    public void setFkChamado(int fkChamado) {
        this.fkChamado = fkChamado;
    }

    public String getResposta() {
        return resposta;
    }

    public void setResposta(String resposta) {
        this.resposta = resposta;
    }

    public Date getDataResposta() {
        return dataResposta;
    }

    public void setDataResposta(Date dataResposta) {
        this.dataResposta = dataResposta;
    }
}