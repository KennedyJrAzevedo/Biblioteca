using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    class Avl
    {
        public class Node
        {
            //Declaração de Variáveis
            public int id;
            public string NomeLivro;
            public string AutorLivro;
            public DateTime DataPublicacao;

            //

            //nó esquerda e direita da árvore
            public Node left, right;
            //altura da árvore
            public int altura;

            public Node(int id, string NomeLivro, string AutorLivro, DateTime DataPublicacao)
            {
                this.id = id;
                this.NomeLivro = NomeLivro;
                this.AutorLivro = AutorLivro;
                this.DataPublicacao = DataPublicacao;
                altura = 1;
            }
        }
        public class AVLTree
        {
            //Nó raiz
            public Node root;

            int altura(Node N)
            {
                if (N == null)
                    return 0;
                return N.altura;
            }
            int max(int a, int b)
            {
                return (a > b) ? a : b;
            }
            //Rotação para direita
            Node rightRotate(Node y)
            {
                Node x = y.left;
                Node T2 = x.right;

                x.right = y;
                y.left = T2;

                y.altura = max(altura(y.left), altura(y.right)) + 1;
                x.altura = max(altura(x.left), altura(x.right)) + 1;

                return x;
            }
            //Rotação para esquerda
            Node leftRotate(Node x)
            {
                Node y = x.right;
                Node T2 = y.left;

                y.left = x;
                x.right = T2;

                x.altura = max(altura(x.left), altura(x.right)) + 1;
                y.altura = max(altura(y.left), altura(y.right)) + 1;

                return y;
            }
            int getBalance(Node N)
            {
                if (N == null)
                    return 0;
                return altura(N.left) - altura(N.right);
            }
            // Inserção
            public Node insert(Node node, int id, string NomeLivro, string AutorLivro, DateTime DataPublicacao)
            {

                if (node == null)
                    return new Node(id, NomeLivro, AutorLivro, DataPublicacao);

                if (id < node.id)
                {
                    node.left = insert(node.left, id, NomeLivro, AutorLivro, DataPublicacao);
                }
                else
                {
                    if (id > node.id)
                    {

                        node.right = insert(node.right, id, NomeLivro, AutorLivro, DataPublicacao);
                    }
                    else
                    {
                        Console.WriteLine("Já tem um Livro Cadastrado nesse código!\n");
                        return node;
                    }
                }

                node.altura = 1 + max(altura(node.left),
                                      altura(node.right));

                int balance = getBalance(node);
                if (balance > 1 && id < node.left.id)
                    return rightRotate(node);
                if (balance < -1 && id > node.right.id)
                    return leftRotate(node);
                if (balance > 1 && id > node.left.id)
                {
                    node.left = leftRotate(node.left);
                    return rightRotate(node);
                }
                if (balance < -1 && id < node.right.id)
                {
                    node.right = rightRotate(node.right);
                    return leftRotate(node);
                }
                return node;
            }
            public void inordem(Node node)
            {
                if (node != null)
                {
                    inordem(node.left);
                    MostrarLivro(node);
                    inordem(node.right);
                }
            }
            public Node balance(Node node)
            {
                int balanceFactor = getBalance(node);
                if (balanceFactor > 1)
                {
                    if (getBalance(node.left) >= 0)
                    {
                        return rightRotate(node);
                    }
                    else
                    {
                        node.left = leftRotate(node.left);
                        return rightRotate(node);
                    }
                }
                else if (balanceFactor < -1)
                {
                    if (getBalance(node.right) <= 0)
                    {
                        return leftRotate(node);
                    }
                    else
                    {
                        node.right = rightRotate(node.right);
                        return leftRotate(node);
                    }
                }
                return node;
            }
            public Node deleteNode(Node root, int key)
            {
                if (root == null)
                    return root;
                if (key < root.id)
                    root.left = deleteNode(root.left, key);
                else if (key > root.id)
                    root.right = deleteNode(root.right, key);
                else
                {
                    if ((root.left == null) || (root.right == null))
                    {
                        Node temp = null;
                        if (temp == root.left)
                            temp = root.right;
                        else
                            temp = root.left;

                        if (temp == null)
                        {
                            temp = root;
                            root = null;
                        }
                        else
                            root = temp;
                    }
                    else
                    {
                        Node temp = minValueNode(root.right);
                        root.id = temp.id;
                        root.right = deleteNode(root.right, temp.id);
                    }
                }
                if (root == null)
                    return root;
                root.altura = 1 + max(altura(root.left), altura(root.right));
                int balance = getBalance(root);
                if (balance > 1 && getBalance(root.left) >= 0)
                    return rightRotate(root);
                if (balance > 1 && getBalance(root.left) < 0)
                {
                    root.left = leftRotate(root.left);
                    return rightRotate(root);
                }
                if (balance < -1 && getBalance(root.right) <= 0)
                    return leftRotate(root);
                if (balance < -1 && getBalance(root.right) > 0)
                {
                    root.right = rightRotate(root.right);
                    return leftRotate(root);
                }
                return root;
            }
            Node minValueNode(Node node)
            {
                Node current = node;
                while (current.left != null)
                    current = current.left;
                return current;
            }
            public void printTree(Node root, string indent, bool last)
            {
                if (root != null)
                {
                    Console.Write(indent);
                    if (last)
                    {
                        Console.Write("└─");
                        indent += "  ";
                    }
                    else
                    {
                        Console.Write("├─");
                        indent += "| ";
                    }
                    Console.WriteLine(root.id);

                    printTree(root.left, indent, false);
                    printTree(root.right, indent, true);
                }
            }
            public int getRootBalanceFactor()
            {
                if (root == null)
                { return 0; }
                else
                { return getBalance(root); }
            }
            public Node Search(Node root, int key)
            {
                if (root == null || root.id == key)
                    return root;
                if (root.id < key)
                    return Search(root.right, key);
                return Search(root.left, key);
            }
            public void NomeSearch(Node root, string keyname)
            {
                if (root != null)
                {
                    inordem(root.left);
                    if (root.NomeLivro.Contains(keyname))
                    {
                        MostrarLivro(root);
                    }
                    inordem(root.right);
                }
                Console.ReadKey();
            }
            public void AutorSearch(Node root, string keyname)
            {
                if (root != null)
                {
                    inordem(root.left);
                    if (root.AutorLivro.Contains(keyname))
                    {
                        MostrarLivro(root);
                    }
                    inordem(root.right);
                }
                Console.ReadKey();
            }
            public void YearSearch(Node root, int keyname)
            {
                if (root != null)
                {
                    inordem(root.left);
                    if (root.DataPublicacao.Year.Equals(keyname))
                    {
                        MostrarLivro(root);

                    }
                    inordem(root.right);
                }
                Console.ReadKey();
            }
            public bool MostrarLivro(Node Encontrar)
            {
                bool verifica = false;
                if (Encontrar != null)
                {
                    string data = $"{ Encontrar.DataPublicacao.Day }/{ Encontrar.DataPublicacao.Month}/{ Encontrar.DataPublicacao.Year}";
                    Console.WriteLine($"\nCód.Livro:{Encontrar.id}" + $"\nLivro:{Encontrar.NomeLivro}" + $"\nAutor:{Encontrar.AutorLivro}" + $"\nPublicação: {data}");
                    Console.WriteLine("___________________________");
                }
                else
                {
                    verifica = true;
                    Console.WriteLine("Livro não encontrado...");
                }
                Console.ReadKey();
                return verifica;
            }
        }
    }
}
