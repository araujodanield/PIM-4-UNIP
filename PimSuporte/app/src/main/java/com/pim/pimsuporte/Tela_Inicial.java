package com.pim.pimsuporte;

import android.content.Intent;
import android.os.Bundle;
import android.view.View; // Importação adicionada para View.VISIBLE/GONE
import android.widget.ProgressBar; // Importação adicionada
import android.widget.Toast;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;
import java.util.ArrayList;
import java.util.List;

// Importações para a API (Retrofit e seus modelos)
import com.pim.pimsuporte.models.Chamado;
import com.pim.pimsuporte.API.API;
import com.pim.pimsuporte.API.retrofitclient;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class Tela_Inicial extends AppCompatActivity {

    private static final int ID_USUARIO_LOGADO = 1;

    // Campos para a lista de tickets
    private RecyclerView recyclerView;
    private TicketAdapter adapter;
    private API apiService;

    // NOVO CAMPO: ProgressBar
    private ProgressBar loadingSpinner;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_tela_inicial);

        // Configuração padrão de Insets
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });

        // Inicializa as Views
        recyclerView = findViewById(R.id.recyclerViewTickets);
        // Inicializa a ProgressBar
        loadingSpinner = findViewById(R.id.loading_spinner);

        // Configuração do RecyclerView
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        recyclerView.setHasFixedSize(true);

        // Inicializa o serviço da API
        apiService = retrofitclient.getApiService();

        // Inicia o carregamento dos tickets

        carregarTickets();
    }

    private void carregarTickets() {

        loadingSpinner.setVisibility(View.VISIBLE);
        recyclerView.setVisibility(View.GONE);

        // Executa a chamada de API
        Call<List<Chamado>> call = apiService.listarTodosChamados();

        call.enqueue(new Callback<List<Chamado>>() {


            @Override
            public void onResponse(Call<List<Chamado>> call, Response<List<Chamado>> response) {


                loadingSpinner.setVisibility(View.GONE);

                if (response.isSuccessful() && response.body() != null) {
                    List<Chamado> listarTodosChamados = response.body();

                    // Filtra apenas os tickets do usuário logado
                    List<Chamado> meusTickets = new ArrayList<>();
                    for (Chamado ticket : listarTodosChamados) {
                        if (ticket.getFkUsuario() == ID_USUARIO_LOGADO) {
                            meusTickets.add(ticket);
                        }
                    }

                    // Verifica se o usuário tem tickets
                    if (meusTickets.isEmpty()) {
                        Toast.makeText(Tela_Inicial.this,
                                "Você ainda não tem tickets abertos.",
                                Toast.LENGTH_LONG).show();
                        // Lista permanece GONE
                        recyclerView.setVisibility(View.GONE);
                    } else {
                        Toast.makeText(Tela_Inicial.this,
                                meusTickets.size() + " ticket(s) encontrado(s)",
                                Toast.LENGTH_SHORT).show();


                        recyclerView.setVisibility(View.VISIBLE);
                    }

                    adapter = new TicketAdapter(meusTickets, new TicketAdapter.OnTicketClickListener() {
                        @Override
                        public void onTicketClick(Chamado chamado, int position) {

                            // Abre a tela de detalhes quando clicar em um ticket
                            abrirTelaDetalhes(chamado);
                        }
                    });
                    recyclerView.setAdapter(adapter);

                } else {
                    // Trata erros HTTP (ex: 404, 500)
                    Toast.makeText(Tela_Inicial.this,
                            "Erro ao carregar tickets. Código: " + response.code(),
                            Toast.LENGTH_LONG).show();

                    // ERRO: Lista permanece GONE
                    recyclerView.setVisibility(View.GONE);
                }
            }

            @Override
            public void onFailure(Call<List<Chamado>> call, Throwable t) {

                loadingSpinner.setVisibility(View.GONE);
                recyclerView.setVisibility(View.GONE);

                // Trata erros de rede
                Toast.makeText(Tela_Inicial.this,
                        "Falha de Conexão: " + t.getMessage(),
                        Toast.LENGTH_LONG).show();
                t.printStackTrace();
            }
        });
    }

    private void abrirTelaDetalhes(Chamado chamado) {
        Intent intent = new Intent(Tela_Inicial.this, detalhe_ticket.class);

        intent.putExtra("TICKET_ID", chamado.getIdChamado());
        intent.putExtra("TICKET_TITULO", chamado.getTitulo());
        intent.putExtra("TICKET_STATUS", chamado.getStatus());
        intent.putExtra("TICKET_DESCRICAO", chamado.getDescricao());

        startActivity(intent);
    }

    @Override
    protected void onResume() {
        super.onResume();

        // Recarrega os tickets quando voltar para esta tela
        carregarTickets();
    }
}