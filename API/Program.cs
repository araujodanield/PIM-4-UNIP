using Microsoft.Data.SqlClient;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => 
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowAll");

// String de conexão
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new Exception("String de conexão não encontrada!");

// ============================================
// ENDPOINTS - USUÁRIOS
// ============================================

// GET - Listar todos os usuários
app.MapGet("/api/usuarios", async () =>
{
    var usuarios = new List<object>();
    
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            
            string query = @"
                SELECT 
                    u.id_usuario,
                    u.FK_tipo_usuario,
                    t.tipo_usuario,
                    u.nome,
                    u.email
                FROM TBusuarios u
                INNER JOIN TBtipos_usuario t ON u.FK_tipo_usuario = t.id_tipo_usuario";
            
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    usuarios.Add(new
                    {
                        id_usuario = reader.GetInt32(0),
                        fk_tipo_usuario = reader.GetInt32(1),
                        tipo_usuario = reader.GetString(2),
                        nome = reader.GetString(3),
                        email = reader.GetString(4)
                    });
                }
            }
        }
        
        return Results.Ok(usuarios);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// GET - Buscar usuário por ID
app.MapGet("/api/usuarios/{id}", async (int id) =>
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            
            string query = @"
                SELECT 
                    u.id_usuario,
                    u.FK_tipo_usuario,
                    t.tipo_usuario,
                    u.nome,
                    u.email
                FROM TBusuarios u
                INNER JOIN TBtipos_usuario t ON u.FK_tipo_usuario = t.id_tipo_usuario
                WHERE u.id_usuario = @Id";
            
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return Results.Ok(new
                        {
                            id_usuario = reader.GetInt32(0),
                            fk_tipo_usuario = reader.GetInt32(1),
                            tipo_usuario = reader.GetString(2),
                            nome = reader.GetString(3),
                            email = reader.GetString(4)
                        });
                    }
                }
            }
        }
        
        return Results.NotFound(new { mensagem = "Usuário não encontrado" });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// POST - Criar novo usuário
app.MapPost("/api/usuarios", async (Usuario usuario) =>
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            
            string query = @"
                INSERT INTO TBusuarios (FK_tipo_usuario, nome, email, senha) 
                VALUES (@FK_tipo_usuario, @Nome, @Email, @Senha)";
            
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@FK_tipo_usuario", usuario.FK_tipo_usuario);
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                
                await cmd.ExecuteNonQueryAsync();
            }
        }
        
        return Results.Ok(new { mensagem = "Usuário criado com sucesso!" });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// PUT - Atualizar usuário
app.MapPut("/api/usuarios/{id}", async (int id, Usuario usuario) =>
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            
            string query = @"
                UPDATE TBusuarios 
                SET FK_tipo_usuario = @FK_tipo_usuario,
                    nome = @Nome,
                    email = @Email,
                    senha = @Senha
                WHERE id_usuario = @Id";
            
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@FK_tipo_usuario", usuario.FK_tipo_usuario);
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);
                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                
                int linhas = await cmd.ExecuteNonQueryAsync();
                
                if (linhas == 0)
                    return Results.NotFound(new { mensagem = "Usuário não encontrado" });
            }
        }
        
        return Results.Ok(new { mensagem = "Usuário atualizado com sucesso!" });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// DELETE - Remover usuário
app.MapDelete("/api/usuarios/{id}", async (int id) =>
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            
            string query = "DELETE FROM TBusuarios WHERE id_usuario = @Id";
            
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                
                int linhas = await cmd.ExecuteNonQueryAsync();
                
                if (linhas == 0)
                    return Results.NotFound(new { mensagem = "Usuário não encontrado" });
            }
        }
        
        return Results.Ok(new { mensagem = "Usuário removido com sucesso!" });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// ============================================
// ENDPOINTS - CHAMADOS
// ============================================

