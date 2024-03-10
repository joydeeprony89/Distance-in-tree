namespace Distance_in_tree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sol = new Solution();
            var l1 = new List<int> {1, 2 };
            var l2 = new List<int> {2, 3 };
            var l3 = new List<int> { 3, 4 };
            //var l4 = new List<int> { 2, 5 };
            var l4 = new List<int> { 4, 5 };
            var nodes = new List<List<int>> { l1, l2, l3, l4 };
            //int k = 2;
            int k = 3;
            var (ans, count) = sol.Distance(nodes, k);
            Console.WriteLine(count);
            foreach (var node in ans)
            {
                Console.WriteLine(string.Join(",", node));
            }
        }
    }

    public class Solution
    {

        public (List<List<int>>, int) Distance(List<List<int>> nodes, int k)
        {
            var ans = new List<List<int>>();
            var adj = new Dictionary<int, List<int>>();
            foreach (var node in nodes)
            {
                var source = node[0];
                var destination = node[1];
                if (!adj.ContainsKey(source)) adj[source] = new List<int>();
                adj[source].Add(destination);
                if (!adj.ContainsKey(destination)) adj[destination] = new List<int>();
                adj[destination].Add(source);
            }
            var keys = new HashSet<string>();
            foreach (var node in adj)
            {
                var visited = new HashSet<int>();
                var source = node.Key;
                if (visited.Contains(source)) continue;
                visited.Add(source);
                var q = new Queue<int>();
                q.Enqueue(source);
                var temp = k;
                while (q.Count > 0)
                {
                    var size = q.Count;
                    while (size-- > 0)
                    {
                        var item = q.Dequeue();
                        if (temp == 0)
                        {
                            if (source != item)
                            {
                                var innerAns = new List<int> { source, item };
                                var sorted = innerAns.OrderBy(x => x).ToList();
                                string key = string.Join(",", sorted);
                                if (keys.Contains(key)) continue;
                                keys.Add(key);
                                ans.Add(innerAns);
                            }
                        }
                        else
                        {
                            var neighbours = adj[item];
                            foreach (var neighbor in neighbours)
                            {
                                if (visited.Contains(neighbor)) continue;
                                visited.Add(neighbor);
                                q.Enqueue(neighbor);
                            }
                        }
                    }
                    temp--;
                }
            }

            return (ans, ans.Count);
        }
    }
}
