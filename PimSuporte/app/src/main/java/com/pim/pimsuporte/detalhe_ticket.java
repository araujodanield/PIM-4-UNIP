package com.pim.pimsuporte;

import android.content.DialogInterface;
import android.os.Bundle;
import android.os.Handler;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.pim.pimsuporte.API.API;
import com.pim.pimsuporte.API.retrofitclient;
import com.pim.pimsuporte.models.Chamado;
import com.pim.pimsuporte.models.ChamadosUpdate;
import com.pim.pimsuporte.models.RespostaIA;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class detalhe_ticket extends AppCompatActivity {

    private static final String TAG = "DETALHE_TICKET";

    private Button botaoNao;
    private Button botaoSim;

    // Referência ao contêiner completo que você quer ocultar
    private LinearLayout caixaMensagem;

    private TextView txtTitulo;
    private TextView txtStatus;
    private TextView txtDescricao;
    private RecyclerView recyclerViewRespostas;
    private RespostaIAAdapter respostaAdapter;
    private API apiService;
    private int ticketId;

    // Guardar dados completos do chamado
    private Chamado chamadoOriginal;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_detalhe_ticket);

        Log.d(TAG, "Activity iniciada");

        // Inicializa a API
        apiService = retrofitclient.getApiService();
        Log.d(TAG, "API inicializada");

        // Inicializa as views
        botaoNao = findViewById(R.id.buttonNao);
        botaoSim = findViewById(R.id.buttonSim);
        txtTitulo = findViewById(R.id.txtTitulo);
        txtStatus = findViewById(R.id.txtStatus);
        txtDescricao = findViewById(R.id.txtDescricao);
        recyclerViewRespostas = findViewById(R.id.recyclerViewRespostasIA);

        //  Inicializa o LinearLayout pai
        caixaMensagem = findViewById(R.id.caixa_mensagem);

        if (recyclerViewRespostas != null) {
            recyclerViewRespostas.setLayoutManager(new LinearLayoutManager(this));
            recyclerViewRespostas.setNestedScrollingEnabled(false);
        }

        // Recebe os dados do Intent
        Bundle extras = getIntent().getExtras();
        if (extras != null) {
            ticketId = extras.getInt("TICKET_ID", -1);
            String titulo = extras.getString("TICKET_TITULO", "");
            String status = extras.getString("TICKET_STATUS", ""); // Pega o status
            String descricao = extras.getString("TICKET_DESCRICAO", "");

            Log.d(TAG, "Ticket ID: " + ticketId);

            if (txtTitulo != null) txtTitulo.setText(titulo);
            if (txtStatus != null) txtStatus.setText("Status: " + status);
            if (txtDescricao != null) txtDescricao.setText(descricao);

            controlarBotoesPeloStatus(status);

            if (ticketId > 0) {
                // Buscar dados completos do chamado da API
                buscarDadosChamado(ticketId);
                // Carregar respostas
                carregarRespostasIA(ticketId);
            }
        }

        // Configura os botões
        if (botaoNao != null) {
            botaoNao.setOnClickListener(v -> mostrarDialogNao());
        }

        if (botaoSim != null) {
            botaoSim.setOnClickListener(v -> mostrarDialogSim());
        }
    }

    private void buscarDadosChamado(int idChamado) {
        Log.d(TAG, "Buscando dados do chamado " + idChamado);

        Call<Chamado> call = apiService.buscarChamadoPorId(idChamado);

        call.enqueue(new Callback<Chamado>() {
            @Override
            public void onResponse(Call<Chamado> call, Response<Chamado> response) {
                if (response.isSuccessful() && response.body() != null) {
                    chamadoOriginal = response.body();
                    Log.d(TAG, "Dados do chamado carregados:");
                    Log.d(TAG, "  - ID: " + chamadoOriginal.getIdChamado());
                    Log.d(TAG, "  - Status: " + chamadoOriginal.getStatus());

                    // Re-controlar após carregar dados da API
                    controlarBotoesPeloStatus(chamadoOriginal.getStatus());

                } else {
                    Log.e(TAG, "Erro ao buscar chamado: " + response.code());
                    Toast.makeText(detalhe_ticket.this,
                            "Erro ao carregar dados do chamado",
                            Toast.LENGTH_SHORT).show();
                }
            }

            @Override
            public void onFailure(Call<Chamado> call, Throwable t) {
                Log.e(TAG, "Falha ao buscar chamado: " + t.getMessage());
                Toast.makeText(detalhe_ticket.this,
                        "Erro de conexão",
                        Toast.LENGTH_SHORT).show();
            }
        });
    }

    private void carregarRespostasIA(int idChamado) {
        Log.d(TAG, "Carregando respostas para chamado " + idChamado);

        Call<List<RespostaIA>> call = apiService.listarRespostasIA();

        call.enqueue(new Callback<List<RespostaIA>>() {
            @Override
            public void onResponse(Call<List<RespostaIA>> call, Response<List<RespostaIA>> response) {
                if (response.isSuccessful() && response.body() != null) {
                    List<RespostaIA> todasRespostas = response.body();
                    List<RespostaIA> respostasFiltradas = new ArrayList<>();

                    for (RespostaIA resposta : todasRespostas) {
                        if (resposta.getFkChamado() == idChamado) {
                            respostasFiltradas.add(resposta);
                        }
                    }

                    if (!respostasFiltradas.isEmpty()) {
                        respostaAdapter = new RespostaIAAdapter(respostasFiltradas);
                        recyclerViewRespostas.setAdapter(respostaAdapter);
                        Log.d(TAG, respostasFiltradas.size() + " respostas encontradas");
                    }
                }
            }

            @Override
            public void onFailure(Call<List<RespostaIA>> call, Throwable t) {
                Log.e(TAG, "Falha ao carregar respostas: " + t.getMessage());
            }
        });
    }

    public void onVoltarClick(View view) {
        finish();
    }

    private void mostrarDialogNao() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle("Encaminhar para Técnico");
        builder.setMessage("A solução da IA não resolveu? Vamos encaminhar para um técnico.");

        builder.setPositiveButton("Confirmar", (dialog, which) -> {
            atualizarParaEmAndamento();
        });

        builder.setNegativeButton("Cancelar", (dialog, which) -> dialog.dismiss());

        builder.create().show();
    }

    private void mostrarDialogSim() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle("Confirmar Resolução");
        builder.setMessage("A solução da IA resolveu seu problema? O ticket será fechado.");

        builder.setPositiveButton("Sim, resolveu!", (dialog, which) -> {
            finalizarChamado();
        });

        builder.setNegativeButton("Cancelar", (dialog, which) -> dialog.cancel());

        builder.create().show();
    }


    private void atualizarParaEmAndamento() {
        if (chamadoOriginal == null) {
            Toast.makeText(this, "Aguarde carregar os dados...", Toast.LENGTH_SHORT).show();
            return;
        }

        Log.d(TAG, "========================================");
        Log.d(TAG, "ATUALIZANDO PARA EM ANDAMENTO");
        Log.d(TAG, "========================================");

        ChamadosUpdate chamado = new ChamadosUpdate();

        // Manter dados originais
        chamado.setFkUsuario(chamadoOriginal.getFkUsuario());
        chamado.setFkCategoria(chamadoOriginal.getFkCategoria());
        chamado.setFkPrioridade(chamadoOriginal.getFkPrioridade());
        chamado.setTitulo(chamadoOriginal.getTitulo());
        chamado.setDescricao(chamadoOriginal.getDescricao());

        // ALTERAÇÕES
        chamado.setFkStatus(2);
        chamado.setFkTecnico(2);
        chamado.setFkAvaliacao(chamadoOriginal.getFkAvaliacao());
        chamado.setResolvidoIA(false);
        chamado.setComentarioTecnico(null);


        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss", Locale.getDefault());
        chamado.setDataAbertura(sdf.format(chamadoOriginal.getDataAbertura()));


        if (chamadoOriginal.getDataEncerramento() != null) {
            chamado.setDataEncerramento(sdf.format(chamadoOriginal.getDataEncerramento()));
        } else {
            // Garante que a data de encerramento seja nula se for nula no original
            chamado.setDataEncerramento(null);
        }


        Log.d(TAG, "Enviando: fk_status=2 (Em Andamento)");

        Call<Void> call = apiService.atualizarChamado(ticketId, chamado);

        call.enqueue(new Callback<Void>() {
            @Override
            public void onResponse(Call<Void> call, Response<Void> response) {
                Log.d(TAG, "Resposta: " + response.code());

                if (response.isSuccessful()) {
                    Log.d(TAG, "✅ SUCESSO!");

                    txtStatus.setText("Status: Em Andamento");
                    Toast.makeText(detalhe_ticket.this,
                            "Chamado encaminhado para técnico!",
                            Toast.LENGTH_LONG).show();

                    // Oculta a caixa de mensagem completa no sucesso
                    if (caixaMensagem != null) caixaMensagem.setVisibility(View.GONE);

                    new Handler().postDelayed(() -> finish(), 2000);
                } else {
                    Log.e(TAG, "❌ ERRO: " + response.code());

                    // Ler corpo do erro
                    try {
                        if (response.errorBody() != null) {
                            String errorBody = response.errorBody().string();
                            Log.e(TAG, "Corpo do erro: " + errorBody);
                        }
                    } catch (Exception e) {
                        Log.e(TAG, "Erro ao ler errorBody: " + e.getMessage());
                    }

                    Toast.makeText(detalhe_ticket.this,
                            "Erro ao atualizar: " + response.code(),
                            Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<Void> call, Throwable t) {
                Log.e(TAG, "❌ FALHA: " + t.getMessage());
                t.printStackTrace();
                Toast.makeText(detalhe_ticket.this,
                        "Falha de conexão: " + t.getMessage(),
                        Toast.LENGTH_LONG).show();
            }
        });
    }

    private void finalizarChamado() {
        if (chamadoOriginal == null) {
            Toast.makeText(this, "Aguarde carregar os dados...", Toast.LENGTH_SHORT).show();
            return;
        }

        Log.d(TAG, "========================================");
        Log.d(TAG, "FINALIZANDO CHAMADO");
        Log.d(TAG, "========================================");

        ChamadosUpdate chamado = new ChamadosUpdate();

        // Manter dados originais
        chamado.setFkUsuario(chamadoOriginal.getFkUsuario());
        chamado.setFkCategoria(chamadoOriginal.getFkCategoria());
        chamado.setFkPrioridade(chamadoOriginal.getFkPrioridade());
        chamado.setFkTecnico(chamadoOriginal.getFkTecnico());
        chamado.setTitulo(chamadoOriginal.getTitulo());
        chamado.setDescricao(chamadoOriginal.getDescricao());

        // ALTERAÇÕES
        chamado.setFkStatus(4);  // Finalizado
        chamado.setFkAvaliacao(2);  // Satisfeito (MOCK: Ajuste se necessário)
        chamado.setResolvidoIA(true);  // IA resolveu!
        chamado.setComentarioTecnico(null);

        // Datas
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss", Locale.getDefault());
        chamado.setDataAbertura(sdf.format(chamadoOriginal.getDataAbertura()));
        chamado.setDataEncerramento(sdf.format(new Date()));  // Data de encerramento AGORA

        Log.d(TAG, "Enviando: fk_status=4 (Finalizado)");

        Call<Void> call = apiService.atualizarChamado(ticketId, chamado);

        call.enqueue(new Callback<Void>() {
            @Override
            public void onResponse(Call<Void> call, Response<Void> response) {
                Log.d(TAG, "Resposta: " + response.code());

                if (response.isSuccessful()) {
                    Log.d(TAG, "✅ SUCESSO!");

                    txtStatus.setText("Status: Finalizado");
                    Toast.makeText(detalhe_ticket.this,
                            "Ticket fechado com sucesso! Obrigado pela avaliação.",
                            Toast.LENGTH_LONG).show();

                    // Oculta a caixa de mensagem completa no sucesso
                    if (caixaMensagem != null) caixaMensagem.setVisibility(View.GONE);

                    new Handler().postDelayed(() -> finish(), 2000);
                } else {
                    Log.e(TAG, "❌ ERRO: " + response.code());

                    // Ler corpo do erro
                    try {
                        if (response.errorBody() != null) {
                            String errorBody = response.errorBody().string();
                            Log.e(TAG, "Corpo do erro: " + errorBody);
                        }
                    } catch (Exception e) {
                        Log.e(TAG, "Erro ao ler errorBody: " + e.getMessage());
                    }

                    Toast.makeText(detalhe_ticket.this,
                            "Erro ao finalizar: " + response.code(),
                            Toast.LENGTH_LONG).show();
                }
            }

            @Override
            public void onFailure(Call<Void> call, Throwable t) {
                Log.e(TAG, "❌ FALHA: " + t.getMessage());
                t.printStackTrace();
                Toast.makeText(detalhe_ticket.this,
                        "Falha de conexão: " + t.getMessage(),
                        Toast.LENGTH_LONG).show();
            }
        });
    }


    private void controlarBotoesPeloStatus(String statusAtual) {

        // Verifica se a caixa de mensagem foi inicializada
        if (caixaMensagem == null) {
            return;
        }

        // Remove espaços em branco e ignora maiúsculas/minúsculas para comparação
        String statusLimpo = statusAtual.trim().toLowerCase(Locale.getDefault());

        // A caixa deve ser OCULTADA se o status indicar que a interação do usuário terminou
        boolean deveOcultar = statusLimpo.contains("em andamento") ||
                statusLimpo.contains("finalizado");

        // Aplica a lógica de ocultamento
        if (deveOcultar) {
            caixaMensagem.setVisibility(View.GONE);
            Log.d(TAG, "Controle UI: Caixa de Mensagem Oculta. Status: " + statusAtual);
        } else {
            caixaMensagem.setVisibility(View.VISIBLE);
            Log.d(TAG, "Controle UI: Caixa de Mensagem Visível. Status: " + statusAtual);
        }
    }

    // Compatibilidade com android:onClick no XML
    public void onNaoClick(View view) {
        mostrarDialogNao();
    }

    public void onSimClick(View view) {
        mostrarDialogSim();
    }
}