#ifndef PEERGRADE1CPP__GRAPH_H_
#define PEERGRADE1CPP__GRAPH_H_
#include <istream>

// Абстрактный класс, представляющий Граф.
class Graph {
  
 protected:
  
  // Переменная типа bool, указывающая на то, ориентированный ли граф.
  bool isDirected = false;
  
 public:
  
  // Конструктор графа по умолчанию.
  Graph();
  
  // Констуктор графа, получающий информацию из некоторого данного потока.
  explicit Graph(std::istream & istream);

  // Виртуальная функция, подразумевающая печать Графа в данный поток в виде Списка рёбер.
  virtual void PrintAsEdgeList(std::ostream &ostream);

  // Виртуальная функция, подразумевающая печать Графа в данный поток в виде Списка смежности.
  virtual void PrintAsAdjacencyList(std::ostream &ostream);

  // Виртуальная функция, подразумевающая печать Графа в данный поток в виде Матрицы смежности.
  virtual void PrintAsIncidenceMatrix(std::ostream &ostream);

  // Виртуальная функция, подразумевающая печать Графа в данный поток в виде Списка инциденций.
  virtual void PrintAsAdjacencyMatrix(std::ostream &ostream);

  // Виртуальная функция, подразумевающая печать в данный поток списка (полу)степеней вершин Графа.
  virtual void DegreesCount(std::ostream &ostream);

  // Виртуальная функция, подразумевающая печать в данный поток количества рёбер Графа.
  virtual void EdgesCount(std::ostream &ostream);
};

#endif //PEERGRADE1CPP__GRAPH_H_
