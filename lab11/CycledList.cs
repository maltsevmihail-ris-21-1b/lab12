using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using OOP__lab10;

namespace lab12
{
    public class Node<T> : ICloneable where T : ICloneable<T>
    {
        public Node<T>? next;
        public Node<T>? prev;
        public T? data;

        public Node()
        {
            next = null;
            data = default(T);
        }

        public Node(Node<T> newnode)
        {
            data = newnode.data.Clone();
        }

        public object Clone()
        {
            return new Node<T>(this);
        }
    }

    public class CycledList<T> : IEnumerable<T>, ICollection<T> where T : ICloneable<T>
    {
        public Node<T>? beg = null;

        public bool IsReadOnly
        {
            get;
            set;
        }

        public int Count
        {
            get;
            private set;
        }
        public CycledList()
        {
            beg = null;
        }

        public CycledList(int size)
        {
            beg = null;
            Count = 0;
            for (int i = 0; i < size; ++i)
            {
                Add(default(T));
            }
        }

        public CycledList(IEnumerable<T> collection)
        {
            foreach (T element in collection)
            {
                Add(element.Clone());
            } 
        }

        public void Add(T element)
        {
            if (beg == null)
            {
                Count = 1;
                beg = new Node<T>();
                beg.next = beg;
                beg.prev = beg;
            }
            else
            {
                ++Count;
                var newNode = new Node<T>();
                newNode.next = beg;
                newNode.prev = beg.prev;
                beg.prev.next = newNode;
                beg.prev = newNode;
                beg = newNode;
            }    
            beg.data = element;
        }
        public void Clear()
        {
            if (beg != null)
            {
                Count = 0;
                beg = null;
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            if (beg != null)
            {
                var curNode = beg;
                for (int i = 0; i < Count; ++i)
                {
                    yield return curNode.data;
                    curNode = curNode.next;
                }
            }
            yield break;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public virtual bool Remove(int index)
        {
            if (beg == null)
            {
                return false;
            }
            var curNode = beg;
            for (int i = 0; i < index; ++i)
            {
                curNode = curNode.next;
            }
            if (curNode == beg)
            {
                beg = curNode.next;
            }
            curNode.data = default(T);
            curNode.next.prev = curNode.prev;
            curNode.prev.next = curNode.next;
            --Count;
            return true;
        }

        public virtual T? this[int index]
        {
            get
            {
                if (beg != null)
                {
                    var curNode = beg;
                    for (int i = 0; i < index; ++i)
                    {
                        curNode = curNode.next;
                    }
                    return curNode.data;
                }
                return default(T);
            }
            set
            {
                if (beg != null)
                {
                    var curNode = beg;
                    for (int i = 0; i < index; ++i)
                    {
                        curNode = curNode.next;                
                    }
                    if (curNode != null)
                    {
                        curNode.data = value;
                    }
                }
            }
        }

        public void CopyTo(T[] arr, int beg)
        {
            if (Count + beg <= arr.GetLength(0))
            {
                int i = beg;
                foreach (T element in this)
                {
                    arr[i] = element.Clone();
                    ++i;
                }
            }
        }
        public bool Remove(T t)
        {
            if (beg != null)
            {
                int iterations = 0;
                while (Contains(t))
                {
                    var curNode = beg;
                    int i = 0;
                    while (!curNode.data.Equals(t))
                    {
                        curNode = curNode.next;
                        ++i;
                    }
                    if (i == 0)
                    {
                        beg = beg.next;
                    }
                    curNode.data = default(T);
                    curNode.next.prev = curNode.prev;
                    curNode.prev.next = curNode.next;
                    --Count;
                    iterations++;
                }
                if (iterations > 0)
                {
                    return true;
                }
            }
            return false;
        }
        
        public bool Contains(T toFind)
        {
            if (beg == null)
            {
                return false;
            }
            foreach (T element in this)
            {
                if (element.Equals(toFind))
                {
                    return true;
                }
            }
            return false;
        }

        public CycledList<T> Copy()
        {
            var list = new CycledList<T>();
            var curMain = beg;
            list.beg = (Node<T>)curMain.Clone();
            list.beg.next = list.beg;
            list.beg.prev = list.beg;
            list.Count = 1;
            for ( var i = 0; i < Count - 1; ++i )
            {
                ++list.Count;
                curMain = curMain.next;
                var curList = (Node<T>)curMain.Clone();
                curList.next = list.beg;
                curList.prev = list.beg.prev;
                list.beg.prev.next = curList;
                list.beg.prev = curList;
                list.beg = curList;
            }
            return list;
        }

        public object ShallowCopy()
        {
            CycledList<T> copy = new CycledList<T>();
            foreach (T temp in this)
            {
                copy.Add(temp);
            }
            return copy;
        }

    }




}
