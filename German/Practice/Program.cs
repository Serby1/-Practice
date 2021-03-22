using System;
using System.IO;

namespace Practice
{
    public class BinaryTree //класс, реализующий АТД «дерево бинарного поиска»
    {
        //вложенный класс, отвечающий за узлы и операции допустимы для дерева бинарного
        //поиска
        private class Node
        {
            public object inf; //информационное поле
            public Node left; //ссылка на левое поддерево
            public Node right; //ссылка на правое поддерево
                               //конструктор вложенного класса, создает узел дерева
            public Node(object nodeInf)
            {
                inf = nodeInf;
                left = null;
                right = null;
            }
            //добавляет узел в дерево так, чтобы дерево оставалось деревом бинарного поиска
            public static void Add(ref Node r, object nodeInf)
            {
                if (r == null)
                {
                    r = new Node(nodeInf);
                }
                else
                {
                    if (((IComparable)(r.inf)).CompareTo(nodeInf) > 0)
                    {
                        Add(ref r.left, nodeInf);
                    }
                    else
                    {
                        Add(ref r.right, nodeInf);
                    }
                }
            }
            public static void Preorder(Node r) //прямой обход дерева
            {
                if (r != null)
                {
                    Console.Write("{0} ", r.inf);
                    Preorder(r.left);
                    Preorder(r.right);
                }
            }
            public static void Inorder(Node r) //симметричный обход дерева
            {
                if (r != null)
                {
                    Inorder(r.left);
                    Console.Write("{0} ", r.inf);
                    Inorder(r.right);
                }
            }
            public static void Postorder(Node r) //обратный обход дерева
            {
                if (r != null)
                {
                    Postorder(r.left);
                    Postorder(r.right);
                    Console.Write("{0} ", r.inf);
                }
            }
            //поиск ключевого узла в дереве
            public static void Search(Node r, object key, out Node item)
            {
                if (r == null)
                {
                    item = null;
                }
                else
                {
                    if (((IComparable)(r.inf)).CompareTo(key) == 0)
                    {
                        item = r;
                    }
                    else
                    {
                        if (((IComparable)(r.inf)).CompareTo(key) > 0)
                        {
                            Search(r.left, key, out item);
                        }
                        else
                        {
                            Search(r.right, key, out item);
                        }
                    }
                }
            }
            //методы Del и Delete позволяют удалить узел в дереве так, чтобы дерево при этом
            //оставалось деревом бинарного поиска
            private static void Del(Node t, ref Node tr)
            {
                if (tr.right != null)
                {
                    Del(t, ref tr.right);
                }
                else
                {
                    t.inf = tr.inf;
                    tr = tr.left;
                }
            }
            public static void Delete(ref Node t, object key)
            {
                if (t == null)
                {
                    throw new Exception("Данное значение в дереве отсутствует");
                }
                else
                {
                    if (t.left == null && t.right == null)
                    {
                        t.inf = null;
                    }
                    else
                    if (((IComparable)(t.inf)).CompareTo(key) > 0)
                    {
                        Delete(ref t.left, key);
                    }
                    else
                    {
                        if (((IComparable)(t.inf)).CompareTo(key) < 0)
                        {
                            Delete(ref t.right, key);
                        }
                        else
                        {
                            if (t.left == null)
                            {
                                t = t.right;
                            }
                            else
                            {
                                if (t.right == null)
                                {
                                    t = t.left;
                                }
                                else
                                {
                                    Del(t, ref t.left);
                                }
                            }
                        }
                    }

                }
            }

            public static int Sum(Node r)//вычисление сумму значений узлов, имеющих только одного правого потомка;
            {
                int sum = 0;

                if (r == null) return 0;
                else
                {
                    if (r.right == null)
                    {
                        return sum + Sum(r.left);
                    }
                    if (r.right.right == null)
                    {
                        sum += (int)r.inf;
                    }
                }
                return sum + Sum(r.left) + Sum(r.right);

            }


            public static void DeleteOdd(Node r)
            {
                if (r == null) { return; }
                if (r.right != null) DeleteOdd(r.right);

                //{
                //    DeleteOdd(r.right);
                //    if (r.right.right == null && r.right.left == null)
                //    {
                //        if ((int)r.right.inf % 2 != 0)
                //        {
                //            r.right = null;

                //        }
                //    }

                //}
                if (r.left != null) DeleteOdd(r.left);
                //{
                //    DeleteOdd(r.left);
                //    if (r.left.right == null && r.left.left == null)
                //    {
                //        if ((int)r.left.inf % 2 != 0)
                //        {
                //            r.left = null;

                //        }
                //    }

                //}
                if ((int)r.inf % 2 != 0)
                {

                    Delete(ref r, r.inf);
                }



            }

        } //конец вложенного класса
        Node tree; //ссылка на корень дерева
                   //свойство позволяет получить доступ к значению информационного поля корня дерева
        public object Inf
        {
            set { tree.inf = value; }
            get { return tree.inf; }
        }
        public BinaryTree() //открытый конструктор
        {
            tree = null;
        }
        private BinaryTree(Node r) //закрытый конструктор
        {
            tree = r;
        }
        public void Add(object nodeInf) //добавление узла в дерево
        {
            Node.Add(ref tree, nodeInf);
        }
        //организация различных способов обхода дерева
        public void Preorder()
        {
            Node.Preorder(tree);
        }
        public void Inorder()
        {
            Node.Inorder(tree);
        }
        public void Postorder()
        {
            Node.Postorder(tree);
        }
        //поиск ключевого узла в дереве
        public BinaryTree Search(object key)
        {
            Node r;
            Node.Search(tree, key, out r);
            BinaryTree t = new BinaryTree(r);
            return t;
        }
        //удаление ключевого узла в дереве
        public void Delete(object key)
        {
            Node.Delete(ref tree, key);
        }

        public int Sum()
        {
            return Node.Sum(tree);
        }

        public void DeleteOdd()
        {
            Node.DeleteOdd(tree);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string path = "fileIN.txt";
            string str;
            using (StreamReader fileIN = new StreamReader(path))
            {
                str = fileIN.ReadToEnd();
            }
            char[] separators = new char[] { ' ', ',' };
            string[] strArray = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            int[] array = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; ++i)
            {
                array[i] = int.Parse(strArray[i]);
            }

            BinaryTree tree = new BinaryTree();
            foreach (int item in array)
            {
                tree.Add(item);
            }

            Console.WriteLine("##### Task 21.1 #####");
            Console.WriteLine("Прямой обход:");
            tree.Preorder();//запускаем прямой обход для проверки дерева 
            Console.WriteLine();
            Console.WriteLine($"Сумма:{tree.Sum()}"); // вычисляем сумму узлов, имеющих только одного правого потомка
            Console.WriteLine();



            Console.WriteLine("##### Task 21.2 #####");
            tree.DeleteOdd();// запускаем метод для удаления всех нечетных узлов
            Console.WriteLine("Прямой обход после удаления:");
            tree.Preorder();// Проверка результата с помощью обходов
            Console.WriteLine();
            Console.WriteLine("Симметричный обход после удаления:");
            tree.Inorder();
            Console.ReadLine();
        }
    }
}
