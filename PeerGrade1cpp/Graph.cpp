#include "Graph.h"
#include <iostream>

Graph::Graph(std::istream & istream) {
  int inp;
  istream >> inp;
  istream.ignore();
  isDirected = inp == 1;
}

Graph::Graph() = default;

void Graph::PrintAsEdgeList(std::ostream &ostream) {}

void Graph::PrintAsAdjacencyList(std::ostream &ostream) {}

void Graph::PrintAsIncidenceMatrix(std::ostream &ostream) {}

void Graph::PrintAsAdjacencyMatrix(std::ostream &ostream) {}

void Graph::DegreesCount(std::ostream &ostream) {}

void Graph::EdgesCount(std::ostream &ostream) {}
