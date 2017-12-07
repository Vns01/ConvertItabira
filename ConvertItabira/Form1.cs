using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace ConvertItabira
{


    public partial class Form1 : Form
    {
        //Classe para ajudar a atribuição dos valores da linha 4 
        public class valores
        {
            public int Ano { get; set; }
            public float Valor { get; set; }
            public float Juros { get; set; }
            public float Multa { get; set; }
            public float Correcao { get; set; }
            public float Subdiv { get; set; }
            public float Od { get; set; }
            public int Divida { get; set; }
            public string Vencimento { get; set; }
        }

        FileInfo arq = null;
        string arqConvertido;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelecionaArq_Click(object sender, EventArgs e)
        {
            if(opfdArq.ShowDialog() == DialogResult.OK && opfdArq.FileName.Length > 0)
            {
                arq = new FileInfo(opfdArq.FileName);
            }
        }


        private void Work(int SizeData)
        {
            #region variaveis 
            if(arq != null)
            {
                arqConvertido = $"{arq.DirectoryName}\\{arq.Name.Replace(".txt", "").Replace(".TXT", "")}_Process.txt";
                string linha = string.Empty;

                //exclui arquivo, caso exista
                if(File.Exists(arqConvertido))
                    File.Delete(arqConvertido);


                //variaveis que irá conter o fragmento de cada linha: 1 -- primeira, 4 -- segunda (possui + de 1) e 6 --ultima linha
                string l1 = string.Empty, l6 = string.Empty;
                List<valores> l4Valores = new List<valores>();

                List<string> arquivo = new List<string>();

                //variavel para delimitar cada registro
                char sLinhaOld = ' ';

                //estas varias  definem o posicionamento de cada campo na linha 4
                int valor = 53, juros = 66, multa = 79, correcao = 92, subdiv = 26, Od = 105, divida = 24, nlinhas = 0, ano = 18, vencimento = 45;


                float TotalMulta = 0.0F, ToTalAtuzalizacaoMonetaria = 0.0f;
                bool gravarTotais = false;

                #endregion
                try
                {
                    #region//lendo todo o arquivo e setando em um único list
                    using (StreamReader str = new StreamReader(arq.FullName, Encoding.GetEncoding(CultureInfo.GetCultureInfo("pt-BR").TextInfo.ANSICodePage)))
                    {

                        while ((linha = str.ReadLine()) != null)
                        {
                            arquivo.Add(linha);
                        }
                    }

                    #endregion


                    for (int i = 0; i < arquivo.Count() + 1; i++)
                    {
                        if (i < arquivo.Count)
                        {
                            if (sLinhaOld == '6' && arquivo[i].First() == '1')
                            {

                                using (StreamWriter stw = new StreamWriter(arqConvertido, true, Encoding.GetEncoding(CultureInfo.GetCultureInfo("pt-BR").TextInfo.ANSICodePage)))
                                {
                                    stw.WriteLine(l1);


                                    if (SizeData == 4)
                                    {
                                        var query1 = l4Valores.GroupBy(x => new { x.Ano, x.Divida }, (key, group) => new
                                        {
                                            Ano = key.Ano,
                                            Divida = key.Divida,
                                            Valor = $"{group.Sum(x => x.Valor):0.00}",
                                            SubDivida = $"{group.Sum(x => x.Subdiv):0.00}",
                                            Juros = $"{group.Sum(x => x.Juros):0.00}",
                                            AtuMonetaria = $"{group.Sum(x => x.Od):0.00}",
                                            Correcao = $"{group.Sum(x => x.Correcao):0.00}",
                                            Multa = $"{group.Sum(x => x.Multa):0.00}",
                                            ValorCorrigdo = $"{((group.Sum(x => x.Valor) - group.Sum(x => x.Multa)) - group.Sum(x => x.Od)):0.00}"
                                        });

                                        //somando todos os juros e atualizações monetarias
                                        foreach (var item in query1)
                                        {
                                            TotalMulta += float.Parse(item.Multa);
                                            ToTalAtuzalizacaoMonetaria += float.Parse(item.AtuMonetaria);
                                        }

                                        foreach (var q in query1)
                                        {
                                            if (q.Ano != 0)
                                            {
                                                stw.Write("4");
                                                stw.Write($"{q.Ano.ToString().PadLeft(12)}");
                                                stw.Write($"{q.Divida.ToString().PadLeft(12)}");
                                                stw.Write($"{q.Correcao.ToString().PadLeft(12)}");
                                                stw.Write($"{q.Valor.ToString().PadLeft(12)}");
                                                stw.Write($"{q.Juros.ToString().PadLeft(12)}");
                                                stw.Write($"{q.Multa.ToString().PadLeft(12)}");
                                                stw.Write($"{q.SubDivida.PadLeft(12)}");
                                                stw.Write($"{q.AtuMonetaria.ToString().PadLeft(12)}");
                                                stw.Write($"{q.ValorCorrigdo.ToString().PadLeft(12)}");

                                                //só grava uma vez na primeira posicão da linha4
                                                if (gravarTotais == false)
                                                {
                                                    stw.Write($"{TotalMulta.ToString().PadLeft(12)}");
                                                    stw.Write($"{ToTalAtuzalizacaoMonetaria.ToString().PadLeft(12)}");
                                                    gravarTotais = true;
                                                }

                                                stw.WriteLine();
                                            }
                                        }


                                        stw.WriteLine(l6);

                                        nlinhas++;

                                        l1 = string.Empty;
                                        l4Valores.Clear();
                                        l6 = string.Empty;
                                        ToTalAtuzalizacaoMonetaria = 0.0F;
                                        TotalMulta = 0.0F;
                                        gravarTotais = false;
                                    }
                                    else
                                    if(SizeData == 8)
                                    {
                                        var query2 = l4Valores.GroupBy(x => new {x.Ano, x.Divida, x.Vencimento }, (key, group) => new
                                        {
                                            Ano = key.Ano,
                                            Divida = key.Divida,
                                            Vencimento = key.Vencimento,
                                            Valor = $"{group.Sum(x => x.Valor):0.00}",
                                            SubDivida = $"{group.Sum(x => x.Subdiv):0.00}",
                                            Juros = $"{group.Sum(x => x.Juros):0.00}",
                                            AtuMonetaria = $"{group.Sum(x => x.Od):0.00}",
                                            Correcao = $"{group.Sum(x => x.Correcao):0.00}",
                                            Multa = $"{group.Sum(x => x.Multa):0.00}",
                                            ValorCorrigdo = $"{((group.Sum(x => x.Valor) - group.Sum(x => x.Multa)) - group.Sum(x => x.Od)):0.00}"
                                        });


                                        //somando todos os juros e atualizações monetarias
                                        foreach (var item in query2)
                                        {
                                            TotalMulta += float.Parse(item.Multa);
                                            ToTalAtuzalizacaoMonetaria += float.Parse(item.AtuMonetaria);
                                        }

                                        foreach (var q in query2)
                                        {
                                            if (q.Ano != 0)
                                            {
                                                stw.Write("4");
                                                stw.Write($"{q.Ano.ToString().PadLeft(12)}");
                                                stw.Write($"{q.Divida.ToString().PadLeft(12)}");
                                                stw.Write($"{q.Correcao.ToString().PadLeft(12)}");
                                                stw.Write($"{q.Valor.ToString().PadLeft(12)}");
                                                stw.Write($"{q.Juros.ToString().PadLeft(12)}");
                                                stw.Write($"{q.Multa.ToString().PadLeft(12)}");
                                                stw.Write($"{q.SubDivida.ToString().PadLeft(12)}");
                                                stw.Write($"{q.AtuMonetaria.ToString().PadLeft(12)}");
                                                stw.Write($"{q.ValorCorrigdo.ToString().PadLeft(12)}");
                                                stw.Write($"{q.Vencimento.ToString().PadLeft(12)}");

                                                //só grava uma vez na primeira posicão da linha4
                                                if (gravarTotais == false)
                                                {
                                                    stw.Write($"{TotalMulta.ToString().PadLeft(12)}");
                                                    stw.Write($"{ToTalAtuzalizacaoMonetaria.ToString().PadLeft(12)}");
                                                    gravarTotais = true;
                                                }

                                                stw.WriteLine();
                                            }
                                        }

                                        stw.WriteLine(l6);

                                        nlinhas++;

                                        l1 = string.Empty;
                                        l4Valores.Clear();
                                        l6 = string.Empty;
                                        ToTalAtuzalizacaoMonetaria = 0.0F;
                                        TotalMulta = 0.0F;
                                        gravarTotais = false;
                                    }
                                }
                            }

                            if (arquivo[i].First() == '1')
                            {
                                l1 = arquivo[i];
                            }


                            if (arquivo[i].First() == '4')
                            {

                                ano = 18;
                                subdiv = 24;
                                divida = 22;
                                valor = 53;
                                juros = 66;
                                multa = 79;
                                correcao = 92;
                                Od = 105;
                                vencimento = 45;

                                for (int l = 0; l < 5; l++)
                                {
                                    l4Valores.Add(new valores
                                    {
                                        Ano = int.Parse(arquivo[i].Substring(ano, 4)),
                                        Subdiv = float.Parse(arquivo[i].Substring(subdiv, 5)) / 100,
                                        Divida = int.Parse(arquivo[i].Substring(divida, 2)),
                                        Valor = float.Parse(arquivo[i].Substring(valor, 11)) / 100,
                                        Juros = float.Parse(arquivo[i].Substring(juros, 11)) / 100,
                                        Multa = float.Parse(arquivo[i].Substring(multa, 11)) / 100,
                                        Correcao = float.Parse(arquivo[i].Substring(correcao, 11)) / 100,
                                        Od = float.Parse(arquivo[i].Substring(Od, 11)) / 100,
                                        Vencimento = arquivo[i].Substring(vencimento, 8)
                                    });

                                    ano += 300;
                                    subdiv += 300;
                                    divida += 300;
                                    valor += 300;
                                    juros += 300;
                                    multa += 300;
                                    correcao += 300;
                                    Od += 300;
                                    vencimento += 300;
                                }

                            }

                            if (arquivo[i].First() == '6')
                            {
                                l6 = arquivo[i];
                                sLinhaOld = arquivo[i].First();
                            }

                        }
                        else
                        {
                            using (StreamWriter stw = new StreamWriter(arqConvertido, true, Encoding.GetEncoding(CultureInfo.GetCultureInfo("pt-BR").TextInfo.ANSICodePage)))
                            {
                                stw.WriteLine(l1);

                                if (SizeData == 4)
                                {
                                    var query3 = l4Valores.GroupBy(x => new { x.Ano, x.Divida }, (key, group) => new
                                    {
                                        Ano = key.Ano,
                                        Divida = key.Divida,
                                        Valor = $"{group.Sum(x => x.Valor):0.00}",
                                        SubDivida = $"{group.Sum(x => x.Subdiv):0.00}",
                                        Juros = $"{group.Sum(x => x.Juros):0.00}",
                                        AtuMonetaria = $"{group.Sum(x => x.Od):0.00}",
                                        Correcao = $"{group.Sum(x => x.Correcao):0.00}",
                                        Multa = $"{group.Sum(x => x.Multa):0.00}",
                                        ValorCorrigdo = $"{((group.Sum(x => x.Valor) - group.Sum(x => x.Multa)) - group.Sum(x => x.Od)):0.00}"
                                    });


                                    //somando todos os juros e atualizações monetarias
                                    foreach (var item in query3)
                                    {
                                        TotalMulta += float.Parse(item.Multa);
                                        ToTalAtuzalizacaoMonetaria += float.Parse(item.AtuMonetaria);
                                    }

                                    foreach (var q in query3)
                                    {
                                        if (q.Ano != 0)
                                        {
                                            stw.Write("4");
                                            stw.Write($"{q.Ano.ToString().PadLeft(12)}");
                                            stw.Write($"{q.Divida.ToString().PadLeft(12)}");
                                            stw.Write($"{q.Correcao.ToString().PadLeft(12)}");
                                            stw.Write($"{q.Valor.ToString().PadLeft(12)}");
                                            stw.Write($"{q.Juros.ToString().PadLeft(12)}");
                                            stw.Write($"{q.Multa.ToString().PadLeft(12)}");
                                            stw.Write($"{q.SubDivida.ToString().PadLeft(12)}");
                                            stw.Write($"{q.AtuMonetaria.ToString().PadLeft(12)}");
                                            stw.Write($"{q.ValorCorrigdo.ToString().PadLeft(12)}");

                                            //só grava uma vez na primeira posicão da linha4
                                            if (gravarTotais == false)
                                            {
                                                stw.Write($"{TotalMulta.ToString().PadLeft(12)}");
                                                stw.Write($"{ToTalAtuzalizacaoMonetaria.ToString().PadLeft(12)}");
                                                gravarTotais = true;
                                            }
                                            stw.WriteLine();
                                        }
                                    }

                                    stw.WriteLine(l6);

                                    l1 = string.Empty;
                                    l4Valores.Clear();
                                    l6 = string.Empty;
                                    ToTalAtuzalizacaoMonetaria = 0.0F;
                                    TotalMulta = 0.0F;
                                    gravarTotais = false;
                                }
                                else
                                if(SizeData == 8)
                                {
                                    var query4 = l4Valores.GroupBy(x => new {x.Ano, x.Divida, x.Vencimento }, (key, group) => new
                                    {
                                        Ano = key.Ano,
                                        Divida = key.Divida,
                                        Vencimento = key.Vencimento,
                                        Valor = $"{group.Sum(x => x.Valor):0.00}",
                                        SubDivida = $"{group.Sum(x => x.Subdiv):0.00}",
                                        Juros = $"{group.Sum(x => x.Juros):0.00}",
                                        AtuMonetaria = $"{group.Sum(x => x.Od):0.00}",
                                        Correcao = $"{group.Sum(x => x.Correcao):0.00}",
                                        Multa = $"{group.Sum(x => x.Multa):0.00}",
                                        ValorCorrigdo = $"{((group.Sum(x => x.Valor) - group.Sum(x => x.Multa)) - group.Sum(x => x.Od)):0.00}"
                                    });


                                    //somando todos os juros e atualizações monetarias
                                    foreach (var item in query4)
                                    {
                                        TotalMulta += float.Parse(item.Multa);
                                        ToTalAtuzalizacaoMonetaria += float.Parse(item.AtuMonetaria);
                                    }

                                    foreach (var q in query4)
                                    {
                                        if (q.Ano != 0)
                                        {
                                            stw.Write("4");
                                            stw.Write($"{q.Ano.ToString().PadLeft(12)}");
                                            stw.Write($"{q.Divida.ToString().PadLeft(12)}");
                                            stw.Write($"{q.Correcao.ToString().PadLeft(12)}");
                                            stw.Write($"{q.Valor.ToString().PadLeft(12)}");
                                            stw.Write($"{q.Juros.ToString().PadLeft(12)}");
                                            stw.Write($"{q.Multa.ToString().PadLeft(12)}");
                                            stw.Write($"{q.SubDivida.ToString().PadLeft(12)}");
                                            stw.Write($"{q.AtuMonetaria.ToString().PadLeft(12)}");
                                            stw.Write($"{q.ValorCorrigdo.ToString().PadLeft(12)}");
                                            stw.Write($"{q.Vencimento.ToString().PadLeft(12)}");

                                            //só grava uma vez na primeira posicão da linha4
                                            if (gravarTotais == false)
                                            {
                                                stw.Write($"{TotalMulta.ToString().PadLeft(12)}");
                                                stw.Write($"{ToTalAtuzalizacaoMonetaria.ToString().PadLeft(12)}");
                                                gravarTotais = true;
                                            }
                                          
                                            stw.WriteLine();
                                        }
                                    }

                                    stw.WriteLine(l6);

                                    l1 = string.Empty;
                                    l4Valores.Clear();
                                    l6 = string.Empty;
                                    ToTalAtuzalizacaoMonetaria = 0.0F;
                                    TotalMulta = 0.0F;
                                    gravarTotais = false;
                                }



                            }



                        }
                        
                    }
                        clearRadios();
                        arq = null;
                        arquivo.Clear();
                        arquivo = null;
                        GC.Collect();
                        MessageBox.Show($"Arquivo: {arqConvertido}", "Conversão Concluida!", MessageBoxButtons.OK);
                    
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Arquivo não selecionado!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void clearRadios()
        {
            rdbSize4.Checked = false;
            rdbSize8.Checked = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (rdbSize4.Checked == true || rdbSize8.Checked == true)
            {
                if (rdbSize4.Checked == true)
                    Work(4);
                else
                    Work(8);
            }
            
            else
                MessageBox.Show("Selecione uma Opção de Data.","Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
