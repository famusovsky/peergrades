#ifndef PEERGRADE1CPP__EDGELIST_H_
#define PEERGRADE1CPP__EDGELIST_H_
#include <map>
#include "Graph.h"

// Класс, представляющий Список рёбер и наследующийся от класса Граф.
class EdgeList : virtual Graph {

  // Карта, хранящая пары, состоящие из ребра и её веса.
  std::map<std::pair<int, int>, int> map;
  
 public:

  // Конструктор данного класса, получающий информацию из некоторого данного потока.
  explicit EdgeList(std::istream & istream);

  // Функция, выполняющая печать Списка рёбер в данный поток в виде Списка рёбер.
  void PrintAsEdgeList(std::ostream &ostream) override;

  // Функция, выполняющая печать Списка рёбер в данный поток в виде Списка смежности.
  void PrintAsAdjacencyList(std::ostream &ostream) override;

  // Функция, выполняющая печать Списка рёбер в данный поток в виде Матрицы смежности.
  void PrintAsAdjacencyMatrix(std::ostream &ostream) override;

  // Функция, выполняющая печать Списка рёбер в данный поток в виде Списка инциденций.
  void PrintAsIncidenceMatrix(std::ostream &ostream) override;

  // Функция, выполняющая печать в данный поток списка (полу)степеней вершин Списка рёбер.
  void DegreesCount(std::ostream &ostream) override;

  // Функция, выполняющая печать в данный поток количества рёбер Списка смежности.
  void EdgesCount(std::ostream &ostream) override;
};

#endif //PEERGRADE1CPP__EDGELIST_H_
