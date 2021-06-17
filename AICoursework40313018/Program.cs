using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace AICoursework40313018
{
    
    public class Node
    {
        public int cavernNum;
        public Point coords;
        public Node parentNode = null;
        public double G;
        public double F;
        public double H;
        public List<int> connectedCaves = new List<int>();
        public double heuristicFunction(Node current, Node next)
        {
            double distanceX;
            double distanceY;
            double euclideanDistance;
            distanceX = current.coords.X - next.coords.X;
            distanceY = current.coords.Y - next.coords.Y;
            return euclideanDistance = Math.Sqrt(Math.Pow(distanceX, 2.0) + Math.Pow(distanceY, 2.0));
           
        }

        public double calculateHDist(Node current, Node end)
        {
            current.H = current.heuristicFunction(current, end);

            return current.H;
        }

        public double calculateGDist(Node parent, Node current)
        {
            current.G = current.heuristicFunction(parent, current);

            return current.G;
        }

        public double calculateFDist(Node current)
        {
            current.F = current.G + current.H;

            return current.F;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //double startDistance = 0.0;
            //double distance = 0.0;
            int j = 2;
            int[] caveFileDetails;
            String line;
            int numOfCaves;
            string cavesn = args[0] + ".csn";
            int numOfPoints;
            string caveInput = args[0] + ".cav";
            //string outFile = args[0] + "csn";

            List<Node> open = new List<Node>();
            List<Node> closed = new List<Node>();
            List<Node> cavernList = new List<Node>();

            using (StreamReader caveReader = new StreamReader(caveInput))
            {
                line = caveReader.ReadToEnd();
            }

            caveFileDetails = line.Split(',').Select(int.Parse).ToArray();

            numOfCaves = caveFileDetails[0];

            numOfPoints = numOfCaves * 2;

            int[,] caveMatricies = new int[numOfCaves, numOfCaves];

            List<System.Drawing.Point> caveCoords = new List<Point>();

            for(int i = 1; i < numOfPoints + 1; i+=2)
            {
                caveCoords.Add( new System.Drawing.Point(caveFileDetails[i], caveFileDetails[j]));
                j += 2;
            }

            for (int x = 0; x < numOfCaves; x++)
            {
                Node listNode = new Node();
                listNode.cavernNum = x + 1;
                listNode.coords = caveCoords[x];
                cavernList.Add(listNode);
            }
            
            int connectionStartPoint = numOfPoints + 1;

            int caveCounter = numOfCaves + 1;

            for (int r = 0; r < numOfCaves; r++)
            {
                for(int n = 0; n < numOfCaves; n++)
                {
                    caveMatricies[r, n] = caveFileDetails[connectionStartPoint];
                    connectionStartPoint++;
                }
            }

            for (int i = 0; i < caveMatricies.GetLength(0); i++)
            {
                for (int y = 0; y < caveMatricies.GetLength(1); y++)
                {
                    if (caveMatricies[i, y] == 1)
                    {
                        cavernList[y].connectedCaves.Add(i);
                    }
                }
            }

            if (cavernList[0].connectedCaves.Count != 0)
            {
                AStarSearch(cavernList, cavesn);
            }
            else
            {
                using (StreamWriter no = new StreamWriter(cavesn))
                {
                    no.Write("0");
                }
            }
        }
        
        public static void AStarSearch(List<Node> listOfCaves, string outputFile)
        {
            List<Node> solution = new List<Node>();
            //Console.WriteLine("Hello");  
            Node aStarFirstNode = listOfCaves[0];
            //Console.WriteLine(aStarFirstNode.cavernNum);
            Node aStarEndNode = listOfCaves[listOfCaves.Count - 1];
            //Console.WriteLine(aStarEndNode.cavernNum);
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();
            //List<Node> connectedNodes = new List<Node>();
            openList.Add(aStarFirstNode);
            //Console.WriteLine(current.cavernNum);

            while (openList.Count != 0)
            {
                //Console.WriteLine("Hello2");
                Node current = openList[0];
                closedList.Add(current);
                openList.Remove(current);
                if(current.connectedCaves.Count != 0)
                {
                    //Console.WriteLine("Hello3");
                    List<Node> connectedNodes = new List<Node>();
                    foreach (var cavern in current.connectedCaves)
                    {
                        connectedNodes.Add(listOfCaves[cavern]);
                    }   
                     
                   foreach(Node cave in connectedNodes)
                   {
                        //Console.WriteLine("Reached here");
                        double nodeDistance = cave.calculateGDist(current, cave);
                        if (!closedList.Contains(cave))
                        {
                            if (openList.Contains(cave) && nodeDistance > openList[openList.IndexOf(cave)].G)
                            {
                                continue;
                            }
                            else if (openList.Contains(cave) && nodeDistance < openList[openList.IndexOf(cave)].G)
                            {
                                if(openList[openList.IndexOf(cave)].G > nodeDistance)
                                {
                                    openList[openList.IndexOf(cave)].parentNode = current;
                                    openList[openList.IndexOf(cave)].G = nodeDistance;
                                    openList = openList.OrderBy(x => x.F).ToList();
                                }
                                //openList[openList.IndexOf(cave)].parentNode = current;
                                //openList[openList.IndexOf(cave)].G = nodeDistance;
                            }
                            else if(!openList.Contains(cave))
                            {
                                cave.G = cave.calculateGDist(current, cave);
                                cave.H = cave.calculateHDist(cave, aStarEndNode);
                                cave.F = cave.calculateFDist(cave);
                                openList.Add(cave);
                                openList[openList.IndexOf(cave)].parentNode = current; 
                            }
                        }

                        if(current == aStarEndNode)
                        {
                            break;
                        }
                    }
                }
               
            }
            
            if (!closedList.Contains(aStarEndNode) || !closedList.Contains(aStarFirstNode))
            {
                //Console.WriteLine(0);

                using (StreamWriter nosolution = new StreamWriter(outputFile))
                { 
                    nosolution.Write("0");
                }

            }
            else
            {
                Node caveRoute = closedList[closedList.IndexOf(aStarEndNode)];
                if (closedList.Contains(aStarEndNode))
                {
                    //Console.WriteLine(aStarEndNode.cavernNum + " ");
                    solution.Add(aStarEndNode);
                    while (caveRoute.parentNode != null)
                    {
                        //Console.WriteLine(caveRoute.cavernNum);
                        //Console.Write(caveRoute.parentNode.cavernNum + " ");
                        solution.Add(caveRoute.parentNode);
                        caveRoute = caveRoute.parentNode;
                    }
                    solution.Reverse();
                }
                else
                {
                    if (!closedList.Contains(aStarFirstNode))
                    {
                        //Console.WriteLine(0);
                    }
                }

                using (StreamWriter cavesolution = new StreamWriter(outputFile))
                {
                    foreach (Node cave in solution)
                    {
                        cavesolution.Write(cave.cavernNum + " ");
                    }
                }
            }

            if (!closedList.Contains(aStarFirstNode))
            {
                //Console.WriteLine(0);
                
                File.WriteAllText(outputFile, "0");
            }
    }
}