// GET - Listar todos os chamados (com JOIN)
app.MapGet("/api/chamados", async () =>
{
    var chamados = new List<object>();
    
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            
            string query = @"
                SELECT
                    c.id_chamado,
                    c.FK_usuario,
                    u.nome AS usuario_emissor,
                    c.FK_categoria,
                    cat.categoria,
                    c.FK_prioridade,
                    p.prioridade,
                    c.FK_status,
                    s.status,
                    c.FK_tecnico,
                    t.nome AS tecnico,
                    c.FK_avaliacao,
                    a.descricao AS avaliacao,
                    c.titulo,
                    c.descricao,
                    c.Resolvido_IA,
                    c.comentario_tecnico,
                    c.data_abertura,
                    c.data_encerramento
                FROM TBchamados c
                INNER JOIN TBusuarios u ON c.FK_usuario = u.id_usuario
                INNER JOIN TBcategorias cat ON c.FK_categoria = cat.id_categoria
                INNER JOIN TBniveis_prioridade p ON c.FK_prioridade = p.id_prioridade
                INNER JOIN TBstatus s ON c.FK_status = s.id_status
                INNER JOIN TBusuarios t ON c.FK_tecnico = t.id_usuario
                INNER JOIN TBavaliacao a ON c.FK_avaliacao = a.id_avaliacao";
            
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    chamados.Add(new
                    {
                        id_chamado = reader.GetInt32(0),
                        fk_usuario = reader.GetInt32(1),
                        usuario_emissor = reader.GetString(2),
                        fk_categoria = reader.GetInt32(3),
                        categoria = reader.GetString(4),
                        fk_prioridade = reader.GetInt32(5),
                        prioridade = reader.GetString(6),
                        fk_status = reader.GetInt32(7),
                        status = reader.GetString(8),
                        fk_tecnico = reader.GetInt32(9),
                        tecnico = reader.GetString(10),
                        fk_avaliacao = reader.GetInt32(11),
                        avaliacao = reader.GetString(12),
                        titulo = reader.GetString(13),
                        descricao = reader.GetString(14),
                        resolvido_ia = reader.GetBoolean(15),
                        comentario_tecnico = reader.IsDBNull(16) ? null : reader.GetString(16),
                        data_abertura = reader.GetDateTime(17),
                        data_encerramento = reader.GetDateTime(18)
                    });
                }
            }
        }
        
        return Results.Ok(chamados);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// GET - Buscar chamado por ID
app.MapGet("/api/chamados/{id}", async (int id) =>
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            
            string query = @"
                SELECT
                    c.id_chamado,
                    c.FK_usuario,
                    u.nome AS usuario_emissor,
                    c.FK_categoria,
                    cat.categoria,
                    c.FK_prioridade,
                    p.prioridade,
                    c.FK_status,
                    s.status,
                    c.FK_tecnico,
                    t.nome AS tecnico,
                    c.FK_avaliacao,
                    a.descricao AS avaliacao,
                    c.titulo,
                    c.descricao,
                    c.Resolvido_IA,
                    c.comentario_tecnico,
                    c.data_abertura,
                    c.data_encerramento
                FROM TBchamados c
                INNER JOIN TBusuarios u ON c.FK_usuario = u.id_usuario
                INNER JOIN TBcategorias cat ON c.FK_categoria = cat.id_categoria
                INNER JOIN TBniveis_prioridade p ON c.FK_prioridade = p.id_prioridade
                INNER JOIN TBstatus s ON c.FK_status = s.id_status
                INNER JOIN TBusuarios t ON c.FK_tecnico = t.id_usuario
                INNER JOIN TBavaliacao a ON c.FK_avaliacao = a.id_avaliacao
                WHERE c.id_chamado = @Id";
            
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return Results.Ok(new
                        {
                            id_chamado = reader.GetInt32(0),
                            fk_usuario = reader.GetInt32(1),
                            usuario_emissor = reader.GetString(2),
                            fk_categoria = reader.GetInt32(3),
                            categoria = reader.GetString(4),
                            fk_prioridade = reader.GetInt32(5),
                            prioridade = reader.GetString(6),
                            fk_status = reader.GetInt32(7),
                            status = reader.GetString(8),
                            fk_tecnico = reader.GetInt32(9),
                            tecnico = reader.GetString(10),
                            fk_avaliacao = reader.GetInt32(11),
                            avaliacao = reader.GetString(12),
                            titulo = reader.GetString(13),
                            descricao = reader.GetString(14),
                            resolvido_ia = reader.GetBoolean(15),
                            comentario_tecnico = reader.IsDBNull(16) ? null : reader.GetString(16),
                            data_abertura = reader.GetDateTime(17),
                            data_encerramento = reader.GetDateTime(18)
                        });
                    }
                }
            }
        }
        
        return Results.NotFound(new { mensagem = "Chamado não encontrado" });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// POST - Criar novo chamado
