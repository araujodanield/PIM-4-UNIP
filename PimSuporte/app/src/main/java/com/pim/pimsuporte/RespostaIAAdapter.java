package com.pim.pimsuporte;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.pim.pimsuporte.models.RespostaIA;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;

public class RespostaIAAdapter extends RecyclerView.Adapter<RespostaIAAdapter.RespostaViewHolder> {

    private List<RespostaIA> listaRespostas;
    private SimpleDateFormat inputFormat;
    private SimpleDateFormat outputFormat;

    public RespostaIAAdapter(List<RespostaIA> listaRespostas) {
        this.listaRespostas = listaRespostas != null ? listaRespostas : new ArrayList<RespostaIA>();

        // Formato que vem da API: "2025-09-06T16:14:38.227"
        this.inputFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.SSS", Locale.getDefault());

        // Formato para exibir: "06/09/2025 16:14"
        this.outputFormat = new SimpleDateFormat("dd/MM/yyyy HH:mm", Locale.getDefault());
    }

    public static class RespostaViewHolder extends RecyclerView.ViewHolder {
        public TextView txtResposta;
        public TextView txtDataHora;

        public RespostaViewHolder(View itemView) {
            super(itemView);
            txtResposta = itemView.findViewById(R.id.txtMensagem);
            txtDataHora = itemView.findViewById(R.id.txtDataHora);
        }
    }

    @NonNull
    @Override
    public RespostaViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.mensagem_ia, parent, false);
        return new RespostaViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull RespostaViewHolder holder, int position) {
        RespostaIA resposta = listaRespostas.get(position);

        // Define o texto da resposta
        holder.txtResposta.setText(resposta.getResposta());

        // Formata a data
        if (resposta.getDataResposta() != null) {
            try {
                // Tenta converter de Date para String formatada
                String dataFormatada = outputFormat.format(resposta.getDataResposta());
                holder.txtDataHora.setText(dataFormatada);
            } catch (Exception e) {
                // Se der erro, exibe a data sem formatação
                holder.txtDataHora.setText(resposta.getDataResposta().toString());
            }
        } else {
            holder.txtDataHora.setText("");
        }
    }

    @Override
    public int getItemCount() {
        return listaRespostas.size();
    }

    public void updateList(List<RespostaIA> newList) {
        this.listaRespostas = newList != null ? newList : new ArrayList<RespostaIA>();
        notifyDataSetChanged();
    }
}