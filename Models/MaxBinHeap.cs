using MuniApp.Models;
using MuniApp.Models.MuniApp.Models;
using System.Collections.Generic;

namespace MuniApp.Models
{
    public class MaxBinHeap
    {
        private Node? root;
        private Node? insert_pos;

        public int Count { get; private set; }

        public MaxBinHeap() { }

        public void Insert(Node n)
        {
            if (root == null)
            {
                root = n;
                insert_pos = n;
                Count++;
                return;
            }

            if (insert_pos!.Left == null)
            {
                insert_pos.Left = n;
                n.Parent = insert_pos;
                balance_heap(n);
                Count++;
                return;
            }
            else
            {
                insert_pos.Right = n;
                n.Parent = insert_pos;
                adjust_insert_pos();
                balance_heap(n);
                Count++;
            }
        }

        private void balance_heap(Node n)
        {
            while (n.Parent != null)
            {
                if (n.Parent.Data.Priority < n.Data.Priority)
                {
                    var temp = n.Data;
                    n.Data = n.Parent.Data;
                    n.Parent.Data = temp;
                    n = n.Parent;
                }
                else break;
            }
        }

        private void adjust_insert_pos()
        {
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root!);

            while (q.Count > 0)
            {
                Node node = q.Dequeue();

                if (node.Left != null)
                    q.Enqueue(node.Left);
                else
                {
                    insert_pos = node;
                    return;
                }

                if (node.Right != null)
                    q.Enqueue(node.Right);
                else
                {
                    insert_pos = node;
                    return;
                }
            }
        }

        public Issue? PeekMax()
        {
            return root?.Data;
        }

        public Issue? ExtractMax()
        {
            if (root == null)
                return null;

            Issue maxIssue = root.Data;

            Node lastNode = get_last_node();

            if (lastNode == root)
            {
                root = null;
                insert_pos = null;
                Count--;
                return maxIssue;
            }

            root.Data = lastNode.Data;

            if (lastNode.Parent!.Right == lastNode)
                lastNode.Parent.Right = null;
            else
                lastNode.Parent.Left = null;

            adjust_insert_pos();
            max_heapify(root);
            Count--;

            return maxIssue;
        }

        private Node get_last_node()
        {
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root!);
            Node last = root!;

            while (q.Count > 0)
            {
                last = q.Dequeue();

                if (last.Left != null)
                    q.Enqueue(last.Left);
                if (last.Right != null)
                    q.Enqueue(last.Right);
            }

            return last;
        }

        private void max_heapify(Node node)
        {
            Node largest = node;

            while (true)
            {
                Node currentLargest = largest;

                if (largest.Left != null && largest.Left.Data.Priority > currentLargest.Data.Priority)
                    currentLargest = largest.Left;

                if (largest.Right != null && largest.Right.Data.Priority > currentLargest.Data.Priority)
                    currentLargest = largest.Right;

                if (currentLargest == largest)
                    break;

                var temp = largest.Data;
                largest.Data = currentLargest.Data;
                currentLargest.Data = temp;

                largest = currentLargest;
            }
        }
    }
}
