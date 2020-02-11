﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class Classe_Controle_Licao1_Atv1 : MonoBehaviour {

    public GameObject pnl_loading;
    //VARIAVEIS DE PRAFAB E LISTAGEM DE NPCS
    public GameObject NPC;
    public List<GameObject> npcs = new List<GameObject>();

    public Dropdown dpw_nomes_1, dpw_nascionalidades_1, dpw_nomes_2, dpw_nascionalidades_2, dpw_nomes_3, dpw_nascionalidades_3, dpw_nomes_4, dpw_nascionalidades_4, dpw_nomes_5, dpw_nascionalidades_5, dpw_nomes_6, dpw_nascionalidades_6;

    //LISTAGEM DE NOMES, SOBRENOMES E NASCIONALIDADES
    public List<string> nomes_masculinos = new List<string>();
    public List<string> nomes_femininos = new List<string>();
    public List<string> sobrenomes = new List<string>();
    public List<string> nascionalidades = new List<string>();

    // TODAS AS POSIÇÕES POSSIVEIS
    public object[,] posicoes_possiveis = new object[6, 2];

    // LISTAGEM DE POSIÇÕES A SEREM TRABALHADAS
    List<List<Vector3>> posicoesNPC = new List<List<Vector3>>();
    List<Vector3> posicoes1 = new List<Vector3>();
    List<Vector3> posicoes2 = new List<Vector3>();

    //LISTA DE ROUPAS
    public List<Sprite> cima_r_m = new List<Sprite>();
    public List<Sprite> baixo_r_m = new List<Sprite>();
    public List<Sprite> cima_r_f = new List<Sprite>();
    public List<Sprite> baixo_r_f = new List<Sprite>();
    //LISTA DE CABELOS
    public List<Sprite> cima_c_m = new List<Sprite>();
    public List<Sprite> baixo_c_m = new List<Sprite>();
    public List<Sprite> cima_c_f = new List<Sprite>();
    public List<Sprite> baixo_c_f = new List<Sprite>();
    // SPRITES BASE
    public Sprite cima_m, baixo_m, cima_f, baixo_f;

    //VARIVAVEL DO NIVEL DA ATIVIDADE
    public int nivel = 1;

    //VARIAVEIS DE BANCO
    MySqlCommand comando;
    MySqlDataReader dados;

    void Start () {
        //INICIALIZA AS CLASSES 'CONEXAO' E 'PLAYER'
        Banco_Conexao conexao = new Banco_Conexao();
        Objeto_Player player = new Objeto_Player();

        //DEFINE TODAS AS POSIÇÕES POSSIVEIS E SUA CONSECUTIVA POSIÇÃO
        posicoes_possiveis[0, 0] = "CIMA";
        posicoes_possiveis[0, 1] = new Vector3(-25, 3, 0);
        posicoes_possiveis[1, 0] = "CIMA";
        posicoes_possiveis[1, 1] = new Vector3(-31, 3, 0);
        posicoes_possiveis[2, 0] = "CIMA";
        posicoes_possiveis[2, 1] = new Vector3(-34, 3, 0);

        posicoes_possiveis[3, 0] = "BAIXO";
        posicoes_possiveis[3, 1] = new Vector3(-35, 1, 0);
        posicoes_possiveis[4, 0] = "BAIXO";
        posicoes_possiveis[4, 1] = new Vector3(-31, 1, 0);
        posicoes_possiveis[5, 0] = "BAIXO";
        posicoes_possiveis[5, 1] = new Vector3(-26, 1, 0);

        //INDICA TODAS AS POSIÇÕES NA LISTAGEM 1 PARA SEREM TRABALHADAS
        posicoes1.Add(new Vector3(-25, 3, 0));
        posicoes1.Add(new Vector3(-31, 3, 0));
        posicoes1.Add(new Vector3(-34, 3, 0));
        //INDICA TODAS AS POSIÇÕES NA LISTAGEM 2 PARA SEREM TRABALHADAS
        posicoes2.Add(new Vector3(-35, 1, 0));
        posicoes2.Add(new Vector3(-31, 1, 0));
        posicoes2.Add(new Vector3(-26, 1, 0));

        //ADICIONA TODAS AS LISTAGEM DE POSIÇÕES PARA A LISTA FINAL
        posicoesNPC.Add(posicoes1);
        posicoesNPC.Add(posicoes2);

        //PEGA TODOS OS SPRITES
        Sprite[] resources = Resources.LoadAll<Sprite>("Sprites");

        //COLOCANDO TODOS OS SPRITES EM SEUS DEVIDOS ARRAYS
        for (int i = 0; i < resources.Length; i++)
        {
            string[] sprite = resources[i].ToString().Split('_');
            sprite[sprite.Length - 1] = sprite[sprite.Length - 1].Replace(" (UnityEngine.Sprite)", "");

            string caminho = "";

            switch (sprite[0])
            {
                //PEGANDO BASES
                case "B":
                    caminho = "Sprites/" + sprite[0] + "_" + sprite[1];
                    switch (sprite[1])
                    {
                        case "M":
                            if (sprite[2] == "1")
                            {
                                cima_m = Resources.LoadAll<Sprite>(caminho)[System.Convert.ToInt32(sprite[2])];
                            }
                            if (sprite[2] == "10")
                            {
                                baixo_m = Resources.LoadAll<Sprite>(caminho)[System.Convert.ToInt32(sprite[2])];
                            }
                            break;
                        case "F":
                            if (sprite[2] == "1")
                            {
                                cima_f = Resources.LoadAll<Sprite>(caminho)[System.Convert.ToInt32(sprite[2])];
                            }
                            if (sprite[2] == "10")
                            {
                                baixo_f = Resources.LoadAll<Sprite>(caminho)[System.Convert.ToInt32(sprite[2])];
                            }
                            break;
                    }
                    break;
                //PEGANDO CABELOS
                case "C":
                    caminho = "Sprites/" + sprite[0] + "_" + sprite[1] + "_" + sprite[2];
                    switch (sprite[1])
                    {
                        case "M":
                            if (sprite[3] == "1")
                            {
                                cima_c_m.Add(Resources.LoadAll<Sprite>(caminho)[System.Convert.ToInt32(sprite[3])]);
                            }
                            if (sprite[3] == "10")
                            {
                                baixo_c_m.Add(Resources.LoadAll<Sprite>(caminho)[System.Convert.ToInt32(sprite[3])]);
                            }
                            break;
                        case "F":
                            if (sprite[3] == "1")
                            {
                                cima_c_f.Add(Resources.LoadAll<Sprite>(caminho)[System.Convert.ToInt32(sprite[3])]);
                            }
                            if (sprite[3] == "10")
                            {
                                baixo_c_f.Add(Resources.LoadAll<Sprite>(caminho)[System.Convert.ToInt32(sprite[3])]);
                            }
                            break;
                    }
                    break;
                //PEGANDO ROUPAS
                case "R":
                    if (sprite[2] == "Escritorio")
                    {
                        caminho = "Sprites/" + sprite[0] + "_" + sprite[1] + "_" + sprite[2] + "_" + sprite[3];
                        switch (sprite[1])
                        {
                            case "M":
                                if (sprite[4] == "1")
                                {
                                    cima_r_m.Add(Resources.LoadAll<Sprite>(caminho)[System.Convert.ToInt32(sprite[4])]);
                                }
                                if (sprite[4] == "10")
                                {
                                    baixo_r_m.Add(Resources.LoadAll<Sprite>(caminho)[System.Convert.ToInt32(sprite[4])]);
                                }
                                break;
                            case "F":
                                if (sprite[4] == "1")
                                {
                                    cima_r_f.Add(Resources.LoadAll<Sprite>(caminho)[System.Convert.ToInt32(sprite[4])]);
                                }
                                if (sprite[4] == "10")
                                {
                                    baixo_r_f.Add(Resources.LoadAll<Sprite>(caminho)[System.Convert.ToInt32(sprite[4])]);
                                }
                                break;
                        }
                    }
                    break;
            }
        }

        //CONTATO COM O BANCO DE DADOS
        try
        {
            conexao.conectarBanco();

            //COMANDO SQL NIVEL 2
            conexao.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=1";
            comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
            dados = comando.ExecuteReader();

            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    int n = (int)dados["NIVEL_ATIVIDADE"];
                    if (n <= 10)
                    {
                        nivel = 1;
                    }
                    else if (n <= 20)
                    {
                        nivel = 2;
                    }
                    else
                    {
                        nivel = 3;
                    }
                }
            }

            dados.Close();
            comando.Dispose();

            // PEGAR NOMES MASCULINOS E FEMININOS
            conexao.Sql = "SELECT CONTEUDO_TEXTO, CONTEUDO_TAG1 FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Nome' AND (CONTEUDO_TAG1 = 'Masculino' OR CONTEUDO_TAG1 = 'Feminino');";
			comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
			dados = comando.ExecuteReader();
			
			if(dados.HasRows){
				while(dados.Read()){
					if(dados["CONTEUDO_TAG1"].ToString() == "Masculino"){
						nomes_masculinos.Add(dados["CONTEUDO_TEXTO"].ToString());						
					}else if(dados["CONTEUDO_TAG1"].ToString() == "Feminino")
                    {
						nomes_femininos.Add(dados["CONTEUDO_TEXTO"].ToString());
					}
				}
			}            

            //ADICIONANDO NOMES NOS DROPDOWNS
			dpw_nomes_1.AddOptions(nomes_masculinos);
			dpw_nomes_1.AddOptions(nomes_femininos);
			dpw_nomes_2.AddOptions(nomes_masculinos);
			dpw_nomes_2.AddOptions(nomes_femininos);
			dpw_nomes_3.AddOptions(nomes_masculinos);
			dpw_nomes_3.AddOptions(nomes_femininos);
			dpw_nomes_4.AddOptions(nomes_masculinos);
			dpw_nomes_4.AddOptions(nomes_femininos);
			dpw_nomes_5.AddOptions(nomes_masculinos);
			dpw_nomes_5.AddOptions(nomes_femininos);
			dpw_nomes_6.AddOptions(nomes_masculinos);
			dpw_nomes_6.AddOptions(nomes_femininos);			

			dados.Close();
			comando.Dispose();

			// PEGAR SOBRENOMES
			conexao.Sql = "SELECT CONTEUDO_TEXTO FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Sobrenome';";
			comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
			dados = comando.ExecuteReader();

			if(dados.HasRows){
				while(dados.Read()){
					sobrenomes.Add(dados["CONTEUDO_TEXTO"].ToString());
				}
			}

			dados.Close();
			comando.Dispose();

			// PEGAR NASCIONALIDADES
			conexao.Sql = "SELECT CONTEUDO_TEXTO FROM TB_CONTEUDOS WHERE CONTEUDO_TIPO = 'Nacionalidade';";
			comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
			dados = comando.ExecuteReader();

			if(dados.HasRows){
				while(dados.Read()){
					nascionalidades.Add(dados["CONTEUDO_TEXTO"].ToString());
				}
			}

            //ADICIONANDO NASCIONALIDADES AOS DROPDOWNS
			dpw_nascionalidades_1.AddOptions(nascionalidades);
			dpw_nascionalidades_2.AddOptions(nascionalidades);
			dpw_nascionalidades_3.AddOptions(nascionalidades);
			dpw_nascionalidades_4.AddOptions(nascionalidades);
			dpw_nascionalidades_5.AddOptions(nascionalidades);
			dpw_nascionalidades_6.AddOptions(nascionalidades);

			dados.Close();
			comando.Dispose();

			conexao.fecharBanco();
		}catch(MySqlException e){
            print(e);
		}

		int nNpcs;

		switch(nivel){
			case 1:
				nNpcs = 2;
				break;
			case 2:
				nNpcs = 4;
				break;
			case 3:
				nNpcs = 6;
				break;
			default:
				nNpcs = 2;
				break;
		}

        int rPosicao1;
        int rPosicao2;
        // GERA NPCS
        for (int i = 0; i < nNpcs; i++)
        {
            rPosicao1 = Random.Range(0, posicoesNPC.Count);

            if (posicoesNPC[rPosicao1].Count == 0)
            {
                posicoesNPC.RemoveAt(rPosicao1);
                rPosicao1 = Random.Range(0, posicoesNPC.Count);
            }

            rPosicao2 = Random.Range(0, posicoesNPC[rPosicao1].Count);
            GameObject npc = Instantiate(NPC, posicoesNPC[rPosicao1][rPosicao2], Quaternion.identity);
            Classe_NPC_Licao1_Atv1 c = npc.GetComponent<Classe_NPC_Licao1_Atv1>();
            c.id = i + 1;
            npcs.Add(npc);
            posicoesNPC[rPosicao1].RemoveAt(rPosicao2);

        }
    }

    public void Metodo_Adicionar_Pontos_Licao1_Atv1()
    {
        Banco_Conexao conexao = new Banco_Conexao();
        Objeto_Player player = new Objeto_Player();

        try
        {
            //PEGA NIVEL ATUAL DA ATIVIDADE
            conexao.conectarBanco();

            conexao.Sql = "SELECT NIVEL_ATIVIDADE FROM TB_NIVEL_ATIVIDADE WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=1";
            comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
            dados = comando.ExecuteReader();
            int n = 0;
            if (dados.HasRows)
            {
                while (dados.Read())
                {
                    n = (int)dados["NIVEL_ATIVIDADE"];
                    print(n);
                }
            }

            dados.Close();
            comando.Dispose();

            //ALMENTA UM NIVEL
            conexao.Sql = "UPDATE TB_NIVEL_ATIVIDADE SET NIVEL_ATIVIDADE=" + (n + 1) + " WHERE COD_ESTUDANTE=" + player.Cpf + " AND COD_ATIVIDADE=1";
            comando = new MySqlCommand(conexao.Sql, conexao.ConexaoBanco);
            comando.ExecuteNonQuery();

            conexao.fecharBanco();
        }
        catch
        {

        }
    }

    public void Metodo_Voltar_Menu_Principal()
    {
        pnl_loading.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene("telaPrincipal");
    }
}
