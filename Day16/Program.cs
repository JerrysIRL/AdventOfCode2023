using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Day16
{
    internal class Program
    {
        private static readonly string[] Input = File.ReadAllLines(@"E:\AdventOfCode\AdventOfCode2023\Day16\Input.txt");
        private static Dictionary<Node, (int y, int x)> Grid = new Dictionary<Node, (int y, int x)>();

        public static void Main(string[] args)
        {
            for (int y = 0; y < Input.Length; y++)
            {
                for (int x = 0; x < Input[y].Length; x++)
                {
                    Node outNode = new Node() { Symbol = Input[y][x], Coordinates = (y, x) };
                    Grid.Add(outNode, (y, x));
                }
            }

            var nodesToCheck = new HashSet<Node>();
            var startNode = GetNode((0, 0));

            Directions currentDirection;
            SetEnergized(startNode.Coordinates);
            startNode.Direction = Directions.Right;
            nodesToCheck.Add(startNode);

            while (nodesToCheck.Any())
            {
                var currentNode = nodesToCheck.First();
                nodesToCheck.Remove(currentNode);
                currentDirection = currentNode.Direction;
                if (currentDirection == Directions.Right)
                {
                    SetEnergized(currentNode.Coordinates);
                    SetVisited(currentNode.Coordinates);
                    if (currentNode.Symbol == '/')
                    {
                        var up = GetUpNode(currentNode);
                        if (up != null)
                            nodesToCheck.Add(up);
                    }

                    if (currentNode.Symbol.Equals((char)92))
                    {
                        var down = GetDownNode(currentNode);
                        if (down != null)
                            nodesToCheck.Add(down);
                    }

                    if (currentNode.Symbol == '|')
                    {
                        var up = GetUpNode(currentNode);
                        if (up != null)
                            nodesToCheck.Add(up);

                        var down = GetDownNode(currentNode);
                        if (down != null)
                            nodesToCheck.Add(down);
                    }

                    if (currentNode.Symbol == '.' || currentNode.Symbol == '-')
                    {
                        var node = GetRightNode(currentNode);
                        if (node != null)
                        {
                            //node.Direction = currentDirection;
                            AddDirection(node.Coordinates, currentDirection);
                            nodesToCheck.Add(node);
                        }
                    }
                }

                if (currentDirection == Directions.Up)
                {
                    SetEnergized(currentNode.Coordinates);
                    if (currentNode.Symbol == '/')
                    {
                        var right = GetRightNode(currentNode);
                        if (right != null)
                            nodesToCheck.Add(right);
                    }

                    if (currentNode.Symbol.Equals((char)92))
                    {
                        var left = GetLeftNode(currentNode);
                        if (left != null)
                            nodesToCheck.Add(left);
                    }

                    if (currentNode.Symbol == '-')
                    {
                        var left = GetLeftNode(currentNode);
                        if (left != null)
                            nodesToCheck.Add(left);

                        var right = GetRightNode(currentNode);
                        if (right != null)
                            nodesToCheck.Add(right);
                    }

                    if (currentNode.Symbol == '.' || currentNode.Symbol == '|')
                    {
                        var node = GetUpNode(currentNode);
                        if (node != null)
                        {
                            //node.Direction = currentDirection;
                            AddDirection(node.Coordinates, currentDirection);
                            nodesToCheck.Add(node);
                        }
                    }
                }

                if (currentDirection == Directions.Down)
                {
                    SetEnergized(currentNode.Coordinates);
                    SetVisited(currentNode.Coordinates);
                    if (currentNode.Symbol == '/')
                    {
                        var left = GetLeftNode(currentNode);
                        if (left != null)
                            nodesToCheck.Add(left);
                    }

                    if (currentNode.Symbol.Equals((char)92))
                    {
                        var right = GetRightNode(currentNode);
                        if (right != null)
                            nodesToCheck.Add(right);
                    }

                    if (currentNode.Symbol == '-')
                    {
                        var left = GetLeftNode(currentNode);
                        if (left != null)
                            nodesToCheck.Add(left);

                        var right = GetRightNode(currentNode);
                        if (right != null)
                            nodesToCheck.Add(right);
                    }

                    if (currentNode.Symbol == '.' || currentNode.Symbol == '|')
                    {
                        var node = GetDownNode(currentNode);
                        if (node != null)
                        {
                            //node.Direction = currentDirection;
                            AddDirection(node.Coordinates, currentDirection);
                            nodesToCheck.Add(node);
                        }
                    }
                }

                if (currentDirection == Directions.Left)
                {
                    SetEnergized(currentNode.Coordinates);
                    SetVisited(currentNode.Coordinates);
                    if (currentNode.Symbol == '/')
                    {
                        var up = GetDownNode(currentNode);
                        if (up != null)
                            nodesToCheck.Add(up);
                    }

                    if (currentNode.Symbol.Equals((char)92))
                    {
                        var down = GetUpNode(currentNode);
                        if (down != null)
                            nodesToCheck.Add(down);
                    }

                    if (currentNode.Symbol == '|')
                    {
                        var up = GetUpNode(currentNode);
                        if (up != null)
                            nodesToCheck.Add(up);

                        var down = GetDownNode(currentNode);
                        if (down != null)
                            nodesToCheck.Add(down);
                    }

                    if (currentNode.Symbol == '.' || currentNode.Symbol == '-')
                    {
                        var node = GetLeftNode(currentNode);
                        if (node != null)
                        {
                            //node.Direction = currentDirection;
                            AddDirection(node.Coordinates, currentDirection);
                            nodesToCheck.Add(node);
                        }
                    }
                }

            }

            int amount = 0;
            foreach (var node in Grid.Keys)
            {
                if (node.Energized)
                    amount++;
            }

            Console.WriteLine(amount);
            var sum = Grid.Where(n => n.Key.Energized == true);
            //Console.WriteLine(sum.Count());
            /*for (int y = 0; y < Input.Length; y++)
            {
                for (int x = 0; x < Input[y].Length; x++)
                {
                    var node = GetNode((y, x));
                    if (node.Energized)
                    {
                        node.Symbol = '#';
                    }

                    Console.Write(GetNode((y, x)).Symbol);
                }

                Console.WriteLine();
            }*/
        }

        private static Node GetUpNode(Node currentNode)
        {
            var upNode = GetNode((currentNode.Coordinates.y - 1, currentNode.Coordinates.x));
            if (upNode != null && !upNode.PreviousDirection.Contains(Directions.Up))
            {
                AddDirection(upNode.Coordinates, Directions.Up);
                upNode.Direction = Directions.Up;
                return upNode;
            }

            return null;
        }

        private static Node GetDownNode(Node currentNode)
        {
            var downNode = GetNode((currentNode.Coordinates.y + 1, currentNode.Coordinates.x));
            if (downNode != null && !downNode.PreviousDirection.Contains(Directions.Down))
            {
                AddDirection(downNode.Coordinates, Directions.Down);
                downNode.Direction = Directions.Down;
                return downNode;
            }

            return null;
        }

        private static Node GetRightNode(Node currentNode)
        {
            var rightNode = GetNode((currentNode.Coordinates.y, currentNode.Coordinates.x + 1));
            if (rightNode != null && !rightNode.PreviousDirection.Contains(Directions.Right))
            {
                AddDirection(rightNode.Coordinates, Directions.Right);
                rightNode.Direction = Directions.Right;
                return rightNode;
            }

            return null;
        }

        private static Node GetLeftNode(Node currentNode)
        {
            var leftNode = GetNode((currentNode.Coordinates.y, currentNode.Coordinates.x - 1));
            if (leftNode != null && !leftNode.PreviousDirection.Contains(Directions.Left))
            {
                AddDirection(leftNode.Coordinates, Directions.Left);
                leftNode.Direction = Directions.Left;
                return leftNode;
            }

            return null;
        }

        static Node GetNode((int y, int x) coord)
        {
            return Grid.FirstOrDefault(c => c.Value == (coord.y, coord.x)).Key;
        }

        static void SetEnergized((int y, int x) coord)
        {
            Grid.FirstOrDefault(c => c.Value == (coord.y, coord.x)).Key.Energized = true;
        }

        static void SetVisited((int y, int x) coord)
        {
            Grid.FirstOrDefault(c => c.Value == (coord.y, coord.x)).Key.Visited = true;
        }

        static void AddDirection((int y, int x) coord, Directions directionToAdd)
        {
            Grid.FirstOrDefault(c => c.Value == (coord.y, coord.x)).Key.PreviousDirection.Add(directionToAdd);
        }

        public class Node
        {
            public char Symbol { get; set; }
            public (int y, int x) Coordinates { get; set; }
            public bool Energized { get; set; } = false;
            public Directions Direction { get; set; } = Directions.Undefined;
            public List<Directions> PreviousDirection { get; set; } = new List<Directions>();
            public bool Visited { get; set; }
        }

        public enum Directions
        {
            Up,
            Down,
            Right,
            Left,
            Undefined
        }
    }
}