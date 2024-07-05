using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    class Abb
    {
        public class Node
        {
            public int id;
            public string NomeLivro;
            public string AutorLivro;
            public DateTime DataPublicacao;
            public DateTime DataLivroRemocao;
            public string usuario;

            public Node Left;
            public Node Right;

            public Node(int id, string NomeLivro, string AutorLivro, DateTime DataPublicacao, DateTime DataLivroRemocao, string usuario)
            {
                this.id = id;
                this.NomeLivro = NomeLivro;
                this.AutorLivro = AutorLivro;
                this.DataPublicacao = DataPublicacao;
                this.DataLivroRemocao = DataLivroRemocao;
                this.usuario = usuario;

                Left = null;
                Right = null;
            }
        }

        public class ABBTree
        {
            public Node root;
            public ABBTree()
            {
                root = null;
            }
            public void Insert(int id, string NomeLivro, string AutorLivro, DateTime DataPublicacao, DateTime DataLivroRemocao, string usuario)
            {
                root = InsertRec(root, id, NomeLivro, AutorLivro, DataPublicacao, DataLivroRemocao, usuario);
            }
            private Node InsertRec(Node root, int id, string NomeLivro, string AutorLivro, DateTime DataPublicacao, DateTime DataLivroRemocao, string usuario)
            {
                if (root == null)
                {
                    root = new Node(id, NomeLivro, AutorLivro, DataPublicacao, DataLivroRemocao, usuario);
                    return root;
                }

                if (id > root.id)
                    root.Right = InsertRec(root.Right, id, NomeLivro, AutorLivro, DataPublicacao, DataLivroRemocao, usuario);
                else
                    root.Left = InsertRec(root.Left, id, NomeLivro, AutorLivro, DataPublicacao, DataLivroRemocao, usuario);

                return root;
            }

            public Node InOrderRec(Node root)
            {
                if (root != null)
                {
                    InOrderRec(root.Left);
                    InOrderRec(root.Right);
                }
                return root;
            }
            public void PrintTree()
            {
                if (root == null)
                {
                    Console.WriteLine("A árvore está vazia");
                    return;
                }

                PrintTreeRec(root, 0);
            }
            private void PrintTreeRec(Node root, int space)
            {
                int COUNT = 10; // Define a distância entre os níveis
                if (root == null)
                    return;

                space += COUNT;

                PrintTreeRec(root.Right, space);

                Console.WriteLine();
                for (int i = COUNT; i < space; i++)
                    Console.Write(" ");
                Console.WriteLine(root.id);

                PrintTreeRec(root.Left, space);
            }
            public bool Find(int id)
            {
                Node current = root;
                while (current != null)
                {
                    if (id < current.id)
                    {
                        current = current.Left;
                    }
                    else if (id > current.id)
                    {
                        current = current.Right;
                    }
                    else
                    {
                        return true; // Valor encontrado
                    }
                }
                return false; // Valor não encontrado
            }

            public void Delete(int id)
            {
                root = DeleteRec(root, id);
            }
            private Node DeleteRec(Node root, int id)
            {
                if (root == null) return root;

                // Procurando o valor
                if (id < root.id)
                    root.Left = DeleteRec(root.Left, id);
                else if (id > root.id)
                    root.Right = DeleteRec(root.Right, id);
                else
                {
                    // Nó com apenas um filho ou sem filhos
                    if (root.Left == null)
                        return root.Right;
                    else if (root.Right == null)
                        return root.Left;

                    // Nó com dois filhos: Pegue o sucessor em ordem (menor na subárvore direita)
                    root.id = Minid(root.Right);

                    // Delete o sucessor
                    root.Right = DeleteRec(root.Right, root.id);
                }

                return root;
            }
            private int Minid(Node root)
            {
                int minid = root.id;
                while (root.Left != null)
                {
                    minid = root.Left.id;
                    root = root.Left;
                }
                return minid;
            }
            private int Height(Node root)
            {
                if (root == null) return -1; // Base para nó nulo
                return 1 + Math.Max(Height(root.Left), Height(root.Right));
            }
            public int BalanceFactor()
            {
                return BalanceFactor(root);
            }

            private int BalanceFactor(Node root)
            {
                if (root == null) return 0; // Um nó nulo é considerado balanceado
                return Height(root.Left) - Height(root.Right);
            }
            public bool MostrarLivro(Node Encontrar)
            {
                bool verifica = false;

                if (Encontrar != null)
                {
                    string data = $"{ Encontrar.DataPublicacao.Day }/{ Encontrar.DataPublicacao.Month}/{ Encontrar.DataPublicacao.Year}";
                    Console.WriteLine($"\nCód.Livro:{Encontrar.id}" + $"\nLivro: {Encontrar.NomeLivro}" + $"\nAutor: {Encontrar.AutorLivro}" + $"\nPublicação: {data}" + $"\nRemovido da Biblioteca: {Encontrar.DataLivroRemocao}" + $"\nUsuário: {Encontrar.usuario}");
                    Console.WriteLine("___________________________");
                }
                else
                {
                    verifica = true;
                    Console.WriteLine("Livro não encontrado...");
                }
                return verifica;
            }
        }
    }
}
