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
            /*
            Node startNode = new Node();

            startNode.cavernNum = 1;
            startNode.coords = caveCoords[0];

            Node endNode = new Node();
            endNode.cavernNum = numOfCaves;
            endNode.coords = caveCoords[numOfCaves -1];

            startDistance = cavernList[0].heuristicFunction(cavernList[0], cavernList[numOfCaves - 1]);
            Console.WriteLine(startDistance);
            */
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

            //DijkstaSearch(caveMatricies, startNode, endNode)
            /*
            Console.WriteLine(numOfPoints);

            foreach(Point xy in caveCoords)
            {
                Console.WriteLine(xy);
                Console.WriteLine("\n");
            }
            */
            /*
            for ( int x = 0; x < numOfCaves; x++)
            {
                for( int y = 0; y < numOfCaves; y++)
                {
                    Console.WriteLine(caveMatricies[x, y] + "\t");
                }
                Console.WriteLine();
            }
            */
            /*
            foreach (Node item in cavernList)
            {
                Console.WriteLine(item.cavernNum.ToString() + "," + item.coords);
                foreach(var cave in item.connectedCaves)
                {
                    Console.WriteLine(cave);
                }
                //distance = startNode.heuristicFunction(startNode, item);
                //Console.WriteLine(distance);

            }
            */
            //Console.WriteLine(cavernList[2].connectedCaves);
            /*
            foreach (int cave in cavernList[0].connectedCaves)
            {
                Console.WriteLine(cave);
            }
            */
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
                        //Console.WriteLine("Hello" + " " + cavern);
                        //listOfCaves[cavern].G = listOfCaves[cavern].calculateGDist(current, listOfCaves[cavern]);
                        //listOfCaves[cavern].H = listOfCaves[cavern].calculateHDist(listOfCaves[cavern], aStarEndNode);
                        //listOfCaves[cavern].H = 0.0;
                        //listOfCaves[cavern].F = listOfCaves[cavern].calculateFDist(listOfCaves[cavern]);
                        //listOfCaves[cavern].parentNode = current;
                        //Console.WriteLine(listOfCaves[cavern].cavernNum.ToString());
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

                //using (StreamWriter nosolution = new StreamWriter(outputFile))
                //{
                //    nosolution.Write("0");
                //}
                File.WriteAllText(outputFile, "0");
            }
            /*
            foreach (var cave in current.connectedCaves)
            {
                Console.WriteLine(cave);
                if(listOfCaves[cave - 1].calculateGDist(current, listOfCaves[cave -1], gdist) < gdist)
                {
                    if(listOfCaves[cave -1].calculateHDist(aStarEndNode, listOfCaves[cave - 1], hdist) < hdist)
                    {
                        if(listOfCaves[cave - 1].calculateHDist(aStarEndNode, listOfCaves[cave - 1], fdist) < fdist)
                        {
                            current = listOfCaves[cave];
                            Console.WriteLine(current.cavernNum);
                        }  
                    }  
                }
            }
            */
            /*
            for (int i = 0; i < connectedNodes.Count; i++)
            {
                Console.WriteLine(connectedNodes[i].cavernNum + ",");
            }
            */
        }

        /*
        public void DijkstraSearch(int[,] caverns, Node startCave, Node endCave, double distance, int numCaverns, List<int> caveRoute, List<Node> caveNodes, Node parent, Node nextNode)
        {
            double startDistance = 0.0;
            double shortestDistance = 100;
            startCave.cavernNum = 1;
            endCave.cavernNum = numCaverns;
            startDistance = startCave.heuristicFunction(startCave, endCave, distance);
            caveRoute.Add(startCave.cavernNum);

            for(int i = 0; i < numCaverns; i++)
            {
                for(int j = 0; j < numCaverns; j++)
                {
                    if(caverns[i,j] == 1)
                    {
                        if(caveNodes[j].heuristicFunction(startCave, nextNode, shortestDistance) < shortestDistance)
                        {
                            nextNode.cavernNum = j;
                            nextNode.coords = caveNodes[j].coords;
                            Console.WriteLine(nextNode.cavernNum);
                            parent.cavernNum = nextNode.cavernNum;
                            parent.coords = nextNode.coords;
                        }
                    }
                }
            }

        }
        */
    }
}

