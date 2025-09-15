--Consulta na tabela Chamados
SELECT
TBchamados.id_chamado,
TBusuarios.nome AS usuarioemissor,
TBcategorias.categoria,
TBniveis_prioridade.prioridade,
TBstatus.status,
tecnicos.nome AS tecnico,
TBavaliacao.descricao AS avaliacao_atendimento,
TBchamados.titulo,
TBchamados.descricao AS descricao_chamado,
TBrespostas_ia.resposta AS resposta_ia,
TBchamados.Resolvido_IA,
TBchamados.comentario_tecnico,
TBchamados.data_abertura,
TBchamados.data_encerramento
FROM TBchamados

INNER JOIN TBusuarios ON TBchamados.FK_usuario = TBusuarios.id_usuario
INNER JOIN TBcategorias ON TBchamados.FK_categoria = TBcategorias.id_categoria
INNER JOIN TBniveis_prioridade ON TBchamados.FK_prioridade = TBniveis_prioridade.id_prioridade
INNER JOIN TBstatus ON TBchamados.FK_status = TBstatus.id_status
INNER JOIN TBusuarios AS tecnicos ON TBchamados.FK_tecnico = tecnicos.id_usuario
INNER JOIN TBavaliacao ON TBchamados.FK_avaliacao = TBavaliacao.id_avaliacao
INNER JOIN TBrespostas_ia ON TBchamados.id_chamado = TBrespostas_ia.FK_chamado;

--CONSULTA NA TABELA Respostas IA
SELECT
TBchamados.id_chamado,
TBrespostas_ia.id_resposta,
TBrespostas_ia.resposta,
TBrespostas_ia.data_resposta
FROM TBrespostas_ia

INNER JOIN TBchamados ON TBrespostas_ia.FK_chamado = TBchamados.id_chamado;

--CONSULTA NA TABELA Usuários
SELECT
TBusuarios.id_usuario,
TBtipos_usuario.tipo_usuario,
TBusuarios.nome,
TBusuarios.email
FROM TBusuarios

INNER JOIN TBtipos_usuario ON TBusuarios.FK_tipo_usuario = TBtipos_usuario.id_tipo_usuario;

--CONSULTA NA TABELA Relatórios
SELECT
TBrelatorios.id_relatorio,
TBusuarios.nome AS usuario_emissor,
TBrelatorios.data_emissao
FROM TBrelatorios

INNER JOIN TBusuarios ON TBrelatorios.FK_usuario = TBusuarios.id_usuario;

