
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    class Program
    {
        static void ListarVetor(String[] ItemMenu)
        {
            int i = 1;
            Console.WriteLine("0 - Sair");
            foreach (string Item in ItemMenu)
            {
                Console.WriteLine($"{i} - {Item}");
                i++;
            }
            Console.Write("Selecione:");
        }
        static void Alerta(string s, bool cor)
        {
            if (cor)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.Beep();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            Console.WriteLine(s);
            Console.ResetColor();
            Console.WriteLine("Clique qualquer tecla para continuar...");
            Console.ReadKey();
        }
        //Início do programa
        static void Main(string[] args)
        {
            // Alocando a árvore Avl no sistema
            Avl.AVLTree AVLTree = new Avl.AVLTree();
            Abb.ABBTree ABBTree = new Abb.ABBTree();

        menu:
            Console.Title = "Biblioteca VI-I";
            Console.Clear();
            Console.WriteLine("██████╗ ███████╗███╗   ███╗      ██╗   ██╗██╗███╗   ██╗██████╗  ██████╗");
            Console.WriteLine("██╔══██╗██╔════╝████╗ ████║      ██║   ██║██║████╗  ██║██╔══██╗██╔═══██╗");
            Console.WriteLine("██████╔╝█████╗  ██╔████╔██║█████╗██║   ██║██║██╔██╗ ██║██║  ██║██║   ██║");
            Console.WriteLine("██╔══██╗██╔══╝  ██║╚██╔╝██║╚════╝╚██╗ ██╔╝██║██║╚██╗██║██║  ██║██║   ██║");
            Console.WriteLine("██████╔╝███████╗██║ ╚═╝ ██║       ╚████╔╝ ██║██║ ╚████║██████╔╝╚██████╔╝");
            Console.WriteLine("╚═════╝ ╚══════╝╚═╝     ╚═╝        ╚═══╝  ╚═╝╚═╝  ╚═══╝╚═════╝  ╚═════╝");

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Biblioteca VI-I");
            Console.ResetColor();

            string[] ItemMenu = new string[6] { "Inserir Livro", "Buscar Livro", "Listar Livros", "Remover Livro", "Imprimir Árvore", "Livros Removidos" };
            ListarVetor(ItemMenu);
            int opc = -1;
            int.TryParse(Console.ReadLine(), out opc);
            switch (opc)
            {
                case 0:
                    Console.WriteLine("0 - Cancelar");
                    Console.WriteLine("1 - Fechar programa");
                    char.TryParse(Console.ReadLine(), out char resp);
                    switch (resp)
                    {
                        case '0':
                            goto menu;
                        case '1':
                            return;

                    }
                    break;
                case 1:
                    InserirLivro(AVLTree);
                    break;
                case 2:
                    BuscarLivro(AVLTree);
                    break;
                case 3:
                    ListarLivro(AVLTree);
                    break;
                case 4:
                    RemoverLivro(AVLTree, ABBTree);
                    break;
                case 5:
                    ImprimirArvore(AVLTree);
                    break;
                case 6:
                    LivroRemovido(ABBTree);
                    break;
                default:
                    string cont = "A Opção digitada é inexistente!";
                    Alerta(cont, false);
                    break;
            }

            goto menu;
        }
        static void InserirLivro(Avl.AVLTree AVLTree)
        {
            Livro livro = new Livro();
            Console.Title = "Inserir Livro";
        menuinserir:
            Console.Clear();
            Console.WriteLine("Preencha os dados abaixo:");
            try
            {
                int id;
                //Pegar as informações digitadas pelo usuário
                do
                {
                    Console.Write("Digite o Código do livro: ");
                } while (int.TryParse(Console.ReadLine(), out id) == false);
                livro.Id = id;
                Console.Write("\nDigite o Nome do livro: ");
                livro.Nome_livro = Console.ReadLine();
                Console.Write("\nDigite o Autor do livro: ");
                livro.Autor_livro = Console.ReadLine();
                CultureInfo culturaBrasileira = new CultureInfo("pt-BR");
                DateTime data;
                do
                {
                    Console.Write("\nDigite a Data de Publicação do livro(yyyy/mm/dd)*: ");
                } while (DateTime.TryParse(Console.ReadLine(), culturaBrasileira, System.Globalization.DateTimeStyles.AllowTrailingWhite, out data) == false);
                livro.Data_publicação = data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                string cont = "Dado digitado incorretamente.";
                Alerta(cont, false);
                return;

            }
        confirmar:
            string[] confirmar = new string[2] { "Confirmar Inserção", "Cancelar" };
            ListarVetor(confirmar);
            char.TryParse(Console.ReadLine(), out char resp);
            switch (resp)
            {
                case '0': return;
                case '1': goto salvar;
                case '2': goto menuinserir;
                default:
                    string alerta = "Opção fora do parâmetro.";
                    Alerta(alerta, false);
                    goto confirmar;
            }
        salvar:
            AVLTree.root = AVLTree.insert(AVLTree.root, livro.Id, livro.Nome_livro, livro.Autor_livro, livro.Data_publicação);
            int balanceFactor = AVLTree.getRootBalanceFactor();
            Console.WriteLine("Fator de balanceamento do nó raiz: " + balanceFactor);
            string salvo = "Inserção feita com sucesso";
            Alerta(salvo, true);
            //Salva as informações digitadas pelo usuário

        }
        static void BuscarLivro(Avl.AVLTree AVLTree)
        {
            Console.Title = "Buscar Livro";
        menubuscar:
            Console.Clear();
            Console.WriteLine("Selecione o tipo de busca que você deseja fazer:");
            string[] ItemMenu = new string[4] { "Código do livro", "Nome do Livro", "Autor do Livro", "Ano de publicação" };
            ListarVetor(ItemMenu);
            char.TryParse(Console.ReadLine(), out char opc);
            switch (opc)
            {
                case '0':
                    return;
                case '1':
                    Console.Write("Informe o Código do Livro que deseja encontrar: ");
                    int.TryParse(Console.ReadLine(), out int CodValue);
                    Avl.Node Encontrar = AVLTree.Search(AVLTree.root, CodValue);
                    AVLTree.MostrarLivro(Encontrar);
                    break;

                case '2':
                    Console.Write("Informe o Nome do Livro que deseja encontrar: ");
                    string Nomevalue = Console.ReadLine();
                    AVLTree.NomeSearch(AVLTree.root, Nomevalue);

                    break;
                case '3':
                    Console.Write("Informe o Autor do Livro que deseja encontrar: ");
                    string Autorvalue = Console.ReadLine();
                    AVLTree.AutorSearch(AVLTree.root, Autorvalue);

                    break;

                case '4':
                    Console.WriteLine("Informe o Ano de publicação do livro: ");
                    DateTime.TryParse(Console.ReadLine() + "-10", out DateTime Datavalue);
                    AVLTree.YearSearch(AVLTree.root, Datavalue.Year);
                    break;
                default:
                    string cont = "A Opção digitada é inexistente!";
                    Alerta(cont, false);

                    break;
            }

            goto menubuscar;
        }
        static void ListarLivro(Avl.AVLTree AVLTree)
        {
            Console.Title = "Listar Livros";
            AVLTree.inordem(AVLTree.root);
            Console.ReadKey();
        }
        static void RemoverLivro(Avl.AVLTree AVLTree, Abb.ABBTree ABBTree)
        {
            Console.Title = "Remover Livro";
        menuremover:
            Console.Clear();
            Console.WriteLine("**Por questões de integridade, só é póssivel remover o Livro com o Código do mesmo**");
            Console.WriteLine("Insira o Código do Livro que deseja remover: ");
            int.TryParse(Console.ReadLine(), out int value);
            Avl.Node Encontrar = AVLTree.Search(AVLTree.root, value);
            if (AVLTree.MostrarLivro(Encontrar))
            {
                return;
            };
        confirmar:
            string[] confirmar = new string[2] { "Confirmar Operação", "Cancelar" };
            ListarVetor(confirmar);
            char.TryParse(Console.ReadLine(), out char resp);
            switch (resp)
            {
                case '0': return;
                case '1': goto salvar;
                case '2': goto menuremover;
                default:
                    string alerta = "Opção fora do parâmetro.";
                    Alerta(alerta, false);
                    goto confirmar;
            }
        salvar:
            DateTime DataAtual = DateTime.Now;
            string usuario = Environment.UserName;
            ABBTree.Insert(Encontrar.id, Encontrar.NomeLivro, Encontrar.AutorLivro, Encontrar.DataPublicacao, DataAtual, usuario);
            AVLTree.root = AVLTree.deleteNode(AVLTree.root, Encontrar.id);
            int balanceFactor = AVLTree.getRootBalanceFactor();
            Console.WriteLine("Fator de balanceamento do nó raiz: " + balanceFactor);
            string salvo = "Remoção feita com Sucesso.";
            Alerta(salvo, true);
        }
        static void ImprimirArvore(Avl.AVLTree AVLTree)
        {
            AVLTree.printTree(AVLTree.root, "", true);
            Console.ReadKey();
        }
        static void LivroRemovido(Abb.ABBTree ABBTree)
        {
            Console.Title = "Livros Removidos";
            ABBTree.MostrarLivro(ABBTree.InOrderRec(ABBTree.root));
            Console.ReadKey();
        }

    }
}