app.MapPost("/api/chamados", async (Chamado chamado) =>
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            
            string query = @"
                INSERT INTO TBchamados 
                (FK_usuario, FK_categoria, FK_prioridade, FK_status, FK_tecnico, FK_avaliacao,
                 titulo, descricao, Resolvido_IA, comentario_tecnico, data_abertura, data_encerramento)
                VALUES 
                (@FK_usuario, @FK_categoria, @FK_prioridade, @FK_status, @FK_tecnico, @FK_avaliacao,
                 @Titulo, @Descricao, @Resolvido_IA, @Comentario_tecnico, @Data_abertura, @Data_encerramento)";
            
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@FK_usuario", chamado.FK_usuario);
                cmd.Parameters.AddWithValue("@FK_categoria", chamado.FK_categoria);
                cmd.Parameters.AddWithValue("@FK_prioridade", chamado.FK_prioridade);
                cmd.Parameters.AddWithValue("@FK_status", chamado.FK_status);
                cmd.Parameters.AddWithValue("@FK_tecnico", chamado.FK_tecnico);
                cmd.Parameters.AddWithValue("@FK_avaliacao", chamado.FK_avaliacao);
                cmd.Parameters.AddWithValue("@Titulo", chamado.Titulo);
                cmd.Parameters.AddWithValue("@Descricao", chamado.Descricao);
                cmd.Parameters.AddWithValue("@Resolvido_IA", chamado.Resolvido_IA);
                cmd.Parameters.AddWithValue("@Comentario_tecnico", (object)chamado.Comentario_tecnico ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Data_abertura", chamado.Data_abertura);
                cmd.Parameters.AddWithValue("@Data_encerramento", chamado.Data_encerramento);
                
                await cmd.ExecuteNonQueryAsync();
            }
        }
        
        return Results.Ok(new { mensagem = "Chamado criado com sucesso!" });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// PUT - Atualizar chamado
app.MapPut("/api/chamados/{id}", async (int id, Chamado chamado) =>
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            
            string query = @"
                UPDATE TBchamados 
                SET FK_usuario = @FK_usuario,
                    FK_categoria = @FK_categoria,
                    FK_prioridade = @FK_prioridade,
                    FK_status = @FK_status,
                    FK_tecnico = @FK_tecnico,
                    FK_avaliacao = @FK_avaliacao,
                    titulo = @Titulo,
                    descricao = @Descricao,
                    Resolvido_IA = @Resolvido_IA,
                    comentario_tecnico = @Comentario_tecnico,
                    data_encerramento = @Data_encerramento
                WHERE id_chamado = @Id";
            
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@FK_usuario", chamado.FK_usuario);
                cmd.Parameters.AddWithValue("@FK_categoria", chamado.FK_categoria);
                cmd.Parameters.AddWithValue("@FK_prioridade", chamado.FK_prioridade);
                cmd.Parameters.AddWithValue("@FK_status", chamado.FK_status);
                cmd.Parameters.AddWithValue("@FK_tecnico", chamado.FK_tecnico);
                cmd.Parameters.AddWithValue("@FK_avaliacao", chamado.FK_avaliacao);
                cmd.Parameters.AddWithValue("@Titulo", chamado.Titulo);
                cmd.Parameters.AddWithValue("@Descricao", chamado.Descricao);
                cmd.Parameters.AddWithValue("@Resolvido_IA", chamado.Resolvido_IA);
                cmd.Parameters.AddWithValue("@Comentario_tecnico", (object)chamado.Comentario_tecnico ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Data_encerramento", chamado.Data_encerramento);
                
                int linhas = await cmd.ExecuteNonQueryAsync();
                
                if (linhas == 0)
                    return Results.NotFound(new { mensagem = "Chamado não encontrado" });
            }
        }
        
        return Results.Ok(new { mensagem = "Chamado atualizado com sucesso!" });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// DELETE - Remover chamado
app.MapDelete("/api/chamados/{id}", async (int id) =>
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            
            string query = "DELETE FROM TBchamados WHERE id_chamado = @Id";
            
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                
                int linhas = await cmd.ExecuteNonQueryAsync();
                
                if (linhas == 0)
                    return Results.NotFound(new { mensagem = "Chamado não encontrado" });
            }
        }
        
        return Results.Ok(new { mensagem = "Chamado removido com sucesso!" });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// ============================================
// ENDPOINTS - CATEGORIAS
// ============================================

app.MapGet("/api/categorias", async () =>
{
    var categorias = new List<object>();
    
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM TBcategorias", conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    categorias.Add(new
                    {
                        id_categoria = reader.GetInt32(0),
                        categoria = reader.GetString(1)
                    });
                }
            }
        }
        
        return Results.Ok(categorias);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// ============================================
// ENDPOINTS - STATUS
// ============================================

app.MapGet("/api/status", async () =>
{
    var status = new List<object>();
    
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM TBstatus", conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    status.Add(new
                    {
                        id_status = reader.GetInt32(0),
                        status = reader.GetString(1)
                    });
                }
            }
        }
        
        return Results.Ok(status);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// ============================================
// ENDPOINTS - PRIORIDADES
// ============================================

app.MapGet("/api/prioridades", async () =>
{
    var prioridades = new List<object>();
    
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM TBniveis_prioridade", conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    prioridades.Add(new
                    {
                        id_prioridade = reader.GetInt32(0),
                        prioridade = reader.GetString(1)
                    });
                }
            }
        }
        
        return Results.Ok(prioridades);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// ============================================
// ENDPOINTS - AVALIAÇÕES
// ============================================

