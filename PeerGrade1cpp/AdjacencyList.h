#ifndef PEERGRADE1CPP__ADJACENCYLIST_H_
#define PEERGRADE1CPP__ADJACENCYLIST_H_
#include <map>
#include <set>
#include "Graph.h"

// Класс, представляющий Список смежности и наследующийся от класса Граф.
class AdjacencyList : virtual Graph {
  
  // Карта, хранящая пары, состоящие из вершины и списка её соседей.
  std::map<int,std::set<int>> map;
  
 public:
  
  // Конструктор данного класса, получающий информацию из некоторого данного потока.
  explicit AdjacencyList(std::istream &istream);
  
  // Функция, выполняющая печать Списка смежности в данный поток в виде Списка рёбер.
  void PrintAsEdgeList(std::ostream &ostream) override;
  
  // Функция, выполняющая печать Списка смежности в данный поток в виде Списка смежности.
  void PrintAsAdjacencyList(std::ostream &ostream) override;

  // Функция, выполняющая печать Списка смежности в данный поток в виде Матрицы смежности.
  void PrintAsAdjacencyMatrix(std::ostream &ostream) override;

  // Функция, выполняющая печать Списка смежности в данный поток в виде Списка инциденций.
  void PrintAsIncidenceMatrix(std::ostream &ostream) override;

  // Функция, выполняющая печать в данный поток списка (полу)степеней вершин Списка смежности.
  void DegreesCount(std::ostream &ostream) override;
  
  // Функция, выполняющая печать в данный поток количества рёбер Списка смежности.
  void EdgesCount(std::ostream &ostream) override;
};

#endif //PEERGRADE1CPP__ADJACENCYLIST_H_
