package com.pim.pimsuporte;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import java.util.ArrayList;
import java.util.List;
import com.pim.pimsuporte.models.Chamado;

public class TicketAdapter extends RecyclerView.Adapter<TicketAdapter.TicketViewHolder> {

    private List<Chamado> listaTickets;
    private OnTicketClickListener listener;

    // Interface para o click
    public interface OnTicketClickListener {
        void onTicketClick(Chamado chamado, int position);
    }

    // Construtor atualizado com listener
    public TicketAdapter(List<Chamado> listaTickets, OnTicketClickListener listener) {
        this.listaTickets = listaTickets != null ? listaTickets : new ArrayList<>();
        this.listener = listener;
    }

    // ViewHolder
    public class TicketViewHolder extends RecyclerView.ViewHolder {
        public TextView txtTituloTicket;
        public TextView txtStatusTicket;

        public TicketViewHolder(View itemView) {
            super(itemView);

            txtTituloTicket = itemView.findViewById(R.id.txtTituloTicket);
            txtStatusTicket = itemView.findViewById(R.id.txtStatusTicket);

            // Configura o click no item inteiro
            itemView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    int position = getAdapterPosition();
                    if (position != RecyclerView.NO_POSITION && listener != null) {
                        listener.onTicketClick(listaTickets.get(position), position);
                    }
                }
            });
        }
    }

    @NonNull
    @Override
    public TicketViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.item_ticket, parent, false);
        return new TicketViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull TicketViewHolder holder, int position) {
        Chamado currentChamado = listaTickets.get(position);

        holder.txtTituloTicket.setText(currentChamado.getTitulo());
        holder.txtStatusTicket.setText("Status: " + currentChamado.getStatus());
    }

    @Override
    public int getItemCount() {
        return listaTickets.size();
    }

    // Método para atualizar a lista
    public void updateList(List<Chamado> newList) {
        this.listaTickets = newList != null ? newList : new ArrayList<>();
        notifyDataSetChanged();
    }
}