app.MapGet("/api/avaliacoes", async () =>
{
    var avaliacoes = new List<object>();
    
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM TBavaliacao", conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    avaliacoes.Add(new
                    {
                        id_avaliacao = reader.GetInt32(0),
                        descricao = reader.GetString(1)
                    });
                }
            }
        }
        
        return Results.Ok(avaliacoes);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// ============================================
// ENDPOINTS - RESPOSTAS IA
// ============================================

app.MapGet("/api/respostas-ia", async () =>
{
    var respostas = new List<object>();
    
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            
            string query = @"
                SELECT 
                    r.id_resposta,
                    r.FK_chamado,
                    c.titulo AS titulo_chamado,
                    r.resposta,
                    r.data_resposta
                FROM TBrespostas_ia r
                INNER JOIN TBchamados c ON r.FK_chamado = c.id_chamado";
            
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    respostas.Add(new
                    {
                        id_resposta = reader.GetInt32(0),
                        fk_chamado = reader.GetInt32(1),
                        titulo_chamado = reader.GetString(2),
                        resposta = reader.GetString(3),
                        data_resposta = reader.GetDateTime(4)
                    });
                }
            }
        }
        
        return Results.Ok(respostas);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// POST - Criar resposta IA
app.MapPost("/api/respostas-ia", async (RespostaIA resposta) =>
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            
            string query = @"
                INSERT INTO TBrespostas_ia (FK_chamado, resposta, data_resposta)
                VALUES (@FK_chamado, @Resposta, @Data_resposta)";
            
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@FK_chamado", resposta.FK_chamado);
                cmd.Parameters.AddWithValue("@Resposta", resposta.Resposta);
                cmd.Parameters.AddWithValue("@Data_resposta", resposta.Data_resposta);
                
                await cmd.ExecuteNonQueryAsync();
            }
        }
        
        return Results.Ok(new { mensagem = "Resposta IA criada com sucesso!" });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// ============================================
// ENDPOINTS - RELATÓRIOS
// ============================================

app.MapGet("/api/relatorios", async () =>
{
    var relatorios = new List<object>();
    
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            
            string query = @"
                SELECT 
                    r.id_relatorio,
                    r.FK_usuario,
                    u.nome AS usuario_emissor,
                    r.data_emissao
                FROM TBrelatorios r
                INNER JOIN TBusuarios u ON r.FK_usuario = u.id_usuario";
            
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    relatorios.Add(new
                    {
                        id_relatorio = reader.GetInt32(0),
                        fk_usuario = reader.GetInt32(1),
                        usuario_emissor = reader.GetString(2),
                        data_emissao = reader.GetDateTime(3)
                    });
                }
            }
        }
        
        return Results.Ok(relatorios);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// POST - Criar relatório
app.MapPost("/api/relatorios", async (Relatorio relatorio) =>
{
    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync();
            
            string query = @"
                INSERT INTO TBrelatorios (FK_usuario, data_emissao)
                VALUES (@FK_usuario, @Data_emissao)";
            
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@FK_usuario", relatorio.FK_usuario);
                cmd.Parameters.AddWithValue("@Data_emissao", relatorio.Data_emissao);
                
                await cmd.ExecuteNonQueryAsync();
            }
        }
        
        return Results.Ok(new { mensagem = "Relatório criado com sucesso!" });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Erro: {ex.Message}");
    }
});

// Endpoint raiz
app.MapGet("/", () => "🚀 API HelpDesk funcionando!");

Console.WriteLine("🚀 API HelpDesk rodando!");
Console.WriteLine("📍 Endpoints disponíveis:");
Console.WriteLine("   Usuários:     /api/usuarios");
Console.WriteLine("   Chamados:     /api/chamados");
Console.WriteLine("   Categorias:   /api/categorias");
Console.WriteLine("   Status:       /api/status");
Console.WriteLine("   Prioridades:  /api/prioridades");
Console.WriteLine("   Avaliações:   /api/avaliacoes");
Console.WriteLine("   Respostas IA: /api/respostas-ia");
Console.WriteLine("   Relatórios:   /api/relatorios");

app.Run();

// ============================================
// MODELS (Classes de dados)
// ============================================

public record Usuario(int FK_tipo_usuario, string Nome, string Email, string Senha);

public record Chamado(
    int FK_usuario,
    int FK_categoria,
    int FK_prioridade,
    int FK_status,
    int FK_tecnico,
    int FK_avaliacao,
    string Titulo,
    string Descricao,
    bool Resolvido_IA,
    string? Comentario_tecnico,
    DateTime Data_abertura,
    DateTime Data_encerramento
);

public record RespostaIA(int FK_chamado, string Resposta, DateTime Data_resposta);

public record Relatorio(int FK_usuario, DateTime Data_emissao);