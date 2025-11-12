namespace MuniApp.Models
{
    namespace MuniApp.Models
    {
        public class Node
        {
            public Node? Left { get; set; }
            public Node? Right { get; set; }
            public Node? Parent { get; set; }
            public Issue Data { get; set; }

            public Node(Issue data)
            {
                Data = data;
            }
        }

    }


}
